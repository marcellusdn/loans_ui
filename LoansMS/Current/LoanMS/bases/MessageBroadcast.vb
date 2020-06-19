
Imports MySql.Data.MySqlClient
Imports System.Text
Imports System.Math
Imports System.IO
Imports System.Random
Imports System.Web
Imports System.Collections
Imports System.Collections.Generic

Friend Class MessageBroadcast

#Region "Global Variables"

    Protected Friend rd As MySqlDataReader

    Protected Friend _Dept As String = Nothing

    Protected Friend rnd As New Random

    Protected Friend mapID As String = Nothing

    Protected Friend username As String = Nothing
    Protected Friend apikey As String = Nothing
    Protected Friend sender As String = Nothing

    Protected Friend _CustomerMobile As String = Nothing
    Protected Friend _CustomerName As String = Nothing
    Protected Friend _CustomerAccount As String = Nothing

    Protected Friend _ActionStart As String = Nothing
    Protected Friend _ActionEnd As String = Nothing
    Protected Friend _Action As String
    Protected Friend _ActionStatus As Boolean
    Protected Friend _ReferenceNo As String

    Protected Friend _ActionedRecords As Integer

    Protected Friend _CurrentBalance As Double
    Protected Friend _AccountBalance As Double

    Protected Friend _TransactionID As String = Nothing

    Protected Friend _TransationBatchNo As String = Nothing

#End Region

#Region "Message Delivery Variables"

    Protected Friend messagelist As New GridView
    Protected Friend _Message As String = Nothing
    Protected Friend _PushMobile As String = Nothing
    Protected Friend _ReturnMobileNo As String = Nothing
    Protected Friend _MessageID As String = Nothing
    Protected Friend _MessageStatus As String = Nothing
    Protected Friend _Cost As String = Nothing
    Protected Friend _StatusCode As String = Nothing
    Protected Friend _MessageType As String = Nothing

    Private ReadOnly _BulkSMSMode As Integer = 0
    Private ReadOnly _QueueStatus As Integer = Nothing

    Protected Friend _MessageTime As DateTime = Nothing
    Protected Friend _BatchNo As String = Nothing
    Protected Friend _DeliveryDate As Date = Nothing

    Private _BatchCount As Integer = Nothing
    Private _Records As Integer = Nothing

    Private ReadOnly r() As String
    Private ReadOnly db() As String

    Protected Friend _SuccessfulMessages As Integer = Nothing
    Protected Friend _FailedMessages As Integer = Nothing
    Protected Friend _RejectedMessages As Integer = Nothing

    Protected Friend _MessageLogStatus As String = Nothing
    Private ReadOnly _UnloggedCount As Integer

    Protected Friend _ActionCost As Double = Nothing
    Protected Friend _TotalCost As Double = Nothing

#End Region

#Region "Airtime Variables"

    Private ReadOnly _TotalAirtime As Double = Nothing
    Private ReadOnly _LoadedAirtime As Double = Nothing
    Private ReadOnly _SuccessfulAirtime As Double = Nothing
    Private ReadOnly _FailedAirtime As Double = Nothing
    Private ReadOnly _RejectedAirtime As Double = Nothing

    Private _AirtimeRequest As Double = Nothing

    Private _AirtimeAmount As Double = Nothing
    Private ReadOnly _AirtimeTime As DateTime = Nothing
    Private _AirtimeStatus As String = Nothing
    Private _AirtimeID As String = Nothing
    Private _AirtimeMobile As String = Nothing
    Private _AirtimeReturnMobile As String = Nothing
    Private _AirtimeDiscount As String = Nothing
    Private _AirtimeErrorMessage As String = Nothing
    Private _AirtimeReturnAmount As String = Nothing

    Private _AirtimeTopUp As New GridView

    Private _AirtimeLogStatus As String = Nothing

    Private _AirtimeBatch As String = Nothing

    Private ReadOnly _AirtimeList As New List(Of Hashtable)
    Private ReadOnly _AirtimeRecipient As New Hashtable

    Private _CheckTopUp As Boolean

    Private _LoanAmount As Double = Nothing

#End Region

