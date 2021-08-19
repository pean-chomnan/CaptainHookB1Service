Public Class CreateGoodsReceiptPO
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of GoodsReceiptPO.OPDN)) As List(Of ReturnStatus)

        '  Dim Utilities As New UtilitiesFunction
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim myClasss As New myClassOfFuntion
        Dim returnstatus As ReturnStatus
        Dim GPO As SAPbobsCOM.Documents = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim ErrLine As New List(Of String)
        Dim sline As Boolean = False
        Dim BaseOnPO As Boolean = False
        'Dim batch As Boolean = False
        Dim Manag As String = ""
        Dim ItemSetpBy As Integer

        'Item Master For Update BarCode

        Dim CreatItem As New CreatItem
        Dim OITM As New List(Of ItemMasterData)
        Dim ITM As New ItemMasterData ' List(Of ocrd)
        Dim ls_result_BarCode As List(Of ReturnStatus) = Nothing
        Dim CB As New List(Of ItemMasterData.CodeBars)
        Dim BS As New ItemMasterData.CodeBars

        ITM = New ItemMasterData

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                GPO = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes)

                Dim i As Integer = 0
                Dim x As Integer = 0

                Do While i < obj.Count
                    'If myClasss.Has("U_WebDocNum", obj(i).WebDocNum, "OPDN") = False Then
                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OPDN"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & obj(i).WebDocNum, oCompany) = 0 Then
                        GPO.Series = obj(i).Series
                        GPO.CardCode = obj(i).CardCode
                        GPO.DocDate = obj(i).DocDate
                        GPO.DocDueDate = obj(i).DocDueDate
                        GPO.DocDueDate = obj(i).TaxDate
                        GPO.BPL_IDAssignedToInvoice = obj(i).RequestByBranch
                        GPO.DiscountPercent = obj(i).DiscountPercent

                        If myClasss.ICaseNumber(obj(i).ContactPersonCode) > 0 Then
                            GPO.ContactPersonCode = obj(i).ContactPersonCode
                        End If

                        If myClasss.ICaseNumber(obj(i).SalesPersonCode) > 0 Then
                            GPO.SalesPersonCode = obj(i).SalesPersonCode
                        End If
                        If myClasss.ICaseNumber(obj(i).DocumentsOwner) > 0 Then
                            GPO.DocumentsOwner = obj(i).DocumentsOwner
                        End If


                        GPO.NumAtCard = obj(i).NumAtCard
                        GPO.Comments = obj(i).Comments

                        'GPO.BP
                        'GPO.Series = obj(i).SeriesID
                        GPO.UserFields.Fields.Item("U_WebDocNum").Value = obj(i).WebDocNum
                        GPO.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items
                        If obj(i).PriceMode = "N" Then
                            GPO.PriceMode = SAPbobsCOM.PriceModeDocumentEnum.pmdNet
                        ElseIf obj(i).PriceMode = "G" Then
                            GPO.PriceMode = SAPbobsCOM.PriceModeDocumentEnum.pmdGross
                        Else
                            GPO.PriceMode = SAPbobsCOM.PriceModeDocumentEnum.pmdNetAndGross
                        End If
                        Dim j As Integer = 0
                        For Each L In obj(i).Lines
                            GPO.Lines.ItemCode = L.ItemCode
                            GPO.Lines.BarCode = L.BarCode
                            GPO.Lines.Quantity = L.Quantity
                            GPO.Lines.UnitPrice = L.Price
                            GPO.Lines.GrossPrice = L.GrossPrice
                            GPO.Lines.DiscountPercent = L.DiscPercent
                            GPO.Lines.VatGroup = L.VatGroup
                            GPO.Lines.UoMEntry = L.UomEntry
                            GPO.Lines.WarehouseCode = L.WhsCode

                            GPO.Lines.COGSCostingCode = L.CogsCode ' Distribution Rul 1 to 5
                            GPO.Lines.COGSCostingCode2 = L.CogsCode2
                            GPO.Lines.COGSCostingCode3 = L.CogsCode3
                            GPO.Lines.COGSCostingCode4 = L.CogsCode4
                            GPO.Lines.COGSCostingCode5 = L.CogsCode5

                            If L.U_WeightTotal <> 0 And L.U_WeightTotal.ToString <> "" Then
                                GPO.Lines.UserFields.Fields.Item("U_WeightTotal").Value = L.U_WeightTotal
                            End If


                            ItemSetpBy = myClasss.ItemSetupBy(L.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0
                                '     Dim x As Integer = 0

                                For Each B In obj(i).Lines(j).ls_Serial
                                    If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then

                                        GPO.Lines.SerialNumbers.InternalSerialNumber = B.SerialNumber
                                        GPO.Lines.SerialNumbers.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                        GPO.Lines.SerialNumbers.ExpiryDate = B.ExpirationDate
                                        GPO.Lines.SerialNumbers.ManufactureDate = B.ManufactureDate
                                        GPO.Lines.SerialNumbers.Notes = B.Note
                                        GPO.Lines.SerialNumbers.Location = B.Location
                                        GPO.Lines.SerialNumbers.ReceptionDate = B.ReceptionDate

                                        GPO.Lines.SerialNumbers.UserFields.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                        GPO.Lines.SerialNumbers.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                        GPO.Lines.SerialNumbers.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                        GPO.Lines.SerialNumbers.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking
                                        GPO.Lines.SerialNumbers.Add()

                                        ' GPO.Lines.SerialNumbers.

                                        If myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""WhsCode""='" & L.WhsCode & "'", oCompany) = "Y" Then
                                            GPO.Lines.BinAllocations.BinAbsEntry = B.BinAbsEntry
                                            GPO.Lines.BinAllocations.Quantity = B.Quantity
                                            GPO.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = k
                                            GPO.Lines.BinAllocations.Add()
                                        End If
                                        Manag = ""
                                    Else
                                        Manag = "Serial"
                                    End If
                                    k = k + 1
                                Next
                            ElseIf ItemSetpBy = 2 Then
                                Dim k As Integer = 0, Z As Integer = 0
                                For Each B In obj(i).Lines(j).ls_Batch
                                    If (B.Batch <> "" Or B.Batch <> Nothing) And (B.Quantity <> Nothing Or B.Quantity <> 0) Then

                                        GPO.Lines.BatchNumbers.BatchNumber = B.Batch
                                        GPO.Lines.BatchNumbers.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                        GPO.Lines.BatchNumbers.ManufacturingDate = B.ManufacturingDate
                                        GPO.Lines.BatchNumbers.Notes = B.Notes
                                        GPO.Lines.BatchNumbers.Location = B.Location

                                        GPO.Lines.BatchNumbers.Quantity = B.Quantity
                                        GPO.Lines.BatchNumbers.AddmisionDate = B.AdmissionDate
                                        GPO.Lines.BatchNumbers.ExpiryDate = B.ExpirationDate

                                        GPO.Lines.BatchNumbers.UserFields.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                        GPO.Lines.BatchNumbers.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                        GPO.Lines.BatchNumbers.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                        GPO.Lines.BatchNumbers.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking

                                        GPO.Lines.BatchNumbers.Add()

                                        If myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""WhsCode""='" & L.WhsCode & "'", oCompany) = "Y" Then
                                            If myClasss.ICaseListOfBIN(B.ls_BatchBIN) <> 0 Then
                                                For Each BB In B.ls_BatchBIN
                                                    GPO.Lines.BinAllocations.BinAbsEntry = BB.BinAbsEntry
                                                    GPO.Lines.BinAllocations.Quantity = BB.BinQuantity
                                                    GPO.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = Z
                                                    GPO.Lines.BinAllocations.Add()
                                                    Z = Z + 1
                                                Next
                                            End If
                                        End If

                                        Manag = ""
                                    Else
                                        Manag = "Batch"
                                    End If
                                    '  k = k + 1
                                Next
                            Else
                                If myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""WhsCode""='" & L.WhsCode & "'", oCompany) = "Y" Then
                                    If myClasss.ICaseListOfBIN(L.ls_LineBIN) <> 0 Then
                                        Dim Z As Integer = 0
                                        For Each BB In L.ls_LineBIN
                                            GPO.Lines.BinAllocations.BinAbsEntry = BB.BinAbsEntry
                                            GPO.Lines.BinAllocations.Quantity = BB.BinQuantity
                                            GPO.Lines.BinAllocations.BaseLineNumber = Z
                                            GPO.Lines.BinAllocations.Add()
                                            Z = Z + 1
                                        Next
                                    End If
                                End If
                            End If

                            If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.Baseline <> "" Or L.Baseline <> Nothing) And (L.BaseType <> "" Or L.BaseType <> Nothing) Then
                                GPO.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                GPO.Lines.BaseType = "22"
                                GPO.Lines.BaseLine = Convert.ToInt32(L.Baseline)
                                BaseOnPO = False
                            Else
                                BaseOnPO = True
                            End If

                            If myClasss.Has("ItemCode", L.ItemCode, "OITM") = True Then
                                ErrLine.Add("Line " & j & ". Completed")
                            Else
                                ErrLine.Add("Line " & j & ". Item Code: " & L.ItemCode & " don't have!")
                                sline = True
                            End If
                            GPO.Lines.Add()
                            j = j + 1
                        Next
                        If BaseOnPO = False Then
                            If Manag = "" Then
                                If sline = False Then
                                    RetVal = GPO.Add
                                    If RetVal <> 0 Then
                                        'Write Error
                                        oCompany.GetLastError(_lErrCode, _sErrMsg)
                                        returnstatus = New ReturnStatus With {
                                            .ErrirMsg = _sErrMsg,
                                            .ErrorCode = _lErrCode,
                                            .DocEntry = "",
                                            .SAPDocNum = ""
                                        }
                                        '.RefDocNum = obj(i).RefDocNum,
                                        ls_returnstatus.Add(returnstatus)
                                    Else
                                        'Write successfully 
                                        returnstatus = New ReturnStatus With {
                                             .ErrirMsg = "Add Successfully",
                                             .ErrorCode = 0,
                                             .SAPDocNum = myClasss.Get_DocNum(oCompany.GetNewObjectKey(), "OPDN"),
                                             .DocEntry = oCompany.GetNewObjectKey()
                                        }
                                        '.RefDocNum = obj(i).RefDocNum,
                                        ls_returnstatus.Add(returnstatus)

                                    End If
                                Else
                                    returnstatus = New ReturnStatus With {
                                       .ErrirMsg = "Error Line ",
                                       .ErrorCode = 9999,
                                       .SAPDocNum = "",
                                       .DocEntry = "",
                                       .ErrLine = ErrLine.ToList()
                                    }
                                    '.RefDocNum = obj(i).RefDocNum,
                                    ls_returnstatus.Add(returnstatus)
                                End If
                            Else
                                returnstatus = New ReturnStatus With {
                                    .ErrirMsg = "Item Manage by " & Manag,
                                    .ErrorCode = 9999,
                                    .SAPDocNum = "",
                                    .DocEntry = ""
                                }
                                '.RefDocNum = obj(i).RefDocNum,
                                ls_returnstatus.Add(returnstatus)
                            End If
                        Else
                            returnstatus = New ReturnStatus With {
                               .ErrirMsg = "Don't have references of PO",
                               .ErrorCode = 9999,
                               .SAPDocNum = "",
                               .DocEntry = ""
                            }
                            '.RefDocNum = obj(i).RefDocNum,
                            ls_returnstatus.Add(returnstatus)
                        End If
                    Else
                        returnstatus = New ReturnStatus With {
                            .ErrirMsg = "Duplicate WebDocNum : " & obj(i).WebDocNum,
                            .ErrorCode = 9999,
                            .DocEntry = "",
                            .SAPDocNum = ""
                        }
                        ' .RefDocNum = obj(i).RefDocNum,
                        ls_returnstatus.Add(returnstatus)
                    End If
                    i = i + 1
                Loop
            Else
                ' Login Error
                returnstatus = New ReturnStatus With {
                    .ErrirMsg = oLoginService.sErrMsg,
                    .ErrorCode = oLoginService.lErrCode,
                    .SAPDocNum = "",
                    .DocEntry = ""
                }
                '     .RefDocNum = "",
                ls_returnstatus.Add(returnstatus)
            End If
        Catch ex As Exception
            returnstatus = New ReturnStatus With {
                .ErrirMsg = ex.Message,
                .ErrorCode = ex.HResult,
                .SAPDocNum = "",
                .DocEntry = ""
            }
            '  .RefDocNum = "",
            ls_returnstatus.Add(returnstatus)
        End Try
        Return ls_returnstatus
    End Function

End Class
