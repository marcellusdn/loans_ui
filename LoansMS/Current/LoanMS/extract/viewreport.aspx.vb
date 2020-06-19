
Imports Microsoft.Reporting.WebForms
Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Public Class ViewReport

    Inherits System.Web.UI.Page

    Dim rd As ReportDataSource
    Dim rd1 As ReportDataSource

    Dim bi As New dsBI
    Dim mq As New myQuick

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then

            If Session("report_name") = "Messages Reports" Then
                LoadMessagesReport()
            ElseIf Session("report_name") = "Topup Reports" Then
                LoadAirtimeReport()
            ElseIf Session("report_name") = "PAR Reports" Then
                LoadPAR()
            ElseIf Session("report_name") = "Provisions Reports" Then
                'LoadCRBUpdates()
            ElseIf Session("report_name") = "CRB Update Reports" Then
                LoadCRBUpdates()
            ElseIf Session("report_name") = "CRB Submission Reports" Then
                LoadCRB()
            ElseIf Session("report_name") = "Portfolio Reports" Then
                LoadPortfolios()
            ElseIf Session("report_name") = "Collection Reports" Then
                LoadCollections()
            ElseIf Session("report_name") = "PostBank Client Statement" Then
                LoadStatement()
            End If

        ElseIf Request.IsAuthenticated = False Then

            FormsAuthentication.RedirectToLoginPage()

        End If

    End Sub

    Protected Friend Sub LoadMessagesReport()

        mq.EnforceConstraints = False

        Dim ta As New myQuickTableAdapters.get_messages_listingTableAdapter
        Dim Qresponse As String = Nothing
        ta.Fill(mq.get_messages_listing, Request.QueryString("CustomerMobile"), Request.QueryString("CustomerName"),
                Nothing, Request.QueryString("ReportStart"), CDate(Request.QueryString("ReportEnd")),
                If(Session("report_type") = "General Messages", Nothing, If(Session("report_type") = "Successful Messages", "Sent", "Failed")), Nothing, Qresponse)
        rd = New ReportDataSource("ds", mq.Tables(mq.Tables.IndexOf("get_messages_listing")))

        With rvMain
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.ReportPath = Server.MapPath("~/reports/MessagesReport.rdlc")
            .LocalReport.DataSources.Clear()
            .LocalReport.DataSources.Add(rd)
            .LocalReport.Refresh()
        End With

    End Sub

    Protected Friend Sub LoadAirtimeReport()

        mq.EnforceConstraints = False

        Dim ta As New myQuickTableAdapters.get_airtime_listingTableAdapter
        Dim Qresponse As String = Nothing
        ta.Fill(mq.get_airtime_listing, Request.QueryString("CustomerMobile"), Request.QueryString("CustomerName"),
                Nothing, Request.QueryString("ReportStart"), Request.QueryString("ReportEnd"), Nothing,
                If(Session("report_type") = "Global Topup", Nothing, If(Session("report_type") = "Successful Topup", "Sent", "Failed")), Qresponse)
        rd = New ReportDataSource("ds", mq.Tables(mq.Tables.IndexOf("get_airtime_listing")))

        With rvMain
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.ReportPath = Server.MapPath("~/reports/AirtimeReport.rdlc")
            .LocalReport.DataSources.Clear()
            .LocalReport.DataSources.Add(rd)
            .LocalReport.Refresh()
        End With

    End Sub

    Protected Friend Sub LoadPortfolios()

        bi.EnforceConstraints = False

        If Session("report_type") = "Mobile Portfolio" Then

            Dim ta As New dsBITableAdapters.spGetMobileLoansTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetMobileLoans, If(Session("report_date") = Nothing, Today.Date, CDate(Session("report_date"))), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetMobileLoans")))
            Dim dtr As New ReportParameter("reportdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))

            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/MobileLoans.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
            End With

        ElseIf Session("report_type") = "Advances Portfolio" Then

            Dim ta As New dsBITableAdapters.spGetPortfolioTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetPortfolio, CDate(Session("report_date")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetPortfolio")))
            Dim dtr As New ReportParameter("reportdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))

            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/LoanPortfolio.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
            End With

        ElseIf Session("report_type") = "Sector Portfolio" Then

            Dim ta As New dsBITableAdapters.spGetSectorPortfolioTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetSectorPortfolio, CDate(Session("report_date")), "Sector")
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetSectorPortfolio")))
            Dim dtr As New ReportParameter("reportdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))

            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/SectorPortfolio.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
            End With

        ElseIf Session("report_type") = "Products Portfolio" Then

            Dim ta As New dsBITableAdapters.spGetSectorPortfolioTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetSectorPortfolio, CDate(Session("report_date")), "Product")
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetSectorPortfolio")))
            Dim dtr As New ReportParameter("reportdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))

            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/SectorPortfolio.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
            End With

        ElseIf Session("report_type") = "Comprehensive Portfolio" Then

            Dim ta As New dsBITableAdapters.spGetComprehensivePortfolioTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetComprehensivePortfolio, CDate(Session("report_date")))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetComprehensivePortfolio")))
            Dim dtr As New ReportParameter("reportdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))

            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/ComprehensivePortfolio.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
            End With

        End If

    End Sub

    Protected Friend Sub LoadPAR()

        bi.EnforceConstraints = False

        If Session("report_type") = "Current PAR" Then

            Dim ta As New dsBITableAdapters.spGetCurrentPARReportTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetCurrentPARReport, CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetCurrentPARReport")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/CurrentPAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Prudential (CBK) PAR" Then

            Dim ta As New dsBITableAdapters.spGetPrudentialPARTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetPrudentialPAR, CDate(Session("report_date")))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetPrudentialPAR")))

            Dim dtr As New ReportParameter("reportdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/PrudentialPAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Sector PAR" Then

            Dim ta As New dsBITableAdapters.spGetSectorPARTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetSectorPAR, CDate(Session("report_date")), "Sector")
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetSectorPAR")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            Dim rtyp As New ReportParameter("reportname", "Sectors")
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/SectorPAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.SetParameters(rtyp)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Product PAR" Then

            Dim ta As New dsBITableAdapters.spGetSectorPARTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetSectorPAR, CDate(Session("report_date")), "Product")
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetSectorPAR")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            Dim rtyp As New ReportParameter("reportname", "Products")
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/SectorPAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.SetParameters(rtyp)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Advances PAR" Then

            Dim ta As New dsBITableAdapters.spGetAdvancesPARTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetAdvancesPAR, CDate(Session("report_date")))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetAdvancesPAR")))

            Dim dtr As New ReportParameter("reportdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/AdvancesPAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Sector (Pre 2014) PAR" Then

            Dim ta As New dsBITableAdapters.spGetPre2014PARTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetPre2014PAR, CDate(Session("report_date")))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetPre2014PAR")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/Pre2014PAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Sector (Post 2014) PAR" Then

            Dim ta As New dsBITableAdapters.spGetPost2014PARTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetPost2014PAR, CDate(Session("report_date")))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetPost2014PAR")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/Post2014PAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Detailed PAR" Then

            Dim ta As New dsBITableAdapters.spGetCurrentPARReportTableAdapter
            Dim ta1 As New dsBITableAdapters.spGetPARMovementTableAdapter
            Dim ta2 As New dsBITableAdapters.spGetPost2014PARTableAdapter
            Dim ta3 As New dsBITableAdapters.spGetPre2014PARTableAdapter

            Dim rd1 As ReportDataSource
            Dim rd2 As ReportDataSource
            Dim rd3 As ReportDataSource

            ta.Extend()
            ta2.Extend()
            ta1.Extend()
            ta3.Extend()

            ta.Fill(bi.spGetCurrentPARReport, CDate(Session("report_date")))
            ta1.Fill(bi.spGetPARMovement, CDate(Session("report_date")))
            ta2.Fill(bi.spGetPost2014PAR, CDate(Session("report_date")))
            ta3.Fill(bi.spGetPre2014PAR, CDate(Session("report_date")))

            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetCurrentPARReport")))
            rd1 = New ReportDataSource("ds1", bi.Tables(bi.Tables.IndexOf("spGetPARMovement")))
            rd2 = New ReportDataSource("ds2", bi.Tables(bi.Tables.IndexOf("spGetPost2014PAR")))
            rd3 = New ReportDataSource("ds3", bi.Tables(bi.Tables.IndexOf("spGetPre2014PAR")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(CDate(Session("report_date")) = Nothing, Today.Date, CDate(Session("report_date")))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/DetailedPAR.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.DataSources.Add(rd1)
                .LocalReport.DataSources.Add(rd2)
                .LocalReport.DataSources.Add(rd3)
                .LocalReport.Refresh()
            End With

        End If

    End Sub

    Protected Friend Sub LoadCRBUpdates()

        bi.EnforceConstraints = False

        If Request.QueryString("ReportType") = "Daily Facilities Update" Then

            Dim ta As New dsBITableAdapters.spGetDailyCRBUpdatesTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetDailyCRBUpdates, CDate(Request.QueryString("ReportStart")), CDate(Request.QueryString("ReportEnd")))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetDailyCRBUpdates")))

            Dim rps As New ReportParameter("startdate", CDate(IIf(Session("ReportStart") = Nothing, Today.Date, Session("ReportStart"))))
            Dim rpe As New ReportParameter("enddate", CDate(IIf(Session("ReportEnd") = Nothing, Today.Date, Session("ReportEnd"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/CRBDailyUpdateLoans.rdlc")
                .LocalReport.SetParameters(rps)
                .LocalReport.SetParameters(rpe)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Request.QueryString("ReportType") = "Daily Mobile Facilities" Then

            Dim ta As New dsBITableAdapters.spGetDailyMobileLoansTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetDailyMobileLoans, CDate(Request.QueryString("ReportStart")), CDate(Request.QueryString("ReportEnd")))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetDailyMobileLoans")))

            Dim rps As New ReportParameter("startdate", CDate(IIf(Session("ReportStart") = Nothing, Today.Date, Session("ReportStart"))))
            Dim rpe As New ReportParameter("enddate", CDate(IIf(Session("ReportEnd") = Nothing, Today.Date, Session("ReportEnd"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/CRBDailyMobileLoans.rdlc")
                .LocalReport.SetParameters(rps)
                .LocalReport.SetParameters(rpe)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        End If

    End Sub

    Protected Friend Sub LoadCollections()

        bi.EnforceConstraints = False
        mq.EnforceConstraints = False

        If Request.QueryString("ReportType") = "Debt Collection" Then

            Dim ta As New dsBITableAdapters.spGetDebtCollectionTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetDebtCollection, CDate(Request.QueryString("ReportStart")), CDate(Request.QueryString("ReportEnd")), Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetDebtCollection")))

            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/DebtCollection.rdlc")
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Request.QueryString("ReportType") = "PostBank Balances" Then

            Dim ta As New myQuickTableAdapters.get_postbank_balancesTableAdapter
            ta.Fill(mq.get_postbank_balances, Session("active_user"), Session("session_id"))

            rd = New ReportDataSource("ds", mq.Tables(mq.Tables.IndexOf("get_postbank_balances")))

            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/PostBankBalances.rdlc")
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        End If

    End Sub

    Protected Friend Sub LoadCRB()

        bi.EnforceConstraints = False

        If Session("report_type") = "Individual Submission" Then

            Dim ta As New dsBITableAdapters.spGetIndivCRBDataTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetIndivCRBData, CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetIndivCRBData")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/IndividualsCRBFile.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        ElseIf Session("report_type") = "Corporate Submission" Then

            Dim ta As New dsBITableAdapters.spGetBusinessCRBDataTableAdapter
            ta.Extend()
            ta.Fill(bi.spGetBusinessCRBData, CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            rd = New ReportDataSource("ds", bi.Tables(bi.Tables.IndexOf("spGetBusinessCRBData")))

            Dim dtr As New ReportParameter("rptdate", CDate(IIf(Session("report_date") = Nothing, Today.Date, Session("report_date"))))
            With rvMain
                .ProcessingMode = ProcessingMode.Local
                .LocalReport.ReportPath = Server.MapPath("~/reports/BusinessCRBFile.rdlc")
                .LocalReport.SetParameters(dtr)
                .LocalReport.DataSources.Clear()
                .LocalReport.DataSources.Add(rd)
                .LocalReport.Refresh()
            End With

        End If

    End Sub

    Protected Friend Sub LoadStatement()

        bi.EnforceConstraints = False
        mq.EnforceConstraints = False

        Dim stm As New myQuickTableAdapters.get_postbank_statementTableAdapter
        Dim cln As New myQuickTableAdapters.get_postbank_clientdataTableAdapter

        stm.Fill(mq.get_postbank_statement, Request.QueryString("ReportID"), Request.QueryString("ReportAccount"), Session("active_user"), Session("session_id"))
        cln.Fill(mq.get_postbank_clientdata, Request.QueryString("ReportID"), Session("active_user"), Session("session_id"))

        rd = New ReportDataSource("ds", mq.Tables(mq.Tables.IndexOf("get_postbank_statement")))
        rd1 = New ReportDataSource("ds1", mq.Tables(mq.Tables.IndexOf("get_postbank_clientdata")))

        With rvMain
            .ProcessingMode = ProcessingMode.Local
            .LocalReport.ReportPath = Server.MapPath("~/reports/PostBankStatement.rdlc")
            .LocalReport.DataSources.Clear()
            .LocalReport.DataSources.Add(rd)
            .LocalReport.DataSources.Add(rd1)
            .LocalReport.Refresh()
        End With

    End Sub

End Class