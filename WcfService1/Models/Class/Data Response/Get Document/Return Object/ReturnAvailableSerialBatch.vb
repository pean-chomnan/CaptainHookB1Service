Public Class ReturnAvailableSerialBatch
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of AvailableSerialBatch)
End Class

Public Class AvailableSerialBatch
    Public Property ItemCode As String
    Public Property DistNumber As String
    Public Property BatchAttribute1 As String
    Public Property AvailableQty As Double
    Public Property ExpiryDate As Date
    Public Property AllocatedQty As Double
    Public Property CountQty As Double
    Public Property SysNumber As Double
    Public Property BinCode As String
    Public Property U_ACT_WeightOnBatch As Double
    Public Property U_CompanyAddress As String
    Public Property U_BarCodeBoxNumber As String
    Public Property U_SmokingSystem As String
    Public Property Status As String
End Class

Public Class CReturnGetAvailableSerialBatch
    Public Function FGetReturnAvailableSerialBatch(ByVal ItemCode As String, ByVal WarehouseCode As String) As ReturnAvailableSerialBatch
        Try
            Dim ls As New List(Of AvailableSerialBatch)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_GetAvaibleSerialBatch""('" & ItemCode & "','" & WarehouseCode & "')"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New AvailableSerialBatch With {
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .DistNumber = oRs.Fields.Item("DistNumber").Value.ToString.Trim,
                        .BatchAttribute1 = oRs.Fields.Item("BatchAttribute1").Value.ToString.Trim,
                        .AvailableQty = oRs.Fields.Item("AvailableQty").Value,
                        .ExpiryDate = oRs.Fields.Item("ExpiryDate").Value,
                        .AllocatedQty = oRs.Fields.Item("AllocatedQty").Value,
                        .CountQty = oRs.Fields.Item("CountQty").Value,
                        .SysNumber = oRs.Fields.Item("SysNumber").Value,
                        .BinCode = oRs.Fields.Item("BinCode").Value.ToString.Trim,
                        .U_ACT_WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value,
                        .U_CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .U_BarCodeBoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .U_SmokingSystem = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .Status = oRs.Fields.Item("Status").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnAvailableSerialBatch With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnAvailableSerialBatch With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnAvailableSerialBatch With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class







