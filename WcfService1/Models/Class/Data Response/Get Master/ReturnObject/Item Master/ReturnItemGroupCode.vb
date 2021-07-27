Public Class ReturnItemGroupCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ItemGroupCode)
End Class

Public Class ItemGroupCode
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnReturnItemGroupCode
    Public Function FGetReturnItemGroupCode() As ReturnItemGroupCode
        Try
            Dim ls As New List(Of ItemGroupCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""ItmsGrpCod"", ""ItmsGrpNam"" FROM " & _DBNAME & ".""OITB"" WHERE ""Locked""='N' ORDER BY ""ItmsGrpNam"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ItemGroupCode With {
                        .Code = oRs.Fields.Item("ItmsGrpCod").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("ItmsGrpNam").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnItemGroupCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnItemGroupCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnItemGroupCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class
