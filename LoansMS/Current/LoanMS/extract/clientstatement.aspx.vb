

Imports Microsoft.Reporting.WebForms

Public Class ClientStatement
    Inherits System.Web.UI.Page

    Dim st As New Statement

    Dim rd As ReportDataSource
    Dim rd1 As ReportDataSource

    Dim ds As New myQuick
    Dim stm As New myQuickTableAdapters.get_postbank_statementTableAdapter
    Dim cln As New myQuickTableAdapters.get_postbank_clientdataTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then
            'GetReports()
        ElseIf Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Protected Sub BtnExtractClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtract.ServerClick

        'Dim bt As DateTime
        'Dim et As DateTime
        'Dim tt As Integer

        'Try

        'If String.IsNullOrEmpty(txtAccountNo.Value) = False And IsNumeric(txtAccountNo.Value) = True And String.IsNullOrEmpty(txtstartdate.Value) = False And String.IsNullOrEmpty(txtAccountNo.Value) = False Then

        '    bt = Now
        '    st.BookStatementRequest(txtstartdate.Value, txtenddate.Value, txtAccountNo.Value)

        'End If

        'While st.IsProcessed = False
        '    et = Now
        '    tt = DateDiff(DateInterval.Second, bt, et)
        'End While

        'et = Now


        Dim str As String = Nothing
        'str = "ReportName=" & Session("report_name") & "&ReportID=" & st.StatementID & "&ReportAccount=" & txtAccountNo.Value
        str = "ReportName=" & Session("report_name") & "&ReportID=" & "PSid_e7317bc6421359aa1b4" & "&ReportAccount=" & "0001050012814"
        Response.Redirect("~/extract/viewreport.aspx?" & str)



        'Catch ex As Exception

        'End Try

    End Sub

End Class