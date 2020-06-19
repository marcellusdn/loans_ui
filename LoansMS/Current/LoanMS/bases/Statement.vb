

Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Timers
Imports System.Diagnostics
'Imports System.ServiceProcess

Public Class Statement

    Protected Friend StatementID As String = Nothing
    Protected Friend IsProcessed As Boolean = False

    Protected Friend WithEvents StatementTimer As New Timer

    Protected Friend Sub BookStatementRequest(ByVal StartDate As Date, ByVal EndDate As Date, ByVal LoanAccount As String)

        If String.IsNullOrEmpty(LoanAccount) = False And StartDate <> Nothing And EndDate <> Nothing Then

            LogRequest(StartDate, EndDate, LoanAccount)

        End If

        If String.IsNullOrEmpty(StatementID) = False Then

            IsProcessed = False
            StatementTimer.Enabled = True
            StatementTimer.Interval = 3000
            StatementTimer.Start()

        End If

    End Sub

    Private Sub LogRequest(ByVal StartDate As Date, ByVal EndDate As Date, ByVal LoanAccount As String)

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "log_statement_request"

                .Parameters.Clear()

                .Parameters.AddWithValue("@bankaccount", LoanAccount)
                .Parameters.AddWithValue("@begindate", StartDate)
                .Parameters.AddWithValue("@enddate", EndDate)

                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))
                .Parameters.AddWithValue("@sessionid", HttpContext.Current.Session("session_id"))

                .Parameters.Add("@reqid", MySqlDbType.String)
                .Parameters("@reqid").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                StatementID = If(IsDBNull(.Parameters("@reqid").Value), Nothing, .Parameters("@reqid").Value)

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Statement request log for account" & LoanAccount & " failed.", ._ErrorMethod = "Log Statement Request", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Protected Friend Sub CheckStatementTimer(ByVal sender As Object, ByVal e As ElapsedEventArgs) Handles StatementTimer.Elapsed

        Try

            IsProcessed = If(CheckRequest(StatementID) = 2, True, False)

            If IsProcessed = True Then
                StatementTimer.Enabled = False
                StatementTimer.Stop()
            End If

        Catch ex As Exception

            IsProcessed = False
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Statement timer execution failed.", ._ErrorMethod = "CheckStatementTimer", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function CheckRequest(ByVal TranID As String) As Integer

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "check_postbank_statement"

                .Parameters.Clear()

                .Parameters.AddWithValue("@reqid", TranID)

                .Parameters.Add("@chk", MySqlDbType.Int16)
                .Parameters("@chk").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                CheckRequest = .Parameters("@chk").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            Return CheckRequest

        Catch ex As Exception

            Return 0
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Statement request checking account for request " & TranID & " failed.", ._ErrorMethod = "Check Statement Request", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Function

End Class
