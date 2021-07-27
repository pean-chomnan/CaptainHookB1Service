Public Class ReturnDimension
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of Dimension)
End Class

Public Class Dimension
    Public Property OcrCode As String
    Public Property OcrName As String
    Public Property Dimension As String
End Class

Public Class CReturnGetDimension
    Public Function FGetReturnDimension(ByVal OneOrTwo As Integer) As ReturnDimension
        Try
            Dim ls As New List(Of Dimension)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""OcrCode"",""OcrName"",CASE ""DimCode"" WHEN 1 THEN 'PROFIT CENTER' ELSE 'DEPARTMENT' END AS ""Dimension"" FROM " & _DBNAME & ".""OOCR"" WHERE ""DimCode""=" & OneOrTwo
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New Dimension With {
                        .OcrCode = oRs.Fields.Item("OcrCode").Value.ToString.Trim,
                        .OcrName = oRs.Fields.Item("OcrName").Value.ToString.Trim,
                        .Dimension = oRs.Fields.Item("Dimension").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnDimension With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnDimension With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnDimension With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class







