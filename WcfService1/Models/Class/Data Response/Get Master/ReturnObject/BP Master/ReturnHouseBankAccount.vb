Public Class ReturnHouseBankAccount
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls As List(Of HouseBankAccountCode)
End Class

Public Class HouseBankAccountCode
    Public Property Code
End Class
Public Class CGetReturnHouseBankAccount
    Public Function FGetHouseBankAccount(ByVal BankAcctCode As String) As ReturnHouseBankAccount
        Try
            Dim ls_acc As New List(Of HouseBankAccountCode)
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
                strSql = "SELECT ""Account"" FROM " & _DBNAME & ".""DSC1"" WHERE ""BankCode""='" & BankAcctCode & "' ORDER BY ""Account"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls_acc.Add(New HouseBankAccountCode With {
                        .Code = oRs.Fields.Item("Account").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnHouseBankAccount With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls = ls_acc
                    })
            Else
                Return (New ReturnHouseBankAccount With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnHouseBankAccount With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls = Nothing
                   })
        End Try
    End Function

End Class

