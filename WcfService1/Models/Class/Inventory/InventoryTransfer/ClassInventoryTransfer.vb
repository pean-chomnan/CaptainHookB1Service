Public Class ClassInventoryTransfer
    Public Class OWTR
        Public Property DocEntry As Integer
        Public Property DocNum As Integer
        Public Property Series As Integer
        Public Property DocDate As Date
        Public Property TaxDate As Date

        Public Property CardCode As String
        Public Property CardName As String
        Public Property ContactPersonCode As Integer
        Public Property ShipToCode As String
        Public Property Address As String
        Public Property PriceListNum As Integer
        Public Property FromWhs As String
        Public Property ToWhs As String
        Public Property ToBinCode As String
        Public Property SaleEmployee As String
        Public Property WebDocNum As Integer
        Public Property Comments As String
        Public Property JournalRemark As String
        Public Property Lines As List(Of Line)
    End Class

    Public Class Line
        Public Property DocEntry As String
        Public Property LineNum As String
        Public Property ItemCode As String
        Public Property ItemDescription As String
        Public Property BarCode As String
        Public Property FromWhs As String
        '  Public Property FromBinCode As String
        Public Property ToWhs As String
        ' Public Property ToBinCode
        Public Property Quantity As Double
        Public Property Price As Double
        Public Property GrossPrice As Double
        Public Property DiscPercent As Double
        ' Public Property TaxCode As String
        Public Property Rate As Double
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

    End Class

End Class
