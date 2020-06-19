

'Imports Microsoft.Reporting.WebForms
Imports System.Configuration


Public Class Home
    Inherits System.Web.UI.MasterPage

    Dim ds As New dsBI

    Dim ts As New myQuick
    Dim ta As New myQuickTableAdapters.get_display_roleTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(60))
        Response.Cache.SetCacheability(HttpCacheability.Private)
        Response.Cache.SetSlidingExpiration(True)

        If Session("active_user") = Nothing Then
            FormsAuthentication.SignOut()
            Session.Abandon()
        End If

        If Request.IsAuthenticated = True And Session("LoginStatus") = "Success" Then
            mnuMaster.Items.Clear()
            ActivateModule()
        End If

    End Sub

    Private Sub ActivateModule()

        Dim au As String = HttpContext.Current.Session("active_user")

        ts.EnforceConstraints = False
        ta.Fill(ts.get_display_role, au)

        Dim tv As DataView
        tv = New DataView(ts.get_display_role) With {.RowFilter = "menuparent is NULL"}

        For Each rw As DataRowView In tv

            Dim mnu As New MenuItem
            With mnu
                .Text = rw("menudisplay")
                .NavigateUrl = rw("navigationlink")
                .Value = rw("menuid")
            End With

            GetChildMenus(ts.get_display_role, mnu)
            mnuMaster.Items.Add(mnu)

        Next

    End Sub

    Private Sub GetChildMenus(ByVal AllRoles As DataTable, ByVal ParentMenu As MenuItem)

        Dim vw As DataView
        vw = New DataView(AllRoles) With {.RowFilter = "menuparent= '" & ParentMenu.Value.ToString & "'"}

        For Each cvw As DataRowView In vw

            Dim chmnu As New MenuItem
            With chmnu
                .Text = cvw("menudisplay")
                .NavigateUrl = cvw("navigationlink")
                .Value = cvw("menuid")
            End With

            ParentMenu.ChildItems.Add(chmnu)

            GetChildMenus(AllRoles, chmnu)

        Next

    End Sub

End Class