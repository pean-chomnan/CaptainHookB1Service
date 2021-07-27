Public Class ReturnBPReturnPaymentTerms
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls As List(Of PaymentTerms)
End Class

Public Class PaymentTerms
    Public Property TermsCode As String
    Public Property Terms As String
    Public Property ExtraDay As Integer
End Class
Public Class CGetBPReturnPaymentTerms
    Public Function FGetAccountDownPayment() As ReturnBPReturnPaymentTerms
        Try
            Dim ls_acc As New List(Of PaymentTerms)
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
                strSql = "SELECT ""GroupNum"",""PymntGroup"",""ExtraDays"" FROM " & _DBNAME & ".""OCTG"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls_acc.Add(New PaymentTerms With {
                        .TermsCode = oRs.Fields.Item("GroupNum").Value,
                        .Terms = oRs.Fields.Item("PymntGroup").Value.ToString.Trim,
                        .ExtraDay = oRs.Fields.Item("ExtraDays").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBPReturnPaymentTerms With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls = ls_acc
                    })
            Else
                Return (New ReturnBPReturnPaymentTerms With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBPReturnPaymentTerms With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls = Nothing
                   })
        End Try
    End Function

End Class


