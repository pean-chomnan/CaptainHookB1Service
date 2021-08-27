Public Class ReturnBatchNumber
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of BatchNumber)
End Class

Public Class BatchNumber
    Public Property BatchNo As String
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property InDate As Date
    Public Property MnfDate As Date
    Public Property ExpDate As Date
    Public Property Notes As String
    Public Property WeightOnBatch As Double
    Public Property CompanyAddress As String
    Public Property BoxNumber As String
    Public Property SmokingSystemType As String
    Public Property AvailableQty As Double

    Public Property BinCode As String = ""
    Public Property BinName As String = ""
    Public Property WhsCode As String = ""
    Public Property WhsName As String = ""
    Public Property ItemType As String = "Batch"





End Class

Public Class CReturnGetBatchNo
    Public Function GetBatchMasterByBox(BoxNumber As String) As ReturnBatchNumber
        Try
            Dim ls As New List(Of BatchNumber)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_GetBatchMasterByBox""('" & BoxNumber & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BatchNumber With {
                        .BatchNo = oRs.Fields.Item("DistNumber").Value.ToString.Trim,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("itemName").Value.ToString.Trim,
                        .InDate = oRs.Fields.Item("InDate").Value.ToString.Trim,
                        .MnfDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .ExpDate = oRs.Fields.Item("ExpDate").Value.ToString.Trim,
                        .Notes = oRs.Fields.Item("Notes").Value.ToString.Trim,
                        .WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value.ToString.Trim,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .BoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .SmokingSystemType = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .AvailableQty = 1 'Update procedure later
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBatchNumber With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBatchNumber With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBatchNumber With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
    Public Function GetBatchMaster(batchNum As String) As ReturnBatchNumber
        Try
            Dim ls As New List(Of BatchNumber)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_GetBatchMaster""('" & batchNum & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BatchNumber With {
                        .BatchNo = oRs.Fields.Item("DistNumber").Value.ToString.Trim,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("itemName").Value.ToString.Trim,
                        .InDate = oRs.Fields.Item("InDate").Value.ToString.Trim,
                        .MnfDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .ExpDate = oRs.Fields.Item("ExpDate").Value.ToString.Trim,
                        .Notes = oRs.Fields.Item("Notes").Value.ToString.Trim,
                        .WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value.ToString.Trim,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .BoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .SmokingSystemType = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .AvailableQty = 1 'Update procedure later
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBatchNumber With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBatchNumber With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBatchNumber With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function


    Public Function GetStockBatchMaster(ItemCode As String, WhsCode As String, batchNum As String) As ReturnBatchNumber
        Try
            Dim ls As New List(Of BatchNumber)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                ' strSql = "CALL " & _DBNAME & ".""USP_GetBatchMaster""('" & batchNum & "');"

                strSql = "CALL """ & _DBNAME & """.""USP_StockAvaibleBatch""('" & ItemCode & "','" & WhsCode & "','" & batchNum & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BatchNumber With {
                        .BatchNo = oRs.Fields.Item("BatchNum").Value.ToString.Trim,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("ItemName").Value.ToString.Trim,
                        .InDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .MnfDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .ExpDate = oRs.Fields.Item("ExpDate").Value.ToString.Trim,
                        .Notes = oRs.Fields.Item("Notes").Value.ToString.Trim,
                        .WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value.ToString.Trim,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .BoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .SmokingSystemType = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .AvailableQty = oRs.Fields.Item("Quantity").Value.ToString
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBatchNumber With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBatchNumber With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBatchNumber With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function

    Public Function GetStockBatchMaster(WhsCode As String, batchNum As String) As ReturnBatchNumber
        Try
            Dim ls As New List(Of BatchNumber)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                ' strSql = "CALL " & _DBNAME & ".""USP_GetBatchMaster""('" & batchNum & "');"

                strSql = "CALL """ & _DBNAME & """.""USP_StockAvaibleBatch_ByBatchWhsCode""('" & WhsCode & "','" & batchNum & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BatchNumber With {
                        .BatchNo = oRs.Fields.Item("BatchNum").Value.ToString.Trim,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("ItemName").Value.ToString.Trim,
                        .InDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .MnfDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .ExpDate = oRs.Fields.Item("ExpDate").Value.ToString.Trim,
                        .Notes = oRs.Fields.Item("Notes").Value.ToString.Trim,
                        .WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value.ToString.Trim,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .BoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .SmokingSystemType = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .AvailableQty = oRs.Fields.Item("Quantity").Value.ToString,
                        .BinCode = "",
                        .BinName = "",
                        .WhsCode = oRs.Fields.Item("WhsCode").Value.ToString,
                        .WhsName = oRs.Fields.Item("WhsName").Value.ToString
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBatchNumber With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBatchNumber With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBatchNumber With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function

    Public Function GetStockBatchMaster(batchNum As String) As ReturnBatchNumber
        Dim strSql As String = ""
        Try
            Dim ls As New List(Of BatchNumber)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing

            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                ' strSql = "CALL " & _DBNAME & ".""USP_GetBatchMaster""('" & batchNum & "');"

                strSql = "CALL """ & _DBNAME & """.""USP_StockAvaibleBatch_ByBatch""('" & batchNum & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BatchNumber With {
                        .BatchNo = oRs.Fields.Item("BatchNum").Value.ToString.Trim,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("ItemName").Value.ToString.Trim,
                        .InDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .MnfDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .ExpDate = oRs.Fields.Item("ExpDate").Value.ToString.Trim,
                        .Notes = oRs.Fields.Item("Notes").Value.ToString.Trim,
                        .WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value.ToString.Trim,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .BoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .SmokingSystemType = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .AvailableQty = oRs.Fields.Item("Quantity").Value.ToString,
                        .BinCode = "",
                        .BinName = "",
                        .WhsCode = oRs.Fields.Item("WhsCode").Value.ToString,
                        .WhsName = oRs.Fields.Item("WhsName").Value.ToString
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBatchNumber With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBatchNumber With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBatchNumber With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString() & "SQL: " & strsql,
                       .ls_data = Nothing
                   })
        End Try
    End Function

    Public Function FGetReturnBatchByBatchNumber(ByVal WhsCode As String, ByVal ItemCode As String, ByVal BatchNo As String) As ReturnBatchNumber
        Try
            Dim ls As New List(Of BatchNumber)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_GetBatchByBatch""('" & WhsCode & "','" & ItemCode & "','" & BatchNo & "');"
                    oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BatchNumber With {
                        .BatchNo = oRs.Fields.Item("DistNumber").Value.ToString.Trim,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("itemName").Value.ToString.Trim,
                        .InDate = oRs.Fields.Item("InDate").Value.ToString.Trim,
                        .MnfDate = oRs.Fields.Item("MnfDate").Value.ToString.Trim,
                        .ExpDate = oRs.Fields.Item("ExpiryDate").Value.ToString.Trim,
                        .Notes = oRs.Fields.Item("Notes").Value.ToString.Trim,
                        .WeightOnBatch = oRs.Fields.Item("U_ACT_WeightOnBatch").Value.ToString.Trim,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .BoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .SmokingSystemType = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .AvailableQty = oRs.Fields.Item("AvailableQty").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBatchNumber With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBatchNumber With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBatchNumber With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class







