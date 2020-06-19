Public Class ErrorPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

End Class