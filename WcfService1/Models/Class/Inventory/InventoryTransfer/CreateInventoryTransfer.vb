Public Class CreateInventoryTransfer
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function Send(ByVal obj As List(Of ClassInventoryTransfer.OWTR)) As List(Of ReturnStatus)

        '  Dim Utilities As New UtilitiesFunction
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim myClasss As New myClassOfFuntion
        Dim returnstatus As ReturnStatus
        Dim OWTR As SAPbobsCOM.StockTransfer = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim ErrLine As New List(Of String)
        Dim sline As Boolean = False
        Dim Manag As String = ""
        Dim ItemSetpBy As Integer

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                OWTR = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer)
                '    Dim OWTR As SAPbobsCOM.StockTransfer = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer)
                Dim i As Integer = 0
                Dim x As Integer = 0

                Do While i < obj.Count
                    'If myClasss.Has("U_WebDocNum", obj(i).WebDocNum, "OOWTR") = False Then
                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT * FROM " & _DBNAME & ".""OWTR"" WHERE ""CANCELED""='N' AND ""U_WebDocNum""=" & obj(i).WebDocNum, oCompany) = 0 Then
                        OWTR.Series = obj(i).Series
                        OWTR.DocDate = obj(i).DocDate
                        OWTR.TaxDate = obj(i).TaxDate
                        OWTR.PriceList = obj(i).PriceListNum
                        OWTR.CardCode = obj(i).CardCode
                        If myClasss.ICaseString(obj(i).ContactPersonCode) > 0 Then
                            OWTR.ContactPerson = obj(i).ContactPersonCode
                        End If

                        If myClasss.ICaseString(obj(i).ShipToCode) <> "" Then
                            OWTR.ShipToCode = obj(i).ShipToCode
                        End If

                        OWTR.Address = obj(i).Address
                        OWTR.FromWarehouse = obj(i).FromWhs
                        OWTR.ToWarehouse = obj(i).ToWhs
                        OWTR.SalesPersonCode = obj(i).SaleEmployee
                        OWTR.Comments = obj(i).Comments
                        OWTR.JournalMemo = obj(i).JournalRemark
                        OWTR.UserFields.Fields.Item("U_WebDocNum").Value = obj(i).WebDocNum
                        'OWTR.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items

                        Dim j As Integer = 0
                        For Each L In obj(i).Lines
                            OWTR.Lines.ItemCode = L.ItemCode
                            'OWTR.Lines.BarCode = L.BarCode Don't have
                            OWTR.Lines.Quantity = L.Quantity
                            OWTR.Lines.UnitPrice = L.Price
                            '    OWTR.Lines.GrossPrice = L.GrossPrice
                            OWTR.Lines.DiscountPercent = L.DiscPercent
                            OWTR.Lines.FromWarehouseCode = L.FromWhs
                            OWTR.Lines.WarehouseCode = L.ToWhs
                            OWTR.Lines.Rate = L.Rate

                            OWTR.Lines.DistributionRule = L.CogsCode   ' Distribution Rul 1 to 5
                            OWTR.Lines.DistributionRule2 = L.CogsCode2
                            OWTR.Lines.DistributionRule3 = L.CogsCode3
                            OWTR.Lines.DistributionRule4 = L.CogsCode4
                            OWTR.Lines.DistributionRule5 = L.CogsCode5

                            ItemSetpBy = myClasss.ItemSetupBy(L.ItemCode)

                            If ItemSetpBy = 1 Then
                                Dim k As Integer = 0
                                For Each B In obj(i).Lines(j).ls_Serial
                                    If (B.SerialNumber <> "" Or B.SerialNumber <> Nothing) Then
                                        OWTR.Lines.SerialNumbers.InternalSerialNumber = B.SerialNumber
                                        OWTR.Lines.SerialNumbers.Add()
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
                                        'OWTR.Lines.BatchNumbers.SetCurrentLine(k)
                                        OWTR.Lines.BatchNumbers.BatchNumber = B.Batch
                                        OWTR.Lines.BatchNumbers.Quantity = B.Quantity
                                        OWTR.Lines.BatchNumbers.Add()
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
                            OWTR.Lines.Add()
                            j = j + 1
                        Next

                        If Manag = "" Then
                            If sline = False Then
                                RetVal = OWTR.Add
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
