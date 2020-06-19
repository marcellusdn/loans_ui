

Public Class updatetext

    Inherits System.Web.UI.Page

    Dim cs As New Customer
    Dim ds As New dsBI
    Dim ta As New dsBITableAdapters.spGetMessageTypesTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Clear_Close()
        End If

    End Sub

    Protected Friend Sub UpdateClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If btnEdit.Value = "Update" Then
            AllowEdit()
        Else
            If String.IsNullOrEmpty(txtRecordNo.Value) = False And String.IsNullOrEmpty(txtMessage.Value) = False And String.IsNullOrEmpty(slStatusMsgs.Value) = False Then
                cs.UpdateText(txtRecordNo.Value, txtMessage.Value, txtCustomerGroup.Value, txtCustomerType.Value, slStatusMsgs.Value)
            Else
                resp = "Invalid or blank message selection."
            End If
        End If

        ClientScript.RegisterStartupScript(Me.GetType, "response", "alert('" & resp & "')", True)

    End Sub

    Protected Friend Sub SearchClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Clear_Close()

    End Sub

    Protected Friend Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Clear_Close()

    End Sub

    Private Sub Clear_Close()

        btnEdit.Disabled = False

        ds.EnforceConstraints = False
        ta.Fill(ds.spGetMessageTypes, slSearchType.Value, slSearchStatus.Value)

        'With lsMsgs
        '    .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_message_types))
        '    .DataTextField = "message"
        '    .DataValueField = "message"
        '    .DataBind()
        '    .Items.Insert(0, "")
        'End With

        With gview
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.spGetMessageTypes))
            .DataBind()
            '.HeaderRow.TableSection = TableRowSection.TableHeader
            .Visible = True
        End With

    End Sub

    Private Sub Gview_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gview.RowCommand

        Dim rid As Integer = 0

        If e.CommandName = "Select" Then

            rid = e.CommandArgument.ToString

            With gview.Rows(rid)

                txtRecordNo.Value = .Cells(1).Text.ToString
                txtCustomerGroup.Value = .Cells(2).Text.ToString
                txtMessage.Value = .Cells(3).Text.ToString
                slStatusMsgs.Value = .Cells(5).Text.ToString
                txtCustomerType.Value = .Cells(4).Text.ToString

            End With

        End If

    End Sub

    Private Sub AllowEdit()

        btnEdit.Value = "Save"

        txtMessage.Disabled = False
        slStatusMsgs.Disabled = False

    End Sub

End Class