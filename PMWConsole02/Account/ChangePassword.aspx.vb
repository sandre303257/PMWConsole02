Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

        If Not IsPostBack AndAlso Not manager.HasPassword(User.Identity.GetUserId()) Then
            PageHeader.InnerText = "Set password"
            PageDescription.InnerText = "You do not have a local password for this site. Add a local password so you can log in without an external login."
            tbCurrentPassword.Visible = False
            btnChangePassword.Visible = False
            btnSetPassword.Visible = True
        End If
    End Sub

    Protected Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()
        Dim result As IdentityResult = manager.ChangePassword(User.Identity.GetUserId(), tbCurrentPassword.Text, tbPassword.Text)
        If result.Succeeded Then
            Dim userInfo = manager.FindById(User.Identity.GetUserId())
            signInManager.SignIn(userInfo, isPersistent := False, rememberBrowser := False)
            Response.Redirect("~/Account/Manage.aspx?m=ChangePwdSuccess")
        Else
            ErrorMessage.Text = result.Errors.FirstOrDefault()
        End If
    End Sub

        Protected Sub btnSetPassword_Click(sender As Object, e As EventArgs)
            If IsValid Then
                ' Create the local login info and link the local account to the user
                Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
                Dim result As IdentityResult = manager.AddPassword(User.Identity.GetUserId(), tbPassword.Text)
                If result.Succeeded Then
                    Response.Redirect("~/Account/Manage.aspx?m=SetPwdSuccess")
                Else
                    ErrorMessage.Text = result.Errors.FirstOrDefault()
                End If
            End If
        End Sub

End Class