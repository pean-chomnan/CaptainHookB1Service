Imports SAPbobsCOM

Public Class CreateInventoryPosting
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of ClassInventoryPosting.OIQR)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim returnstatus As New ReturnStatus
        Dim PostService As SAPbobsCOM.InventoryPostingsService
        Dim InvPost As SAPbobsCOM.InventoryPosting
        Dim InvPostLines As SAPbobsCOM.InventoryPostingLines
        Dim InvPostLine As SAPbobsCOM.InventoryPostingLine

        Dim InvPostSerial As SAPbobsCOM.InventoryPostingSerialNumber = Nothing
        Dim InvPostBatch As SAPbobsCOM.InventoryPostingBatchNumber = Nothing
        Dim InventoryPostingLineUoMs As SAPbobsCOM.InventoryPostingLineUoM = Nothing

        Dim InvPostParam As SAPbobsCOM.InventoryPostingParams
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
                PostService = companyService.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryPostingsService)   'InventoryCountingsService
                InvPost = PostService.GetDataInterface(SAPbobsCOM.InventoryPostingsServiceDataInterfaces.ipsInventoryPosting)
                For Each header In obj
                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OIQR"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & header.WebDocNum, oCompany) = 0 Then
                        InvPost.Series = header.Series
                        InvPost.PostingDate = header.PostingDate
                        InvPost.CountDate = header.CountDate
                        InvPost.CountTime = header.CountTime

                        If header.PriceSouce = 1 Then
                            InvPost.PriceSource = InventoryPostingPriceSourceEnum.ippsByPriceList
                            InvPost.PriceList = header.PriceList
                        ElseIf header.PriceSouce = 2 Then
                            InvPost.PriceSource = InventoryPostingPriceSourceEnum.ippsLastEvaluatedPrice
                        ElseIf header.PriceSouce = 3 Then
                            InvPost.PriceSource = InventoryPostingPriceSourceEnum.ippsItemCost
                        End If

                        InvPost.Reference2 = header.Ref2
                        InvPost.Remarks = header.Remark
                        InvPost.JournalRemark = header.JournalRemark
                        InvPost.UserFields.Item("U_WebDocNum").Value = header.WebDocNum
                        InvPostLines = InvPost.InventoryPostingLines

                        For Each Line In header.Lines
                            InvPostLine = InvPostLines.Add()
                            InvPostLine.ItemCode = Line.ItemCode
                            InvPostLine.BarCode = Line.BarCode
                            InvPostLine.WarehouseCode = Line.WhsCode
                            InvPostLine.BinEntry = Line.BinCode

                            If myClasss.ICaseNumber(Line.CountedQuantity) <> 0 Then
                                InvPostLine.CountedQuantity = Line.CountedQuantity
                            End If

                            If myClasss.ICaseNumber(Line.VarianceQty) <> 0 Then
                                InvPostLine.Variance = Line.VarianceQty
                            End If

                            InvPostLine.Price = Line.Price
                            InvPostLine.Remarks = Line.Remark

                            ' If Copied from Inventory Counting
                            If Line.BaseEntry <> Nothing And Line.BaseLine <> Nothing And Line.BaseType <> Nothing Then
                                InvPostLine.BaseEntry = Line.BaseEntry
                                InvPostLine.BaseLine = Line.BaseLine
                                InvPostLine.BaseType = Line.BaseType
                            End If

                            If myClasss.ICaseString(Line.ProjectCode) <> "" Then
                                InvPostLine.ProjectCode = Line.ProjectCode
                            End If

                            If myClasss.ICaseNumber(Line.FirmCode) <> 0 Then
                                InvPostLine.Manufacturer = Line.FirmCode
                            End If

                            InvPostLine.SupplierCatalogNo = Line.SupplierCatalogNo
                            InvPostLine.PreferredVendor = Line.CardCode

                            If myClasss.ICaseString(Line.NagativeBin) <> "" Then
                                If Line.NagativeBin = "Y" Then
                                    InvPostLine.AllowBinNegativeQuantity = BoYesNoEnum.tYES
                                End If
                            End If

                            If myClasss.ICaseString(Line.UomCode) <> "" Then
                                InvPostLine.UoMCode = Line.UomCode
                            End If

                            If myClasss.ICaseListInventoryPosting(header.Lines(j).ls_InventoryPostingLineUoMs) <> 0 Then
                                For Each U In header.Lines(j).ls_InventoryPostingLineUoMs
                                    InventoryPostingLineUoMs = InvPostLine.InventoryPostingLineUoMs.Add()
                                    InventoryPostingLineUoMs.BarCode = U.BarCode
                                    InventoryPostingLineUoMs.UoMCode = U.UomCode
                                    InventoryPostingLineUoMs.UoMCountedQuantity = U.UomCountedQty
                                    InventoryPostingLineUoMs.CountedQuantity = U.CountedQty
                                Next
                            End If

                            InvPostLine.CostingCode = Line.CogsCode
                            InvPostLine.CostingCode2 = Line.CogsCode2
                            InvPostLine.CostingCode3 = Line.CogsCode3
                            InvPostLine.CostingCode4 = Line.CogsCode4
                            InvPostLine.CostingCode5 = Line.CogsCode5

                            ItemSetpBy = myClasss.ItemSetupBy(Line.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0
                                For Each B In header.Lines(j).ls_Serial
                                    If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                        If B.Quantity < 0 Then
                                            InvPostSerial = InvPostLine.InventoryPostingSerialNumbers.Add
                                            InvPostSerial.InternalSerialNumber = B.SerialNumber
                                        Else
                                            InvPostSerial = InvPostLine.InventoryPostingSerialNumbers.Add
                                            InvPostSerial.InternalSerialNumber = B.SerialNumber
                                            InvPostSerial.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                            InvPostSerial.ExpiryDate = B.ExpirationDate
                                            InvPostSerial.ManufactureDate = B.ManufactureDate
                                            InvPostSerial.Notes = B.Note
                                            InvPostSerial.Location = B.Location
                                            InvPostSerial.ReceptionDate = B.ReceptionDate

                                            'InvPostSerial.UserFields.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                            'InvPostSerial.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                            'InvPostSerial.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                            'InvPostSerial.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking

                                        End If

                                    End If
                                    k = k + 1
                                Next
                            ElseIf ItemSetpBy = 2 Then
                                Dim k As Integer = 0
                                For Each B In obj(i).Lines(j).ls_Batch
                                    If (B.Batch <> "" Or B.Batch <> Nothing) And (B.Quantity <> Nothing Or B.Quantity <> 0) Then
                                        If B.Quantity < 0 Then
                                            InvPostBatch = InvPostLine.InventoryPostingBatchNumbers.Add
                                            InvPostBatch.BatchNumber = B.Batch
                                            InvPostBatch.Quantity = B.Quantity
                                        Else
                                            InvPostBatch = InvPostLine.InventoryPostingBatchNumbers.Add
                                            InvPostBatch.BatchNumber = B.Batch
                                            InvPostBatch.Quantity = B.Quantity

                                            InvPostBatch.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                            InvPostBatch.ManufactureDate = B.ManufacturingDate
                                            InvPostBatch.Notes = B.Notes
                                            InvPostBatch.Location = B.Location
                                            InvPostBatch.AddmisionDate = B.AdmissionDate
                                            InvPostBatch.ExpiryDate = B.ExpirationDate

                                            'InvPostBatch.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                            'InvPostBatch.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                            'InvPostBatch.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                            'InvPostBatch.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking
                                        End If

                                    End If
                                    k = k + 1
                                Next

                            End If
                            j = j + 1
                        Next
                        InvPostParam = PostService.Add(InvPost)
                        oCompany.GetLastError(_lErrCode, _sErrMsg)
                        If _lErrCode <> 0 Then
                            oCompany.GetLastError(_lErrCode, _sErrMsg)
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = _sErrMsg,
                                .ErrorCode = _lErrCode,
                                .SAPDocNum = InvPostParam.DocumentNumber,
                                .WEBDocNum = header.WebDocNum,
                                .DocEntry = InvPostParam.DocumentEntry
                            }
                            ls_returnstatus.Add(returnstatus)
                        Else
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = "Add Successfully",
                                .ErrorCode = 0,
                                .SAPDocNum = InvPostParam.DocumentNumber,
                                .WEBDocNum = header.WebDocNum,
                                .DocEntry = InvPostParam.DocumentEntry
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
