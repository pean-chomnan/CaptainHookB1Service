Public Class ReturnBankCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls As List(Of BankCode)
End Class

Public Class BankCode
    Public Property Code As String
    Public Property Name As String
End Class
Public Class CGetReturnBankCode
    Public Function FGetBankCode() As ReturnBankCode
        Try
            Dim ls_acc As New List(Of BankCode)
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
                strSql = "SELECT ""BankCode"",""BankName"" FROM " & _DBNAME & ".""ODSC"" WHERE ""Locked""='N' Order By ""BankCode"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls_acc.Add(New BankCode With {
                        .Code = oRs.Fields.Item("BankCode").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("BankName").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBankCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls = ls_acc
                    })
            Else
                Return (New ReturnBankCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBankCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls = Nothing
                   })
        End Try
    End Function

End Class

