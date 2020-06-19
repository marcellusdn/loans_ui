

Public Class createrole
    Inherits System.Web.UI.Page

    Dim u As New User

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Clear()
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Protected Friend Sub SaveOnClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If btnNew.Value = "New" Then
            allowNew()
        ElseIf btnNew.Value = "Save" Then
            If txtCode.Value <> "" And txtCode.Value <> "Error" And txtName.Value <> "" Then
                u.RecordRole(txtCode.Value, txtName.Value, "Active")
                scriptstr = "<script> alert('" & resp & "') </script>"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", scriptstr)
            End If
            Clear()
        End If

    End Sub

    Protected Friend Sub EditOnClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If txtCode.Value <> "" And txtName.Value <> "" Then
            u.RecordRole(txtCode.Value, txtName.Value, "Active")
            scriptstr = "<script> alert('" & resp & "') </script>"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", scriptstr)
        End If

    End Sub

    Protected Friend Sub CancelOnClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Private Sub Clear()

        txtCode.Value = Nothing
        txtName.Value = Nothing

        txtCode.Disabled = True
        txtName.Disabled = True
        lstRoles.Disabled = True

        btnNew.Value = "New"
        btnEdit.Value = "Edit"

        btnNew.Disabled = False
        btnEdit.Disabled = False

        btnEdit.Visible = True
        btnNew.Visible = True

    End Sub

    Private Sub allowNew()

        txtCode.Disabled = False
        txtName.Disabled = False
        lstRoles.Disabled = False

        btnNew.Value = "Save"

        txtCode.Value = u.GetRoleNo

        btnEdit.Disabled = True
        btnEdit.Visible = False

    End Sub

    Private Sub allowEdit()

        txtCode.Disabled = False
        txtName.Disabled = False
        lstRoles.Disabled = False

        btnEdit.Value = "Save"

        btnNew.Disabled = True
        btnNew.Visible = False

    End Sub

End Class