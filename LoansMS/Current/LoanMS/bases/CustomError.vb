
Imports MySql.Data.MySqlClient

Public Class CustomError

    Protected Friend Class ErrorsRec

        Property _ErrorMethod As String = Nothing
        Property _ErrorDescription As String = Nothing
        Property _ErrorSource As String = Nothing
        Property _ErrorMessage As String = Nothing
        Property _ErrorDate As Date = Nothing
        Property _ErrorTime As DateTime = Nothing

    End Class

    Sub New()

    End Sub

    Protected Friend Sub LogError(ByVal SysError As ErrorsRec)

        With cmd

            .Connection = GetDatabaseConnection()
            .Connection.Open()

            .CommandType = CommandType.StoredProcedure
            .CommandText = "log_systemerrors"

            .Parameters.Clear()

            .Parameters.AddWithValue("@errmthd", SysError._ErrorMethod)
            .Parameters.AddWithValue("@errdesc", SysError._ErrorDescription)
            .Parameters.AddWithValue("@errsrc", SysError._ErrorSource)
            .Parameters.AddWithValue("@errmsg", SysError._ErrorMessage)
            .Parameters.AddWithValue("@errdate", SysError._ErrorDate)
            .Parameters.AddWithValue("@errtime", SysError._ErrorTime)
            .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
            .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))

            .ExecuteNonQuery()

            .Parameters.Clear()
            .Connection.Close()

            .Dispose()

        End With

    End Sub

End Class

