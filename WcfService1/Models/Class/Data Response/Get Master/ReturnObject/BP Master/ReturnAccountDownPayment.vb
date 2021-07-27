Public Class ReturnAccountDownPayment
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls As List(Of AccountDownPayment)
End Class

Public Class AccountDownPayment
    Public Property AccountCode As String
    Public Property AccountName As String
End Class
Public Class CGetBPAcctDownPayment
    Public Function FGetAccountDownPayment() As ReturnAccountDownPayment
        Try
            Dim ls_acc As New List(Of AccountDownPayment)
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
                strSql = "SELECT Top 300 ""AcctCode"",""AcctName"" FROM " & _DBNAME & ".""OACT"" WHERE  ""LocManTran""<>'Y' AND ""FatherNum"" IS NOT NULL"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls_acc.Add(New AccountDownPayment With {
                        .AccountCode = oRs.Fields.Item("AcctCode").Value.ToString.Trim,
                        .AccountName = oRs.Fields.Item("AcctName").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnAccountDownPayment With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls = ls_acc
                    })
            Else
                Return (New ReturnAccountDownPayment With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnAccountDownPayment With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls = Nothing
                   })
        End Try
    End Function

End Class