

Imports System.Data.SqlClient

Namespace dsBITableAdapters

    Partial Class spGetMessageTypesTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetClientsTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetCRBRequestsTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetBusinessCRBDataTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetIndivCRBDataTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetDailyCRBUpdatesTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetCurrentPARReportTableAdapter
        Public Sub Extend()

            If Me.CommandCollection Is Nothing Then
                Me.InitCommandCollection()
            End If

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetDebtCollectionTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetDefaultChargesTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetDailyMobileLoansTableAdapter
        Public Sub Extend()

            If Me.CommandCollection Is Nothing Then
                Me.InitCommandCollection()
            End If

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetMobileLoansTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetMobileRequestsTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetPARAccountsMovementTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetPortfolioTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetProductsTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetPrudentialPARTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetSectorPARTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetAdvancesPARTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetPre2014PARTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetPost2014PARTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetPARMovementTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetSectorPortfolioTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetComprehensivePortfolioTableAdapter

        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

    Partial Class spGetSectorsTableAdapter
        Public Sub Extend()

            For Each cmd As SqlCommand In Me.CommandCollection
                cmd.CommandTimeout = 600
            Next

        End Sub

    End Class

End Namespace

Partial Class dsBI
End Class
