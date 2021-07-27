Public Class ClassIssueProduction
    Public Class OIGE
        Public Property DocNum As Integer
        Public Property Series As Integer
        Public Property DocDate As Date
        Public Property TaxDate As Date
        Public Property Ref2 As String
        Public Property Comments As String
        Public Property JournalRemark As String
        Public Property WebDocNum As Integer
        Public Property Lines As List(Of Line)
    End Class

    Public Class Line
        Public Property BaseEntry As String
        Public Property BaseLine As String
        'Public Property Series As Integer
        'Public Property LineNum As Integer
        'Public Property Type As Integer
        'Test Change   kk
        Public Property ItemCode As String
        Public Property BarCode As String
        Public Property VendorNum As String
        Public Property Quantity As Double
        Public Property DiscountPercent As Double
        Public Property Price As Double
        Public Property Warehouse As String

        Public Property CogsCode As String
        Public Property CogsCode2 As String
        Public Property CogsCode3 As String
        Public Property CogsCode4 As String
        Public Property CogsCode5 As String

        Public Property ls_Batch As List(Of BatchNumbers)
        Public Property ls_Serial As List(Of SerialNumbers)

    End Class

    Public Class BatchNumbers
        Public Property Batch As String
        Public Property Quantity As Double

    End Class
    Public Class SerialNumbers

        Public Property SerialNumber As String
        Public Property Quantity As Double

    End Class
End Class

