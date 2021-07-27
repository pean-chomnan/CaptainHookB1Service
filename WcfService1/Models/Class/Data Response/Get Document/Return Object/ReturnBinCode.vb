
Public Class ReturnBinCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of BinCode)
End Class

Public Class BinCode
    Public Property Code As Integer
    Public Property Name As String
End Class

Public Class CReturnGetBinCode
    Public Function FGetReturnBinCode(ByVal WarehouseCode As String) As ReturnBinCode
        Try
            Dim ls As New List(Of BinCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""AbsEntry"",""BinCode"" FROM " & _DBNAME & ".""OBIN""  WHERE ""WhsCode""='" & WarehouseCode & "'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BinCode With {
                        .Code = oRs.Fields.Item("AbsEntry").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("BinCode").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBinCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBinCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBinCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


