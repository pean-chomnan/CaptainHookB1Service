Public Class ReturnFirmCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of FirmCode)
End Class

Public Class FirmCode
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnFirmCode
    Public Function FGetReturnFirmCode() As ReturnFirmCode
        Try
            Dim ls As New List(Of FirmCode)
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
                    ls.Add(New FirmCode With {
                        .Code = oRs.Fields.Item("FirmCode").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("FirmName").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnFirmCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnFirmCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnFirmCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function

End Class

