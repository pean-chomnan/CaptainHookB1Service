Public Class ReturnAvaibleStock
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of AvaibleStock)
End Class

Public Class AvaibleStock
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property Warehouse As String
    Public Property Quantity As Double

End Class

Public Class CReturnAvaibleStock
    Public Function FGetReturnAvaibleStock(ByVal ItemCode As String, ByVal WhsCode As String) As ReturnAvaibleStock
        Try
            Dim ls As New List(Of AvaibleStock)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT T0.""ItemCode"",T0.""WhsCode"",T0.""OnHand"" FROM " & _DBNAME & ".""OITW"" T0 WHERE ""ItemCode""='" & ItemCode & "' AND ""WhsCode""='" & WhsCode & "'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New AvaibleStock With {
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .ItemName = oRs.Fields.Item("WhsCode").Value.ToString.Trim,
                        .Quantity = oRs.Fields.Item("OnHand").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnAvaibleStock With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnAvaibleStock With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnAvaibleStock With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class