#Region "Scheduled Jobs Variables"

    Private _Schedule As New GridView

    Private _JobNo As String = Nothing
    Private _JobDate As Date
    Private _JobTime As DateTime
    Private ReadOnly _JobStatus As String = Nothing
    Private ReadOnly _Messages As Integer = Nothing

    Private _JobRecord As String = Nothing

    Private ReadOnly _RecordNo As String = Nothing
    Private _ScheduledMessage As String = Nothing
    Private ReadOnly _ScheduledAccount As String = Nothing
    Private ReadOnly _ScheduledDate As String = Nothing
    Private ReadOnly _ScheduledStatus As String = Nothing
    Private _ScheduledJobNo As String = Nothing

    Private ReadOnly _ScheduledMessageStatus As String = Nothing

    Private ReadOnly _QueuedSchedules As Integer = Nothing

    Private ReadOnly ds As New DataSet
    Private ReadOnly ta As MySqlDataAdapter

    Private ReadOnly _PendingRecords As Integer
    Private ReadOnly _FailedRecords As Integer

    Private ReadOnly _JobStart As DateTime = Nothing
    Private ReadOnly _JobEnd As DateTime = Nothing

    Private ReadOnly _CurrentMessageStatus As String = Nothing

#End Region

#Region "Message Delivery Procedures, Methods and Functions"

    Protected Friend Sub SendMessage(ByVal Log As GridView,
                                     Optional ByVal Dept As String = Nothing,
                                     Optional ByVal MsgType As String = Nothing,
                                     Optional ByVal Mode As String = Nothing,
                                     Optional ByVal Queing As String = Nothing)

        Try

            messagelist = Log

            _DeliveryDate = Today.Date
            _BatchNo = GenerateBatchNo()
            _Dept = Dept
            _MessageType = MsgType

            _Records = messagelist.Rows.Count
            _CurrentBalance = Round(GetBalance(), 2)

            _SuccessfulMessages = 0
            _FailedMessages = 0
            _RejectedMessages = 0

            If _Records > 0 Then

                If _CurrentBalance > 0 And _Records <= _CurrentBalance Then

                    If _Records <= 50 Then

                        _ActionStart = Now

                        If LogMessageBatch() = True Then

                            ExecuteDelivery()

                        End If

                        _ActionEnd = Now

                    Else

                        ScheduleMessages(messagelist, Today.Date, Now, Dept, MsgType)

                    End If

                End If

            End If

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Preparation of the messages to be delivered failed.", ._ErrorMethod = "Message Delivery Preparation", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Sub ExecuteDelivery()

        Try

            GetCredo()

            Dim costlength As Integer = Nothing

            _ActionedRecords = 0

            _SuccessfulMessages = 0
            _FailedMessages = 0
            _RejectedMessages = 0

            _ActionCost = 0
            _TotalCost = 0

            Dim gateway As New AfricasTalkingGateway(username, apikey)

            For Each gv As GridViewRow In messagelist.Rows

                Try

                    _CustomerMobile = gv.Cells(0).Text
                    _Message = gv.Cells(3).Text
                    _CustomerAccount = gv.Cells(1).Text
                    _CustomerName = gv.Cells(2).Text
                    _MessageTime = Now

                    Dim cusmobno As String = _CustomerMobile
                    If Left(cusmobno, 1) = "+" Then
                        cusmobno = Strings.Mid(cusmobno, 2)
                    End If
                    If Left(cusmobno, 1) = "0" Then
                        cusmobno = Strings.Mid(cusmobno, 2)
                    End If
                    If Left(cusmobno, 3) = "254" Then
                        cusmobno = Strings.Mid(cusmobno, 4)
                    End If
                    If cusmobno.Contains("-") Then
                        Strings.Replace(cusmobno, "-", "")
                    End If

                    _PushMobile = "+254" & cusmobno

                    If Len(_PushMobile) <> 13 Or Left(_PushMobile, 5) <> "+2547" Then

                        _MessageStatus = "Failed"
                        _MessageID = "ATix_" & Now().ToLongTimeString & _TransactionID & _CustomerMobile
                        _ReturnMobileNo = _PushMobile
                        _Cost = "KES 0.00"
                        _StatusCode = "444"

                        costlength = Len(_Cost)

                        If costlength > 1 Then
                            _ActionCost = CDbl(Right(_Cost, Len(_Cost) - 4))
                        Else
                            _ActionCost = 0
                        End If

                        _TotalCost = _TotalCost + _ActionCost

                        CreateLog()

                    Else

                        Dim result As Object
                        Dim response As Object() = gateway.SendMessage(_PushMobile, _Message, sender)

                        For Each result In response

                            _MessageStatus = result("status")
                            _MessageID = result("messageId")
                            _ReturnMobileNo = result("number")
                            _Cost = result("cost")
                            _StatusCode = result("statusCode")

                            _ActionedRecords = _ActionedRecords + 1

                            If _MessageStatus <> "" And _MessageID = "None" Then
                                _MessageID = "ATixMobile_" & cusmobno & "_StatusCode_" & _StatusCode
                            End If

                            If _MessageStatus = "Sent" Or _MessageStatus = "Success" Then
                                _SuccessfulMessages = _SuccessfulMessages + 1
                            End If
                            If _MessageStatus = "Failed" Then
                                _FailedMessages = _FailedMessages + 1
                            End If
                            If _MessageStatus = "User in BlackList" Then
                                _RejectedMessages = _RejectedMessages + 1
                            End If

                            costlength = Len(_Cost)

                            If costlength > 1 Then
                                _ActionCost = CDbl(Right(_Cost, Len(_Cost) - 4))
                            Else
                                _ActionCost = 0
                            End If

                            _TotalCost = _TotalCost + _ActionCost

                            CreateLog()

                        Next

                        CreateLog()

                    End If

                Catch ex As AfricasTalkingGatewayException

                    Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Message delivery failed.", ._ErrorMethod = "Send Message - Africa's Talking", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
                    With err
                        .LogError(_Error)
                    End With

                End Try

            Next

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Message delivery failed.", ._ErrorMethod = "Send Message - Global", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Protected Friend Sub CreateLog()

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "save_message_log"

                .Parameters.Clear()

                .Parameters.AddWithValue("@cusMobNumber", _CustomerMobile)
                .Parameters.AddWithValue("@custName", _CustomerName)
                .Parameters.AddWithValue("@cusLoanNo", _CustomerAccount)
                .Parameters.AddWithValue("@cusMessage", _Message)
                .Parameters.AddWithValue("@mesStatus", _MessageStatus)
                .Parameters.AddWithValue("@retID", _MessageID)
                .Parameters.AddWithValue("@retPhoneNo", _ReturnMobileNo)
                .Parameters.AddWithValue("@retCost", _Cost)
                .Parameters.AddWithValue("@message_batch", _BatchNo)
                .Parameters.AddWithValue("@msg_queue_time", _MessageTime)
                .Parameters.AddWithValue("@msg_date", _DeliveryDate)
                .Parameters.AddWithValue("@msg_code", _StatusCode)

                .Parameters.Add("@msg_log_status", MySqlDbType.String)
                .Parameters("@msg_log_status").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The logging of the message with ID:" + _MessageID + "Failed", ._ErrorMethod = "Log Message Delivery", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Protected Friend Function LogMessageBatch() As Boolean

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "log_message_job_batch"

                .Parameters.Clear()

                .Parameters.AddWithValue("@jobbatch", _BatchNo)
                .Parameters.AddWithValue("@reccount", _Records)
                .Parameters.AddWithValue("@jobdate", _DeliveryDate)
                .Parameters.AddWithValue("@userdept", _Dept)
                .Parameters.AddWithValue("@batchtype", _MessageType)
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))

                .Parameters.Add("@response", MySqlDbType.String)
                .Parameters("@response").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                _MessageLogStatus = .Parameters("@response").Value

                If _MessageLogStatus = "Success" Then
                    Return True
                Else
                    Return False
                End If

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Creation and logging of message batch log failed.", ._ErrorMethod = "Message Batch Log Preparation", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            Return False

        End Try

    End Function

    Protected Friend Function GenerateBatchNo() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "get_batch_count"

                .Parameters.Clear()

                .Parameters.Add("@batchcount", MySqlDbType.String)
                .Parameters("@batchcount").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                _BatchCount = .Parameters("@batchcount").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            If _BatchCount = 0 Then
                Return Format(Today.Date, "yyyyMMdd").ToString + "001"
            Else
                Return Format(Today.Date, "yyyyMMdd").ToString & StrDup(3 - Len((_BatchCount + 1).ToString), "0") & (_BatchCount + 1)
            End If

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Retrieval and creation of a batch number failed.", ._ErrorMethod = "Message Batch Generation", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            Return Nothing

        End Try

    End Function

