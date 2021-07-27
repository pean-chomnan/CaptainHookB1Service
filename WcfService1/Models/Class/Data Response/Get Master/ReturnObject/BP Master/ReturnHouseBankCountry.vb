Public Class ReturnHouseBankCountry
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls As List(Of BPCountry)
End Class

Public Class CGetReturnHouseBankCountry
    Public Function FGetHouseBankCountry() As ReturnHouseBankCountry
        Try
            Dim ls_acc As New List(Of BPCountry)
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
                strSql = "SELECT ""Code"",""Name"" FROM " & _DBNAME & ".""OCRY"" WHERE ""Code"" IN(SELECT ""CountryCod"" FROM " & _DBNAME & ".""ODSC"" WHERE ""Locked""='N') Order By ""Name"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls_acc.Add(New BPCountry With {
                        .Code = oRs.Fields.Item("Code").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("Name").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnHouseBankCountry With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls = ls_acc
                    })
            Else
                Return (New ReturnHouseBankCountry With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnHouseBankCountry With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls = Nothing
                   })
        End Try
    End Function

End Class
