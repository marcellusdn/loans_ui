
Imports System.Security.Cryptography
Imports System.Text
Imports System.Net.Mail
Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Management
Imports System.Reflection
Imports System.Web

Public Class User

#Region "Global Variables"

    Private ReadOnly _OperationStatus As Boolean
    Private _LogoutStatus As String = Nothing
    Private ReadOnly _TransactionID As String = Nothing

    Protected Shared cmd As New MySqlCommand
    Private ReadOnly cn As New My.MySettings

    Private rnd As New Random

    Protected Friend mapID As String = Nothing

    Private Const minPasswordLength As Short = 8
    Private Const maxPasswordLength As Short = 20

    Private _RoleID As String
    Private ReadOnly _ControlID As String
    Private _ControlName As String
    Private _ControlText As String
    Private ReadOnly _Level As String
    Private ReadOnly _ControlStatus As String

    Public _Visibility As Boolean
    Public _Access As Boolean

    Protected Friend _UserID As String
    Protected Friend _Name As String
    Protected Friend _Role As String
    Protected Friend _Email As String

    Private _IsActive As Boolean
    Private ReadOnly _IsSuspended As Boolean
    Private _IsDeleted As Boolean
    Private ReadOnly _IsNewUser As Boolean

    Private ReadOnly _IsNewPassword As Boolean
    Private _IsExpired As Boolean

    Private _IsLogedIn As Boolean

    Private ReadOnly _IsVisible As Boolean
    Private ReadOnly _IsAccessible As Boolean

    Private _Password As String = Nothing
    Private _Salt As String = Nothing
    Private _HashedPassword As String = Nothing
    Private _UserPassword As String = Nothing
    Private ReadOnly _UserPasswordDate As Date

    Private ReadOnly _PasswordStatus As String = Nothing
    Private _UserStatus As String = Nothing
    Protected Friend _LoginStatus As String = Nothing

    Private ReadOnly Crypto As New RNGCryptoServiceProvider
    Private ReadOnly Hasher As New SHA384CryptoServiceProvider

    Public Users As New List(Of User)
    Private ReadOnly PasswordHistory As New List(Of String)

    Private _Roles As New SortedList(Of String, String)

    Private _MachineID As String = Nothing
    Private _SerialNo As String = Nothing
    Private _MachineName As String = Nothing
    Private _NetworkID As String = Nothing
    Private _Model As String = Nothing

    Private ReadOnly ct As Integer

    Private _SessionID As String = Nothing

    Private _GroupEMail As String = Nothing
    Private _GroupCard As String = Nothing
    Private _UserEMail As String = Nothing
    Private _EMailHost As String = Nothing
    Private _EMailPort As String = Nothing
    Private _EMailSSL As Boolean = False

#End Region

#Region "System User Properties"

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = StrConv(value, VbStrConv.ProperCase)
        End Set
    End Property

    Public Property UserId As String
        Get
            Return _UserID
        End Get
        Set(value As String)
            _UserID = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property

    Public Property Role As String
        Get
            Return _Role
        End Get
        Set(value As String)
            _Role = value
        End Set
    End Property

    Public Property IsActive As Boolean
        Get
            Return _IsActive
        End Get
        Set(value As Boolean)
            _IsActive = value
        End Set
    End Property

    Public Property IsDeleted As Boolean
        Get
            Return _IsDeleted
        End Get
        Set(value As Boolean)
            _IsDeleted = value
        End Set
    End Property

    Public Property IsExpired As Boolean
        Get
            Return _IsExpired
        End Get
        Set(value As Boolean)
            _IsExpired = value
        End Set
    End Property

    Public Property IsLoggedIn As Boolean
        Get
            Return _IsLogedIn
        End Get
        Set(value As Boolean)
            _IsLogedIn = value
        End Set
    End Property

    Private ReadOnly Property UserPasswordDate As Date
        Get
            Return _UserPasswordDate
        End Get
    End Property

    Private ReadOnly Property UserPassword As String
        Get
            Return _UserPassword
        End Get
    End Property

    Private Property Salt As String
        Get
            Return _Salt
        End Get
        Set(value As String)
            _Salt = value
        End Set
    End Property

    Private WriteOnly Property HashedPassword As String
        Set(value As String)
            _HashedPassword = value
        End Set
    End Property

    Private WriteOnly Property Password As String
        Set(value As String)
            _Password = value
        End Set
    End Property

    Protected Friend Property Roles As SortedList(Of String, String)
        Get
            Return _Roles
        End Get
        Set(value As SortedList(Of String, String))
            _Roles = value
        End Set
    End Property

    Protected Friend Property SessionID As String
        Get
            Return _SessionID
        End Get
        Set(ByVal value As String)
            _SessionID = value
        End Set
    End Property

