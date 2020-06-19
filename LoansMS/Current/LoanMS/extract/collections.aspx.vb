Public Class Collections
    Inherits System.Web.UI.Page

    Dim ds As New myQuick
    Dim ta As New myQuickTableAdapters.get_child_reports_displayTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then
            GetReports()
        ElseIf Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Protected Sub BtnExtractClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim str As String = Nothing
        str = "ReportName=" & Session("report_name") & "&ReportType=" & slDebtCollection.Value & "&ReportDate=" & txtreportdate.Value & "&ReportStart=" & txtreportstartdate.Value & "&ReportEnd=" & txtreportenddate.Value & "&CreditOfficer=" & slCreditOfficer.Value & "&BranchVisits=" & txtBranchVisits.Value

        Response.Redirect("~/extract/viewreport.aspx?" & str)

    End Sub

    Private Sub GetReports()

        ds.EnforceConstraints = False
        ta.Fill(ds.get_child_reports_display, Session("report_id"), HttpContext.Current.Session("active_user"))

        With slDebtCollection
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_child_reports_display))
            .DataTextField = "reportdisplay"
            .DataValueField = "reportdisplay"
            .DataBind()
            .Items.Insert(0, "")
        End With

    End Sub

End Class