
Imports System.Web.Script.Serialization

Public Class Mobrequests

    Inherits System.Web.UI.Page

    Dim ds As New dsBI
    Dim ta As New dsBITableAdapters.spGetMobileRequestsTableAdapter

    Dim ser As New JavaScriptSerializer

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
        txtStartDate.Value = ""
        txtEndDate.Value = ""

    End Sub

    Protected Friend Sub GetMobile(ByVal sender As Object, ByVal e As System.EventArgs)

        ds.EnforceConstraints = False
        ta.Fill(ds.spGetMobileRequests, txtMobile.Value, CDate(IIf(txtStartDate.Value = "", Nothing, txtStartDate.Value)), CDate(IIf(txtEndDate.Value = "", Nothing, txtEndDate.Value)))

        Dim reqid As String = Nothing
        Dim reqmob As String = Nothing
        Dim reqresp As String = Nothing
        Dim rawreq As String = Nothing
        Dim rawresp As String = Nothing
        Dim reqtyp As String = Nothing
        Dim mobno As String = Nothing
        Dim reqdt As Date = Nothing

        Dim dt As New DataTable
        With dt
            .Columns.Add("MobileNo")
            .Columns.Add("Type")
            .Columns.Add("Request")
            .Columns.Add("Response")
            .Columns.Add("Date")
        End With

        With ds.spGetMobileRequests

            For i = 0 To .Rows.Count - 1

                Dim tr As DataRow
                tr = dt.NewRow

                reqdt = .Rows(i).Item(4)
                reqid = .Rows(i).Item(0)
                rawreq = .Rows(i).Item(1)
                rawresp = .Rows(i).Item(2)

                If .Rows(i).Item(1).ToString.Count(Function(x) x = ":") = 4 Then
                    Dim req = ser.Deserialize(Of LoanStatusRequests)(.Rows(i).Item(1))
                    reqmob = "Mobile No: " & req.req.p0
                    mobno = req.req.p0
                    reqtyp = "Loan Inquiry"
                    If .Rows(i).Item(2) = "" Then
                        reqresp = Nothing
                    Else
                        Dim rs = ser.Deserialize(Of LoanStatusResponses)(.Rows(i).Item(2))
                        reqresp = "Account Status:" & IIf(rs.rs.p0 = 1, "Inactive", "Active") & " Current Loans:" & rs.rs.p1 & " Pending Instalments:" & rs.rs.p2 & " Loan Amount:" & rs.rs.p3 _
                            & " Next Instal Date:" & rs.rs.p9 & " Next Instalment:" & rs.rs.p5 & " Unpaid Instalments:" & rs.rs.p10
                    End If
                ElseIf .Rows(i).Item(1).ToString.Count(Function(x) x = ":") = 6 Then
                    Dim req = ser.Deserialize(Of LoanScoringRequests)(.Rows(i).Item(1))
                    reqmob = "Mobile No:" & req.req.p0 & " Product:" & req.req.p1 & " Period:" & req.req.p8
                    mobno = req.req.p0
                    reqtyp = "Loan Scoring"
                    If .Rows(i).Item(2) = "" Then
                        reqresp = Nothing
                    Else
                        Dim rs = ser.Deserialize(Of LoanScoringResponses)(.Rows(i).Item(2))
                        reqresp = "Account Status:" & IIf(rs.rs.p0 = 0, "Inactive", "Active") & " Running Periods:" & rs.rs.p1 & " Remaining Instalments:" & rs.rs.p2 & " Loan Balance:" & rs.rs.p3 _
                            & " Interest Rate:" & FormatPercent(rs.rs.p4 / 100, 2, TriState.True, TriState.True, TriState.False) & " Instalment:" & rs.rs.p5 & " Defaulter Status:" & IIf(rs.rs.p6 = "01", "Defaulter", "Good")
                    End If
                ElseIf .Rows(i).Item(1).ToString.Count(Function(x) x = ":") = 8 Then
                    Dim req = ser.Deserialize(Of LoanPostingRequests)(.Rows(i).Item(1))
                    reqmob = "Mobile No:" & req.req.p0 & " Product:" & req.req.p1 & " Amount: " & FormatNumber(req.req.p3, 2, TriState.True, TriState.True, TriState.True) _
                        & " Period:" & req.req.p4 & " Loan Type:" & IIf(req.req.p5 = "N", "New", "Top-Up")
                    mobno = req.req.p0
                    reqtyp = "Loan Posting"
                    If .Rows(i).Item(2) = "400 Bad request" OrElse .Rows(i).Item(2) = "400 Bad Request" Then
                        reqresp = "400: Bad Request"
                    ElseIf .Rows(i).Item(2) = "401 Unauthorised" Then
                        reqresp = "Fatal Error"
                    Else
                        Dim rs = ser.Deserialize(Of LoanPostingResponses)(.Rows(i).Item(2))
                        reqresp = IIf(rs.rs.p7 = 200, "Posted", IIf(rs.rs.p7 = 400, "Bad Request", IIf(rs.rs.p7 = 401, "Fatal Error", IIf(rs.rs.p7 = 407, "FinMFI Defaulter", IIf(rs.rs.p7 = 406, "Not Eliigible", IIf(rs.rs.p7 = 408, "System Timeout", IIf(rs.rs.p7 = 409, "Duplicate Mobile No", IIf(rs.rs.p7 = 500, "Internal Error", "Service Unavailable"))))))))
                    End If

                End If

                With tr
                    .Item(0) = mobno
                    .Item(1) = reqtyp
                    .Item(2) = reqmob
                    .Item(3) = reqresp
                    .Item(4) = reqdt
                End With

                dt.Rows.Add(tr)

            Next

        End With

        With gview
            .DataSource = dt
            .DataBind()
            .Visible = True
            .HeaderRow.TableSection = TableRowSection.TableHeader
        End With

    End Sub

    Protected Friend Sub Reset(ByVal sender As Object, ByVal e As System.EventArgs)

        ClearClose()

    End Sub

