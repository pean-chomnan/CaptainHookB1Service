Public Class CreateInventoryCounting
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of ClassInventoryCounting.OINC)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim returnstatus As New ReturnStatus
        Dim CountService As SAPbobsCOM.InventoryCountingsService
        Dim InvCount As SAPbobsCOM.InventoryCounting
        Dim InvCountLines As SAPbobsCOM.InventoryCountingLines
        Dim InvCountLine As SAPbobsCOM.InventoryCountingLine

        Dim InvCountSerial As SAPbobsCOM.IInventoryCountingSerialNumber = Nothing
        Dim InvCountBatch As SAPbobsCOM.InventoryCountingBatchNumber = Nothing
        Dim InventoryCountingLineUoMs As SAPbobsCOM.InventoryCountingLineUoM = Nothing

        Dim InvCountParam As SAPbobsCOM.InventoryCountingParams
        Dim companyService As SAPbobsCOM.CompanyService
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim ErrLine As New List(Of String)
        Dim sline As Boolean = False
        Dim myClasss As New myClassOfFuntion
        Dim ItemSetpBy As Integer
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim x As Integer = 0
        Dim dttime As New DateTime
        dttime = DateTime.Now

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                companyService = oCompany.GetCompanyService()
                CountService = companyService.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryCountingsService)
                InvCount = CountService.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCounting)
                For Each header In obj
                    ' If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OINC"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & header.WebDocNum, oCompany) = 0 Then
                    If True Then
                        InvCount.Series = header.Series
                        InvCount.CountDate = header.CountingDate
                        InvCount.CountTime = header.CountingTime
                        InvCount.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter
                        InvCount.SingleCounterType = SAPbobsCOM.CounterTypeEnum.ctUser
                        InvCount.Reference2 = header.Ref2
                        InvCount.Remarks = header.Comments
                        'InvCount.UserFields.Fields.Item("U_WebDocNum").Value = header.WebDocNum
                        'InvCount.UserFields.Item("U_WebDocNum").Value = header.WebDocNum
                        InvCountLines = InvCount.InventoryCountingLines
                        For Each Line In header.Lines
                            InvCountLine = InvCountLines.Add()
                            InvCountLine.ItemCode = Line.ItemCode
                            InvCountLine.BarCode = Line.BarCode
                            InvCountLine.CountedQuantity = Line.CountedQuantity
                            InvCountLine.UoMCode = Line.UomCode


                            If myClasss.ICaseString(Line.Freeze) = "Y" Then
                                InvCountLine.Freeze = SAPbobsCOM.BoYesNoEnum.tYES
                            ElseIf myClasss.ICaseString(Line.Freeze) = "N" Then
                                InvCountLine.Freeze = SAPbobsCOM.BoYesNoEnum.tNO
                            End If

                            InvCountLine.WarehouseCode = Line.WhsCode
                            InvCountLine.BinEntry = Line.BinCode

                            If myClasss.ICaseList(header.Lines(j).ls_InventoryCountingLineUoMs) <> 0 Then
                                For Each U In header.Lines(j).ls_InventoryCountingLineUoMs
                                    InventoryCountingLineUoMs = InvCountLine.InventoryCountingLineUoMs.Add()
                                    InventoryCountingLineUoMs.BarCode = U.BarCode
                                    InventoryCountingLineUoMs.UoMCode = U.UomCode
                                    InventoryCountingLineUoMs.UoMCountedQuantity = U.UomCountedQty
                                    InventoryCountingLineUoMs.CountedQuantity = U.CountedQty
                                Next
                            End If

                            InvCountLine.Counted = SAPbobsCOM.BoYesNoEnum.tYES
                            InvCountLine.CostingCode = Line.CogsCode
                            InvCountLine.CostingCode2 = Line.CogsCode2
                            InvCountLine.CostingCode3 = Line.CogsCode3
                            InvCountLine.CostingCode4 = Line.CogsCode4
                            InvCountLine.CostingCode5 = Line.CogsCode5

                            ItemSetpBy = myClasss.ItemSetupBy(Line.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0
                                'InvCountSerials = InvCountLine.InventoryCountingSerialNumbers.Add

                                For Each B In header.Lines(j).ls_Serial
                                    If B.SerialNumber <> Nothing And (B.ExpirationDate <> Nothing Or B.Location <> Nothing Or B.ManufactureDate <> Nothing Or B.ManufacturerSerialNumber <> Nothing Or B.Note <> Nothing Or B.ReceptionDate <> Nothing) Then
                                        If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                            InvCountSerial = InvCountLine.InventoryCountingSerialNumbers.Add
                                            InvCountSerial.InternalSerialNumber = B.SerialNumber
                                            InvCountSerial.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                            InvCountSerial.ExpiryDate = B.ExpirationDate
                                            InvCountSerial.ManufactureDate = B.ManufactureDate
                                            InvCountSerial.Notes = B.Note
                                            InvCountSerial.Location = B.Location
                                            InvCountSerial.ReceptionDate = B.ReceptionDate
                                        End If
                                    Else
                                        If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                            InvCountSerial = InvCountLine.InventoryCountingSerialNumbers.Add
                                            InvCountSerial.InternalSerialNumber = B.SerialNumber
                                        End If
                                    End If
                                    k = k + 1
                                Next
                            ElseIf ItemSetpBy = 2 Then
                                Dim k As Integer = 0
                                For Each B In obj(i).Lines(j).ls_Batch
                                    If B.Batch <> Nothing And (B.ExpirationDate <> Nothing Or B.Location <> Nothing Or B.ManufacturerSerialNumber <> Nothing Or B.Notes <> Nothing Or B.ReceptionDate <> Nothing) Then
                                        InvCountBatch = InvCountLine.InventoryCountingBatchNumbers.Add
                                        InvCountBatch.BatchNumber = B.Batch
                                        InvCountBatch.Quantity = B.Quantity
                                        InvCountBatch.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                        InvCountBatch.ManufactureDate = B.ManufacturingDate
                                        InvCountBatch.Notes = B.Notes
                                        InvCountBatch.Location = B.Location
                                        InvCountBatch.AddmisionDate = B.AdmissionDate
                                        InvCountBatch.ExpiryDate = B.ExpirationDate
                                    Else
                                        If (B.Batch <> "" Or B.Batch <> Nothing) And (B.Quantity <> Nothing Or B.Quantity <> 0) Then
                                            InvCountBatch = InvCountLine.InventoryCountingBatchNumbers.Add
                                            InvCountBatch.BatchNumber = B.Batch
                                            InvCountBatch.Quantity = B.Quantity
                                        End If
                                    End If
                                    k = k + 1
                                Next

                            End If
                            j = j + 1
                        Next
                        InvCountParam = CountService.Add(InvCount)
                        oCompany.GetLastError(_lErrCode, _sErrMsg)
                        If _lErrCode <> 0 Then
                            oCompany.GetLastError(_lErrCode, _sErrMsg)
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = _sErrMsg,
                                .ErrorCode = _lErrCode,
                                .SAPDocNum = InvCountParam.DocumentNumber,
                                .WEBDocNum = header.WebDocNum,
                                .DocEntry = InvCountParam.DocumentEntry
                            }
                            ls_returnstatus.Add(returnstatus)
                            Dim xx As Integer = 0
                        Else
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = "Add Successfully",
                                .ErrorCode = 0,
                                .SAPDocNum = InvCountParam.DocumentNumber,
                                .WEBDocNum = header.WebDocNum,
                                .DocEntry = InvCountParam.DocumentEntry
                            }
                            ls_returnstatus.Add(returnstatus)
                        End If
                    Else
                        returnstatus = New ReturnStatus With {
                                .ErrirMsg = "Duplicate WebDocNum : " & header.WebDocNum,
                                .ErrorCode = 1,
                                .WEBDocNum = header.WebDocNum,
                                .DocEntry = "",
                                .SAPDocNum = ""
                            }
                        ls_returnstatus.Add(returnstatus)
                    End If
                    i = i + 1
                Next
            Else
                'Get Error
                returnstatus = New ReturnStatus With {
                    .ErrirMsg = oLoginService.sErrMsg,
                    .ErrorCode = oLoginService.lErrCode,
                    .SAPDocNum = "",
                    .WEBDocNum = "",
                    .DocEntry = ""
                }
                ls_returnstatus.Add(returnstatus)
            End If
        Catch ex As Exception
            returnstatus = New ReturnStatus With {
                    .ErrirMsg = ex.Message,
                    .ErrorCode = ex.GetHashCode,
                    .SAPDocNum = "",
                    .WEBDocNum = "",
                    .DocEntry = ""
            }
            ls_returnstatus.Add(returnstatus)
        End Try
        Return ls_returnstatus
    End Function

End Class
