
Imports System.Net
Imports System.Web
Imports System.Management
Imports System.Reflection

Public Class Login

    Inherits System.Web.UI.Page

    Dim u As New User
    Dim _GetOSInfo As ManagementObjectSearcher
    Dim _GetCSInfo As ManagementObjectSearcher
    Dim _GetBIOSInfo As ManagementObjectSearcher

    Dim Model As String = Nothing
    Dim Maked As String = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Count > 0 Then
            Response.Redirect("~/account/logoutsuccess.aspx")
        Else
            Clear()
        End If

    End Sub

    Protected Friend Sub BtnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim ipa = If(String.IsNullOrEmpty(Dns.GetHostName.ToString), Nothing, Dns.GetHostName.ToString)
        'Dim ipb = If(Dns.GetHostEntry(ipa) Is Nothing, Nothing, Dns.GetHostEntry(ipa))
        'Dim ipv6add As String = If(String.IsNullOrEmpty(Dns.GetHostEntry(ipa).AddressList.GetValue(0).ToString), Nothing, Dns.GetHostEntry(ipa).AddressList.GetValue(0).ToString)

        Dim ctx As System.Web.HttpContext = System.Web.HttpContext.Current
        Dim ip2add As String = Request.UserHostAddress
        Dim IPAddress As String = Request.UserHostName 'Request.ServerVariables("HTTP_X_FORWARDED_FOR")

        'Session("access_point") = If(String.IsNullOrEmpty(ipv6add), IPAddress.ToString, ipv6add.ToString)

        Session("access_name") = IPAddress.ToString 'If(String.IsNullOrWhiteSpace(System.Net.Dns.GetHostEntry(ip2add).HostName.ToString), Request.UserHostName.ToString, System.Net.Dns.GetHostEntry(ip2add).HostName.ToString)
        Session("access_address") = ip2add.ToString

        _GetCSInfo = New ManagementObjectSearcher("Select * From Win32_ComputerSystem")
        _GetOSInfo = New ManagementObjectSearcher("Select * From Win32_OperatingSystem")
        _GetBIOSInfo = New ManagementObjectSearcher("Select * From Win32_BIOS")

        For Each _getinfo In _GetCSInfo.Get
            Session("access_point") = _getinfo("Model").ToString
        Next

        For Each _getinfo In _GetBIOSInfo.Get
            Session("serial_no") = _getinfo("serialnumber").ToString
        Next

        For Each _getinfo In _GetOSInfo.Get
            Session("machine_no") = _getinfo("serialnumber").ToString
        Next

        If String.IsNullOrEmpty(txtLoginID.Value) = False And String.IsNullOrEmpty(txtPassword.Value) = False Then
            u.ValidateLogin(txtLoginID.Value, txtPassword.Value)
        End If

        If u._LoginStatus = "Success" Then

            FormsAuthentication.SetAuthCookie(u._Name, False)
            Response.Redirect("~/index.aspx")

        ElseIf u._LoginStatus = "Generated" Or u._LoginStatus = "Expired" Or u._LoginStatus = "New" Or u._LoginStatus = "Generated" Then

            FormsAuthentication.SetAuthCookie(u._Name, False)
            Response.Redirect("~/account/changepassword.aspx")

        ElseIf u._LoginStatus = "Locked" Then

            vallogin.Visible = True
            vallogin.InnerText = "Account is Locked"

        Else

            vallogin.Visible = True
            vallogin.InnerText = "Invalid account login credentials"

        End If

    End Sub

    Protected Friend Sub BtnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Response.Redirect("~/account/logoutsuccess.aspx")

    End Sub

    Private Sub Clear()

        'txtLoginID.Value = Nothing
        'txtPassword.Value = Nothing
        vallogin.Disabled = True
        vallogin.Visible = False

        txtLoginID.Focus()

    End Sub

End Class