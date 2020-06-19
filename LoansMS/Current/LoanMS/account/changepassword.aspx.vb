Public Class changepassword
    Inherits System.Web.UI.Page

    Dim u As New User

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then
            Clear()
        Else
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Protected Friend Sub ChangeClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If txtConfirmPassword.Value <> "" And txtCurrentPassword.Value <> "" And txtNewPassword.Value <> "" Then
            u.UserChangePassword(Session("active_user"), txtNewPassword.Value, txtCurrentPassword.Value)
            If u.mapID = "Success" Then
                Response.Redirect("/account/logoutsuccess.aspx")
            Else
                Clear()
            End If

        End If

    End Sub

    Protected Friend Sub CancelClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Private Sub Clear()

        txtConfirmPassword.Value = Nothing
        txtCurrentPassword.Value = Nothing
        txtNewPassword.Value = Nothing

    End Sub

End Class