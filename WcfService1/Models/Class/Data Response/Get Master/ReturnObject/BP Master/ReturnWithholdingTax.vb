
Public Class ReturnWithholdingTax
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls As List(Of WithholdingTax)
End Class

Public Class WithholdingTax
    Public Property Code As String
    Public Property Name As String
    Public Property Rate As Double
End Class
Public Class CGetReturnWithholdingTax
    Public Function FGetWithholdingTax() As ReturnWithholdingTax
        Try
            Dim ls_acc As New List(Of WithholdingTax)
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
                strSql = "SELECT ""WTCode"",""WTName"",""Rate"" FROM " & _DBNAME & ".""OWHT"" WHERE ""Inactive""='N' ORDER BY ""WTCode"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls_acc.Add(New WithholdingTax With {
                        .Code = oRs.Fields.Item("WTCode").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("WTName").Value.ToString.Trim,
                        .Rate = oRs.Fields.Item("Rate").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnWithholdingTax With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls = ls_acc
                    })
            Else
                Return (New ReturnWithholdingTax With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnWithholdingTax With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls = Nothing
                   })
        End Try
    End Function

End Class
