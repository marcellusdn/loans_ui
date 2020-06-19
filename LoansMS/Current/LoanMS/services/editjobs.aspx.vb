

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.IO
Imports System.Data
Imports System.Data.OleDb

Public Class Editjobs
    Inherits System.Web.UI.Page

    Dim schtime As String = Nothing
    Dim schdate As String = Nothing

    Dim ds As New myQuick
    Dim dps As New myQuickTableAdapters.get_departmentsTableAdapter
    Dim msg As New myQuickTableAdapters.get_message_typesTableAdapter
    Dim jbs As New myQuickTableAdapters.get_pending_jobsTableAdapter

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.IsAuthenticated = False Then
            Response.Redirect("~/account/logoutsuccess/aspx")
        Else
            Clear_Close()
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Protected Friend Sub UpdateClick(ByVal sender As Object, ByVal e As System.EventArgs)


    End Sub

    Protected Friend Sub DeleteClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'If String.IsNullOrEmpty(txtAccountNos.Value) = False Then

        '    btnPush.Disabled = True
        '    'm.ScheduleMessages(gvTextFile, txtDate.Value, txtTime.Value, lsDept.Value, lsMsgs.Value)
        '    Clear_Close()

        'End If

    End Sub

    Protected Friend Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Clear_Close()

    End Sub

    Private Sub Clear_Close()

        btnDelete.Disabled = False
        btnEdit.Disabled = False

        ds.EnforceConstraints = False

        txtTime.Value = ""
        txtDate.Value = ""

        msg.Fill(ds.get_message_types)
        dps.Fill(ds.get_departments)
        jbs.Fill(ds.get_pending_jobs, Session("active_user"), Session("session_id"))

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

        With gview
            .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_pending_jobs))
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

                txtJobNo.Value = .Cells(1).Text.ToString
                txtDate.Value = CDate(.Cells(3).Text.ToString)
                txtTime.Value = .Cells(4).Text.ToString

            End With

        End If


    End Sub

End Class