End Class

Public Class LoanStatusRequests

    Public Property req As LoanStatusRequest

    Public Sub New()
        req = New LoanStatusRequest
    End Sub

End Class

Public Class LoanStatusRequest

    Public Property p0 As String
    Public Property p6 As String
    Public Property p7 As String

    Public Sub New()

    End Sub

End Class

Public Class LoanScoringRequests

    Public Property req As LoanScoringRequest

    Public Sub New()
        req = New LoanScoringRequest
    End Sub

End Class

Public Class LoanScoringRequest

    Public Property p0 As String
    Public Property p1 As String
    Public Property p6 As String
    Public Property p7 As String
    Public Property p8 As String

    Public Sub New()

    End Sub

End Class

Public Class LoanPostingRequests

    Public Property req As LoanPostingRequest

    Public Sub New()

    End Sub

End Class

Public Class LoanPostingRequest

    Public Property p0 As String
    Public Property p1 As String
    Public Property p3 As String
    Public Property p4 As String
    Public Property p5 As String
    Public Property p6 As String
    Public Property p7 As String

    Public Sub New()

    End Sub

End Class

Public Class LoanScoringResponses

    Public Property rs As LoanScoringResponse

    Public Sub New()

    End Sub

End Class

Public Class LoanScoringResponse

    Public Property p0 As String
    Public Property p1 As String
    Public Property p2 As String
    Public Property p3 As String
    Public Property p4 As String
    Public Property p5 As String
    Public Property p6 As String

    Public Sub New()

    End Sub

End Class

Public Class LoanPostingResponses

    Public Property rs As LoanPostingResponse

    Public Sub New()

    End Sub

End Class

Public Class LoanPostingResponse

    Public Property p7 As String

    Public Sub New()

    End Sub

End Class

Public Class LoanStatusResponses

    Public Property rs As LoanStatusResponse

    Public Sub New()

    End Sub

End Class

Public Class LoanStatusResponse

    Public Property p0 As String
    Public Property p1 As String
    Public Property p2 As String
    Public Property p3 As String
    Public Property p9 As String
    Public Property p5 As String
    Public Property p10 As String

    Public Sub New()

    End Sub

End Class