#End Region
    
#Region "User Management Procedures, Methods and Functions"

    Protected Friend Sub RecordUser(ByVal uid As String, ByVal uname As String, ByVal uemail As String, ByVal ustatus As String, 
                                    ByVal urole As String)

        Try

            _UserID = StrConv(uid, VbStrConv.Uppercase)
            _Name = StrConv(uname, VbStrConv.ProperCase)
            _Email = StrConv(uemail, VbStrConv.Lowercase)

            _Role = urole
            _Salt = Generate_Salt()
            _Password = Generate_Password()

            Dim emailbody As String = Nothing
            Dim emailhead As String = Nothing

            resp = NewUser()

            If Left(resp, resp.IndexOf(":")) = "Success" Then

                GetEmailData(_UserID)

                emailbody = "Dear " + _Name & vbLf & vbLf & "Your account has been successfully created with the details:---------" & vbLf & vbLf & vbTab & "User ID: " & _UserID & vbLf & vbTab & "Password: " & _Password & vbLf & vbLf & "Please change your password on your first logon."
                emailhead = "User Account Creation"

                SendEMail(_GroupEMail, _GroupCard, emailbody, emailhead, _Email, _EMailHost, _EMailPort, _EMailSSL)

            End If

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User creation failed.", ._ErrorMethod = "Record User", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function NewUser() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "record_user"

                .Parameters.Clear()

                .Parameters.AddWithValue("@uid", _UserID)
                .Parameters.AddWithValue("@uname", _Name)
                .Parameters.AddWithValue("@urole", _Role)
                .Parameters.AddWithValue("@uemail", _Email)
                .Parameters.AddWithValue("@usalt", _Salt)
                .Parameters.AddWithValue("@upwd", _Password)
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))

                .Parameters.Add("@ureps", MySqlDbType.String)
                .Parameters("@ureps").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                NewUser = .Parameters("@ureps").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User creation failed.", ._ErrorMethod = "New User", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            NewUser = "Failed:"

        End Try

        Return NewUser

    End Function

    Private Function Generate_Password() As String

        Dim u_case As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        Dim l_case As String() = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "y"}
        Dim nums As Short() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}
        Dim special_chars As String() = {"!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "/"}

        Dim ustring As String = String.Empty
        Dim lstring As String = String.Empty
        Dim nstring As String = String.Empty
        Dim spstring As String = String.Empty

        For i = 0 To 2

            ustring &= u_case(Rnd.Next(i, u_case.Count - 1))

        Next

        For i = 0 To 2

            lstring &= l_case(Rnd.Next(i, l_case.Count - 1))

        Next

        spstring = special_chars(Rnd.Next(special_chars.Count - 1))

        For i = 0 To 2

            nstring &= nums(Rnd.Next(i, nums.Count - 1))

        Next

        Dim passwordLength As Integer = Len(ustring & spstring & nstring & lstring)
        Dim genpword As String = Nothing
        Dim pstring As String = ustring & spstring & nstring & lstring
        Dim tranchar As Char = Nothing

        While Len(pstring) > 0

            tranchar = Nothing
            tranchar = pstring(Rnd.Next(0, pstring.Count - 1))
            genpword = genpword + tranchar

            pstring = pstring.Replace(tranchar, "")

        End While

        Return genpword

    End Function

    Private Function Generate_Salt() As String

        Dim gen_Salt As New RNGCryptoServiceProvider

        Dim min_len_salt As Int16 = 10
        Dim max_len_salt As Int16 = 30
        Dim rnd_len_salt As New Random

        Dim len_salt As Int16 = rnd_len_salt.Next(min_len_salt, max_len_salt)

        Dim salt_array(len_salt) As Byte

        gen_Salt.GetNonZeroBytes(salt_array)

        Return Convert.ToBase64String(salt_array)

    End Function

    Protected Friend Sub UserChangePassword(ByVal id As String, ByVal pword As String, ByVal oldpword As String)

        Try

            _Password = pword
            _UserID = StrConv(id, VbStrConv.Uppercase)
            _UserPassword = oldpword

            resp = UpdatePassword(_UserID, _Password, _UserPassword)

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User initiated password change failed.", ._ErrorMethod = "User Change Password", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function UpdatePassword(ByVal UserName As String, ByVal NewPassword As String, ByVal CurrentPassword As String) As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "update_user_password"

                .Parameters.Clear()

                .Parameters.AddWithValue("@uid", UserName)
                .Parameters.AddWithValue("@upwd", NewPassword)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@oldpwd", CurrentPassword)

                .Parameters.Add("@utest", MySqlDbType.String)
                .Parameters("@utest").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                UpdatePassword = .Parameters("@utest").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            UpdatePassword = ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User initiated password change failed.", ._ErrorMethod = "Update Password", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return UpdatePassword

    End Function

    Protected Friend Sub ResetPassword(ByVal uid As String, ByVal uname As String)

        Try

            _UserID = StrConv(uid, VbStrConv.Uppercase)
            _Name = uname
            _Password = Generate_Password()

            Dim emailbody As String = Nothing
            Dim emailhead As String = Nothing

            resp = Reset(_UserID, _Password)

            If Left(resp, resp.IndexOf(":")) = "Success" Then

                emailbody = "Dear " + _Name & vbLf & vbLf & "Your account (" & _UserID & ") password was reset recently:---------" & vbLf & vbLf & vbTab & "New Password: " & _Password & vbLf & vbLf & "Please change your password on your next logon."
                emailhead = "Password Reset"

                GetEmailData(_UserID)

                SendEMail(_GroupEMail, _GroupCard, emailbody, emailhead, _UserEMail, _EMailHost, _EMailPort, _EMailSSL)

            End If

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Administrator initiated password change failed.", ._ErrorMethod = "Reset Password", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function Reset(ByVal UserName As String, ByVal NewPassword As String) As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "reset_user_password"

                .Parameters.Clear()

                .Parameters.AddWithValue("@uid", _UserID)
                .Parameters.AddWithValue("@pwd", _Password)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@st", MySqlDbType.String)
                .Parameters("@st").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                Reset = .Parameters("@st").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Reset = False
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Administrator initiated password change failed.", ._ErrorMethod = "Reset", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return Reset

    End Function

    Private Function SendEMail(ByVal EMailAccount As String, ByVal EMailCode As String, ByVal EMailText As String,
                               ByVal EMailHead As String, ByVal EMailRecepient As String, ByVal EMailHost As String,
                               ByVal EMailPort As String, ByVal EMailSSL As Boolean) As Boolean

        Try

            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()

            With mail
                mail.From = New MailAddress(EMailAccount)
                mail.To.Add(EMailRecepient)
                mail.Subject = EMailHead
                mail.Body = EMailText
            End With

            With SmtpServer

                .UseDefaultCredentials = False
                .Credentials = New Net.NetworkCredential(EMailAccount, EMailCode)
                .Port = EMailPort
                .Host = EMailHost
                .EnableSsl = EMailSSL
                .Send(mail)

            End With

            Return True

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Email delivery to user failed.", ._ErrorMethod = "Send EMail", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            Return False

        End Try

    End Function

    Protected Friend Sub EditUser(ByVal uID As String, ByVal uName As String, ByVal uEmail As String, ByVal uRole As String, ByVal uStatus As String)

        Try

            _UserID = uID
            _Name = uName
            _Email = uEmail
            _Role = uRole
            _UserStatus = uStatus

            resp = EditUser()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User details modification failed.", ._ErrorMethod = "Edit User", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function EditUser() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "update_user"

                .Parameters.Clear()
                .Parameters.AddWithValue("@uid", _UserID)
                .Parameters.AddWithValue("@uname", _Name)
                .Parameters.AddWithValue("@uemail", _Email)
                .Parameters.AddWithValue("@urole", _Role)
                .Parameters.AddWithValue("@ustatus", _UserStatus)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@upd", MySqlDbType.String)
                .Parameters("@upd").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                EditUser = .Parameters("@upd").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            EditUser = ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Data saving of User details modification failed.", ._ErrorMethod = "Edit User Function", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return EditUser

    End Function

    Private Sub GetEmailData(ByVal userid As String)

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "get_email_data"

                .Parameters.Clear()

                .Parameters.AddWithValue("@uid", userid)

                .Parameters.Add("@uemail", MySqlDbType.String)
                .Parameters("@uemail").Direction = ParameterDirection.Output

                .Parameters.Add("@grpmail", MySqlDbType.String)
                .Parameters("@grpmail").Direction = ParameterDirection.Output

                .Parameters.Add("@grpnas", MySqlDbType.String)
                .Parameters("@grpnas").Direction = ParameterDirection.Output

                .Parameters.Add("@grpport", MySqlDbType.String)
                .Parameters("@grpport").Direction = ParameterDirection.Output

                .Parameters.Add("@grphost", MySqlDbType.String)
                .Parameters("@grphost").Direction = ParameterDirection.Output

                .Parameters.Add("@grpssl", MySqlDbType.String)
                .Parameters("@grpssl").Direction = ParameterDirection.Output

                If .Connection.State <> ConnectionState.Open Then
                    .Connection.Open()
                End If

                .ExecuteNonQuery()

                _UserEMail = .Parameters("@uemail").Value
                _GroupEMail = .Parameters("@grpmail").Value
                _GroupCard = .Parameters("@grpnas").Value
                _EMailPort = .Parameters("@grpport").Value
                _EMailHost = .Parameters("@grphost").Value
                _EMailSSL = .Parameters("@grpssl").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "EMail credentials retrieval (Salt) failed.", ._ErrorMethod = "Retreive EMail", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

#End Region

#Region "User Access - Login & Logout Methods, Procedures and Functions"

    Private Sub RetrieveComputerInformation()

        _Model = HttpContext.Current.Session("access_point").ToString
        _MachineName = HttpContext.Current.Session("access_name").ToString
        _NetworkID = HttpContext.Current.Session("access_address").ToString
        _SerialNo = HttpContext.Current.Session("serial_no").ToString
        _MachineID = HttpContext.Current.Session("machine_no").ToString

    End Sub

    Protected Friend Sub ValidateLogin(ByVal Id As String, ByVal Pword As String)

        Try

            _Password = Pword
            _UserID = Id

            RetrieveComputerInformation()
            CheckLogin(_UserID, _Password, _MachineID, _MachineName, _NetworkID, _Model, _SerialNo)

            If _LoginStatus = "Success" Or _LoginStatus = "Generated" Or _LoginStatus = "Expired" Or _LoginStatus = "New" Or _LoginStatus = "Generated" Then

                HttpContext.Current.Session("session_id") = _SessionID
                HttpContext.Current.Session("active_user") = _UserID
                HttpContext.Current.Session("LoginStatus") = _LoginStatus

            End If

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User login validation failed.", ._ErrorMethod = "Login Validation", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Sub CheckLogin(ByVal UserID As String, ByVal UserProvidedPassword As String, ByVal MachineID As String, ByVal MachineName As String,
                           ByVal MachineNetworkID As String, ByVal MachineModel As String, ByVal MachineSerial As String)

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "login_validation"

                .Parameters.Clear()

                .Parameters.AddWithValue("@uid", UserID)
                .Parameters.AddWithValue("@pwd", UserProvidedPassword)
                .Parameters.AddWithValue("@logdate", Today.Date)
                .Parameters.AddWithValue("@machineID", _MachineID)
                .Parameters.AddWithValue("@machinenname", _MachineName)
                .Parameters.AddWithValue("@networkid", _NetworkID)
                .Parameters.AddWithValue("@modelno", _Model)
                .Parameters.AddWithValue("@serialno", _SerialNo)
                .Parameters.AddWithValue("@logintime", My.Computer.Clock.LocalTime)

                .Parameters.Add("@ssid", MySqlDbType.VarChar)
                .Parameters("@ssid").Direction = ParameterDirection.Output

                .Parameters.Add("@lst", MySqlDbType.VarChar)
                .Parameters("@lst").Direction = ParameterDirection.Output

                .Parameters.Add("@uname", MySqlDbType.String)
                .Parameters("@uname").Direction = ParameterDirection.Output

                .Parameters.Add("@urole", MySqlDbType.String)
                .Parameters("@urole").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                _LoginStatus = .Parameters("@lst").Value

                If _LoginStatus <> "Failed" Then
                    _SessionID = .Parameters("@ssid").Value
                    _Name = .Parameters("@uname").Value
                    _Role = .Parameters("@urole").Value
                Else
                    _SessionID = Nothing
                    _Name = Nothing
                    _Role = Nothing
                End If

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            _LoginStatus = "Failed"
            _SessionID = Nothing
            _Name = Nothing
            _Role = Nothing

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User login data extraction and validation failed.", ._ErrorMethod = "Check Login", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Protected Friend Sub Logout()

        Try

            _LogoutStatus = LogLogout()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User logout initiation failed.", ._ErrorMethod = "Logout", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function LogLogout() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "record_user_logout"

                .Parameters.Clear()

                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@ssid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@logouttime", My.Computer.Clock.LocalTime)

                .Parameters.Add("@lstatus", MySqlDbType.String)
                .Parameters("@lstatus").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                LogLogout = .Parameters("@lstatus").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            LogLogout = ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User logout initiation and recording failed.", ._ErrorMethod = "Logout Data", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return LogLogout

    End Function

    Protected Friend Sub UnlockUser(ByVal LoggedUser As String)

        Try

            _UserID = LoggedUser
            resp = Unlock()

            Exit Sub

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User account unlocking failed.", ._ErrorMethod = "Unlock User", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function Unlock() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "unlock_user"

                .Parameters.Clear()

                .Parameters.AddWithValue("@uid", _UserID)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@ssid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@resp", MySqlDbType.String)
                .Parameters("@resp").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                Return .Parameters("@resp").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Return ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User account unlocking failed.", ._ErrorMethod = "Unlock Data", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return Unlock()

        Exit Function

    End Function

    Protected Friend Sub GetDetails(ByVal LoginID As String)

        _UserID = LoginID

        Try

            If _UserID <> HttpContext.Current.Session("active_user") Then
                UserDetails()
            End If

        Catch ex As Exception

            _UserStatus = Nothing
            _Name = Nothing
            _Role = Nothing
            _Email = Nothing

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User details retrieval failed.", ._ErrorMethod = "User Data", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Sub UserDetails()

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "get_user"

                .Parameters.Clear()

                .Parameters.AddWithValue("@uid", _UserID)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@ssid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@uname", MySqlDbType.String)
                .Parameters("@uname").Direction = ParameterDirection.Output

                .Parameters.Add("@uemail", MySqlDbType.VarChar)
                .Parameters("@uemail").Direction = ParameterDirection.Output

                .Parameters.Add("@urole", MySqlDbType.String)
                .Parameters("@urole").Direction = ParameterDirection.Output

                .Parameters.Add("@ustatus", MySqlDbType.VarChar)
                .Parameters("@ustatus").Direction = ParameterDirection.Output

                .Parameters.Add("@ulog", MySqlDbType.String)
                .Parameters("@ulog").Direction = ParameterDirection.Output

                .Parameters.Add("@ucmt", MySqlDbType.VarChar)
                .Parameters("@ucmt").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                resp = If(IsDBNull(.Parameters("@ucmt").Value), Nothing, .Parameters("@ucmt").Value)

                If resp = Nothing Then
                    _UserStatus = .Parameters("@ustatus").Value
                    _Name = .Parameters("@uname").Value
                    _Role = .Parameters("@urole").Value
                    _Email = .Parameters("@uemail").Value
                    _LoginStatus = .Parameters("@ulog").Value
                Else
                    _UserStatus = Nothing
                    _Name = Nothing
                    _Role = Nothing
                    _Email = Nothing
                    _LoginStatus = Nothing
                End If

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            _UserStatus = Nothing
            _Name = Nothing
            _Role = Nothing
            _Email = Nothing
            _LoginStatus = Nothing

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User login data extraction and validation failed.", ._ErrorMethod = "Get User Data", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