#End Region

#Region "Global Methods, Functions and Procedures"

    Private Function GetBalance() As Double

        Try

            GetCredo()

            Dim gateway As New AfricasTalkingGateway(username, apikey)

            Dim obj As Object = gateway.GetUserData()
            Dim bal As String = obj("balance")
            GetBalance = CDbl(Right(bal, Len(bal) - 4))

        Catch ex As Exception

            GetBalance = 0
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Retrieval of available units balance at Africa's Talking failed.", ._ErrorMethod = "Get Account Balance - Africa's Talking", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        'Return GetBalance
        Return 100000

    End Function

    Private Sub GetCredo()

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "get_sms_data"

                .Parameters.Clear()

                .Parameters.Add("@acc", MySqlDbType.String)
                .Parameters("@acc").Direction = ParameterDirection.Output

                .Parameters.Add("@cde", MySqlDbType.String)
                .Parameters("@cde").Direction = ParameterDirection.Output

                .Parameters.Add("@codeac", MySqlDbType.String)
                .Parameters("@codeac").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                username = .Parameters("@acc").Value
                apikey = .Parameters("@codeac").Value
                sender = .Parameters("@cde").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Retrieval of system credentials for Africa's Talking failed.", ._ErrorMethod = "Credentials Retrieval - Africa's Talking", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Protected Friend Function ShowBalance() As Double

        Return GetBalance()

    End Function

