Imports System.Collections.Generic
Imports DevExpress.XtraReports.Web.Extensions

Public Class ReportStorageWebExtension2
    Inherits DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension

    Public Overrides Function CanSetData(url As String) As Boolean
        Return MyBase.CanSetData(url)
    End Function

    Public Overrides Function IsValidUrl(url As String) As Boolean
        Return MyBase.IsValidUrl(url)
    End Function

    Public Overrides Function GetData(url As String) As Byte()
        Return MyBase.GetData(url)
    End Function

    Public Overrides Function GetUrls() As Dictionary(Of String, String)
        Return MyBase.GetUrls()
    End Function

    Public Overrides Sub SetData(report As DevExpress.XtraReports.UI.XtraReport, url As String)
        MyBase.SetData(report, url)
    End Sub

    Public Overrides Function SetNewData(report As DevExpress.XtraReports.UI.XtraReport, defaultUrl As String) As String
        Return MyBase.SetNewData(report, defaultUrl)
    End Function

End Class