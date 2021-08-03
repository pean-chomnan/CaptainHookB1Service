Public Class ReturnProjectCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ProjectCode)
End Class

Public Class ProjectCode
    Public Property Code As String
    Public Property Name As String
    Public Property ValidFrom As Date
    Public Property ValidTo As Date
End Class

Public Class CReturnProjectCode
    Public Function FGetProjectCode() As ReturnProjectCode
        Try
            Dim ls As New List(Of ProjectCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""PrjCode"" As ""Code"",""PrjName"",""ValidFrom"",""ValidTo"" FROM " & _DBNAME & ".""OPRJ"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ProjectCode With {
                        .Code = oRs.Fields.Item("Code").Value,
                        .Name = oRs.Fields.Item("PrjName").Value,
                        .ValidFrom = oRs.Fields.Item("ValidFrom").Value,
                        .ValidTo = oRs.Fields.Item("ValidTo").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnProjectCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnProjectCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnProjectCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class



