Public Class Clients
    Inherits System.Web.UI.Page

    Dim c As New Customer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack And Request.IsAuthenticated Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            Session.Abandon()
            Response.Redirect("/account/logoutsuccess.aspx")
        End If

    End Sub

    Protected Friend Sub ResetClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

    Protected Friend Sub RecordClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If btnNew.Value = "New" Then
            AllowNew()
        End If

    End Sub

    Protected Friend Sub GetClientClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim mobno As String = txtMobileNo.Value

        If Left(mobno, 1) = 0 Then
            mobno = Right(mobno, Len(mobno) - 1)
        End If
        If Len(mobno) < 9 Then
            mobno = Nothing
        End If
        If Left(mobno, 3) <> "254" Then
            mobno = "254" & mobno
        End If
        If Len(mobno) <> 12 Then
            mobno = Nothing
        End If

        If String.IsNullOrWhiteSpace(mobno) = False Then

            With c

                .GetClientDetails(mobno)

                txtClientNo.Value = .CustomerID
                txtCustomerID.Value = .NationalID
                txtCustomerName.Value = .CustomerName
                txtDOB.Value = IIf(.DateofBirth Is Nothing, "", .DateofBirth)
                txtLimit.Value = FormatNumber(.CusLimit, 2, TriState.True, TriState.False, TriState.True)
                txtMobileNum.Value = .MobileNo
                lsCustomerType.Value = .CustomerType
                chkActivateMessage.Value = .CusStatus
                txtAccountNo.Value = .AccountNo

            End With

        Else

            resp = "Error: Please provide a valid and non-blank mobile number."

        End If

        If Left(resp, 7) <> "Success" Then
            ClientScript.RegisterStartupScript(Me.GetType, "response", "alert('" & resp & "')", True)
        End If

    End Sub

    Protected Friend Sub DeactivateClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If btnDeactivate.Value = "Deactivate" Then

            Deactivate()

        Else

            Dim mobno As String = txtMobileNo.Value

            If Left(mobno, 1) = 0 Then
                mobno = Right(mobno, Len(mobno) - 1)
            End If
            If Len(mobno) < 9 Then
                mobno = Nothing
            End If
            If Left(mobno, 3) <> "254" Then
                mobno = "254" & mobno
            End If
            If Len(mobno) <> 12 Then
                mobno = Nothing
            End If

            If String.IsNullOrWhiteSpace(mobno) = False Then

                With c

                    .DeactivateClient(mobno)

                End With

            Else

                resp = "Please provide a valid and non-blank mobile number"

            End If

            ClientScript.RegisterStartupScript(Me.GetType, "response", "alert('" & resp & ".')", True)

            Clear()

        End If

    End Sub

    Private Sub AllowNew()

        txtClientNo.Disabled = False
        txtCustomerName.Disabled = False
        txtLimit.Disabled = False
        txtMobileNum.Disabled = False
        txtCustomerID.Disabled = False
        txtLimit.Disabled = False
        chkActivateMessage.Disabled = False
        txtDOB.Disabled = False
        txtAccountNo.Disabled = False

        chkActivateMessage.Visible = True
        lbActivateMessage.Visible = True

        lsCustomerType.Disabled = False

        btnGet.Disabled = True
        btnGet.Visible = False

        txtMobileNo.Disabled = True
        txtMobileNo.Visible = False

        btnEdit.Disabled = True
        btnDeactivate.Disabled = True

        btnNew.Value = "Save"

    End Sub

    Private Sub Deactivate()

        txtClientNo.Disabled = True
        txtCustomerName.Disabled = True
        txtLimit.Disabled = True
        txtMobileNum.Disabled = True
        txtCustomerID.Disabled = True
        txtLimit.Disabled = True
        chkActivateMessage.Disabled = True
        txtDOB.Disabled = True
        lsCustomerType.Disabled = True
        txtAccountNo.Disabled = True

        txtMobileNo.Disabled = False
        txtMobileNo.Visible = True
        txtMobileNo.Focus()

        lbMobileNo.Visible = True

        chkActivateMessage.Visible = False
        lbActivateMessage.Visible = False

        btnEdit.Disabled = True
        btnNew.Disabled = True

        btnGet.Disabled = False
        btnGet.Visible = True

        btnDeactivate.Value = "Save"

    End Sub

    Private Sub Clear()

        txtClientNo.Value = Nothing
        txtCustomerID.Value = Nothing
        txtCustomerName.Value = Nothing
        txtLimit.Value = Nothing
        txtMobileNum.Value = Nothing
        txtDOB.Value = Nothing
        txtMobileNo.Value = Nothing
        chkActivateMessage.Value = Nothing
        lsCustomerType.Value = Nothing
        txtAccountNo.Value = Nothing

        txtClientNo.Disabled = True
        txtCustomerName.Disabled = True
        txtLimit.Disabled = True
        txtMobileNum.Disabled = True
        txtCustomerID.Disabled = True
        txtLimit.Disabled = True
        chkActivateMessage.Disabled = True
        lsCustomerType.Disabled = True
        txtAccountNo.Disabled = True

        lbMobileNo.Visible = False

        chkActivateMessage.Visible = True
        lbActivateMessage.Visible = True

        btnGet.Disabled = True
        btnGet.Visible = False

        txtMobileNo.Disabled = True
        txtMobileNo.Visible = False

        btnEdit.Disabled = False
        btnNew.Disabled = False
        btnDeactivate.Disabled = False

        btnDeactivate.Value = "Deactivate"
        btnNew.Value = "New"
        btnEdit.Value = "Edit"

    End Sub

End Class