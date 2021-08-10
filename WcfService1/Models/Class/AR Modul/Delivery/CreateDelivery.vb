Public Class CreateDelivery
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of ClassDelivery.ODLN)) As List(Of ReturnStatus)

        '  Dim Utilities As New UtilitiesFunction
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim myClasss As New myClassOfFuntion
        Dim returnstatus As ReturnStatus
        Dim DLN As SAPbobsCOM.Documents = Nothing
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
                DLN = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDeliveryNotes)

                Dim i As Integer = 0
                Dim x As Integer = 0

                Do While i < obj.Count
                    'If myClasss.Has("U_WebDocNum", obj(i).WebDocNum, "ODLN") = False Then
                    If True Then
                        'If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""ODLN"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & obj(i).WebDocNum, oCompany) = 0 Then
                        DLN.Series = obj(i).Series
                        DLN.CardCode = obj(i).CardCode
                        DLN.DocDate = obj(i).DocDate
                        DLN.DocDueDate = obj(i).DocDueDate
                        DLN.TaxDate = obj(i).TaxDate
                        DLN.BPL_IDAssignedToInvoice = obj(i).RequestByBranch

                        DLN.DiscountPercent = obj(i).DiscountPercent

                        If obj(i).ContactPersonCode <> 0 And obj(i).ContactPersonCode.ToString <> "" Then
                            DLN.ContactPersonCode = obj(i).ContactPersonCode
                        End If

                        If obj(i).SalesPersonCode <> 0 And obj(i).SalesPersonCode.ToString <> "" Then
                            DLN.SalesPersonCode = obj(i).SalesPersonCode
                        End If


                        If obj(i).DocumentsOwner <> 0 And obj(i).DocumentsOwner.ToString <> "" Then
                            DLN.DocumentsOwner = obj(i).DocumentsOwner
                        End If


                        DLN.NumAtCard = obj(i).NumAtCard
                        DLN.Comments = obj(i).Comments

                        'DLN.BP
                        'DLN.Series = obj(i).SeriesID
                        DLN.UserFields.Fields.Item("U_WebDocNum").Value = obj(i).WebDocNum
                        DLN.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items
                        If obj(i).PriceMode = "N" Then
                            DLN.PriceMode = SAPbobsCOM.PriceModeDocumentEnum.pmdNet
                        ElseIf obj(i).PriceMode = "G" Then
                            DLN.PriceMode = SAPbobsCOM.PriceModeDocumentEnum.pmdGross
                        Else
                            DLN.PriceMode = SAPbobsCOM.PriceModeDocumentEnum.pmdNetAndGross
                        End If
                        Dim j As Integer = 0
                        For Each L In obj(i).Lines

                            DLN.Lines.ItemCode = L.ItemCode
                            DLN.Lines.BarCode = L.BarCode
                            DLN.Lines.Quantity = L.Quantity
                            DLN.Lines.UnitPrice = L.Price
                            DLN.Lines.GrossPrice = L.GrossPrice
                            DLN.Lines.DiscountPercent = L.DiscPercent
                            DLN.Lines.VatGroup = L.VatGroup
                            DLN.Lines.UoMEntry = L.UomEntry
                            DLN.Lines.WarehouseCode = L.WhsCode
                            DLN.Lines.Weight1 = L.Weight
                            DLN.Lines.UserFields.Fields.Item("U_PriceWeight").Value = L.PriceWeight
                            DLN.Lines.COGSCostingCode = L.CogsCode ' Distribution Rul 1 to 5
                            DLN.Lines.COGSCostingCode2 = L.CogsCode2
                            DLN.Lines.COGSCostingCode3 = L.CogsCode3
                            DLN.Lines.COGSCostingCode4 = L.CogsCode4
                            DLN.Lines.COGSCostingCode5 = L.CogsCode5

                            ItemSetpBy = myClasss.ItemSetupBy(L.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0
                                '     Dim x As Integer = 0

                                For Each B In obj(i).Lines(j).ls_Serial
                                    If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                        DLN.Lines.SerialNumbers.InternalSerialNumber = B.SerialNumber
                                        DLN.Lines.SerialNumbers.Add()
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
                                        'DLN.Lines.BatchNumbers.SetCurrentLine(k)
                                        DLN.Lines.BatchNumbers.BatchNumber = B.Batch
                                        DLN.Lines.BatchNumbers.Quantity = B.Quantity
                                        DLN.Lines.BatchNumbers.Add()
                                        Manag = ""
                                    Else
                                        Manag = "Batch"
                                    End If
                                    k = k + 1
                                Next

                            End If

                            If (L.BaseEntry <> "" Or L.BaseEntry <> Nothing) And (L.Baseline <> "" Or L.Baseline <> Nothing) And (L.BaseType <> "" Or L.BaseType <> Nothing) Then
                                DLN.Lines.BaseEntry = Convert.ToInt32(L.BaseEntry)
                                DLN.Lines.BaseType = Convert.ToInt32(L.BaseType)
                                DLN.Lines.BaseLine = Convert.ToInt32(L.Baseline)
                                BaseOnSO = False
                            Else
                                BaseOnSO = True
                            End If

                            If myClasss.Has("ItemCode", L.ItemCode, "OITM") = True Then
                                ErrLine.Add("Line " & j & ". Completed")
                            Else
                                ErrLine.Add("Line " & j & ". Item Code: " & L.ItemCode & " don't have!")
                                sline = True
                            End If
                            DLN.Lines.Add()
                            j = j + 1
                        Next
                        If BaseOnSO = False Then
                            If Manag = "" Then
                                If sline = False Then
                                    RetVal = DLN.Add
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
