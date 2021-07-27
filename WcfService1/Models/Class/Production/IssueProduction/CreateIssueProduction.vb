Public Class CreateIssueProduction
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of ClassIssueProduction.OIGE)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim myClasss As New myClassOfFuntion
        Dim returnstatus As ReturnStatus
        Dim IGE As SAPbobsCOM.Documents = Nothing
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
                IGE = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenExit) 'BoObjectTypes.oInventoryGenEntry

                Dim i As Integer = 0
                Dim x As Integer = 0

                Do While i < obj.Count
                    'If myClasss.Has("U_WebDocNum", obj(i).WebDocNum, "OIGE") = False Then
                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OIGE"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & obj(i).WebDocNum, oCompany) = 0 Then
                        IGE.Series = obj(i).Series
                        IGE.DocDate = obj(i).DocDate
                        'IGE.TaxDate = obj(i).TaxDate
                        IGE.Reference2 = obj(i).Ref2
                        IGE.Comments = obj(i).Comments
                        IGE.UserFields.Fields.Item("U_WebDocNum").Value = obj(i).WebDocNum

                        IGE.EDocStatus = SAPbobsCOM.EDocStatusEnum.edoc_Ok

                        Dim j As Integer = 0
                        For Each L In obj(i).Lines
                            IGE.Lines.VendorNum = L.VendorNum
                            IGE.Lines.Quantity = L.Quantity
                            IGE.Lines.WarehouseCode = L.Warehouse
                            IGE.Lines.CostingCode = L.CogsCode   ' Distribution Rul 1 to 5
                            IGE.Lines.CostingCode2 = L.CogsCode2
                            IGE.Lines.CostingCode3 = L.CogsCode3
                            IGE.Lines.CostingCode4 = L.CogsCode4
                            IGE.Lines.CostingCode5 = L.CogsCode5

                            ItemSetpBy = myClasss.ItemSetupBy(L.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0

                                For Each B In obj(i).Lines(j).ls_Serial
                                    If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                        IGE.Lines.SerialNumbers.InternalSerialNumber = B.SerialNumber
                                        IGE.Lines.SerialNumbers.Add()
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
                                        'IGE.Lines.BatchNumbers.SetCurrentLine(k)
                                        IGE.Lines.BatchNumbers.BatchNumber = B.Batch
                                        IGE.Lines.BatchNumbers.Quantity = B.Quantity
                                        IGE.Lines.BatchNumbers.Add()
                                        Manag = ""
                                    Else
                                        Manag = "Batch"
                                    End If
                                    k = k + 1
                                Next
                            End If

                            If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.BaseLine <> "" Or L.BaseLine <> Nothing) Then
                                IGE.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                IGE.Lines.BaseType = "202"
                                IGE.Lines.BaseLine = Convert.ToInt32(L.BaseLine)
                                BaseOnSO = False
                            Else
                                BaseOnSO = True
                            End If

                            'If myClasss.Has("ItemCode", L.ItemCode, "OITM") = True Then
                            '    ErrLine.Add("Line " & j & ". Completed")
                            'Else
                            '    ErrLine.Add("Line " & j & ". Item Code: " & L.ItemCode & " don't have!")
                            '    sline = True
                            'End If
                            IGE.Lines.Add()
                            j = j + 1
                        Next
                        If BaseOnSO = False Then
                            If Manag = "" Then
                                If sline = False Then
                                    RetVal = IGE.Add
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
