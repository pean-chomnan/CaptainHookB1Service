Module GetSample
    'Public Function GetData(ByVal obj As String) As GetMasterResponse
    '    Dim listItemCode As New List(Of GetItemMaster)
    '    Try
    '        Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
    '        Dim Table As String = ""
    '        Dim _type As Integer = 0
    '        Dim oCompany As SAPbobsCOM.Company = Nothing
    '        Dim oRs As SAPbobsCOM.Recordset = Nothing
    '        Dim strSql As String = ""
    '        Dim oLoginService As New LoginServiceWebRef
    '        If oLoginService.lErrCode = 0 Then
    '            oCompany = oLoginService.Company
    '            oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
    '            strSql = "SELECT TOP 10 * FROM " & _DBNAME & ".""OITM"" Where 1=" & obj
    '            oRs.DoQuery(strSql)
    '            Do While Not oRs.EoF
    '                listItemCode.Add(New GetItemMaster With {
    '                    .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim
    '                })
    '                oRs.MoveNext()
    '            Loop
    '            Return (New GetMasterResponse With {
    '                    .ErrCode = 0,
    '                    .ErrMsg = "",
    '                    .ItemCodes = listItemCode
    '                })
    '        Else
    '            Return (New GetMasterResponse With {
    '                    .ErrCode = oLoginService.lErrCode,
    '                    .ErrMsg = oLoginService.sErrMsg,
    '                    .ItemCodes = Nothing
    '                })
    '        End If
    '    Catch ex As Exception
    '        Return (New GetMasterResponse With {
    '                   .ErrCode = ex.HResult,
    '                   .ErrMsg = ex.Message.ToString(),
    '                   .ItemCodes = Nothing
    '               })
    '    End Try
    'End Function

End Module
