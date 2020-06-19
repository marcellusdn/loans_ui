Public Class msgview
    Inherits System.Web.UI.Page

    Dim ds As New myQuick
    Dim ta As New myQuickTableAdapters.get_messages_listingTableAdapter
    Dim resp As String = Nothing

    Dim ref As New HtmlMeta

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(60))
        Response.Cache.SetCacheability(HttpCacheability.Private)
        Response.Cache.SetSlidingExpiration(True)

        If IsPostBack = False Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Protected Friend Sub Clear()

        txtAccount.Value = Nothing
        txtEndDate.Value = Nothing
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

    Protected Friend Sub BtnSearchClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If String.IsNullOrEmpty(txtAccount.Value) = False OrElse String.IsNullOrEmpty(txtEndDate.Value) = False _
            OrElse String.IsNullOrEmpty(txtMobile.Value) = False OrElse String.IsNullOrEmpty(txtName.Value) = False _
            OrElse String.IsNullOrEmpty(txtStartDate.Value) = False Then
            GetMessagesData()
        End If

    End Sub

    Private Sub GetMessagesData()

        ds.EnforceConstraints = False

        Dim eddt, stdt As DateTime
        Dim sdt, edt As Nullable(Of Date)

        If Date.TryParse(txtStartDate.Value, stdt) = True Then

            sdt = txtStartDate.Value

        Else

            sdt = Nothing

        End If

        If Date.TryParse(txtEndDate.Value, eddt) = True Then

            edt = txtEndDate.Value

        Else

            edt = Nothing

        End If

        ta.Fill(ds.get_messages_listing,
                If(String.IsNullOrEmpty(txtMobile.Value), Nothing, txtMobile.Value),
                If(String.IsNullOrEmpty(txtName.Value), Nothing, txtName.Value),
                If(String.IsNullOrEmpty(txtAccount.Value), Nothing, txtAccount.Value),
                If(sdt Is Nothing, Nothing, sdt),
                If(edt Is Nothing, Nothing, edt),
                Nothing, Nothing, resp)

        Dim dt As Integer = ds.get_messages_listing.Rows.Count

        If dt > 0 Then

            With gview
                .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_messages_listing))
                .DataBind()
                .UseAccessibleHeader = True
                .Visible = True
            End With

        Else

            Clear()

        End If

    End Sub

    Protected Friend Sub Gview_PageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gview.PageIndexChanging

        gview.PageIndex = e.NewPageIndex
        GetMessagesData()

    End Sub

End Class