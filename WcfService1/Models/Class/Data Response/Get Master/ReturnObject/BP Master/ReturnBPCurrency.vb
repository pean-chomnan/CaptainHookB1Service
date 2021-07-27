'Public Class ReturnBPCurrency

'End Class
Public Class ReturnBPCurrency
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_BPCurr As List(Of CurrencyCode)
End Class

Public Class CurrencyCode
    Public Property CurrCode As String
    Public Property CurrName As String
End Class
Public Class CGetBPCurrency
    Public Function FGetCurrency() As ReturnBPCurrency
        Try
            Dim listCurr As New List(Of CurrencyCode)
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            Dim Table As String = ""
            Dim _type As Integer = 0
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim oLoginService As New LoginServiceWebRef
            '   Dim listItemCode As New List(Of GetItemMaster)

            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""CurrCode"",""CurrName"" FROM " & _DBNAME & ".""OCRN"" Where ""Locked""='N'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    listCurr.Add(New CurrencyCode With {
                        .CurrCode = oRs.Fields.Item("CurrCode").Value.ToString.Trim,
                        .CurrName = oRs.Fields.Item("CurrName").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBPCurrency With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_BPCurr = listCurr
                    })
            Else
                Return (New ReturnBPCurrency With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_BPCurr = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBPCurrency With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_BPCurr = Nothing
                   })
        End Try
    End Function

End Class
