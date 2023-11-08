Imports MySql.Data.MySqlClient
Imports System.Data
Imports MySql.Data
Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.Web


Public Class tasks
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Protected Sub MasterGrid_Init(sender As Object, e As EventArgs) Handles MasterGrid.Init
        Dim filter As String = "([TaskStatus] = 'In Progress') and (([ProductType] = 'Products Manufactured') or ([ProductType] = 'Repair Services') or ([ProductType] = 'Testing Services') or ([ProductType] = 'Electrically OK or As IS') or ([ProductType] = 'Internal Work'))"

        MasterGrid.FilterExpression = filter
    End Sub
    Protected Sub DetailGridJob_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("ID") = CType(sender, ASPxGridView).GetMasterRowKeyValue
    End Sub

    Protected Sub DetailGridTaskDescription_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("ID") = CType(sender, ASPxGridView).GetMasterRowKeyValue
    End Sub

    Protected Sub DetailGridTaskNotes_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("ID") = CType(sender, ASPxGridView).GetMasterRowKeyValue
    End Sub
End Class