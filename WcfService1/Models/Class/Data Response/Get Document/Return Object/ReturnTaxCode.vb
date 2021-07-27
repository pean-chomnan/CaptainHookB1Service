Public Class ReturnTaxCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of TaxCode)
End Class

Public Class TaxCode
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnGetTaxCode
    Public Function FGetReturnTaxCode(ByVal IorO As String) As ReturnTaxCode
        Try
            Dim ls As New List(Of TaxCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""Code"",""Name"",""Rate"" FROM " & _DBNAME & ".""OVTG"" WHERE ""Category""='" & IorO & "' AND ""Locked""='N' AND ""Account"" IS NOT NULL"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New TaxCode With {
                        .Code = oRs.Fields.Item("Code").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("Name").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnTaxCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnTaxCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnTaxCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class






