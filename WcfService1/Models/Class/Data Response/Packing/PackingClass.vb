Public Class PackingClass
    Public Class ClassPackingResponse
        Public Property ErrCode As Integer
        Public Property ErrMsg As String
        Public Property Obj As List(Of ClassPacking)
    End Class
    Public Class ClassPacking
        Public Property ItemCode As String
        Public Property DistNumber As String
        Public Property MnfSerial As String
        Public Property LotNumber As String
        Public Property ExpiredDate As Date
        Public Property WeightOnBatch As Double
        Public Property CompanyAddress As String
        Public Property BarCodeBoxNumber As String
        Public Property SmokingSystem As String
        Public Property Type As String
    End Class
End Class

