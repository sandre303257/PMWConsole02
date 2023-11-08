Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Public Partial Class VerifyPhoneNumber
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs)
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim phonenumber = Request.QueryString("PhoneNumber")
        Dim code = manager.GenerateChangePhoneNumberToken(User.Identity.GetUserId(), phonenumber)
        HiddenField("PhoneNumber") = phonenumber
    End Sub

    Protected Sub Code_Click(sender As Object, e As EventArgs)
        If Not ModelState.IsValid Then
            Code.ErrorText = "Invalid code"
            Code.IsValid = false
            Return
        End If

        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager = Context.GetOwinContext().[Get](Of ApplicationSignInManager)()

        Dim result = manager.ChangePhoneNumber(User.Identity.GetUserId(), HiddenField("PhoneNumber").ToString(), Code.Text)

        If result.Succeeded Then
            Dim appUser = manager.FindById(User.Identity.GetUserId())

            If appUser IsNot Nothing Then
                signInManager.SignIn(appUser, isPersistent := False, rememberBrowser := False)
                Response.Redirect("/Account/Manage.aspx?m=AddPhoneNumberSuccess")
            End If
        End If

        ' If we got this far, something failed, redisplay form
        Code.ErrorText = "Invalid code"
        Code.IsValid = false
    End Sub
End Class