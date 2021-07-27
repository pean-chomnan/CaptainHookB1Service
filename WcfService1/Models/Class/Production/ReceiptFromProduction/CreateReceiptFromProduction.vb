Public Class CreateReceiptFromProduction
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of ClassReceiptFromProduction.OIGN), ByVal iCopyFrom As Integer) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim myClasss As New myClassOfFuntion
        Dim returnstatus As ReturnStatus
        Dim IGN As SAPbobsCOM.Documents = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim ErrLine As New List(Of String)
        Dim sline As Boolean = False
        Dim BaseOnSO As Boolean = False
        'Dim batch As Boolean = False
        Dim Manag As String = ""
        Dim ItemSetpBy As Integer

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                IGN = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry) 'BoObjectTypes.oInventoryGenEntry

                Dim i As Integer = 0
                Dim x As Integer = 0

                Do While i < obj.Count
                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OIGN"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & obj(i).WebDocNum, oCompany) = 0 Then
                        IGN.Series = obj(i).Series
                        IGN.DocDate = obj(i).DocDate
                        IGN.Reference2 = obj(i).Ref2
                        IGN.Comments = obj(i).Comments
                        IGN.UserFields.Fields.Item("U_WebDocNum").Value = obj(i).WebDocNum
                        IGN.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items

                        Dim j As Integer = 0
                        For Each L In obj(i).Lines
                            '  IGN.Lines.ItemCode = L.ItemCode
                            If L.TransType = "C" Then
                                IGN.Lines.TransactionType = SAPbobsCOM.BoTransactionTypeEnum.botrntComplete
                            ElseIf L.TransType = "R" Then
                                IGN.Lines.TransactionType = SAPbobsCOM.BoTransactionTypeEnum.botrntReject
                            End If

                            IGN.Lines.Quantity = L.Quantity
                            IGN.Lines.WarehouseCode = L.Warehouse
                            IGN.Lines.CostingCode = L.CogsCode   ' Distribution Rul 1 to 5
                            IGN.Lines.CostingCode2 = L.CogsCode2
                            IGN.Lines.CostingCode3 = L.CogsCode3
                            IGN.Lines.CostingCode4 = L.CogsCode4
                            IGN.Lines.CostingCode5 = L.CogsCode5

                            ItemSetpBy = myClasss.ItemSetupBy(L.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0
                                If iCopyFrom = 1 Or iCopyFrom = 2 Then  '  1=Copy From Production Order need to key-in batch,lot,expired date,...., 2= Copy From Issue For Production But Create New Serial or Batch
                                    For Each B In obj(i).Lines(j).ls_Serial
                                        If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                            IGN.Lines.SerialNumbers.InternalSerialNumber = B.SerialNumber
                                            IGN.Lines.SerialNumbers.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                            IGN.Lines.SerialNumbers.ExpiryDate = B.ExpirationDate
                                            IGN.Lines.SerialNumbers.ManufactureDate = B.ManufactureDate
                                            IGN.Lines.SerialNumbers.Notes = B.Note
                                            IGN.Lines.SerialNumbers.Location = B.Location
                                            IGN.Lines.SerialNumbers.ReceptionDate = B.ReceptionDate

                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking
                                            IGN.Lines.SerialNumbers.Add()

                                            If myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""WhsCode""='" & L.Warehouse & "'", oCompany) = "Y" Then
                                                IGN.Lines.BinAllocations.BinAbsEntry = B.BinAbsEntry
                                                IGN.Lines.BinAllocations.Quantity = B.Quantity
                                                IGN.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = k
                                                IGN.Lines.BinAllocations.Add()
                                            End If

                                            Manag = ""
                                        Else
                                            Manag = "Serial"
                                        End If
                                        k = k + 1
                                    Next

                                    If iCopyFrom = 2 Then
                                        If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.BaseLine <> "" Or L.BaseLine <> Nothing) Then
                                            IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                            IGN.Lines.BaseType = "202"
                                            IGN.Lines.BaseLine = Convert.ToInt32(L.BaseLine)
                                            BaseOnSO = False
                                        Else
                                            BaseOnSO = True
                                        End If
                                    Else
                                        If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) Then
                                            IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                            IGN.Lines.BaseType = "202"
                                            BaseOnSO = False
                                        Else
                                            BaseOnSO = True
                                        End If
                                    End If

                                ElseIf iCopyFrom = 3 Then  ' 2= Copy From Issue For Production No need to input serial,..... just select is ok
                                    For Each B In obj(i).Lines(j).ls_Serial
                                        If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                            IGN.Lines.SerialNumbers.InternalSerialNumber = B.SerialNumber
                                            IGN.Lines.SerialNumbers.Add()
                                            Manag = ""
                                        Else
                                            Manag = "Serial"
                                        End If
                                        k = k + 1
                                    Next

                                    If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.BaseLine <> "" Or L.BaseLine <> Nothing) Then
                                        IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                        IGN.Lines.BaseType = "202"
                                        IGN.Lines.BaseLine = Convert.ToInt32(L.BaseLine)
                                        BaseOnSO = False
                                    Else
                                        BaseOnSO = True
                                    End If
                                End If
                                
                            ElseIf ItemSetpBy = 2 Then
                                Dim k As Integer = 0
                                If iCopyFrom = 1 Or iCopyFrom = 2 Then   ' 1=Copy From Production Order need to key-in batch,lot,expired date,...., 2= Copy From Issue For Production But Create New Serial or Batch
                                    For Each B In obj(i).Lines(j).ls_Batch
                                        If (B.Batch <> "" Or B.Batch <> Nothing) And (B.Quantity <> Nothing Or B.Quantity <> 0) Then
                                            'IGN.Lines.BatchNumbers.SetCurrentLine(k)
                                            IGN.Lines.BatchNumbers.BatchNumber = B.Batch
                                            IGN.Lines.BatchNumbers.ManufacturerSerialNumber = B.ManufacturerSerialNumber
                                            IGN.Lines.BatchNumbers.ManufacturingDate = B.ManufacturingDate
                                            IGN.Lines.BatchNumbers.Notes = B.Notes
                                            IGN.Lines.BatchNumbers.Location = B.Location

                                            IGN.Lines.BatchNumbers.Quantity = B.Quantity
                                            IGN.Lines.BatchNumbers.AddmisionDate = B.AdmissionDate
                                            IGN.Lines.BatchNumbers.ExpiryDate = B.ExpirationDate

                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_ACT_WeightOnBatch").Value = B.ACT_WeightOnBatch
                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_CompanyAddress").Value = B.CompanyAddress
                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_BarCodeBoxNumber").Value = B.BarCodeBoxNumber
                                            IGN.Lines.SerialNumbers.UserFields.Fields.Item("U_SmokingSystem").Value = B.Smoking
                                            'IGN.Lines.BatchNumbers.BaseLineNumber = L.Baseline

                                            IGN.Lines.BatchNumbers.Add()

                                            If myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""WhsCode""='" & L.Warehouse & "'", oCompany) = "Y" Then
                                                IGN.Lines.BinAllocations.BinAbsEntry = B.BinAbsEntry
                                                IGN.Lines.BinAllocations.Quantity = B.Quantity
                                                IGN.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = k
                                                IGN.Lines.BinAllocations.Add()
                                            End If

                                            Manag = ""
                                        Else
                                            Manag = "Batch"
                                        End If
                                        k = k + 1
                                    Next

                                    If iCopyFrom = 2 Then
                                        If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.BaseLine <> "" Or L.BaseLine <> Nothing) Then
                                            IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                            IGN.Lines.BaseType = "202"
                                            IGN.Lines.BaseLine = Convert.ToInt32(L.BaseLine)
                                            BaseOnSO = False
                                        Else
                                            BaseOnSO = True
                                        End If
                                    Else
                                        If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) Then
                                            IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                            IGN.Lines.BaseType = "202"
                                            BaseOnSO = False
                                        Else
                                            BaseOnSO = True
                                        End If
                                    End If
                                ElseIf iCopyFrom = 3 Then  ' No need to key-in batch just only select
                                    For Each B In obj(i).Lines(j).ls_Batch
                                        If (B.Batch <> "" Or B.Batch <> Nothing) And (B.Quantity <> Nothing Or B.Quantity <> 0) Then
                                            IGN.Lines.BatchNumbers.BatchNumber = B.Batch
                                            IGN.Lines.BatchNumbers.Quantity = B.Quantity
                                            IGN.Lines.BatchNumbers.Add()
                                            Manag = ""
                                        Else
                                            Manag = "Batch"
                                        End If
                                        k = k + 1
                                    Next

                                    If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.BaseLine <> "" Or L.BaseLine <> Nothing) Then
                                        IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                        IGN.Lines.BaseType = "202"
                                        IGN.Lines.BaseLine = Convert.ToInt32(L.BaseLine)
                                        BaseOnSO = False
                                    Else
                                        BaseOnSO = True
                                    End If
                                End If
                            Else
                                If iCopyFrom = 2 Or iCopyFrom = 3 Then
                                    If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.BaseLine <> "" Or L.BaseLine <> Nothing) Then
                                        IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                        IGN.Lines.BaseType = "202"
                                        IGN.Lines.BaseLine = Convert.ToInt32(L.BaseLine)
                                        BaseOnSO = False
                                    Else
                                        BaseOnSO = True
                                    End If
                                ElseIf iCopyFrom = 1 Then
                                    If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) Then
                                        IGN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                        IGN.Lines.BaseType = "202"
                                        BaseOnSO = False
                                    Else
                                        BaseOnSO = True
                                    End If
                                End If
                            End If

                            If myClasss.Has("ItemCode", L.ItemCode, "OITM") = True Then
                                ErrLine.Add("Line " & j & ". Completed")
                            Else
                                ErrLine.Add("Line " & j & ". Item Code: " & L.ItemCode & " don't have!")
                                sline = True
                            End If
                            IGN.Lines.Add()
                            j = j + 1
                        Next
                        If BaseOnSO = False Then
                            If Manag = "" Then
                                If sline = False Then
                                    RetVal = IGN.Add
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
                               .ErrirMsg = "Don't have references of SO",
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



















