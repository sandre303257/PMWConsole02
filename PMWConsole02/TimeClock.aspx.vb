Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.Web




Public Class TimeClock
    Inherits System.Web.UI.Page
    Dim connection_string As String = "server=pmw-console.cya8lwvaozsr.us-east-1.rds.amazonaws.com; user id=admin; password=Andra1v02102435175; persistsecurityinfo=True; database=PMW_Console"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim intEmployeeID As Int32 = DdlEmployees.SelectedItem.Value
        LblStandardClockIn.Text = "   "
        LblStandardClockOut.Text = "   "
        LblInTime.Text = "   "
        LblOutTime.Text = "   "

        Dim mysql As MySqlHelper = New MySqlHelper()
        mysql.Open(connection_string)
        'THIS CONNECTION POPULATES THE TIME LABELS
        Dim sql As String = "SELECT * FROM time_clock WHERE employee_ID = " & intEmployeeID & " AND date_worked = '" & DateValue(Now).ToString("yyyy/M/d") & "'"
        Dim reader As MySqlDataReader = Nothing '
        mysql.OpenReader(reader, sql)
        While reader.Read()
            If reader.HasRows Then
                LblInTime.Text = reader("clock_in_1")
                LblOutTime.Text = If(IsDBNull(reader("clock_out_1")), String.Empty, reader("clock_out_1"))
                Session("ID") = reader("ID")
            Else
                LblInTime.Text = Nothing
                LblOutTime.Text = Nothing
                Session("ID") = Nothing
            End If
        End While
        mysql.CloseReader(reader)
        'THIS CONNECTION POPULATES EMPLOYEE PROPERTIES FROM DROP DOWN SELECT
        mysql = New MySqlHelper()
        mysql.Open(connection_string)
        sql = "SELECT * FROM employees WHERE ID = " & intEmployeeID
        reader = Nothing
        mysql.OpenReader(reader, sql)
        While reader.Read()
            LblStandardClockIn.Text = reader("ClockInTime").ToString()
            LblStandardClockOut.Text = reader("ClockOutTime").ToString()
        End While
        mysql.CloseReader(reader)
        mysql.Close()

        If DdlEmployees.SelectedItem.Text = "---select---" Then
            PanelTimePunches.Enabled = False
            LblInTime.Text = "   "
            LblOutTime.Text = "   "
            DatePicker.Value = DateValue(Now)
            LblStandardClockInLabel.Visible = False
            LblStandardClockOutLabel.Visible = False
        Else
            PanelTimePunches.Enabled = True
            LblStandardClockInLabel.Visible = True
            LblStandardClockOutLabel.Visible = True
        End If

        Dim dateSelected As Date = DatePicker.Value
        Dim intSelectedDateNumber As Int32 = -1 * Weekday(dateSelected)
        Dim Saturday As Date
        If Weekday(dateSelected) = 7 Then
            Saturday = dateSelected
        Else
            Saturday = dateSelected.AddDays(intSelectedDateNumber)
        End If
        Dim Friday As Date
        Friday = Saturday.AddDays(6)
        Dim strFilter As String
        strFilter = "(((date_worked >= #" & Saturday & "#) and (date_worked <= #" & Friday & "#)) and (employee_ID = " & intEmployeeID & "))"
        GridTimePunches.FilterExpression = strFilter
        LblStartDate.Text = Saturday
        LblEndDate.Text = Friday

        'case 1: LblInTime has value today and LblOutTime has value today => Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled
        'case 2: LblInTime has value today and LblOutTime is null => Clock In is disabled, Clock In Early is disabled, Clock Out is enabled, Clock Out Late is enabled
        'case 3: LblInTime is null and LblOutTime is null   =>  Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled

        If Not (LblInTime.Text = Nothing) And LblOutTime.Text = Nothing Then
            BtnClockIn.Enabled = False
            BtnClockOut.Enabled = True
            BtnClockInEarly.Enabled = False
            BtnClockOutLate.Enabled = True
        ElseIf Not (LblInTime.Text = Nothing) And Not (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        ElseIf (LblInTime.Text = Nothing) And (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        End If

    End Sub

    Protected Sub BtnClockIn_Click(sender As Object, e As EventArgs) Handles BtnClockIn.Click
        Dim ClockInDate As Date = DateValue(Now)
        Dim OrdinaryClockInTime As DateTime = Convert.ToDateTime(ClockInDate & " " & LblStandardClockIn.Text)
        Dim PunchTime As DateTime

        If Now > OrdinaryClockInTime Then
            PunchTime = Now
        Else
            PunchTime = OrdinaryClockInTime
        End If

        Dim intEmployeeID As Int32 = DdlEmployees.SelectedItem.Value
        Dim mysql As MySqlHelper = New MySqlHelper()
        mysql.Open(connection_string)
        Dim strSQL As String = "INSERT INTO time_clock (clock_in_1, employee_ID, date_worked) VALUES ('" & PunchTime.ToString("yyyy-MM-dd HH:mm:ss") & "'," & intEmployeeID & ",'" + DateValue(PunchTime).ToString("yyyy-MM-dd HH:mm:ss") + "')"
        mysql.Execute(strSQL)
        Dim sql As String = "SELECT * FROM time_clock WHERE employee_ID = " & intEmployeeID & " AND date_worked = '" & DateValue(Now).ToString("yyyy/M/d") & "'"
        Dim reader As MySqlDataReader = Nothing '
        mysql.OpenReader(reader, sql)
        While reader.Read()
            If reader.HasRows Then
                LblInTime.Text = reader("clock_in_1")
                LblOutTime.Text = If(IsDBNull(reader("clock_out_1")), String.Empty, reader("clock_out_1"))
                Session("ID") = reader("ID")
            Else
                LblInTime.Text = Nothing
                LblOutTime.Text = Nothing
                Session("ID") = Nothing
            End If
        End While
        mysql.CloseReader(reader)
        mysql.Close()
        GridTimePunches.DataBind()

        'case 1: LblInTime has value today and LblOutTime has value today => Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled
        'case 2: LblInTime has value today and LblOutTime is null => Clock In is disabled, Clock In Early is disabled, Clock Out is enabled, Clock Out Late is enabled
        'case 3: LblInTime is null and LblOutTime is null   =>  Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled

        If Not (LblInTime.Text = Nothing) And LblOutTime.Text = Nothing Then
            BtnClockIn.Enabled = False
            BtnClockOut.Enabled = True
            BtnClockInEarly.Enabled = False
            BtnClockOutLate.Enabled = True
        ElseIf Not (LblInTime.Text = Nothing) And Not (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        ElseIf (LblInTime.Text = Nothing) And (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        End If

    End Sub

    Protected Sub BtnClockOut_Click(sender As Object, e As EventArgs) Handles BtnClockOut.Click
        Dim LunchDeduction As Double = 0.5
        'Dim ask As MsgBoxResult = MsgBox("Did you take lunch today?", MsgBoxStyle.YesNo, "Lunch")
        'If ask = MsgBoxResult.Yes Then
        '    LunchDeduction = 0.5
        'Else
        '    LunchDeduction = 0
        'End If

        Dim ClockOutDate As DateTime = DateValue(Convert.ToDateTime(LblInTime.Text))
        Dim OrdinaryClockOutTime As DateTime = Convert.ToDateTime(ClockOutDate & " " & LblStandardClockOut.Text)
        Dim PunchTime As DateTime

        If Now > OrdinaryClockOutTime Then
            PunchTime = OrdinaryClockOutTime
        Else
            PunchTime = Now
        End If

        Dim intEmployeeID As Int32 = DdlEmployees.SelectedItem.Value
        Dim mysql As MySqlHelper = New MySqlHelper()
        mysql.Open(connection_string)
        Dim strSQL As String = "UPDATE time_clock SET lunch = " & LunchDeduction & ", clock_out_1 ='" & PunchTime.ToString("yyyy-MM-dd HH:mm:ss") & "', employee_ID = " & intEmployeeID & " WHERE ID = " & Session("ID")
        mysql.Execute(strSQL)
        Dim sql As String = "SELECT * FROM time_clock WHERE employee_ID = " & intEmployeeID & " AND date_worked = '" & DateValue(Now).ToString("yyyy/M/d") & "'"
        Dim reader As MySqlDataReader = Nothing '
        mysql.OpenReader(reader, sql)
        While reader.Read()
            If reader.HasRows Then
                LblInTime.Text = reader("clock_in_1")
                LblOutTime.Text = If(IsDBNull(reader("clock_out_1")), String.Empty, reader("clock_out_1"))
                Session("ID") = reader("ID")
            Else
                LblInTime.Text = Nothing
                LblOutTime.Text = Nothing
                Session("ID") = Nothing
            End If
        End While
        mysql.CloseReader(reader)
        mysql.Close()
        GridTimePunches.DataBind()

        'case 1: LblInTime has value today and LblOutTime has value today => Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled
        'case 2: LblInTime has value today and LblOutTime is null => Clock In is disabled, Clock In Early is disabled, Clock Out is enabled, Clock Out Late is enabled
        'case 3: LblInTime is null and LblOutTime is null   =>  Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled

        If Not (LblInTime.Text = Nothing) And LblOutTime.Text = Nothing Then
            BtnClockIn.Enabled = False
            BtnClockOut.Enabled = True
            BtnClockInEarly.Enabled = False
            BtnClockOutLate.Enabled = True
        ElseIf Not (LblInTime.Text = Nothing) And Not (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        ElseIf (LblInTime.Text = Nothing) And (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        End If

    End Sub

    Protected Sub BtnClockInEarly_Click(sender As Object, e As EventArgs) Handles BtnClockInEarly.Click
        Dim ClockInDate As Date = DateValue(Now)
        Dim OrdinaryClockInTime As DateTime = Convert.ToDateTime(ClockInDate & " " & LblStandardClockIn.Text)
        Dim PunchTime As DateTime = Now
        Dim intEmployeeID As Int32 = DdlEmployees.SelectedItem.Value

        Dim mysql As MySqlHelper = New MySqlHelper()
        mysql.Open(connection_string)
        Dim strSQL As String = "INSERT INTO time_clock (clock_in_1, employee_ID, date_worked) VALUES ('" & PunchTime.ToString("yyyy-MM-dd HH:mm:ss") & "'," & intEmployeeID & ",'" + DateValue(PunchTime).ToString("yyyy-MM-dd HH:mm:ss") + "')"
        mysql.Execute(strSQL)
        Dim sql As String = "SELECT * FROM time_clock WHERE employee_ID = " & intEmployeeID & " AND date_worked = '" & DateValue(Now).ToString("yyyy/M/d") & "'"
        Dim reader As MySqlDataReader = Nothing '
        mysql.OpenReader(reader, sql)
        While reader.Read()
            If reader.HasRows Then
                LblInTime.Text = reader("clock_in_1")
                LblOutTime.Text = If(IsDBNull(reader("clock_out_1")), String.Empty, reader("clock_out_1"))
                Session("ID") = reader("ID")
            Else
                LblInTime.Text = Nothing
                LblOutTime.Text = Nothing
                Session("ID") = Nothing
            End If
        End While
        mysql.CloseReader(reader)
        mysql.Close()
        GridTimePunches.DataBind()

        'case 1: LblInTime has value today and LblOutTime has value today => Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled
        'case 2: LblInTime has value today and LblOutTime is null => Clock In is disabled, Clock In Early is disabled, Clock Out is enabled, Clock Out Late is enabled
        'case 3: LblInTime is null and LblOutTime is null   =>  Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled

        If Not (LblInTime.Text = Nothing) And LblOutTime.Text = Nothing Then
            BtnClockIn.Enabled = False
            BtnClockOut.Enabled = True
            BtnClockInEarly.Enabled = False
            BtnClockOutLate.Enabled = True
        ElseIf Not (LblInTime.Text = Nothing) And Not (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        ElseIf (LblInTime.Text = Nothing) And (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        End If
    End Sub

    Protected Sub BtnClockOutLate_Click(sender As Object, e As EventArgs) Handles BtnClockOutLate.Click
        Dim LunchDeduction As Double = 0.5
        'Dim ask As MsgBoxResult = MsgBox("Did you take lunch today?", MsgBoxStyle.YesNo, "Lunch")
        'If ask = MsgBoxResult.Yes Then
        '    LunchDeduction = 0.5
        'Else
        '    LunchDeduction = 0
        'End If

        Dim ClockOutDate As DateTime = DateValue(Convert.ToDateTime(LblInTime.Text))
        Dim OrdinaryClockOutTime As DateTime = Convert.ToDateTime(ClockOutDate & " " & LblStandardClockOut.Text)
        Dim PunchTime As DateTime = Now

        Dim intEmployeeID As Int32 = DdlEmployees.SelectedItem.Value
        Dim mysql As MySqlHelper = New MySqlHelper()
        mysql.Open(connection_string)
        Dim strSQL As String = "UPDATE time_clock SET lunch = " & LunchDeduction & ", clock_out_1 ='" & PunchTime.ToString("yyyy-MM-dd HH:mm:ss") & "', employee_ID = " & intEmployeeID & " WHERE ID = " & Session("ID")
        mysql.Execute(strSQL)
        Dim sql As String = "SELECT * FROM time_clock WHERE employee_ID = " & intEmployeeID & " AND date_worked = '" & DateValue(Now).ToString("yyyy/M/d") & "'"
        Dim reader As MySqlDataReader = Nothing '
        mysql.OpenReader(reader, sql)
        While reader.Read()
            If reader.HasRows Then
                LblInTime.Text = reader("clock_in_1")
                LblOutTime.Text = If(IsDBNull(reader("clock_out_1")), String.Empty, reader("clock_out_1"))
                Session("ID") = reader("ID")
            Else
                LblInTime.Text = Nothing
                LblOutTime.Text = Nothing
                Session("ID") = Nothing
            End If
        End While
        mysql.CloseReader(reader)
        mysql.Close()
        GridTimePunches.DataBind()

        'case 1: LblInTime has value today and LblOutTime has value today => Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled
        'case 2: LblInTime has value today and LblOutTime is null => Clock In is disabled, Clock In Early is disabled, Clock Out is enabled, Clock Out Late is enabled
        'case 3: LblInTime is null and LblOutTime is null   =>  Clock In is enabled, Clock In Early is enabled, Clock Out is disabled, Clock Out Late is disabled

        If Not (LblInTime.Text = Nothing) And LblOutTime.Text = Nothing Then
            BtnClockIn.Enabled = False
            BtnClockOut.Enabled = True
            BtnClockInEarly.Enabled = False
            BtnClockOutLate.Enabled = True
        ElseIf Not (LblInTime.Text = Nothing) And Not (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        ElseIf (LblInTime.Text = Nothing) And (LblOutTime.Text = Nothing) Then
            BtnClockIn.Enabled = True
            BtnClockOut.Enabled = False
            BtnClockInEarly.Enabled = True
            BtnClockOutLate.Enabled = False
        End If
    End Sub
End Class