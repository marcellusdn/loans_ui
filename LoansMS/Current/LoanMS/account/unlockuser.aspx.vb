Public Class UnlockUser
    Inherits System.Web.UI.Page

    Dim u As New User

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            Session.Abandon()
            Response.Redirect("/account/logoutsuccess.aspx")
        End If

    End Sub

    Protected Friend Sub ChangeClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If txtLoginID.Value <> "" Then
            u.UnlockUser(txtLoginID.Value)
            scriptstr = "<script> alert('" & resp & "')</script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "response", scriptstr)
            Clear()
        End If

    End Sub

    Protected Friend Sub CancelClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Private Sub Clear()

        txtLoginID.Value = Nothing
        txtUserName.Value = Nothing

    End Sub

    Protected Friend Sub CheckUser(sender As Object, e As EventArgs)

        If txtLoginID.Value <> Nothing Then
            u.GetDetails(txtLoginID.Value)
            txtUserName.Value = u._Name
        End If

    End Sub

End Class