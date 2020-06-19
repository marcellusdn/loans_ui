
Imports MySql.Data.MySqlClient

Module mod_global

    Friend cmd As New MySqlCommand
    Friend cn As New My.MySettings

    Friend resp As String = Nothing
    Friend scriptstr As String = Nothing

    Friend err As New CustomError

    Friend Function GetDatabaseConnection() As MySqlConnection

        Dim str As String = "server=192.168.16.6;user id=marcos;allowzerodatetime=True;convertzerodatetime=True;connectiontimeout=600;database=webquickbi;port=3308;defaultcommandtimeout=600;sslmode=None;password=!@biquick12*Marcos#"
        Return New MySqlConnection(str)

    End Function

End Module
