Public Class Crbrequests

    Inherits System.Web.UI.Page

    Dim ds As New dsBI
    Dim ta As New dsBITableAdapters.spGetCRBRequestsTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ClearClose()
        ElseIf Request.IsAuthenticated = False Then
            Response.Redirect("~/account/logoutsuccess.aspx")
            FormsAuthentication.RedirectToLoginPage()
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Private Sub ClearClose()

        ds.EnforceConstraints = False

        gview.DataSource = New List(Of String)
        gview.DataBind()
        gview.Visible = False

        txtMobile.Value = ""
        txtName.Value = ""
        txtNationalID.Value = ""
        txtQueryDate.Value = ""

    End Sub

    Protected Friend Sub GetCRB(ByVal sender As Object, ByVal e As System.EventArgs)

        ta.Fill(ds.spGetCRBRequests, IIf(txtName.Value = "", Nothing, txtName.Value), IIf(txtNationalID.Value = "", Nothing, txtNationalID.Value), IIf(txtMobile.Value = "", Nothing, txtMobile.Value), Nothing, lsClientType.Value)

        With gview
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetCRBRequests))
            .DataBind()
            .Visible = True
        End With

    End Sub

    Protected Friend Sub Reset(ByVal sender As Object, ByVal e As System.EventArgs)

        ClearClose()

    End Sub

End Class