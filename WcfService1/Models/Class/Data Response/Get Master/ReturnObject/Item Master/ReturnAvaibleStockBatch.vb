Public Class ReturnAvaibleStockBatch
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of AvaibleStockBatch)
End Class

Public Class AvaibleStockBatch
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property Warehouse As String
    Public Property WarehouseName As String
    Public Property Batch As String
    Public Property Quantity As Double
    Public Property MnfSerial As String
    Public Property LotNumber As String
    Public Property ExpiredDate As Date
    Public Property MnfDate As Date
    Public Property Location As String
    Public Property Notes As String
    Public Property ACT_WeightOnBatch As Double
    Public Property CompanyAddress As String
    Public Property BarCodeBoxNumber As String
    Public Property SmokingSystem As String

End Class

Public Class CReturnAvaibleStockBatch
    Public Function FGetReturnAvaibleStockBatch(ByVal ItemCode As String, ByVal WhsCode As String, ByVal Batch As String) As ReturnAvaibleStockBatch
        Try
            Dim ls As New List(Of AvaibleStockBatch)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL """ & _DBNAME & """.USP_StockAvaibleBatch('" & ItemCode & "','" & WhsCode & "','" & Batch & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New AvaibleStockBatch With {
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("ItemName").Value.ToString.Trim,
                        .Warehouse = oRs.Fields.Item("WhsCode").Value.ToString.Trim,
                        .WarehouseName = oRs.Fields.Item("WhsName").Value,
                        .Batch = oRs.Fields.Item("BatchNum").Value,
                        .Quantity = oRs.Fields.Item("Quantity").Value,
                        .MnfSerial = oRs.Fields.Item("MnfSerial").Value,
                        .LotNumber = oRs.Fields.Item("LotNumber").Value,
                        .ExpiredDate = oRs.Fields.Item("ExpDate").Value,
                        .MnfDate = oRs.Fields.Item("MnfDate").Value,
                        .Location = oRs.Fields.Item("Location").Value,
                        .Notes = oRs.Fields.Item("Notes").Value,
                        .ACT_WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value,
                        .BarCodeBoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value,
                        .SmokingSystem = oRs.Fields.Item("U_SmokingSystem").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnAvaibleStockBatch With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnAvaibleStockBatch With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnAvaibleStockBatch With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class




