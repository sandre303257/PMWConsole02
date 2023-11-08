Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Public Partial Class TwoFactorAuthenticationSignIn
    Inherits System.Web.UI.Page
    Private signinManager As ApplicationSignInManager
    Private manager As ApplicationUserManager

    Public Sub New()
        manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        Dim userId = signinManager.GetVerifiedUserId()
        If userId Is Nothing Then
            Response.Redirect("/Account/Error", True)
        End If
        If Not IsPostBack Then
            Dim userFactors = manager.GetValidTwoFactorProviders(userId)
            Providers.DataSource = userFactors.[Select](Function(x) x).ToList()
            Providers.DataBind()
            Providers.SelectedIndex = 0
        End If
    End Sub

    Protected Sub CodeSubmit_Click(sender As Object, e As EventArgs)
        Dim rememberMe As Boolean = False
        Boolean.TryParse(Request.QueryString("RememberMe"), rememberMe)

        Dim result = signinManager.TwoFactorSignIn(HiddenField("Provider").ToString(), Code.Text, isPersistent := rememberMe, rememberBrowser := RememberBrowser.Checked)
        Select Case result
            Case SignInStatus.Success
                IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                Exit Select
            Case SignInStatus.LockedOut
                Response.Redirect("/Account/Lockout.aspx")
                Exit Select
            Case Else
                Code.ErrorText = "Invalid Code"
                Code.IsValid = False
                Exit Select
        End Select
    End Sub

    Protected Sub ProviderSubmit_Click(sender As Object, e As EventArgs)
        If Not signinManager.SendTwoFactorCode(Providers.SelectedItem.Value.ToString()) Then
            Response.Redirect("/Account/Error")
        End If

        Dim user = manager.FindById(signinManager.GetVerifiedUserId())
        If user IsNot Nothing Then
            Dim code = manager.GenerateTwoFactorToken(user.Id, Providers.SelectedItem.Value.ToString())
        End If

        HiddenField("Provider") = Providers.SelectedItem.Value.ToString()
        sendcode.Visible = False
        verifycode.Visible = True
    End Sub
End Class