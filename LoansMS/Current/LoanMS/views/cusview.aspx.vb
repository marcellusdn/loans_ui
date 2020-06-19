Public Class Cusview
    Inherits System.Web.UI.Page

    Dim ds As New dsBI
    Dim ta As New dsBITableAdapters.spGetClientsTableAdapter
    Dim cl As New dsBITableAdapters.spGetCustomerTypesTableAdapter

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

        With gview
            .DataSource = New List(Of String)
            .DataBind()
            .Visible = False
        End With

        cl.Fill(ds.spGetCustomerTypes)
        With lsClientType
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetCustomerTypes))
            .DataTextField = "description"
            .DataBind()
            .Items.Insert(0, "")
        End With

        txtClientNo.Value = ""
        txtMobile.Value = ""
        txtName.Value = ""
        txtNationalID.Value = ""

    End Sub

    Protected Friend Sub GetClients(ByVal sender As Object, ByVal e As System.EventArgs)

        ta.Fill(ds.spGetClients, txtNationalID.Value, txtName.Value, txtMobile.Value, txtClientNo.Value, lsClientType.Value)

        With gview
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetClients))
            .DataBind()
            .HeaderRow.TableSection = TableRowSection.TableHeader
            .Visible = True
        End With

    End Sub

    Protected Friend Sub Reset(ByVal sender As Object, ByVal e As System.EventArgs)

        ClearClose()

    End Sub

End Class