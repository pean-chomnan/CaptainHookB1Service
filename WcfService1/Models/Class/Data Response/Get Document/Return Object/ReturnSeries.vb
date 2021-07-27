Public Class ReturnSeries
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of Series)
End Class

Public Class Series
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnSeries
    Public Function FGetSeries(ByVal ObjectType As String, ByVal PostingDate As Date) As ReturnSeries
        Try
            Dim ls As New List(Of Series)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT DISTINCT A.""Series"", A.""SeriesName"" FROM  " & _DBNAME & ".""NNM1"" A INNER JOIN  " & _DBNAME & ".""OFPR"" B ON A.""Indicator"" = B.""Indicator"" WHERE A.""ObjectCode"" = '" & ObjectType & "' AND '" & PostingDate.ToString("yyyyMMdd") & "' BETWEEN B.""F_RefDate"" AND ""T_RefDate"" ORDER BY A.""Series"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New Series With {
                        .Code = oRs.Fields.Item("Series").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("SeriesName").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnSeries With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnSeries With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnSeries With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function

End Class



