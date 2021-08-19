Public Class ReturnItemCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ItemCode)
End Class

Public Class ItemCode
    Public Property ItemCode As String
    Public Property Descrition As String
    Public Property OnHand As Double
    Public Property ItemGroupID As String
    Public Property UserText As String
End Class

Public Class CReturnGetItemCode
    Public Function FGetReturnItem(ByVal SearchingItem As String) As ReturnItemCode
        Try
            Dim ls As New List(Of ItemCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT TOP 50 ""ItemCode"",""ItemName"",""OnHand"",""ItmsGrpCod"",""UserText"" FROM " & _DBNAME & ".""OITM"" WHERE LOWER(""ItemCode"") LIKE '%" & SearchingItem.ToLower & "%' OR LOWER(""ItemName"") LIKE '%" & SearchingItem.ToLower & "%' ORDER BY ""ItemCode"""

                If SearchingItem = "" Then
                    strSql = "SELECT TOP 50 ""ItemCode"",""ItemName"",""OnHand"",""ItmsGrpCod"",""UserText"" FROM " & _DBNAME & ".""OITM"" ORDER BY ""ItemCode"""

                End If


                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ItemCode With {
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .Descrition = oRs.Fields.Item("ItemName").Value.ToString.Trim,
                        .OnHand = oRs.Fields.Item("OnHand").Value.ToString.Trim,
                        .ItemGroupID = oRs.Fields.Item("ItmsGrpCod").Value.ToString.Trim,
                        .UserText = oRs.Fields.Item("UserText").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnItemCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnItemCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnItemCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class

