

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.IO
Imports System.Data
Imports System.Data.OleDb

Public Class Schedule
    Inherits System.Web.UI.Page

    Dim gvMsgs As New GridView
    Dim m As New MessageBroadcast

    Dim schtime As String = Nothing
    Dim schdate As String = Nothing

    Dim ds As New myQuick
    Dim dps As New myQuickTableAdapters.get_departmentsTableAdapter
    Dim msg As New myQuickTableAdapters.get_message_typesTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.IsAuthenticated And Not IsPostBack Then
            Clear_Close()
        ElseIf Request.IsAuthenticated = False Then
            Response.Redirect("~/account/logoutsuccess.aspx")
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Protected Friend Sub Loadfile(ByVal sender As Object, ByVal e As System.EventArgs)

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
            floc = Server.MapPath("~/scheduled files/" & "\" & fname)
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

            With gvTextFile
                .DataSource = ds.Tables(0)
                .DataBind()
            End With

            con.Close()

            schtime = txtTime.Value
            schdate = txtDate.Value

            Dim lstmob As New List(Of String)
            Dim lstunqmob As New List(Of String)
            Dim lstacc As New List(Of String)
            Dim lstunqacc As New List(Of String)
            Dim curcost As Double = 0
            Dim totcost As Double = 0
            Dim curlgt As Int16 = 0
            Dim maxlgt As Int16 = 0

            For i = 0 To ds.Tables(0).Rows.Count - 1

                Dim mob As String = ds.Tables(0).Rows(i).Item(0).ToString
                Dim msg As String = ds.Tables(0).Rows(i).Item(3).ToString
                Dim acno As String = ds.Tables(0).Rows(i).Item(1).ToString

                lstacc.Add(acno)
                If lstunqacc.Contains(acno) = False Then
                    lstunqacc.Add(acno)
                End If

                lstmob.Add(mob)
                If lstunqmob.Contains(mob) = False Then
                    lstunqmob.Add(mob)
                End If

                curlgt = Strings.Len(msg)
                curcost = Math.Ceiling(curlgt / 160) * 0.4

                totcost = totcost + curcost

                If curlgt > maxlgt Then
                    maxlgt = curlgt
                End If

            Next

            txtAccountNos.Value = lstacc.Count
            txtUniqueAccounts.Value = lstunqacc.Count
            txtMobileNos.Value = lstmob.Count
            txtUniqueMobile.Value = lstunqmob.Count
            txtMaxLength.Value = maxlgt
            txtCost.Value = FormatNumber(totcost, 2, TriState.True, TriState.False, TriState.True)

        End If

        If String.IsNullOrEmpty(txtAccountNos.Value) = False Then
            btnPush.Disabled = False
        End If

    End Sub

    Protected Friend Sub BtnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If String.IsNullOrEmpty(txtAccountNos.Value) = False Then

            btnPush.Disabled = True
            m.ScheduleMessages(gvTextFile, txtDate.Value, txtTime.Value, lsDept.Value, lsMsgs.Value)
            Clear_Close()

        End If

    End Sub

    Protected Friend Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Clear_Close()

    End Sub

    Private Sub Clear_Close()

        btnPush.Disabled = False

        ds.EnforceConstraints = False

        txtTime.Value = ""
        txtDate.Value = ""
        txtCost.Value = ""
        txtAccountNos.Value = ""
        txtMaxLength.Value = ""
        txtMobileNos.Value = ""
        txtUniqueAccounts.Value = ""
        txtUniqueMobile.Value = ""

        gvMsgs.Visible = False

        gvTextFile.Visible = False
        With gvTextFile
            .DataSource = New List(Of String)
            .DataBind()
        End With

        msg.Fill(ds.get_message_types)
        dps.Fill(ds.get_departments)

        With lsDept
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_departments))
            .DataTextField = "department"
            .DataValueField = "department"
            .DataBind()
            .Items.Insert(0, "")
        End With

        With lsMsgs
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_message_types))
            .DataTextField = "message"
            .DataValueField = "message"
            .DataBind()
            .Items.Insert(0, "")
        End With

    End Sub

End Class