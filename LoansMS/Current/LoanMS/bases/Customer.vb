

Imports System.Data.SqlClient

Public Class Customer

    Public NationalID As String = Nothing
    Public CustomerID As String = Nothing
    Public CustomerName As String = Nothing
    Public CusLimit As Double = Nothing
    Public AccountNo As String = Nothing
    Public CusStatus As String = Nothing
    Public CustomerType As String = Nothing
    Public DateofBirth As Nullable(Of Date) = Nothing
    Public MobileNo As String = Nothing

    Protected Friend Sub GetClientDetails(ByVal QueryMobile As String)

        Try

            Using con As New SqlConnection("Data Source=WIN-U7A1OCIE5LS;Initial Catalog=QuickBI;Persist Security Info=True;User ID=sa;Connect Timeout=600;Password=admin123$")

                Using pcmd As New SqlCommand

                    With pcmd

                        .Connection = con
                        .Connection.Open()

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "spGetClient"

                        .Parameters.Clear()

                        .Parameters.AddWithValue("@mobno", QueryMobile)

                        .Parameters.Add("@natid", SqlDbType.VarChar, 8)
                        .Parameters("@natid").Direction = ParameterDirection.Output

                        .Parameters.Add("@cusid", SqlDbType.VarChar, 13)
                        .Parameters("@cusid").Direction = ParameterDirection.Output

                        .Parameters.Add("@cusname", SqlDbType.VarChar, 150)
                        .Parameters("@cusname").Direction = ParameterDirection.Output

                        .Parameters.Add("@mobileno", SqlDbType.VarChar, 12)
                        .Parameters("@mobileno").Direction = ParameterDirection.Output

                        .Parameters.Add("@cuslimit", SqlDbType.Money)
                        .Parameters("@cuslimit").Direction = ParameterDirection.Output

                        .Parameters.Add("@custp", SqlDbType.VarChar, 20)
                        .Parameters("@custp").Direction = ParameterDirection.Output

                        .Parameters.Add("@bknac", SqlDbType.VarChar, 13)
                        .Parameters("@bknac").Direction = ParameterDirection.Output

                        .Parameters.Add("@dob", SqlDbType.Date)
                        .Parameters("@dob").Direction = ParameterDirection.Output

                        .Parameters.Add("@cusstate", SqlDbType.VarChar, 20)
                        .Parameters("@cusstate").Direction = ParameterDirection.Output

                        .ExecuteNonQuery()

                        If IsDBNull(.Parameters("@natid").Value) = False And String.IsNullOrEmpty(.Parameters("@natid").Value) = False Then

                            NationalID = .Parameters("@natid").Value
                            CustomerID = .Parameters("@cusid").Value
                            CustomerName = .Parameters("@cusname").Value
                            MobileNo = .Parameters("@mobileno").Value
                            CusLimit = IIf(IsDBNull(.Parameters("@cuslimit").Value), Nothing, .Parameters("@cuslimit").Value)
                            CustomerType = .Parameters("@custp").Value
                            AccountNo = .Parameters("@bknac").Value
                            DateofBirth = IIf(IsDBNull(.Parameters("@dob").Value), Nothing, .Parameters("@dob").Value)
                            CusStatus = .Parameters("@cusstate").Value

                        Else

                            resp = "Error: No records exist for mobile number: " & QueryMobile

                        End If

                        .Parameters.Clear()
                        .Connection.Close()

                        .Dispose()

                    End With

                End Using

            End Using

        Catch ex As Exception

            resp = "Error: " & ex.Message

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "No details for mobile number" & QueryMobile & " failed.", ._ErrorMethod = "Log Statement Request", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Protected Friend Sub DeactivateClient(ByVal QueryMobile As String)

        Try

            Using con As New SqlConnection("Data Source=WIN-U7A1OCIE5LS;Initial Catalog=QuickBI;Persist Security Info=True;User ID=sa;Connect Timeout=600;Password=admin123$")

                Using pcmd As New SqlCommand

                    With pcmd

                        .Connection = con
                        .Connection.Open()

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "spDeactivateClient"

                        .Parameters.Clear()

                        .Parameters.AddWithValue("@mobno", QueryMobile)

                        .Parameters.Add("@rnstatus", SqlDbType.VarChar, 30)
                        .Parameters("@rnstatus").Direction = ParameterDirection.Output

                        .ExecuteNonQuery()

                        resp = .Parameters("@rnstatus").Value

                        .Parameters.Clear()
                        .Connection.Close()

                        .Dispose()

                    End With

                End Using

            End Using

        Catch ex As Exception

            resp = "Error: Deactivation for client with for mobile number " & QueryMobile & " failed.\nError Message: " & ex.Message

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Deactivation for client with for mobile number" & QueryMobile & " failed.", ._ErrorMethod = "Log Statement Request", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        If resp = "Success" Then
            resp = "Success: Client (" & QueryMobile & ") deactivated from receiving messages."
        End If

    End Sub

    Protected Friend Sub UpdateText(ByVal recid As String, ByVal txtmsg As String, ByVal cusgrp As String,
                                    ByVal custype As String, ByVal msgstatus As String)

        Try

            Using con As New SqlConnection("Data Source=WIN-U7A1OCIE5LS;Initial Catalog=QuickBI;Persist Security Info=True;User ID=sa;Connect Timeout=600;Password=admin123$")

                Using pcmd As New SqlCommand

                    With pcmd

                        .Connection = con
                        .Connection.Open()

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "spUpdateMessageType"

                        .Parameters.Clear()

                        .Parameters.AddWithValue("@recid", recid)
                        .Parameters.AddWithValue("@custype", custype)
                        .Parameters.AddWithValue("@msgtext", txtmsg)
                        .Parameters.AddWithValue("@msgst", msgstatus)

                        .Parameters.Add("@resp", SqlDbType.VarChar, 30)
                        .Parameters("@resp").Direction = ParameterDirection.Output

                        .ExecuteNonQuery()

                        resp = .Parameters("@resp").Value

                        .Parameters.Clear()
                        .Connection.Close()

                        .Dispose()

                    End With

                End Using

            End Using

        Catch ex As Exception

            resp = "Error: Update for the customer group (" & cusgrp & ") text message failed.\nError Message: " & ex.Message

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Error: Update for the customer group (" & cusgrp & ") text message failed", ._ErrorMethod = "Log Update Clients Messages", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

End Class