#End Region

#Region "Airtime Top-Up Methods, Functions and Procedures"

    Protected Friend Sub LoadAirtime(ByVal AirtimeList As GridView, ByVal departmentloading As String)

        Try

            _AirtimeTopUp = AirtimeList
            _DeliveryDate = Today.Date
            _Dept = departmentloading

            _AirtimeBatch = GetAirtimeBatch()
            _AirtimeRequest = 0
            _Records = _AirtimeTopUp.Rows.Count

            For Each gv As GridViewRow In _AirtimeTopUp.Rows
                Dim curamt As Double = Val(gv.Cells(4).Text)
                _AirtimeRequest = _AirtimeRequest + curamt
            Next

            _CurrentBalance = GetBalance()

            If _CurrentBalance > 0 And _CurrentBalance > _AirtimeRequest Then

                If LogAirtimeBatch() = True Then

                    _ActionStart = Now
                    ExecuteTopup()
                    _ActionEnd = Now

                End If

            Else

                Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Insufficient units available to top-up." + _AirtimeRequest, ._ErrorMethod = "Airtime Top-Up", ._ErrorSource = "Data Validation - Topup", ._ErrorMessage = "Insufficient Credit"}
                With err
                    .LogError(_Error)
                End With

            End If

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Retrieval of system credentials for Africa's Talking failed.", ._ErrorMethod = "Credentials Retrieval - Africa's Talking", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Sub ExecuteTopup()

        Try

            Dim costlength As Integer = Nothing

            GetCredo()

            _ActionedRecords = 0

            _ActionCost = 0
            _TotalCost = 0

            Dim gateway As New AfricasTalkingGateway(username, apikey)

            For Each gv As GridViewRow In _AirtimeTopUp.Rows

                _AirtimeList.Clear()

                Dim curmobile As String = Nothing

                _CustomerMobile = gv.Cells(0).Text
                _CustomerAccount = gv.Cells(2).Text
                _CustomerName = gv.Cells(1).Text
                _AirtimeAmount = gv.Cells(4).Text
                _LoanAmount = gv.Cells(3).Text

                Dim cusmobno As String = _CustomerMobile
                If Left(cusmobno, 1) = "+" Then
                    cusmobno = Strings.Mid(cusmobno, 2)
                End If
                If Left(cusmobno, 1) = "0" Then
                    cusmobno = Strings.Mid(cusmobno, 2)
                End If
                If Left(cusmobno, 3) = "254" Then
                    cusmobno = Strings.Mid(cusmobno, 4)
                End If
                If cusmobno.Contains("-") Then
                    Strings.Replace(cusmobno, "-", "")
                End If

                _AirtimeMobile = "+254" & cusmobno

                If Len(_AirtimeMobile) <> 13 And Left(_AirtimeMobile, 5) <> "+2547" Then

                    _AirtimeStatus = "Failed"
                    _AirtimeID = "ATix_" & _AirtimeMobile & Today.Date & _AirtimeAmount & Now
                    _AirtimeReturnMobile = _AirtimeMobile
                    _AirtimeDiscount = "KES 0.00"
                    _AirtimeReturnAmount = "KES 0.00"
                    _AirtimeErrorMessage = "Invalid Mobile Number"

                    LogTopup()

                Else

                    If ValidateAccount() = True Then

                        _AirtimeRecipient("phoneNumber") = _AirtimeMobile
                        _AirtimeRecipient("amount") = "KES " & _AirtimeAmount

                        _AirtimeList.Add(_AirtimeRecipient)

                        Try

                            Dim rp As Object() = gateway.SendAirtime(_AirtimeList)
                            Dim rst As Object

                            For Each rst In rp

                                _AirtimeStatus = rst("status")
                                _AirtimeID = rst("requestId")
                                _AirtimeReturnMobile = rst("phoneNumber")
                                _AirtimeDiscount = rst("discount")
                                _AirtimeReturnAmount = rst("amount")

                                _ActionedRecords = _ActionedRecords + 1

                                _AirtimeErrorMessage = IIf(rst("status") <> "Failed", Nothing, rst("errorMessage"))

                                _Cost = Strings.Replace(_AirtimeReturnAmount, "KES ", "")
                                costlength = Len(_AirtimeReturnAmount)

                                If costlength > 1 Then
                                    _ActionCost = _AirtimeAmount - CDbl(Right(_AirtimeDiscount, Len(_AirtimeDiscount) - 4))
                                Else
                                    _ActionCost = 0
                                End If

                                _TotalCost = _TotalCost + _ActionCost

                                LogTopup()

                                If _AirtimeStatus <> "Failed" Then

                                    Dim msgcontent As String = Nothing
                                    If Left(_CustomerAccount, 5) = "A044-" Then
                                        msgcontent = "Dear Postbank pensioner your safaricom line has been topped up with airtime of KES " & _AirtimeAmount & " by Postbank's lending Partner FinCredit for successful loan application."
                                    Else
                                        msgcontent = "Dear " & StrConv(_CustomerName, VbStrConv.Uppercase) & " your mobile line has been topped up with airtime of KES " & _AirtimeAmount & " by FinCredit as a reward for a successful loan application."
                                    End If

                                    SendAirtimeNotification(_AirtimeMobile, _CustomerName, _CustomerAccount, msgcontent, _AirtimeBatch)

                                End If

                            Next

                        Catch ex As Exception

                            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Top-up of airtime to the client " & StrConv(_CustomerName, VbStrConv.Uppercase) & " via Africa's Talking failed.", ._ErrorMethod = "Airtime Push - Africa's Talking", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
                            With err
                                .LogError(_Error)
                            End With

                        End Try

                    Else

                        _AirtimeStatus = "Failed"
                        _AirtimeID = "ATix_" & "Mob:" & _AirtimeMobile.ToString & "Amt:" & _AirtimeAmount.ToString & "Date:" & Today.Date.ToString("yyyyMMdd")
                        _AirtimeReturnMobile = _AirtimeMobile
                        _AirtimeDiscount = "KES 0.00"
                        _AirtimeReturnAmount = "KES 0.00"
                        _AirtimeErrorMessage = "Invalid Phone No or Airtime Amount"

                        LogTopup()

                    End If

                End If

            Next

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Top-up of airtime to clients via Africa's Talking failed.", ._ErrorMethod = "Execute Airtime Top-Up", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Sub SendAirtimeNotification(ByVal mobileno As String, ByVal clientname As String,
                                        ByVal clientaccount As String, ByVal customnotification As String,
                                        ByVal CurrentBatch As String)

        Try

            If String.IsNullOrEmpty(username) = True Then
                GetCredo()
            End If

            Dim costlength As Integer = Nothing

            Dim gateway As New AfricasTalkingGateway(username, apikey)

            _CustomerMobile = mobileno
            _Message = customnotification
            _CustomerAccount = clientaccount
            _CustomerName = StrConv(clientname, VbStrConv.Uppercase)
            _MessageTime = Now
            _BatchNo = CurrentBatch
            _DeliveryDate = Today.Date

            Dim result As Object
            Dim response As Object() = gateway.SendMessage(_CustomerMobile, _Message, sender)

            For Each result In response

                _MessageStatus = result("status")
                _MessageID = result("messageId")
                _ReturnMobileNo = result("number")
                _Cost = result("cost")
                _StatusCode = result("statusCode")

                costlength = Len(_Cost)

                If costlength > 1 Then
                    _ActionCost = CDbl(Right(_Cost, Len(_Cost) - 4))
                Else
                    _ActionCost = 0
                End If

                _TotalCost = _TotalCost + _ActionCost

                CreateNotificationLog()

            Next

        Catch ex As AfricasTalkingGatewayException

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Delivery of the airtime top-up notification via Africa's Talking failed.", ._ErrorMethod = "Airtime Top-Up Notification", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Protected Friend Sub CreateNotificationLog()

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "save_message_log"

                .Parameters.Clear()

                .Parameters.AddWithValue("@cusMobNumber", _CustomerMobile)
                .Parameters.AddWithValue("@custName", _CustomerName)
                .Parameters.AddWithValue("@cusLoanNo", _CustomerAccount)
                .Parameters.AddWithValue("@cusMessage", _Message)
                .Parameters.AddWithValue("@mesStatus", _MessageStatus)
                .Parameters.AddWithValue("@retID", _MessageID)
                .Parameters.AddWithValue("@retPhoneNo", _ReturnMobileNo)
                .Parameters.AddWithValue("@retCost", _Cost)
                .Parameters.AddWithValue("@message_batch", _BatchNo)
                .Parameters.AddWithValue("@msg_queue_time", _MessageTime)
                .Parameters.AddWithValue("@msg_date", _DeliveryDate)
                .Parameters.AddWithValue("@msg_code", _StatusCode)

                .Parameters.Add("@msg_log_status", MySqlDbType.VarChar)
                .Parameters("@msg_log_status").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                _MessageLogStatus = .Parameters("@msg_log_status").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The logging of the message with ID: " + _MessageID + " Failed", ._ErrorMethod = "Log Message Delivery", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Sub LogTopup()

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "log_airtime_topup"

                .Parameters.Clear()

                .Parameters.AddWithValue("@cusmobile", _CustomerMobile)
                .Parameters.AddWithValue("@cusname", _CustomerName)
                .Parameters.AddWithValue("@cusaccount", _CustomerAccount)
                .Parameters.AddWithValue("@cusloanamt", _LoanAmount)
                .Parameters.AddWithValue("@airtimeloaded", _AirtimeAmount)
                .Parameters.AddWithValue("@batchno", _AirtimeBatch)
                .Parameters.AddWithValue("@deldate", _DeliveryDate)
                .Parameters.AddWithValue("@queuetime", Now)
                .Parameters.AddWithValue("@delstatus", _AirtimeStatus)
                .Parameters.AddWithValue("@deliveryid", _AirtimeID)
                .Parameters.AddWithValue("@retmobile", _AirtimeReturnMobile)
                .Parameters.AddWithValue("@retdiscount", _AirtimeDiscount)
                .Parameters.AddWithValue("@retamount", _AirtimeReturnAmount)
                .Parameters.AddWithValue("@errmessage", _AirtimeErrorMessage)

                .Parameters.Add("@rstatus", MySqlDbType.String)
                .Parameters("@rstatus").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                _AirtimeLogStatus = .Parameters("@rstatus").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Logging of airtime top-up data has failed.", ._ErrorMethod = "Airtime Top-Up Log", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function LogAirtimeBatch() As Boolean

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "log_airtime_job_batch"

                .Parameters.Clear()

                .Parameters.AddWithValue("@jobbatch", _AirtimeBatch)
                .Parameters.AddWithValue("@reccount", _Records)
                .Parameters.AddWithValue("@airtimeamount", _AirtimeRequest)
                .Parameters.AddWithValue("@jobdate", _DeliveryDate)
                .Parameters.AddWithValue("@userdept", _Dept)
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))

                .Parameters.Add("@response", MySqlDbType.String)
                .Parameters("@response").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                _AirtimeLogStatus = .Parameters("@response").Value

                If _AirtimeLogStatus = "Success" Then
                    Return True
                Else
                    Return False
                End If

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Return False
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "Logging of airtime batch top-up data has failed.", ._ErrorMethod = "Airtime Top-Up Log", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Function

    Private Function ValidateAccount() As Boolean

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "validate_airtime_topup"

                .Parameters.Clear()

                .Parameters.AddWithValue("@accno", _CustomerAccount)
                .Parameters.AddWithValue("@amt", _AirtimeAmount)

                .Parameters.Add("@va", MySqlDbType.String)
                .Parameters("@va").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                _CheckTopUp = .Parameters("@va").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            Return _CheckTopUp

        Catch ex As Exception

            Return False
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The validation of airtime top-up details and data has failed.", ._ErrorMethod = "Airtime Validation", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Function

    Private Function GetAirtimeBatch() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "get_airtime_batch"

                .Parameters.Clear()

                .Parameters.Add("@bno", MySqlDbType.String)
                .Parameters("@bno").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                GetAirtimeBatch = .Parameters("@bno").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            Return GetAirtimeBatch

        Catch ex As Exception

            Return Nothing
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The retrieval and creation of the airtime top-up batch number has failed.", ._ErrorMethod = "Airtime Batch Generation", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Function

