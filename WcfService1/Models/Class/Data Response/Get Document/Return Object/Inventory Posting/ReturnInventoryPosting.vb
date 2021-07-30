Public Class ReturnInventoryPosting
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of InventoryPosting)
End Class

Public Class InventoryPosting
    Public Property UomEntry As Integer
    Public Property InventoryPosting As String
End Class

Public Class CReturnGetInventoryPosting
    Public Function FGetReturnInventoryPosting(ByVal ItemCode As String) As ReturnInventoryPosting
        Try
            Dim ls As New List(Of InventoryPosting)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT Distinct B.""UomEntry"",A.""InventoryPosting"" FROM (SELECT ""BuyUnitMsr"" As ""InventoryPosting"" FROM " & _DBNAME & ".""OITM"" WHERE ""ItemCode""='TC0005' AND ""UgpEntry""<>-1 UNION SELECT ""InvntryUom"" As ""InventoryPosting"" FROM " & _DBNAME & ".""OITM"" WHERE ""ItemCode""='TC0005'  AND ""UgpEntry""<>-1  UNION SELECT 'Manual' As ""InventoryPosting"" FROM " & _DBNAME & ".""OITM"" WHERE ""ItemCode""='TC0005'  AND ""UgpEntry""=-1) A INNER JOIN " & _DBNAME & ".""OUOM"" B On A.""InventoryPosting""=B.""InventoryPosting"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New InventoryPosting With {
                        .UomEntry = oRs.Fields.Item("UomEntry").Value.ToString.Trim,
                        .InventoryPosting = oRs.Fields.Item("InventoryPosting").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnInventoryPosting With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnInventoryPosting With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnInventoryPosting With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class
