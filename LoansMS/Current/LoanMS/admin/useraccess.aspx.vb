Public Class Useraccess

    Inherits System.Web.UI.Page

    Dim u As New User
    Dim ds As New myQuick
    Dim ta As New myQuickTableAdapters.get_active_rolesTableAdapter
    Dim ta1 As New myQuickTableAdapters.get_report_menus_roleTableAdapter
    Dim SelMenus As New SortedDictionary(Of String, String)
    Dim SelReports As New SortedDictionary(Of String, String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            Response.Redirect("~/account/logoutsuccess.aspx")
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Protected Friend Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        For Each nd As TreeNode In tvReports.Nodes
            If nd.Value = "rootMenus" Then
                For Each cn As TreeNode In nd.ChildNodes
                    GetChildMenuNodes(cn)
                Next
            Else
                For Each cn As TreeNode In nd.ChildNodes
                    GetChildReportNodes(cn)
                Next
            End If
        Next

        u.UpdateUserAccess(txtActiveRole.Value, SelMenus, SelReports)

    End Sub

    Private Sub GetChildMenuNodes(ByVal Child As TreeNode)

        SelMenus.Add(Child.Value, Child.Checked)
        For Each cnode As TreeNode In Child.ChildNodes
            GetChildMenuNodes(cnode)
        Next

    End Sub

    Private Sub GetChildReportNodes(ByVal Child As TreeNode)

        SelReports.Add(Child.Value, Child.Checked)
        For Each cnode As TreeNode In Child.ChildNodes
            GetChildReportNodes(cnode)
        Next

    End Sub

    Protected Friend Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Protected Friend Sub BtnLoadAccess(ByVal sender As System.Object, ByVal e As System.EventArgs)

        txtActiveRole.Value = tvRoles.SelectedNode.Text

        ta1.Fill(ds.get_report_menus_role, txtActiveRole.Value)
        Dim tv As DataView = New DataView(ds.get_report_menus_role) With {.RowFilter = "Type = 'Menu' AND [Parent] IS NULL"}
        Dim rv As DataView = New DataView(ds.get_report_menus_role) With {.RowFilter = "Type = 'Report' AND [Parent] IS NULL"}

        If tvReports.Nodes.Count > 0 Then
            tvReports.Nodes.Clear()
        End If

        Dim md As New TreeNode With {.Text = "Menus", .Value = "rootMenus"}
        Dim rd As New TreeNode With {.Text = "Reports", .Value = "rootReports"}

        For Each rw As DataRowView In tv

            Dim nd As New TreeNode
            With nd
                .Text = rw("Description")
                .Value = rw("Control")
                .Checked = rw("Access")
            End With

            GetChildMenus(ds.get_report_menus_role, nd)
            md.ChildNodes.Add(nd)

        Next

        For Each rw As DataRowView In rv

            Dim nd As New TreeNode
            With nd
                .Text = rw("Description")
                .Value = rw("Control")
                .Checked = rw("Access")
            End With

            GetChildReports(ds.get_report_menus_role, nd)
            rd.ChildNodes.Add(nd)

        Next

        tvReports.Nodes.Add(md)
        tvReports.Nodes.Add(rd)

        With gview
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_report_menus_role))
            .DataBind()
            '.HeaderRow.TableSection = TableRowSection.TableHeader
            .Visible = True
        End With

    End Sub

    Private Sub GetChildReports(ByVal AllAccess As DataTable, ByVal ParentNode As TreeNode)

        Dim vw As DataView
        vw = New DataView(AllAccess) With {.RowFilter = "[Type] = 'Report' AND [Parent] = '" & ParentNode.Value.ToString & "'"}

        For Each cvw As DataRowView In vw

            Dim cnd As New TreeNode
            With cnd
                .Text = cvw("Description")
                .Value = cvw("Control")
                .Checked = cvw("Access")
            End With

            GetChildReports(AllAccess, cnd)
            ParentNode.ChildNodes.Add(cnd)

        Next

    End Sub

    Private Sub GetChildMenus(ByVal AllAccess As DataTable, ByVal ParentNode As TreeNode)

        Dim vw As DataView
        vw = New DataView(AllAccess) With {.RowFilter = "[Type] = 'Menu' AND [Parent] = '" & ParentNode.Value.ToString & "'"}

        For Each cvw As DataRowView In vw

            Dim cnd As New TreeNode
            With cnd
                .Text = cvw("Description")
                .Value = cvw("Control")
                .Checked = cvw("Access")
            End With

            GetChildMenus(AllAccess, cnd)
            ParentNode.ChildNodes.Add(cnd)

        Next

    End Sub

    Private Sub Clear()

        tvRoles.DataSource = Nothing
        tvRoles.Nodes.Clear()

        ds.EnforceConstraints = False
        ta.Fill(ds.get_active_roles)

        For Each dr As DataRow In ds.get_active_roles.Rows
            Dim nd As New TreeNode
            With nd
                .Text = dr.Item(1).ToString
                .Value = dr.Item(0).ToString
            End With
            tvRoles.Nodes.Add(nd)
        Next

    End Sub

End Class