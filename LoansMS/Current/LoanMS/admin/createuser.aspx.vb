Public Class Createuser

    Inherits System.Web.UI.Page

    Dim u As New User
    Dim ds As New myQuick
    Dim ta As New myQuickTableAdapters.get_active_rolesTableAdapter

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

    Protected Friend Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If btnNew.Value = "New" Then

            AllowNew()

        Else

            If String.IsNullOrEmpty(txtLoginID.Value) = False And String.IsNullOrEmpty(txtName.Value) = False _
                And String.IsNullOrEmpty(txtEMail.Value) = False And String.IsNullOrEmpty(lstRoles.Value) = False _
                And String.IsNullOrEmpty(cboStatus.Value) = False Then


                Dim str As String = Nothing
                Dim cs As ClientScriptManager = Page.ClientScript
                'str = "return confirm('Are you sure you want to create the user \nNo: " & txtLoginID.Value & "\nName: " & txtName.Value & "')"
                cs.RegisterStartupScript(Me.GetType(), "alert", "return confirm('Are you sure!')")

                u.RecordUser(txtLoginID.Value, txtName.Value, txtEMail.Value, cboStatus.Value, lstRoles.Value)

                scriptstr = "<script> alert('" & resp & "') </script>"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", scriptstr)

                Clear()

            Else

                Clear()

            End If

        End If

    End Sub

    Protected Friend Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If btnEdit.Value = "Edit" Then

            AllowEdit()

        Else

            If String.IsNullOrEmpty(txtLoginID.Value) = False And String.IsNullOrEmpty(txtName.Value) = False _
                And String.IsNullOrEmpty(txtEMail.Value) = False And String.IsNullOrEmpty(lstRoles.Value) = False _
                And String.IsNullOrEmpty(cboStatus.Value) = False Then

                u.EditUser(txtLoginID.Value, txtName.Value, txtEMail.Value, lstRoles.Value, cboStatus.Value)
                Clear()

            Else

                Clear()

            End If

        End If

    End Sub

    Protected Friend Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Protected Friend Sub BtnResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If btnResetPassword.Value = "Reset Password" Then

            AllowReset()

        Else

            If String.IsNullOrEmpty(txtLoginID.Value) = False Then

                u.ResetPassword(txtLoginID.Value, txtName.Value)
                Clear()

            Else

                Clear()

            End If

        End If

    End Sub

    Private Sub AllowNew()

        txtLoginID.Disabled = False
        txtName.Disabled = False
        txtEMail.Disabled = False
        lstRoles.Disabled = False
        cboStatus.Disabled = False

        btnNew.Value = "Save"

        btnEdit.Disabled = True
        btnEdit.Visible = False
        btnCancel.Disabled = False
        btnResetPassword.Disabled = True
        btnResetPassword.Visible = False

        btnCheck.Visible = False
        btnCheck.Disabled = True

        ds.EnforceConstraints = False

        btnNew.Focus()

    End Sub

    Private Sub AllowEdit()

        btnEdit.Value = "Save"

        btnNew.Disabled = True
        btnCancel.Disabled = False
        btnResetPassword.Disabled = True

        btnResetPassword.Visible = False
        btnNew.Visible = False

        txtLoginID.Disabled = False
        txtEMail.Disabled = False
        txtName.Disabled = False
        lstRoles.Disabled = False
        cboStatus.Disabled = False

        btnCheck.Visible = True
        btnCheck.Disabled = False

        btnEdit.Focus()

        ds.EnforceConstraints = False

    End Sub

    Private Sub Clear()

        btnNew.Disabled = False
        btnEdit.Disabled = False
        btnCancel.Disabled = False
        btnResetPassword.Disabled = False

        btnNew.Value = "New"
        btnEdit.Value = "Edit"
        btnCancel.Value = "Cancel"
        btnResetPassword.Value = "Reset Password"

        btnResetPassword.Visible = True
        btnNew.Visible = True
        btnEdit.Visible = True
        btnCancel.Visible = True

        txtEMail.Disabled = True
        txtName.Disabled = True
        txtLoginID.Disabled = True
        lstRoles.Disabled = True
        cboStatus.Disabled = True

        txtName.Value = Nothing
        txtEMail.Value = Nothing
        txtLoginID.Value = Nothing
        lstRoles.Value = Nothing
        cboStatus.Value = "Select Status"

        btnCheck.Visible = False
        btnCheck.Disabled = True

        ds.EnforceConstraints = False

        ta.Fill(ds.get_active_roles)
        With lstRoles
            .DataSource = ds.Tables(ds.Tables.IndexOf("get_active_roles"))
            .DataTextField = "Role"
            .DataValueField = "Role"
            .Items.Insert(0, "")
            .DataBind()
        End With

    End Sub

    Private Sub AllowReset()

        btnResetPassword.Value = "Save"

        btnNew.Disabled = True
        btnEdit.Disabled = True
        btnNew.Visible = True
        btnEdit.Visible = True

        btnCancel.Disabled = False

        txtLoginID.Disabled = False
        txtEMail.Disabled = True
        txtName.Disabled = True
        lstRoles.Disabled = True
        cboStatus.Disabled = True

        btnCheck.Visible = True
        btnCheck.Disabled = False

    End Sub

    Protected Friend Sub CheckUser(sender As Object, e As EventArgs)

        If txtLoginID.Value <> Nothing Then
            u.GetDetails(txtLoginID.Value)
            txtName.Value = u._Name
            txtEMail.Value = u._Email
            cboStatus.Value = u._LoginStatus
            lstRoles.Value = u._Role
        End If

    End Sub

End Class