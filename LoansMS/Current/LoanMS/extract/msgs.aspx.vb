Public Class msgs
    Inherits System.Web.UI.Page

    Dim ta As New myQuickTableAdapters.get_child_reports_displayTableAdapter
    Dim ds As New myQuick

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then
            GetReports()
        ElseIf Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Protected Friend Sub BtnExtractClick(sender As Object, e As EventArgs)

        Dim str As String = Nothing
        Session("report_type") = slReportName.Value
        str = "ReportName=" & Session("report_name") & "&ReportType=" & Session("report_type") & "&CustomerMobile=" & txtMobileNo.Value _
            & "&CustomerName=" & txtCustomerName.Value & "&ReportStart=" & txtmessagestartdate.Value & "&ReportEnd=" & txtmessageenddate.Value

        Response.Redirect("~/extract/viewreport.aspx?" & str)

    End Sub

    Protected Friend Sub BtnCancelClick(sender As Object, e As EventArgs)

        txtCustomerName.Value = Nothing
        txtmessagestartdate.Value = Nothing
        txtmessageenddate.Value = Nothing
        txtMobileNo.Value = Nothing

    End Sub

    Private Sub GetReports()

        ds.EnforceConstraints = False
        ta.Fill(ds.get_child_reports_display, Session("report_id"), HttpContext.Current.Session("active_user"))

        With slReportName
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_child_reports_display))
            .DataTextField = "reportdisplay"
            .DataValueField = "reportdisplay"
            .DataBind()
            .Items.Insert(0, "")
        End With

    End Sub


End Class