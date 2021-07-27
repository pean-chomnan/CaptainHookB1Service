Public Class ClassInventoryCounting
    Public Class OINC
        Public Property DocEntry As Integer
        Public Property DocNum As Integer
        Public Property Series As Integer
        Public Property CountingDate As Date
        Public Property CountingTime As DateTime
        Public Property CounterType As Integer
        Public Property InvCounter1 As Integer
        Public Property InvCounter2 As Integer
        Public Property Status As String
        Public Property Ref2 As String
        Public Property WebDocNum As Integer
        Public Property Comments As String
        Public Property Lines As List(Of Line)
    End Class

    Public Class Line
        Public Property DocEntry As String
        Public Property LineNum As String
        Public Property ItemCode As String
        Public Property ItemDescription As String
        Public Property Freeze As String
        Public Property BarCode As String
        Public Property CountedQuantity As Double
        Public Property ls_InventoryCountingLineUoMs As List(Of InventoryCountingLineUoMs)
        Public Property WhsCode As String
        Public Property BinCode As Integer
        Public Property Counted As String
        Public Property CogsCode As String
        Public Property CogsCode2 As String
        Public Property CogsCode3 As String
        Public Property CogsCode4 As String
        Public Property CogsCode5 As String
        Public Property UomCode As String
        Public Property ls_Batch As List(Of BatchNumbers)
        Public Property ls_Serial As List(Of SerialNumbers)
    End Class

    Public Class InventoryCountingLineUoMs
        Public Property BarCode As String
        'Public Property UomEntry As Integer
        Public Property UomCode As String
        Public Property UomCountedQty As Double
        Public Property CountedQty As Double

    End Class

    Public Class BatchNumbers
        Public Property Batch As String
        Public Property Quantity As Double
    End Class
    Public Class SerialNumbers
        Public Property SerialNumber As String
    End Class
End Class
