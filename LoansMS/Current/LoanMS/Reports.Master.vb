Public Class Reports
    Inherits System.Web.UI.MasterPage

    Dim ds As New dsBI

    Dim ts As New myQuick
    Dim ta As New myQuickTableAdapters.get_display_roleTableAdapter

    Dim rps As New myQuickTableAdapters.get_master_reports_displayTableAdapter

    Dim rpts As New SortedDictionary(Of String, String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(60))
        Response.Cache.SetCacheability(HttpCacheability.Private)
        Response.Cache.SetSlidingExpiration(True)

        If Session("active_user") = Nothing Then
            FormsAuthentication.SignOut()
            Session.Abandon()
        End If

        If Not IsPostBack Then
            ActivateModule()
        ElseIf IsPostBack Then
            mnuMaster.Items.Clear()
            mnuReports.Items.Clear()
            If mnuMaster.Items.Count <= 0 Then
                ActivateModule()
            End If
            If mnuReports.Items.Count <= 0 Then
                ActivateModule()
            End If
        End If

    End Sub

    Private Sub ActivateModule()

        Dim au As String = HttpContext.Current.Session("active_user")
        ds.EnforceConstraints = False

        ts.EnforceConstraints = False
        ta.Fill(ts.get_display_role, au)
        rps.Fill(ts.get_master_reports_display, au)

        Dim rv As DataView
        rv = New DataView(ts.get_master_reports_display)

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

        For Each mw As DataRowView In rv

            Dim rnu As New MenuItem
            With rnu
                .Text = mw("reportdisplay")
                .Value = mw("reportid")
            End With
            rpts.Add(mw("reportid"), mw("reportlink"))
            mnuReports.Items.Add(rnu)

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

    Protected Friend Sub MnuReports_MenuItemClick(sender As Object, e As MenuEventArgs)

        Session("report_id") = mnuReports.SelectedValue
        Session("report_name") = e.Item.Text
        Response.Redirect(rpts.Item(Session("report_id")))

    End Sub

End Class