'SELECT ""WhsCode"",""WhsName"" FROM ""CAPTAINHOOK_PRD"".""OWHS"" WHERE ""Locked""='N'

Public Class ReturnWarehouse
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of Warehouse)
End Class

Public Class Warehouse
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnGetWarehouse
    Public Function FGetReturnWarehouse() As ReturnWarehouse
        Try
            Dim ls As New List(Of Warehouse)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""WhsCode"",""WhsName"" FROM " & _DBNAME & ".""OWHS"" WHERE ""Locked""='N'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New Warehouse With {
                        .Code = oRs.Fields.Item("WhsCode").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("WhsName").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnWarehouse With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnWarehouse With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnWarehouse With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class







