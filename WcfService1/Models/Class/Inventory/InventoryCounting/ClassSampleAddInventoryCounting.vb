Public Class ClassSampleAddInventoryCounting
#Region "Inprogess"
    'Public Function InventoryCounting(ByVal obj As List(Of InventoryStockCount.OINC)) As List(Of ReturnStatus)
    '    Dim ls_returnstatus As New List(Of ReturnStatus)
    '    Dim returnstatus As New ReturnStatus
    '    Dim CountService As SAPbobsCOM.InventoryCountingsService
    '    Dim InvCount As SAPbobsCOM.InventoryCounting
    '    Dim InvCountLines As SAPbobsCOM.InventoryCountingLines
    '    Dim InvCountLine As SAPbobsCOM.InventoryCountingLine
    '    Dim InvCountParam As SAPbobsCOM.InventoryCountingParams
    '    Dim companyService As SAPbobsCOM.CompanyService
    '    Dim RetVal As Integer = 0
    '    Dim xDocEntry As Integer = 0
    '    Dim ErrLine As New List(Of String)
    '    Dim sline As Boolean = False
    '    Dim dttime As New DateTime
    '    dttime = DateTime.Now
    '    Try
    '        Dim oLoginService As New LoginServiceWebRef
    '        If oLoginService.lErrCode = 0 Then
    '            oCompany = oLoginService.Company
    '            companyService = oCompany.GetCompanyService()
    '            CountService = companyService.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryCountingsService)
    '            InvCount = CountService.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCounting)
    '            For Each header In obj
    '                If HasWebDocNum("U_WEBID", header.WebID, "OINC") <> True Then
    '                    InvCount.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter
    '                    InvCount.SingleCounterType = SAPbobsCOM.CounterTypeEnum.ctUser
    '                    InvCount.Reference2 = header.Ref
    '                    InvCount.Remarks = header.Remark
    '                    InvCount.CountDate = dttime
    '                    InvCountLines = InvCount.InventoryCountingLines
    '                    For Each Line In header.Line
    '                        InvCountLine = InvCountLines.Add()
    '                        InvCountLine.ItemCode = Line.ItemCode
    '                        InvCountLine.WarehouseCode = Line.WhsCode
    '                        InvCountLine.CountedQuantity = Line.CoutedQTY
    '                        InvCountLine.Counted = SAPbobsCOM.BoYesNoEnum.tYES
    '                    Next
    '                    InvCountParam = CountService.Add(InvCount)
    '                    oCompany.GetLastError(_lErrCode, _sErrMsg)
    '                    If _lErrCode <> 0 Then
    '                        oCompany.GetLastError(_lErrCode, _sErrMsg)
    '                        returnstatus = New ReturnStatus With {
    '                            .ErrirMsg = _sErrMsg,
    '                            .ErrorCode = _lErrCode,
    '                            .SAPDocNum = InvCountParam.DocumentNumber,
    '                            .WEBDocNum = header.WebID,
    '                            .DocEntry = InvCountParam.DocumentEntry
    '                        }
    '                        ls_returnstatus.Add(returnstatus)
    '                    Else
    '                        returnstatus = New ReturnStatus With {
    '                            .ErrirMsg = "Add Successfully",
    '                            .ErrorCode = 0,
    '                            .SAPDocNum = InvCountParam.DocumentNumber,
    '                            .WEBDocNum = header.WebID,
    '                            .DocEntry = InvCountParam.DocumentEntry
    '                        }
    '                        ls_returnstatus.Add(returnstatus)
    '                    End If
    '                Else
    '                    returnstatus = New ReturnStatus With {
    '                            .ErrirMsg = "Duplicate WebDocNum : " & header.WebID,
    '                            .ErrorCode = 1,
    '                            .WEBDocNum = header.WebID,
    '                            .DocEntry = "",
    '                            .SAPDocNum = ""
    '                        }
    '                    ls_returnstatus.Add(returnstatus)
    '                End If
    '            Next
    '        Else
    '            'Get Error
    '            returnstatus = New ReturnStatus With {
    '                .ErrirMsg = oLoginService.sErrMsg,
    '                .ErrorCode = oLoginService.lErrCode,
    '                .SAPDocNum = "",
    '                .WEBDocNum = "",
    '                .DocEntry = ""
    '            }
    '            ls_returnstatus.Add(returnstatus)
    '        End If
    '    Catch ex As Exception
    '        returnstatus = New ReturnStatus With {
    '                .ErrirMsg = ex.Message,
    '                .ErrorCode = ex.GetHashCode,
    '                .SAPDocNum = "",
    '                .WEBDocNum = "",
    '                .DocEntry = ""
    '        }
    '        ls_returnstatus.Add(returnstatus)
    '    End Try
    '    Return ls_returnstatus
    'End Function

#End Region
End Class
