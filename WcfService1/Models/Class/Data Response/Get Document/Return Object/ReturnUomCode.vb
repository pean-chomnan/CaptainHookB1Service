Public Class ReturnUomCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of UomCode)
End Class

Public Class UomCode
    Public Property UomEntry As Integer
    Public Property UomCode As String
End Class

Public Class CReturnGetUomCode
    Public Function FGetReturnUomCode(ByVal ItemCode As String) As ReturnUomCode
        Try
            Dim ls As New List(Of UomCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT Distinct B.""UomEntry"",A.""UomCode"" FROM (SELECT ""BuyUnitMsr"" As ""UomCode"" FROM " & _DBNAME & ".""OITM"" WHERE ""ItemCode""='" & ItemCode & "' AND ""UgpEntry""<>-1 UNION SELECT ""InvntryUom"" As ""UomCode"" FROM " & _DBNAME & ".""OITM"" WHERE ""ItemCode""='" & ItemCode & "'  AND ""UgpEntry""<>-1  UNION SELECT 'Manual' As ""UomCode"" FROM " & _DBNAME & ".""OITM"" WHERE ""ItemCode""='" & ItemCode & "'  AND ""UgpEntry""=-1) A INNER JOIN " & _DBNAME & ".""OUOM"" B On A.""UomCode""=B.""UomCode"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New UomCode With {
                        .UomEntry = oRs.Fields.Item("UomEntry").Value.ToString.Trim,
                        .UomCode = oRs.Fields.Item("UomCode").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnUomCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnUomCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnUomCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class