#End Region

#Region "Scheduled Messages Management"

    Protected Friend Sub ScheduleMessages(ByVal MessagesSchedule As GridView, ByVal ScheduleRunDate As Date, ByVal ScheduleRunTime As DateTime, ByVal ScheduledDepartment As String, Optional ByVal ScheduleType As String = Nothing)

        Try

            _Schedule = MessagesSchedule
            _JobDate = ScheduleRunDate.Date
            _JobTime = ScheduleRunTime
            _Dept = ScheduledDepartment
            _MessageType = ScheduleType
            _Records = _Schedule.Rows.Count
            _JobNo = GetJobNo()

            _JobRecord = Job()

            If Left(_JobRecord, _JobRecord.IndexOf(":")) = "Success" Then

                For Each gv As GridViewRow In _Schedule.Rows

                    _CustomerMobile = gv.Cells(0).Text
                    _CustomerAccount = gv.Cells(1).Text
                    _CustomerName = gv.Cells(2).Text
                    _ScheduledMessage = gv.Cells(3).Text
                    _ScheduledJobNo = _JobNo

                    Schedule()

                Next

            End If

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The recording and saving of the scheduled job " & _JobNo & " has failed.", ._ErrorMethod = "Save Schedule Job", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function Job() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "schedule_job"

                .Parameters.Clear()

                .Parameters.AddWithValue("@jid", _JobNo)
                .Parameters.AddWithValue("@recno", _Records)
                .Parameters.AddWithValue("@jbdate", _JobDate)
                .Parameters.AddWithValue("@jbtime", _JobTime)
                .Parameters.AddWithValue("@jbstatus", _JobStatus)
                .Parameters.AddWithValue("@jbdept", _Dept)
                .Parameters.AddWithValue("@jbmsg", _MessageType)
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@quser", HttpContext.Current.Session("active_user"))

                .Parameters.Add("@st", MySqlDbType.String)
                .Parameters("@st").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                Job = .Parameters("@st").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            Return Job

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The recording and saving of the scheduled job " & _JobNo & " has failed.", ._ErrorMethod = "Save Schedule Job", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            Return Nothing

        End Try

    End Function

    Private Function GetJobNo() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "get_job_no"

                .Parameters.Clear()

                .Parameters.Add("@jobno", MySqlDbType.String)
                .Parameters("@jobno").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                GetJobNo = .Parameters("@jobno").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            Return GetJobNo

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The reetrieval and creation of a scheduled job has failed.", ._ErrorMethod = "Generate Schedule Job No", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            Return Nothing

        End Try

    End Function

    Protected Friend Function GenerateJobNo() As String

        GenerateJobNo = GetJobNo()

        Return GenerateJobNo

    End Function

    Private Function Schedule() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "schedule_messages"

                .Parameters.Clear()

                .Parameters.AddWithValue("@mobno", _CustomerMobile)
                .Parameters.AddWithValue("@cusname", _CustomerName)
                .Parameters.AddWithValue("@msgcontent", _ScheduledMessage)
                .Parameters.AddWithValue("@accno", _CustomerAccount)
                .Parameters.AddWithValue("@jno", _JobNo)

                .Parameters.Add("@st", MySqlDbType.String)
                .Parameters("@st").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                Schedule = .Parameters("@st").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

            Return Schedule

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The scheduling of messages of the scheduled job " & _JobNo & " has failed.", ._ErrorMethod = "Log Schedule Message", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With
            Return Nothing

        End Try

    End Function

    Protected Friend Sub EditJob(ByVal jobno As String, ByVal jobdate As Date, ByVal jobtime As String)

        _JobNo = jobno
        _JobDate = jobdate
        _JobTime = jobtime

        Try

            Edit()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The editing or modification of the scheduled job " & _JobNo & " has failed.", ._ErrorMethod = "Edit Schedule Job", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function Edit() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "update_scheduled_job"

                .Parameters.Clear()

                .Parameters.AddWithValue("@jobno", _JobNo)
                .Parameters.AddWithValue("@deldate", _JobDate)
                .Parameters.AddWithValue("@deltime", _JobTime)
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))

                .Parameters.Add("@ust", MySqlDbType.String)
                .Parameters("@ust").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                Edit = .Parameters("@ust").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Edit = Nothing
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The editing or modification of the scheduled job " & _JobNo & " has failed.", ._ErrorMethod = "Log Schedule Job Modification", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return Edit

    End Function

    Protected Friend Sub DeleteJob(ByVal jobno As String)

        _JobNo = jobno

        Try

            Delete()

        Catch ex As Exception

            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The deletion of the scheduled job " & _JobNo & " has failed.", ._ErrorMethod = "Schedule Job Deletion", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

    End Sub

    Private Function Delete() As String

        Try

            With cmd

                .Connection = GetDatabaseConnection()
                .Connection.Open()

                .CommandType = CommandType.StoredProcedure
                .CommandText = "delete_scheduled_job"

                .Parameters.Clear()

                .Parameters.AddWithValue("@jobno", _JobNo)
                .Parameters.AddWithValue("@sessid", HttpContext.Current.Session("session_id"))
                .Parameters.AddWithValue("@cuser", HttpContext.Current.Session("active_user"))

                .Parameters.Add("@dst", MySqlDbType.String)
                .Parameters("@dst").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                Delete = .Parameters("@dst").Value

                .Parameters.Clear()
                .Connection.Close()

                .Dispose()

            End With

        Catch ex As Exception

            Delete = Nothing
            Dim _Error As New CustomError.ErrorsRec With {._ErrorTime = Now, ._ErrorDate = Today.Date, ._ErrorDescription = "The deletion of the scheduled job " & _JobNo & " has failed.", ._ErrorMethod = "Log Schedule Job Deletion", ._ErrorSource = ex.Source, ._ErrorMessage = ex.Message}
            With err
                .LogError(_Error)
            End With

        End Try

        Return Delete

    End Function

#End Region

End Class