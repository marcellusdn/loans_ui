

Public Class managepasswords
    Inherits System.Web.UI.Page

    Dim c As New setup

    Dim ds As New myQuick
    Dim ta As New myQuickTableAdapters.get_sys_password_valuesTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

    End Sub

    Protected Sub UpdateClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If btnUpdate.Value = "Update" Then
            AllowUpdate()
        Else
            c.ManagePassword(txtDuration.Value, txtMaxLength.Value, txtMinLength.Value,
                             txtNumerics.Value, txtUpperCase.Value, txtSpecialChar.Value,
                             chkEnforeHistory.Checked)
            lbResponse.Visible = True
            Response.Write(c._ManagePassword)
            Clear()
        End If

    End Sub

    Protected Sub CancelClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Private Sub AllowUpdate()

        ds.EnforceConstraints = False
        ta.Fill(ds.get_sys_password_values)

        With ds.get_sys_password_values
            txtDuration.Value = .Rows(0).Item(1).ToString
            txtMaxLength.Value = .Rows(0).Item(2).ToString
            txtMinLength.Value = .Rows(0).Item(3).ToString
            txtNumerics.Value = .Rows(0).Item(4).ToString
            txtSpecialChar.Value = .Rows(0).Item(5).ToString
            txtUpperCase.Value = .Rows(0).Item(6).ToString
            chkEnforeHistory.Value = .Rows(0).Item(0).ToString
        End With

        txtDuration.Disabled = False
        txtMaxLength.Disabled = False
        txtMinLength.Disabled = False
        txtNumerics.Disabled = False
        txtSpecialChar.Disabled = False
        txtUpperCase.Disabled = False
        chkEnforeHistory.Disabled = False

        btnUpdate.Value = "Save"

    End Sub

    Private Sub Clear()

        txtDuration.Disabled = True
        txtMaxLength.Disabled = True
        txtMinLength.Disabled = True
        txtNumerics.Disabled = True
        txtSpecialChar.Disabled = True
        txtUpperCase.Disabled = True
        chkEnforeHistory.Disabled = True

        txtDuration.Value = Nothing
        txtMaxLength.Value = Nothing
        txtMinLength.Value = Nothing
        txtNumerics.Value = Nothing
        txtSpecialChar.Value = Nothing
        txtUpperCase.Value = Nothing
        chkEnforeHistory.Value = Nothing

        lbResponse.Visible = False

        btnUpdate.Value = "Update"

    End Sub

End Class