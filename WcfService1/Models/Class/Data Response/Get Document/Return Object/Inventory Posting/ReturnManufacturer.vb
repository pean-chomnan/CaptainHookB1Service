Public Class ReturnManufacturer
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of Manufacturer)
End Class

Public Class Manufacturer
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnManufacturer
    Public Function FGetManufacturer() As ReturnManufacturer
        Try
            Dim ls As New List(Of Manufacturer)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""FirmCode"",""FirmName"" FROM " & _DBNAME & ".""OMRC"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New Manufacturer With {
                        .Code = oRs.Fields.Item("FirmCode").Value,
                        .Name = oRs.Fields.Item("FirmName").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnManufacturer With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnManufacturer With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnManufacturer With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class



