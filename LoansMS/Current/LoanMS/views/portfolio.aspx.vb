

Public Class Portfolio

    Inherits System.Web.UI.Page

    Dim ds As New dsBI
    Dim ta As New dsBITableAdapters.spGetPortfolioTableAdapter
    Dim pr As New dsBITableAdapters.spGetProductsTableAdapter
    Dim sc As New dsBITableAdapters.spGetSectorsTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ClearClose()
        ElseIf Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Private Sub ClearClose()

        ds.EnforceConstraints = False

        With gvPortfolio
            .DataSource = New List(Of String)
            .DataBind()
            .Visible = False
        End With

        pr.Fill(ds.spGetProducts)
        sc.Fill(ds.spGetSectors)

        With lsProduct
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetProducts))
            .DataTextField = "loanname"
            .DataValueField = "loanname"
            .DataBind()
            .Items.Insert(0, "")
        End With

        With lsSector
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetSectors))
            .DataTextField = "sector"
            .DataValueField = "sector"
            .DataBind()
            .Items.Insert(0, "")
        End With

    End Sub

    Protected Friend Sub LoadPortfolio(ByVal sender As Object, ByVal e As System.EventArgs)

        GetData()

    End Sub

    Protected Friend Sub Reset(ByVal sender As Object, ByVal e As System.EventArgs)

        ClearClose()

    End Sub

    Protected Friend Sub GvPortfolio_PageIndexChanged(sender As Object, e As GridViewPageEventArgs) Handles gvPortfolio.PageIndexChanging

        gvPortfolio.PageIndex = e.NewPageIndex
        GetData()

    End Sub

    Private Sub GetData()

        ds.EnforceConstraints = False

        ta.Fill(ds.spGetPortfolio, Today.Date,
                If(txtAccountNo.Value = "", Nothing, txtAccountNo.Value),
                If(txtCustomer.Value = "", Nothing, txtCustomer.Value),
                If(txtBaseNo.Value = "", Nothing, txtBaseNo.Value),
                If(lsSector.Value = "", Nothing, lsSector.Value),
                If(lsProduct.Value = "", Nothing, lsProduct.Value),
                IIf(txtMaxAmt.Value = "", Nothing, CDbl(txtMaxAmt.Value)),
                If(txtMinAmt.Value = "", Nothing, CDbl(txtMinAmt.Value)), txtStartDate.Value, txtEndDate.Value)

        With gvPortfolio
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetPortfolio))
            .DataBind()
            .Visible = True
        End With

    End Sub

End Class