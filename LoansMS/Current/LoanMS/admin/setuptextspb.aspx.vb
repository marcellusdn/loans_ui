

Public Class setuptextspb

    Inherits System.Web.UI.Page

    Dim st As New Setup

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Clear()
        End If

    End Sub

    Protected Friend Sub EditOnClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If btnEdit.Value = "Update" Then
            allowEdit()
        ElseIf btnEdit.Value = "Save" Then
            If slFrequency.Value <> "" And txtMessageCount.Value <> "" Then
                st.UpdatePostBankTexts(slFrequency.Value, txtMessageCount.Value)
            End If
            Clear()
        End If

        ClientScript.RegisterStartupScript(Me.GetType, "response", "alert('" & resp & "')", True)

    End Sub

    Protected Friend Sub CancelOnClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Private Sub Clear()

        slFrequency.SelectedIndex = 0
        txtMessageCount.Value = Nothing

        txtMessageCount.Disabled = True
        slFrequency.Disabled = True

        btnEdit.Value = "Update"
        btnEdit.Disabled = False
        btnEdit.Visible = True

    End Sub

    Private Sub allowEdit()

        slFrequency.Disabled = False
        txtMessageCount.Disabled = False

        btnEdit.Value = "Save"
        btnEdit.Disabled = False
        btnEdit.Visible = True

    End Sub


End Class