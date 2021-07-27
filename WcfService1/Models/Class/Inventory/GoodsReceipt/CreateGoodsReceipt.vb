Public Class CreateGoodsReceipt
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of ClassGoodsReceipt.OIGN)) As List(Of ReturnStatus)

        '  Dim Utilities As New UtilitiesFunction
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim myClasss As New myClassOfFuntion
        Dim returnstatus As ReturnStatus
        Dim OIGN As SAPbobsCOM.Documents = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim ErrLine As New List(Of String)
        Dim sline As Boolean = False
        'Dim BaseOnSO As Boolean = False
        'Dim batch As Boolean = False
        Dim Manag As String = ""
        Dim ItemSetpBy As Integer

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                OIGN = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry)

                Dim i As Integer = 0
                Dim x As Integer = 0

                Do While i < obj.Count
                    'If myClasss.Has("U_WebDocNum", obj(i).WebDocNum, "OOIGN") = False Then
                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OIGN"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & obj(i).WebDocNum, oCompany) = 0 Then
                        OIGN.Series = obj(i).Series
                        OIGN.DocDate = obj(i).DocDate
                        OIGN.TaxDate = obj(i).TaxDate
                        OIGN.GroupNumber = obj(i).PriceListNum
                        OIGN.Reference2 = obj(i).Ref2
                        OIGN.Comments = obj(i).Comments
                        OIGN.JournalMemo = obj(i).JournalRemark
                        OIGN.UserFields.Fields.Item("U_WebDocNum").Value = obj(i).WebDocNum
                        OIGN.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items
                        
                        Dim j As Integer = 0
                        For Each L In obj(i).Lines
                            OIGN.Lines.ItemCode = L.ItemCode
                            OIGN.Lines.BarCode = L.BarCode
                            OIGN.Lines.Quantity = L.Quantity
                            OIGN.Lines.UnitPrice = L.Price
                            OIGN.Lines.GrossPrice = L.GrossPrice
                            OIGN.Lines.DiscountPercent = L.DiscPercent
                            OIGN.Lines.WarehouseCode = L.WhsCode

                            OIGN.Lines.CostingCode = L.CogsCode   ' Distribution Rul 1 to 5
                            OIGN.Lines.CostingCode2 = L.CogsCode2
                            OIGN.Lines.CostingCode3 = L.CogsCode3
                            OIGN.Lines.CostingCode4 = L.CogsCode4
                            OIGN.Lines.CostingCode5 = L.CogsCode5

                            ItemSetpBy = myClasss.ItemSetupBy(L.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0
                                '     Dim x As Integer = 0

                                For Each B In obj(i).Lines(j).ls_Serial
                                    If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then

                                        OIGN.Lines.SerialNumbers.InternalSerialNumber = B.SerialNumber
                                        OIGN.Lines.SerialNumbers.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                        OIGN.Lines.SerialNumbers.ExpiryDate = B.ExpirationDate
                                        OIGN.Lines.SerialNumbers.ManufactureDate = B.ManufactureDate
                                        OIGN.Lines.SerialNumbers.Notes = B.Note
                                        OIGN.Lines.SerialNumbers.Location = B.Location
                                        OIGN.Lines.SerialNumbers.ReceptionDate = B.ReceptionDate

                                        OIGN.Lines.SerialNumbers.UserFields.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                        OIGN.Lines.SerialNumbers.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                        OIGN.Lines.SerialNumbers.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                        OIGN.Lines.SerialNumbers.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking
                                        OIGN.Lines.SerialNumbers.Add()

                                        If myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""WhsCode""='" & L.WhsCode & "'", oCompany) = "Y" Then
                                            OIGN.Lines.BinAllocations.BinAbsEntry = B.BinAbsEntry
                                            OIGN.Lines.BinAllocations.Quantity = B.Quantity
                                            OIGN.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = k
                                            OIGN.Lines.BinAllocations.Add()
                                        End If

                                        Manag = ""
                                    Else
                                        Manag = "Serial"
                                    End If
                                    k = k + 1
                                Next
                            ElseIf ItemSetpBy = 2 Then
                                Dim k As Integer = 0
                                For Each B In obj(i).Lines(j).ls_Batch
                                    If (B.Batch <> "" Or B.Batch <> Nothing) And (B.Quantity <> Nothing Or B.Quantity <> 0) Then
                                        'OIGN.Lines.BatchNumbers.SetCurrentLine(k)
                                        OIGN.Lines.BatchNumbers.BatchNumber = B.Batch
                                        OIGN.Lines.BatchNumbers.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                        OIGN.Lines.BatchNumbers.ManufacturingDate = B.ManufacturingDate
                                        OIGN.Lines.BatchNumbers.Notes = B.Notes
                                        OIGN.Lines.BatchNumbers.Location = B.Location

                                        OIGN.Lines.BatchNumbers.Quantity = B.Quantity
                                        OIGN.Lines.BatchNumbers.AddmisionDate = B.AdmissionDate
                                        OIGN.Lines.BatchNumbers.ExpiryDate = B.ExpirationDate

                                        OIGN.Lines.BatchNumbers.UserFields.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                        OIGN.Lines.BatchNumbers.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                        OIGN.Lines.BatchNumbers.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                        OIGN.Lines.BatchNumbers.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking
                                        'OIGN.Lines.BatchNumbers.BaseLineNumber = L.Baseline

                                        OIGN.Lines.BatchNumbers.Add()

                                        If myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""WhsCode""='" & L.WhsCode & "'", oCompany) = "Y" Then
                                            OIGN.Lines.BinAllocations.BinAbsEntry = B.BinAbsEntry
                                            OIGN.Lines.BinAllocations.Quantity = B.Quantity
                                            OIGN.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = k
                                            OIGN.Lines.BinAllocations.Add()
                                        End If

                                        Manag = ""
                                    Else
                                        Manag = "Batch"
                                    End If
                                    k = k + 1
                                Next

                            End If

                            If myClasss.Has("ItemCode", L.ItemCode, "OITM") = True Then
                                ErrLine.Add("Line " & j & ". Completed")
                            Else
                                ErrLine.Add("Line " & j & ". Item Code: " & L.ItemCode & " don't have!")
                                sline = True
                            End If
                            OIGN.Lines.Add()
                            j = j + 1
                        Next

                        If Manag = "" Then
                            If sline = False Then
                                RetVal = OIGN.Add
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
                                         .SAPDocNum = myClasss.Get_DocNum(oCompany.GetNewObjectKey(), "ORDR"),
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
