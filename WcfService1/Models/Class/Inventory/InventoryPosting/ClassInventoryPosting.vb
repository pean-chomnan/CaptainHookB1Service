Public Class ClassInventoryPosting
    Public Class OIQR
        Public Property DocEntry As Integer
        Public Property DocNum As Integer
        Public Property Series As Integer
        Public Property PostingDate As Date
        Public Property CountDate As Date
        Public Property CountTime As DateTime
        Public Property PriceSouce As Integer
        Public Property PriceList As Integer
        'Public Property Status As String
        Public Property Ref2 As String
        Public Property Remark As String
        Public Property JournalRemark As String
        Public Property WebDocNum As Integer
        Public Property Lines As List(Of Line)
    End Class

    Public Class Line
        Public Property DocEntry As String
        Public Property LineNum As String
        Public Property ItemCode As String
        Public Property WhsCode As String
        Public Property BarCode As String
        Public Property BinCode As Integer
        Public Property CountedQuantity As Double
        Public Property VarianceQty As Double
        Public Property Price As Double
        Public Property Remark As String
        Public Property BaseEntry As String
        Public Property BaseLine As String
        Public Property BaseType As String
        Public Property ProjectCode As String
        Public Property CogsCode As String
        Public Property CogsCode2 As String
        Public Property CogsCode3 As String
        Public Property CogsCode4 As String
        Public Property CogsCode5 As String
        Public Property FirmCode As Integer
        Public Property SupplierCatalogNo As String
        Public Property CardCode As String
        Public Property NagativeBin As String
        Public Property UomCode As String
        Public Property ls_InventoryPostingLineUoMs As List(Of InventoryPostingLineUoMs)
        'Public Property ls_Batch As List(Of BatchNumbers)
        'Public Property ls_Serial As List(Of SerialNumbers)
    End Class

    Public Class InventoryPostingLineUoMs
        Public Property BarCode As String
        'Public Property UomEntry As Integer
        Public Property UomCode As String
        Public Property UomCountedQty As Double
        Public Property CountedQty As Double

    End Class

    'Public Class BatchNumbers
    '    Public Property Batch As String
    '    Public Property ManufacturerSerialNumber As String
    '    Public Property ManufacturingDate As Date
    '    Public Property Notes As String
    '    Public Property Location As String
    '    Public Property ReceptionDate As Date
    '    Public Property AvailableQty As String
    '    Public Property ExpirationDate As Date
    '    Public Property AdmissionDate As Date
    '    Public Property ACT_WeightOnBatch As Double
    '    Public Property CompanyAddress As String
    '    Public Property BarCodeBoxNumber As String
    '    Public Property Smoking As String
    '    Public Property Quantity As Double
    'End Class
    'Public Class SerialNumbers

    '    Public Property SerialNumber As String
    '    Public Property ManufacturerSerialNumber As String
    '    Public Property ExpirationDate As Date
    '    Public Property ManufactureDate As Date
    '    Public Property Note As String
    '    Public Property Location As String
    '    Public Property ReceptionDate As Date
    '    Public Property ACT_WeightOnBatch As Double
    '    Public Property CompanyAddress As String
    '    Public Property BarCodeBoxNumber As String
    '    Public Property Smoking As String
    '    'Public Property BinAbsEntry As Integer
    '    Public Property Quantity As Double

    'End Class

End Class