#End Region

#Region "User Interface and Controls Management"

    Protected Friend Sub UpdateUserAccess(ByVal RoleName As String, ByVal AccessMenus As SortedDictionary(Of String, String),
                                          ByVal AccessReports As SortedDictionary(Of String, String))

        Try

            _Role = RoleName

            For i = 0 To AccessMenus.Count - 1

                _ControlText = "Menus"
                With AccessMenus
                    _ControlName = .ElementAt(i).Key
                    _Access = .ElementAt(i).Value
                End With

                UpdateRolePermisisons()

            Next

            For i = 0 To AccessReports.Count - 1

                _ControlText = "Reports"
                With AccessReports
                    _ControlName = .ElementAt(i).Key
                    _Access = .ElementAt(i).Value
                End With

                UpdateRolePermisisons()

            Next

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User access update initiation failed.", ._ErrorMethod = "Update User Access", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function UpdateRolePermisisons() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "update_user_access"

                .Parameters.Clear()

                .Parameters.AddWithValue("@urole", _Role)
                .Parameters.AddWithValue("@controltype", _ControlText)
                .Parameters.AddWithValue("@controlname", _ControlName)
                .Parameters.AddWithValue("@mnuaccess", _Access)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@comments", MySqlDbType.String)
                .Parameters("@comments").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                '_ControlText = .Parameters("@comments").Value
                UpdateRolePermisisons = .Parameters("@comments").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            UpdateRolePermisisons = ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User access database update and recording failed.", ._ErrorMethod = "Update Role Permissions", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return UpdateRolePermisisons

    End Function

