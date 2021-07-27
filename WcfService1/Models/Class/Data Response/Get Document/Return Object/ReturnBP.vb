Public Class ReturnBP
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of BP)
End Class

Public Class BP
    Public Property CardCode As String
    Public Property CardName As String
    Public Property GroupName As String
    Public Property Balance As Double
    Public Property Phone1 As String
    Public Property ContactPerson As String
    Public Property LicTradNum As String
End Class

Public Class CReturnGetBP
    Public Function FGetReturnBP(ByVal CardType As String, ByVal SearchingBP As String) As ReturnBP
        Try
            Dim ls As New List(Of BP)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT TOP 50 A.""CardCode"",A.""CardName"",B.""GroupName"",A.""Balance"",A.""Phone1"",A.""CntctPrsn"",A.""LicTradNum"" FROM " & _DBNAME & ".""OCRD"" A INNER JOIN " & _DBNAME & ".""OCRG"" B ON A.""GroupCode""=B.""GroupCode"" WHERE A.""CardType""='" & CardType.ToUpper & "' AND (LOWER(A.""CardCode"") LIKE '%" & SearchingBP.ToLower & "%' OR LOWER(A.""CardName"") LIKE '%" & SearchingBP.ToLower & "%') ORDER BY A.""CardCode"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BP With {
                        .CardCode = oRs.Fields.Item("CardCode").Value.ToString.Trim,
                        .CardName = oRs.Fields.Item("CardName").Value.ToString.Trim,
                        .GroupName = oRs.Fields.Item("GroupName").Value.ToString.Trim,
                        .Balance = oRs.Fields.Item("Balance").Value.ToString.Trim,
                        .Phone1 = oRs.Fields.Item("Phone1").Value.ToString.Trim,
                        .ContactPerson = oRs.Fields.Item("CntctPrsn").Value.ToString.Trim,
                        .LicTradNum = oRs.Fields.Item("LicTradNum").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBP With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBP With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBP With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class

