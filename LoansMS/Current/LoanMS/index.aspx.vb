Public Class Index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.IsAuthenticated = False Then
            Response.Redirect("~/account/logoutsuccess.aspx")
        End If

    End Sub

End Class