#End Region

#Region "Roles Management Procedures, Functions and Methods"

    Dim _RoleStatus As String = Nothing
    Dim _RoleNo As String = Nothing

    Protected Friend Property RoleNo As String
        Get
            Return _RoleNo
        End Get
        Set(ByVal value As String)
            _RoleNo = value
        End Set
    End Property

    Protected Friend Sub RecordRole(ByVal RID As String, ByVal RNAME As String, ByVal RSTATUS As String)

        Try

            _Role = StrConv(RNAME, VbStrConv.ProperCase)
            _RoleStatus = StrConv(RSTATUS, VbStrConv.ProperCase)

            resp = NewRole()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "New user system role creation failed.", ._ErrorMethod = "Record Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function NewRole() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "record_role"

                .Parameters.Clear()

                .Parameters.AddWithValue("@rdescription", _Role)
                .Parameters.AddWithValue("@rstatus", _RoleStatus)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@recst", MySqlDbType.String)
                .Parameters("@recst").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                NewRole = .Parameters("@recst").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            NewRole = ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "New user system role creation and data entry failed.", ._ErrorMethod = "New Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return NewRole

    End Function

    Protected Friend Sub EditRole(ByVal RID As String, ByVal RNAME As String, ByVal RSTATUS As String)

        Try

            _RoleID = RID
            _Role = StrConv(RNAME, VbStrConv.ProperCase)
            _RoleStatus = StrConv(RSTATUS, VbStrConv.ProperCase)

            resp = ERole()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User system role modification failed.", ._ErrorMethod = "Edit Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function ERole() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "update_role"

                .Parameters.Clear()

                .Parameters.AddWithValue("@roleid", _RoleID)
                .Parameters.AddWithValue("@rdescription", _Role)
                .Parameters.AddWithValue("@rstatus", _RoleStatus)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@uptstatus", MySqlDbType.String)
                .Parameters("@uptstatus").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                ERole = .Parameters("@uptstatus").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            ERole = ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User system role modification and data entry failed.", ._ErrorMethod = "Edit (Data) Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return ERole

    End Function

    Protected Friend Sub DeleteRole(ByVal RID As String, ByVal RNAME As String)

        Try

            _RoleID = RID
            _Role = RNAME

            resp = DRole()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User system role deletion failed.", ._ErrorMethod = "Delete Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function DRole() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "delete_role"

                .Parameters.Clear()

                .Parameters.AddWithValue("@roleid", _RoleID)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@dtest", MySqlDbType.String)
                .Parameters("@dtest").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                DRole = .Parameters("@dtest").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            DRole = ex.Message
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User system role deletion and data entry failed.", ._ErrorMethod = "Delete (Data) Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return DRole

    End Function

    Protected Friend Function GetRoleNo() As String

        Try

            _RoleNo = NewRoleNo()

        Catch ex As Exception

            _RoleNo = "Error"
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User system role retrieval failed.", ._ErrorMethod = "Get Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return _RoleNo

    End Function

    Private Function NewRoleNo() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "get_roleno"

                .Parameters.Clear()

                .Parameters.Add("@rid", MySqlDbType.String)
                .Parameters("@rid").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                NewRoleNo = .Parameters("@rid").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            NewRoleNo = Nothing
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "User system role creation and retrieval failed.", ._ErrorMethod = "Get (Data) Role", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return NewRoleNo

    End Function

#End Region

#Region "Database Backup, Restore and Update Procedures, functions and Methods"

    Protected Friend Sub DBBackUp(ByVal Location As String, ByVal BcK As String)

        'If Location = "" Then
        '    Location = "C:\Backup"
        'End If

        'If BcK = "" Then
        '    BcK = Format(Today.Date, "yyyyMMdd") & ".sql"
        'End If

        'Using conn As New MySqlConnection(cn.route_local)

        '    Using cmd As New MySqlCommand

        '        Using mb As New MySqlBackup(cmd)

        '            cmd.Connection = conn
        '            conn.Open()
        '            mb.ExportToFile(Location & "\" & BcK)
        '            conn.Close()

        '        End Using

        '    End Using

        'End Using

    End Sub

#End Region

End Class
