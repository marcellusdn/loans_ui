


Public Class mobileportfolio

    Inherits System.Web.UI.Page

    Dim ds As New dsBI
    Dim ta As New dsBITableAdapters.spGetMobileLoansTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            Response.Redirect("~/account/logoutsuccess.aspx")
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Private Sub Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.ServerClick

        ta.Fill(ds.spGetMobileLoans, Today.Date, If(txtAccount.Value = "", Nothing, txtAccount.Value),
                If(txtMobile.Value = "", Nothing, txtMobile.Value), If(txtName.Value = "", Nothing, txtName.Value),
                Nothing, Nothing, Nothing, Nothing, Nothing, If(txtEmployer.Value = "", Nothing, txtEmployer.Value),
                Nothing, Nothing)

        With gview

            .AllowPaging = True
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetMobileLoans))
            .DataBind()
            .Visible = True

        End With

    End Sub

    Private Sub GrviewPageChange(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles gview.PageIndexChanging

        ta.Fill(ds.spGetMobileLoans, Today.Date, If(txtAccount.Value = "", Nothing, txtAccount.Value),
                If(txtMobile.Value = "", Nothing, txtMobile.Value), If(txtName.Value = "", Nothing, txtName.Value),
                Nothing, Nothing, Nothing, Nothing, Nothing, If(txtEmployer.Value = "", Nothing, txtEmployer.Value), Nothing, Nothing)
        With gview

            .AllowPaging = True
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetMobileLoans))
            .PageIndex = e.NewPageIndex
            .DataBind()

        End With

    End Sub

    Protected Friend Sub Cancel(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Private Sub Clear()

        txtAccount.Value = Nothing
        txtEmployer.Value = Nothing
        txtMobile.Value = Nothing
        txtName.Value = Nothing

        ds.EnforceConstraints = False

        gview.DataSource = New List(Of String)
        gview.Visible = False

    End Sub

End Class