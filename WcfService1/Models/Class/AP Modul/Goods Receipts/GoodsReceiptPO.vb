Public Class GoodsReceiptPO
    Public Class OPDN
        Public Property Series As Integer
        Public Property DocEntry As String
        Public Property DocNum As String
        Public Property DocType As String
        Public Property DocDate As Date
        Public Property DocDueDate As Date
        Public Property TaxDate As Date
        Public Property RequestByBranch As Integer
        Public Property CardCode As String
        Public Property ObjType As String
        Public Property WebDocNum As String
        Public Property PriceMode As String
        Public Property BaseEntry As String
        Public Property NumAtCard As String
        Public Property DiscountPercent As Double
        Public Property ContactPersonCode As Integer
        Public Property SalesPersonCode As Integer
        Public Property DocumentsOwner As Integer
        Public Property Comments As String

        Public Property Lines As List(Of Line)
    End Class
    Public Class Line
        Public Property DocEntry As String
        Public Property LineNum As String
        Public Property ItemCode As String
        Public Property ItemDescription As String
        Public Property BarCode As String
        Public Property Quantity As Double
        Public Property Price As Double
        Public Property GrossPrice As Double
        Public Property DiscPercent As Double
        Public Property VatGroup As String
        Public Property UomEntry As String
        Public Property WhsCode As String
        Public Property BaseType As String
        Public Property BaseEntry As String
        Public Property Baseline As String
        Public Property unitMsr As String
        Public Property NumPerMsr As String
        Public Property UomCode As String
        Public Property UseBaseUn As String
        Public Property CogsCode As String
        Public Property CogsCode2 As String
        Public Property CogsCode3 As String
        Public Property CogsCode4 As String
        Public Property CogsCode5 As String
        Public Property ls_Batch As List(Of BatchNumbers)
        Public Property ls_Serial As List(Of SerialNumbers)
        Public Property ls_LineBIN As List(Of BINCode)

        Public Property U_WeightTotal As Double

    End Class

    Public Class BatchNumbers
        Public Property Batch As String
        Public Property ManufacturerSerialNumber As String
        Public Property ManufacturingDate As Date
        Public Property Notes As String
        Public Property Location As String
        Public Property ReceptionDate As Date
        Public Property AvailableQty As String
        'Public Property Quantity As Integer
        Public Property ExpirationDate As Date
        Public Property AdmissionDate As Date
        Public Property ACT_WeightOnBatch As Double
        Public Property CompanyAddress As String
        Public Property BarCodeBoxNumber As String
        Public Property Smoking As String
        Public Property Quantity As Double
        Public Property ls_BatchBIN As List(Of BINCode)
    End Class

    Public Class BINCode
        Public Property BinAbsEntry As Integer
        Public Property BinQuantity As Double
    End Class

    Public Class SerialNumbers
        Public Property SerialNumber As String
        Public Property ManufacturerSerialNumber As String
        Public Property ExpirationDate As Date
        Public Property ManufactureDate As Date
        Public Property Note As String
        Public Property Location As String
        Public Property ReceptionDate As Date
        Public Property ACT_WeightOnBatch As Double
        Public Property CompanyAddress As String
        Public Property BarCodeBoxNumber As String
        Public Property Smoking As String
        Public Property BinAbsEntry As Integer
        Public Property Quantity As Double

    End Class

End Class
