

Public Class Logoutsuccess

    Inherits System.Web.UI.Page

    Dim u As New User

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        u.Logout()
        Session.Clear()
        Session.RemoveAll()
        Session.Abandon()
        FormsAuthentication.SignOut()


    End Sub

End Class