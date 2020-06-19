
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Math
Imports System.IO
Imports System.Random
Imports System.Web
Imports System.Collections
Imports System.Collections.Generic

Public Class Setup

#Region "Password Complexity Management"

    Private _PAge As Integer = Nothing
    Private _MaxP As Integer = Nothing
    Private _MinP As Integer = Nothing
    Private _Numerics As Integer = Nothing
    Private _Special As Integer = Nothing
    Private _Upper As Integer = Nothing
    Private _History As Boolean = True

    Protected Friend _ManagePassword As String = Nothing

    Protected Friend Sub ManagePassword(ByVal passwordage As Integer, ByVal maxpasswordlength As Integer,
                                        ByVal minpasswordlength As Integer, ByVal numericcharacters As Integer,
                                        ByVal uppercasecharacters As Integer, ByVal specialcharacters As Integer,
                                        ByVal enforcehistory As Boolean)

        Try

            _PAge = passwordage
            _MaxP = maxpasswordlength
            _MinP = minpasswordlength
            _Numerics = numericcharacters
            _Upper = uppercasecharacters
            _Special = specialcharacters
            _History = enforcehistory

            _ManagePassword = RecordPasswordComplexity()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The management of the password complexity rules has failed.", ._ErrorMethod = "Update Password Complexity", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function RecordPasswordComplexity() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "managepasswords"

                .Parameters.Clear()

                .Parameters.AddWithValue("@passage", _PAge)
                .Parameters.AddWithValue("@passmin", _MinP)
                .Parameters.AddWithValue("@passmax", _MaxP)
                .Parameters.AddWithValue("@passhist", _History)
                .Parameters.AddWithValue("@passnumerals", _Numerics)
                .Parameters.AddWithValue("@passupper", _Upper)
                .Parameters.AddWithValue("@passspecial", _Special)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@response", MySqlDbType.String)
                .Parameters("@response").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                RecordPasswordComplexity = .Parameters("@response").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            Return RecordPasswordComplexity

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The entry and update to the password complexity rules has failed.", ._ErrorMethod = "Record Password Complexity", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            Return ex.Message

        End Try

    End Function

#End Region

#Region "PostBank TextMessages Settings"
    Protected Friend Sub UpdatePostBankTexts(ByVal Frequency As String, ByVal MessageCount As String)

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "update_postbank_marketing_setup"

                .Parameters.Clear()

                .Parameters.AddWithValue("@pbfreq", Frequency)
                .Parameters.AddWithValue("@msgcount", MessageCount)
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@response", MySqlDbType.String)
                .Parameters("@response").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                resp = .Parameters("@response").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The update to the PostBank message settings has failed.", ._ErrorMethod = "Update PostBank Texts", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            resp = ex.Message

        End Try

    End Sub

#End Region

End Class
