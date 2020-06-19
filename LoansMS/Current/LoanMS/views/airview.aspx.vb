Public Class Airview

    Inherits System.Web.UI.Page

    Dim ta As New myQuickTableAdapters.get_airtime_listingTableAdapter
    Dim ds As New myQuick
    Dim qresp As String = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            Response.Redirect("~/accounts/logoutsuccess.aspx")
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Private Sub Clear()

        txtAccount.Value = Nothing
        txtEndDate.Value = Nothing
        'txtMaxAmount.Value = Nothing
        'txtMinAmount.Value = Nothing
        txtMobile.Value = Nothing
        txtName.Value = Nothing
        txtStartDate.Value = Nothing

        ds.EnforceConstraints = False

        With gview
            .DataSource = New List(Of String)
            .DataBind()
            .Visible = False
        End With

    End Sub

    Protected Friend Sub SearchClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If String.IsNullOrEmpty(txtAccount.Value) = False OrElse String.IsNullOrEmpty(txtEndDate.Value) = False _
            OrElse String.IsNullOrEmpty(txtMobile.Value) = False OrElse String.IsNullOrEmpty(txtName.Value) = False _
            OrElse String.IsNullOrEmpty(txtStartDate.Value) = False Then
            GetMessagesData()
        End If

    End Sub

    Protected Friend Sub CancelClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Private Sub GetMessagesData()

        ds.EnforceConstraints = False
        ta.Fill(ds.get_airtime_listing, If(txtMobile.Value = "", Nothing, txtMobile.Value),
                If(txtName.Value = "", Nothing, txtName.Value),
                If(txtAccount.Value = "", Nothing, txtAccount.Value),
                If(txtStartDate.Value = Nothing, Nothing, CDate(txtStartDate.Value)),
                If(txtEndDate.Value = Nothing, Nothing, CDate(txtEndDate.Value)), Nothing, Nothing, qresp)

        'If ds.get_airtime_listing.Rows.Count > 0 Then

        With gview
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_airtime_listing))
            .DataBind()
            .UseAccessibleHeader = True
            .Visible = True
        End With

        'Else

        '    Clear()

        'End If

    End Sub

    Protected Friend Sub Gview_PageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gview.PageIndexChanging

        gview.PageIndex = e.NewPageIndex
        GetMessagesData()

    End Sub

End Class