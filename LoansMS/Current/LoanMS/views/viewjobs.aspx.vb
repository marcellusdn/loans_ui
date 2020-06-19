Public Class Viewjobs

    Inherits System.Web.UI.Page

    Dim ta As New myQuickTableAdapters.get_scheduled_jobsTableAdapter
    Dim ds As New myQuick
    Dim qresp As String = Nothing
    Dim dt As Date


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Clear()
        ElseIf Request.IsAuthenticated = False Then
            Response.Redirect("~/account/logoutsuccess.aspx")
            FormsAuthentication.RedirectToLoginPage()
        End If

        'Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) & "; url=\account/logoutsuccess.aspx")

    End Sub

    Private Sub Clear()

        slDepartment.Value = Nothing
        txtEndDate.Value = Nothing
        slMessageType.Value = Nothing
        slUser.Value = Nothing
        txtStartDate.Value = Nothing

        ds.EnforceConstraints = False

        gview.DataSource = New List(Of String)
        gview.DataBind()
        gview.Visible = False

    End Sub

    Protected Friend Sub SearchClick(ByVal sender As Object, ByVal e As System.EventArgs)

        If String.IsNullOrEmpty(txtStartDate.Value) = True Then
            dt = Date.MinValue
        Else
            dt = txtStartDate.Value
        End If

        ta.Fill(ds.get_scheduled_jobs, dt, If(slDepartment.Value = "", Nothing, slDepartment.Value),
                If(slMessageType.Value = "", Nothing, slMessageType.Value), Session("active_user"), Session("session_id"))

        If ds.get_scheduled_jobs.Rows.Count > 0 Then

            With gview
                .DataSource = ds.Tables(ds.Tables.IndexOf(ds.get_scheduled_jobs))
                .DataBind()
                .Visible = True
            End With

        Else

            Clear()

        End If

    End Sub

    Protected Friend Sub CancelClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Clear()

    End Sub

End Class