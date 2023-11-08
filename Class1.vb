

' MySqlHelper VB.NET class
' http://www.maxvergelli.com/

' Copyright (c) 2016 Max Vergelli


' Vers. 2.1.0
' Date: 21/03/2016

' Description:
' .NET helper class to easly execute SQL commands against a MySQL database,
' execute stored procedures, get or programmatically update DataSet objects,
' and convert to and from MySQL datetime types.
'
' Before use this class, You have to install and import in your project the
' last MySQL Connector/NET at
' http://dev.mysql.com/doc/refman/5.0/es/connector-net.html

' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:

' The above copyright notice and this permission notice shall be included in
' all copies or substantial portions of the Software.

' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
' THE SOFTWARE.

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Globalization
Imports MySql.Data.MySqlClient

Public Class MySqlHelper

    Private _cnx_string As String = Nothing
    Private _cnx_obj As MySqlConnection = Nothing

    Private Function IsConnectionAlive() As Boolean
        Dim is_alive As Boolean = False
        If _cnx_obj IsNot Nothing AndAlso _cnx_obj.State <> ConnectionState.Closed Then
            is_alive = True
        End If
        Return is_alive
    End Function

    Private Sub CloseConnection()

        Try

            If IsConnectionAlive() Then
                _cnx_obj.Close()
            End If
            _cnx_obj = Nothing

        Catch ex As Exception

            _cnx_obj = Nothing

        End Try

    End Sub

    Public ReadOnly Property ConnectionString() As String
        Get
            Return _cnx_string
        End Get
    End Property

    Public Function IsOpen() As Boolean
        Return IsConnectionAlive()
    End Function

    Public Sub New(Optional ByVal db_connection_string As String = "",
Optional ByVal open_connection As Boolean = False)

        _cnx_string = db_connection_string

        If _cnx_string.Length > 0 And open_connection = True Then

            Dim is_open As Boolean = Open(_cnx_string)

        End If

    End Sub

    Public Function Open(ByVal db_connection_string As String) As Boolean

        Dim success As Boolean = False
        Try

            If IsConnectionAlive() = False Then

                If db_connection_string.Length > 0 AndAlso _cnx_obj Is Nothing Then

                    _cnx_string = db_connection_string
                    _cnx_obj = New MySqlConnection
                    _cnx_obj.ConnectionString = _cnx_string
                    _cnx_obj.Open()
                    success = True

                End If

            End If

        Catch ex As Exception

            CloseConnection()
            success = False

        End Try
        Return success

    End Function

    Public Sub Close()

        CloseConnection()

    End Sub

    Protected Overrides Sub Finalize()
        CloseConnection()
    End Sub

    'get a DataRow object with the first row of a SQL select command
    Public Function SelectFirstRow(ByVal sql_select As String) As DataRow

        Dim ds As DataSet = SelectRows(sql_select)
        If ds Is Nothing Then
            Return Nothing
        End If
        If ds.Tables.Count < 1 Then
            Return Nothing
        End If
        If ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        End If
        Return ds.Tables(0).Rows(0)

    End Function

    'get a DataSet object with the rows of a SQL select command
    Public Function SelectRows(ByVal sql_select As String) As DataSet

        Dim ds As DataSet = Nothing
        Try

            If IsConnectionAlive() Then

                Dim cmd As MySqlCommand = New MySqlCommand(sql_select, _cnx_obj)

                Dim da As MySqlDataAdapter = New MySqlDataAdapter
                da.SelectCommand = cmd

                ds = New DataSet
                da.Fill(ds)

                da = Nothing

            End If

        Catch ex As Exception

            ds = Nothing

        End Try
        Return ds

    End Function

    'Execute a MySql Stored Procedure
    Public Overloads Function ExecuteStoredProcedure(ByVal stored_procedure_name As String) As DataSet

        Return ExecuteProcedure(stored_procedure_name)

    End Function

    'Execute a MySql Stored Procedure with parameters

    'How to create the parameters
    'Dim param As MySqlParameter = New MySqlParameter("?v_id", MySqlDbType.Int32)
    'param.Value = 1
    'param.Direction = ParameterDirection.Input

    Public Overloads Function ExecuteStoredProcedure(ByVal stored_procedure_name As String,
ByVal ParamArray parameters As MySqlParameter()) As DataSet

        Return ExecuteProcedure(stored_procedure_name, parameters)

    End Function

    Private Function ExecuteProcedure(ByVal procedure_name As String,
ByVal ParamArray sql_parameters As MySqlParameter()) As DataSet

        Dim ds As DataSet = Nothing
        Try
            If IsConnectionAlive() Then

                Dim cmd As New MySqlCommand(procedure_name, _cnx_obj)

                cmd.CommandType = CommandType.StoredProcedure

                If sql_parameters IsNot Nothing Then
                    For Each p As MySqlParameter In sql_parameters
                        cmd.Parameters.Add(p)
                    Next
                End If

                Dim da As MySqlDataAdapter = New MySqlDataAdapter
                da.SelectCommand = cmd
                ds = New DataSet
                da.Fill(ds)
                da = Nothing

                cmd.Parameters.Clear()

            End If
        Catch ex As Exception

            ds = Nothing

        End Try

        Return ds

    End Function

    'UpdateRows() function automatically generates single-table commands used to reconcile changes made to a DataSet
    'with the associated table in the MySQL database.
    'The function gets
    ' – the SQL SELECT used to populate the dataset
    ' – the updated dataset
    ' – the table name that must be updated in the database
    'Note:
    'The SQL SELECT must also return at least one primary key or unique column, otherwise an exception is generated;

    Public Function UpdateRows(ByVal sql_select As String, ByRef input_dataset As DataSet, ByVal table_name As String) As Boolean

        Dim success As Boolean = False
        Try

            If IsConnectionAlive() Then

                Dim data_adapter As New MySqlDataAdapter(sql_select, _cnx_obj)
                Dim cmd_builder As New MySqlCommandBuilder(data_adapter)
                data_adapter.Update(input_dataset, table_name)
                success = True

            End If

        Catch ex As Exception

            success = False

        End Try

        Return success

    End Function

    'To use a MySqlDataReader,
    'You have to use OpenReader() and CloseReader() functions like in the following example:
    '
    ' Public Sub ReadMyData(ByVal connection_string As String)
    '
    ' Dim mysql As MySqlHelper = New MySqlHelper()
    ' mysql.Open(connection_string)
    '
    ' Dim sql As String = "SELECT OrderID, CustomerID FROM Orders"
    ' Dim reader As MySqlDataReader = Nothing
    '
    ' mysql.OpenReader(reader, sql)
    ' While reader.Read()
    ' Console.WriteLine((reader.GetInt32(0) & ", " & reader.GetString(1)))
    ' End While
    ' mysql.CloseReader(reader)
    '
    ' mysql.Close()
    '
    ' End Sub

    Public Sub OpenReader(ByRef reader As MySqlDataReader, ByVal sql_select As String)

        Try
            If IsConnectionAlive() Then

                Dim cmd As New MySqlCommand(sql_select, _cnx_obj)
                reader = cmd.ExecuteReader()

            End If

        Catch ex As Exception

            reader = Nothing

        End Try

    End Sub

    Public Sub CloseReader(ByRef reader As MySqlDataReader)

        Try

            reader.Close()

        Catch ex As Exception

        End Try

    End Sub

    'Execute SQL command and return the number of records affected
    Public Function Execute(ByVal sql_command As String) As Long

        Dim affected_rows As Long = 0

        Try
            If IsConnectionAlive() Then

                Dim cmd As New MySqlCommand(sql_command, _cnx_obj)

                affected_rows = cmd.ExecuteNonQuery()

            End If

        Catch ex As Exception

            affected_rows = 0

        End Try

        Return affected_rows

    End Function

    'GetSingleValue() function executes the query, and returns the value of the first column of the first row in the
    'result set returned by the query. Extra columns or rows are ignored.
    'Note:
    'Use the GetSingleValue() method to retrieve a single value (for example, an aggregate value)
    'from a database, like "select count(*) from region"

    Public Overloads Function GetSingleValue(ByVal sql_select As String) As Object

        Return ExecuteScalar(sql_select, DirectCast(Nothing, MySqlParameter()))

    End Function

    Public Overloads Function GetSingleValue(ByVal sql_select As String, ByVal ParamArray sql_parameters As MySqlParameter()) As Object

        Return ExecuteScalar(sql_select, sql_parameters)

    End Function

    'ExecuteScalar: Executes the query, and returns the value (integer/string) of the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
    Private Function ExecuteScalar(ByVal sql_select As String, ByVal ParamArray sql_parameters As MySqlParameter()) As Object

        Dim row As Object = Nothing
        Try
            If IsConnectionAlive() Then

                Dim cmd As New MySqlCommand(sql_select, _cnx_obj)
                If sql_parameters IsNot Nothing Then
                    For Each p As MySqlParameter In sql_parameters
                        cmd.Parameters.Add(p)
                    Next
                End If
                row = cmd.ExecuteScalar()
                cmd.Parameters.Clear()

            End If
        Catch ex As Exception

            row = Nothing

        End Try

        Return row

    End Function

    '25/1/12 -> 2012-01-25
    Public Overloads Function ConvertToMySqlDate(ByVal input_date As Date) As String

        Return input_date.ToString("yyyy-MM-dd")

    End Function

    '25/1/12 -> 2012-01-25
    Public Overloads Function ConvertToMySqlDate(ByVal input_date As String) As String

        Return ConvertDateTime(input_date, DATETIME_TYPE.MYSQL, True)

    End Function

    '25/1/12 13:00 -> 2012-01-25 13:00:00
    Public Overloads Function ConvertToMySqlDateTime(ByVal input_datetime As DateTime) As String

        Return input_datetime.ToString("yyyy-MM-dd HH:mm:ss")

    End Function

    '25/1/12 13:00 -> 2012-01-25 13:00:00
    Public Overloads Function ConvertToMySqlDateTime(ByVal input_datetime As String) As String

        Return ConvertDateTime(input_datetime, DATETIME_TYPE.MYSQL)

    End Function

    '2012-01-25 -> 25-01-2012
    Public Overloads Function ConvertToDate(ByVal mysql_date As Date) As String

        Return mysql_date.ToString("dd-MM-yyyy")

    End Function

    '2012-01-25 -> 25-01-2012
    Public Overloads Function ConvertToDate(ByVal mysql_date As String) As String

        Return ConvertDateTime(mysql_date, DATETIME_TYPE.SYSTEM, True)

    End Function

    '2012-01-25 13:00:00 -> 25-01-2012 13:00:00
    Public Overloads Function ConvertToDateTime(ByVal mysql_datetime As DateTime) As String

        Return mysql_datetime.ToString("dd-MM-yyyy HH:mm:ss")

    End Function

    '2012-01-25 13:00:00 -> 25-01-2012 13:00:00
    Public Overloads Function ConvertToDateTime(ByVal mysql_datetime As String) As String

        Return ConvertDateTime(mysql_datetime, DATETIME_TYPE.SYSTEM)

    End Function

    Private Enum DATETIME_TYPE As Integer
        SYSTEM = 0
        MYSQL = 1
    End Enum

    Private Function ConvertDateTime(ByVal input_datetime As String, ByVal output_datetime_type As DATETIME_TYPE,
Optional ByVal get_only_date As Boolean = False) As String

        'The function returns a datetime in a well-formatted string for regular or MySQL use.
        'You have to insert a datetime string formatted by {,/,-,.,:} symbols.
        'Besides, for the input date, You need to specify "day, month and year";
        'for the time, only "hour and minutes", "seconds" doesn’t matter.

        'The function returns Nothing if an error is encountered.

        'You can select one of the following datetime types as output:
        'SYSTEM: "(2 digits day)-(2 digits month)-(4 digits year) hours:minutes:seconds"
        'MYSQL: "(4 digits year)-(2 digits month)-(2 digits day) hours:minutes:seconds"

        'examples:
        'for a regular datetime in input and
        'a MySql datetime as output, You ‘ll get…

        '25-1-08 -> 2008-01-25 00:00:00
        '25/1/08 -> 2008-01-25 00:00:00
        '25/1/08 12:00 -> 2008-01-25 12:00:00
        '25-1-08 12:00 -> 2008-01-25 12:00:00
        '25.1.08 12:00 -> 2008-01-25 12:00:00
        '25.1.08 12.00 -> 2008-01-25 12:00:00
        '25.1.08 12.00.23 -> 2008-01-25 12:00:23

        Dim dt_out As String = Nothing
        Dim dt_type As String = ""
        If output_datetime_type = DATETIME_TYPE.MYSQL Then
            dt_type = "yyyy-MM-dd HH’:’mm’:’ss"
        Else
            dt_type = "dd-MM-yyyy HH’:’mm’:’ss"
        End If
        Try
            If input_datetime.IndexOf(".", 0, CompareMethod.Binary) > 0 Then
                Dim tmp() As String = input_datetime.Split(" ")
                If tmp.GetUpperBound(0) > 0 Then
                    input_datetime = tmp(0).Replace(".", "-") & " " & tmp(1).Replace(".", ":")
                End If
            End If
            Dim myDTFI As DateTimeFormatInfo = New CultureInfo("it-IT", False).DateTimeFormat
            myDTFI.FullDateTimePattern = "dd-MM-yyyy HH’:’mm’:’ss"
            myDTFI.DateSeparator = "-"
            myDTFI.TimeSeparator = ":"
            Dim myDT As New DateTime
            myDT = System.Convert.ToDateTime(input_datetime, myDTFI)
            dt_out = Format(myDT, dt_type)
            myDT = Nothing
            myDTFI = Nothing
            If get_only_date = True Then
                Dim s() As String = dt_out.Split(" ")
                If s.GetUpperBound(0) > 0 Then
                    dt_out = s(0)
                End If
            End If
        Catch ex As Exception
            dt_out = Nothing
        End Try
        Return dt_out

    End Function

End Class
