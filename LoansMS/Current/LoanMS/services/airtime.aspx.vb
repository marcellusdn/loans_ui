


Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.IO
Imports System.Data
Imports System.Data.OleDb


Public Class airtime
    Inherits System.Web.UI.Page

    'Dim gvairtime As New GridView
    Dim m As New MessageBroadcast

    Dim ds As New myQuick
    Dim ta As New myQuickTableAdapters.get_departmentsTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Clear_Close()
        ElseIf Request.IsAuthenticated = False Then
            FormsAuthentication.RedirectToLoginPage()
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Private Sub LoaAirtime(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.ServerClick

        Dim cstring As String = "SELECT * FROM [Sheet1$]"
        Dim cnstr = ""

        Dim con As OleDbConnection
        Dim cmd As OleDbCommand
        Dim adt As OleDbDataAdapter
        Dim ds As New DataSet

        Dim fname As String = ""
        Dim fext As String = ""
        Dim floc As String = ""

        If fuMessages.HasFile = True Then

            btnPush.Disabled = False

            fname = Path.GetFileName(fuMessages.PostedFile.FileName)
            fext = Path.GetExtension(fuMessages.PostedFile.FileName)
            floc = Server.MapPath("~/airtime files/" & "\" & fname)
            fuMessages.SaveAs(floc)

            If fext = ".xls" Then
                cnstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & floc & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2;';"
            ElseIf fext = ".xlsx" Then
                cnstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & floc & ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=2;';"
            End If

            con = New OleDbConnection(cnstr)
            cmd = New OleDbCommand(cstring, con)
            With cmd
                .CommandType = CommandType.Text
                .Connection = con
            End With

            If con.State <> ConnectionState.Open Then
                con.Open()
            End If

            adt = New OleDbDataAdapter(cmd)
            adt.Fill(ds)

            With gvairtime
                .DataSource = ds.Tables(0)
                .DataBind()
            End With

            con.Close()

        End If

        Dim lstmob As New List(Of String)
        Dim lstunqmob As New List(Of String)
        Dim lstacc As New List(Of String)
        Dim lstunqacc As New List(Of String)
        Dim curcost As Double = 0
        Dim totcost As Double = 0
        Dim minamt As Double = 0
        Dim maxamt As Double = 0

        For i = 0 To ds.Tables(0).Rows.Count - 1

            Dim mob As String = ds.Tables(0).Rows(i).Item(0).ToString
            Dim acno As String = ds.Tables(0).Rows(i).Item(2).ToString
            Dim curamt As Double = ds.Tables(0).Rows(i).Item(4).ToString

            lstacc.Add(acno)
            If lstunqacc.Contains(acno) = False Then
                lstunqacc.Add(acno)
            End If

            lstmob.Add(mob)
            If lstunqmob.Contains(mob) = False Then
                lstunqmob.Add(mob)
            End If

            curcost = curamt

            totcost = totcost + curcost

            If minamt = 0 Then
                minamt = curamt
            Else
                If minamt > curamt Then
                    minamt = curamt
                End If
            End If

            If maxamt = 0 Then
                maxamt = curamt
            Else
                If maxamt < curamt Then
                    maxamt = curamt
                End If
            End If

        Next

        txtAccountNos.Value = lstacc.Count
        txtUniqueAccounts.Value = lstunqacc.Count
        txtMobileNos.Value = lstmob.Count
        txtUniqueMobile.Value = lstunqmob.Count
        txtMaxAirtime.Value = FormatNumber(maxamt, 2, TriState.True, TriState.False, TriState.True)
        txtMinAirtime.Value = FormatNumber(minamt, 2, TriState.True, TriState.False, TriState.True)
        txtCost.Value = FormatNumber(totcost, 2, TriState.True, TriState.False, TriState.True)

        If String.IsNullOrEmpty(txtAccountNos.Value) = False And CInt(txtAccountNos.Value) > 0 Then
            btnPush.Disabled = False
        End If

    End Sub

    Protected Friend Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Clear_Close()

    End Sub

    Protected Friend Sub BtnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If String.IsNullOrEmpty(txtAccountNos.Value) = False Then
            btnPush.Disabled = True
            m.LoadAirtime(gvairtime, lsDepartments.Value)
            Clear_Close()
        End If

    End Sub

    Private Sub Clear_Close()

        btnPush.Disabled = True

        txtAccountNos.Value = ""
        txtUniqueAccounts.Value = ""
        txtMobileNos.Value = ""
        txtUniqueMobile.Value = ""
        txtMaxAirtime.Value = ""
        txtMinAirtime.Value = ""
        txtCost.Value = ""

        gvairtime.Visible = False

        ds.EnforceConstraints = False
        ta.Fill(ds.get_departments)

        With lsDepartments
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_departments))
            .DataTextField = "department"
            .DataValueField = "department"
            .DataBind()
            .Items.Insert(0, "")
        End With

    End Sub

End Class