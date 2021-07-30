Public Class CreateInventoryTransfer
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal listOfInventoryTransfer As List(Of ClassInventoryTransfer.OWTR)) As List(Of ReturnStatus)

        '  Dim Utilities As New UtilitiesFunction
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim myClasss As New myClassOfFuntion
        Dim returnstatus As ReturnStatus
        Dim B1InventoryTransfer As SAPbobsCOM.StockTransfer
        Dim RetVal As Integer
        Dim listOfErrLine As List(Of String)

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                B1InventoryTransfer = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer)
                Dim iIndex_Header As Integer = 0
                Do While iIndex_Header < listOfInventoryTransfer.Count
                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OWTR"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & listOfInventoryTransfer(iIndex_Header).WebDocNum, oCompany) = 0 Then

                        B1InventoryTransfer.Series = listOfInventoryTransfer(iIndex_Header).Series
                        B1InventoryTransfer.DocDate = listOfInventoryTransfer(iIndex_Header).DocDate
                        B1InventoryTransfer.TaxDate = listOfInventoryTransfer(iIndex_Header).TaxDate

                        B1InventoryTransfer.FromWarehouse = listOfInventoryTransfer(iIndex_Header).FromWhs
                        B1InventoryTransfer.ToWarehouse = listOfInventoryTransfer(iIndex_Header).ToWhs

                        If listOfInventoryTransfer(iIndex_Header).SaleEmployee <> "" Then
                            B1InventoryTransfer.SalesPersonCode = listOfInventoryTransfer(iIndex_Header).SaleEmployee
                        End If

                        B1InventoryTransfer.Comments = listOfInventoryTransfer(iIndex_Header).Comments

                        If listOfInventoryTransfer(iIndex_Header).JournalRemark <> "" Then
                            B1InventoryTransfer.JournalMemo = listOfInventoryTransfer(iIndex_Header).JournalRemark
                        End If

                        B1InventoryTransfer.UserFields.Fields.Item("U_WebDocNum").Value = listOfInventoryTransfer(iIndex_Header).WebDocNum

                        Dim iIndex_Line As Integer = 0
                        listOfErrLine = New List(Of String)
                        For Each inventoryTransferLine In listOfInventoryTransfer(iIndex_Header).Lines
                            B1InventoryTransfer.Lines.ItemCode = inventoryTransferLine.ItemCode
                            B1InventoryTransfer.Lines.Quantity = inventoryTransferLine.Quantity

                            B1InventoryTransfer.Lines.FromWarehouseCode = inventoryTransferLine.FromWhs
                            B1InventoryTransfer.Lines.WarehouseCode = inventoryTransferLine.ToWhs

                            If inventoryTransferLine.CogsCode <> "" Then
                                B1InventoryTransfer.Lines.DistributionRule = inventoryTransferLine.CogsCode
                            End If

                            If inventoryTransferLine.CogsCode2 <> "" Then
                                B1InventoryTransfer.Lines.DistributionRule2 = inventoryTransferLine.CogsCode2
                            End If

                            If inventoryTransferLine.CogsCode3 <> "" Then
                                B1InventoryTransfer.Lines.DistributionRule3 = inventoryTransferLine.CogsCode3
                            End If

                            If inventoryTransferLine.CogsCode4 <> "" Then
                                B1InventoryTransfer.Lines.DistributionRule4 = inventoryTransferLine.CogsCode4
                            End If

                            If inventoryTransferLine.CogsCode5 <> "" Then
                                B1InventoryTransfer.Lines.DistributionRule5 = inventoryTransferLine.CogsCode5
                            End If

                            If inventoryTransferLine.ListOfSerial IsNot Nothing Then 'Add Serial
                                For Each oSerial In inventoryTransferLine.ListOfSerial
                                    B1InventoryTransfer.Lines.SerialNumbers.InternalSerialNumber = oSerial.SerialNumber
                                    B1InventoryTransfer.Lines.SerialNumbers.Add()
                                Next
                            ElseIf inventoryTransferLine.ListOfBatch IsNot Nothing Then 'Add Batch
                                For Each oBatch In inventoryTransferLine.ListOfBatch
                                    B1InventoryTransfer.Lines.BatchNumbers.BatchNumber = oBatch.Batch
                                    B1InventoryTransfer.Lines.BatchNumbers.Quantity = oBatch.Quantity
                                    B1InventoryTransfer.Lines.BatchNumbers.Add()

                                    'Add ToBinLocation 
                                    If oBatch.ListOfToBinLocation IsNot Nothing Then
                                        Dim iIndex_Batch As Integer
                                        For Each oToBin As ClassInventoryTransfer.BinLocation In oBatch.ListOfToBinLocation
                                            B1InventoryTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
                                            B1InventoryTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = iIndex_Batch
                                            B1InventoryTransfer.Lines.BinAllocations.Quantity = oToBin.Quantity
                                            B1InventoryTransfer.Lines.BinAllocations.BinAbsEntry = oToBin.BinEntry
                                            B1InventoryTransfer.Lines.BinAllocations.Add()
                                            iIndex_Batch += 1
                                        Next
                                    End If
                                Next
                            Else
                                'Add ToBinLocation
                                If inventoryTransferLine.ListOfToBinLocation IsNot Nothing Then
                                    For Each oToBin As ClassInventoryTransfer.BinLocation In inventoryTransferLine.ListOfToBinLocation
                                        B1InventoryTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse
                                        B1InventoryTransfer.Lines.BinAllocations.Quantity = oToBin.Quantity
                                        B1InventoryTransfer.Lines.BinAllocations.BinAbsEntry = oToBin.BinEntry
                                        B1InventoryTransfer.Lines.BinAllocations.Add()
                                    Next
                                End If
                            End If

                            'Handle ItemCode
                            If myClasss.Has("ItemCode", inventoryTransferLine.ItemCode, "OITM") = False Then
                                listOfErrLine.Add("Line " & iIndex_Line & ". Item Code: " & inventoryTransferLine.ItemCode & " don't have!")
                            End If
                            B1InventoryTransfer.Lines.Add()
                            iIndex_Line = iIndex_Line + 1
                        Next


                        If listOfErrLine.Count > 0 Then
                            returnstatus = New ReturnStatus With {
                                   .ErrirMsg = "WebDocNum: " & listOfInventoryTransfer(iIndex_Header).WebDocNum & "; Error Line ",
                                   .ErrorCode = 9999,
                                   .SAPDocNum = "",
                                   .DocEntry = "",
                                   .ErrLine = listOfErrLine.ToList()
                                }
                            ls_returnstatus.Add(returnstatus)
                        Else
                            RetVal = B1InventoryTransfer.Add
                            If RetVal <> 0 Then
                                oCompany.GetLastError(_lErrCode, _sErrMsg)
                                returnstatus = New ReturnStatus With {
                                            .ErrirMsg = _sErrMsg,
                                            .ErrorCode = _lErrCode,
                                            .DocEntry = "",
                                            .SAPDocNum = ""
                                        }
                                ls_returnstatus.Add(returnstatus)
                            Else
                                'Write successfully 
                                returnstatus = New ReturnStatus With {
                                             .ErrirMsg = "",
                                             .ErrorCode = 0,
                                             .SAPDocNum = myClasss.Get_DocNum(oCompany.GetNewObjectKey(), "OWTR"),
                                             .DocEntry = oCompany.GetNewObjectKey()
                                        }
                                ls_returnstatus.Add(returnstatus)

                            End If
                        End If
                    Else
                        returnstatus = New ReturnStatus With {
                            .ErrirMsg = "Duplicate WebDocNum : " & listOfInventoryTransfer(iIndex_Header).WebDocNum,
                            .ErrorCode = 9999,
                            .DocEntry = "",
                            .SAPDocNum = ""
                        }
                        ' .RefDocNum = obj(i).RefDocNum,
                        ls_returnstatus.Add(returnstatus)
                    End If
                    iIndex_Header = iIndex_Header + 1
                Loop
            Else
                ' Login Error
                returnstatus = New ReturnStatus With {
                    .ErrirMsg = oLoginService.sErrMsg,
                    .ErrorCode = oLoginService.lErrCode,
                    .SAPDocNum = "",
                    .DocEntry = ""
                }
                ls_returnstatus.Add(returnstatus)
            End If
        Catch ex As Exception
            returnstatus = New ReturnStatus With {
                .ErrirMsg = ex.Message,
                .ErrorCode = ex.HResult,
                .SAPDocNum = "",
                .DocEntry = ""
            }
            ls_returnstatus.Add(returnstatus)
        End Try
        Return ls_returnstatus
    End Function

End Class
