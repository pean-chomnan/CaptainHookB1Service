Public Class ReturnPaymentMeothod
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls As List(Of PaymentMeothod)
End Class

Public Class PaymentMeothod
    Public Property Code As String
    Public Property Name As String
End Class
Public Class CGetReturnPaymentMeothod
    Public Function FGetPaymentMoethod(ByVal IorO As String) As ReturnPaymentMeothod
        Try
            Dim ls_acc As New List(Of PaymentMeothod)
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
                strSql = "SELECT ""PayMethCod"",""Descript"" FROM " & _DBNAME & ".""OPYM"" WHERE ""Type""='" & IorO & "' ORDER BY ""PayMethCod"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls_acc.Add(New PaymentMeothod With {
                        .Code = oRs.Fields.Item("PayMethCod").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("Descript").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnPaymentMeothod With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls = ls_acc
                    })
            Else
                Return (New ReturnPaymentMeothod With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnPaymentMeothod With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls = Nothing
                   })
        End Try
    End Function

End Class


'SELECT ""PayMethCod"",""Descript"" FROM CAPTAINHOOK_PRD.""OPYM"" WHERE ""Type""='O'
