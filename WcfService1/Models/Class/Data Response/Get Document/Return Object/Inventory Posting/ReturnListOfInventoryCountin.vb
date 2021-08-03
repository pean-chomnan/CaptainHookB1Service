Public Class ReturnListOfInventoryCountin
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ListOfInventoryCountin)
End Class

Public Class ListOfInventoryCountin
    Public Property DocEntry As Integer
    Public Property DocNum As Integer
    Public Property CountDate As Date
    Public Property Time As String
    Public Property CountType As Integer
    Public Property Remark As String

End Class

Public Class CReturnGetListOfInventoryCountin
    Public Function FGetReturnListOfInventoryCountin() As ReturnListOfInventoryCountin
        Try
            Dim ls As New List(Of ListOfInventoryCountin)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT T0.""DocEntry"", T0.""DocNum"", T0.""CountDate"", T0.""Time"", T0.""CountType"", T0.""Remarks"" FROM " & _DBNAME & ".""OINC"" T0 WHERE T0.""Status"" = ('O')  AND  (T0.""WddStatus"" = ('-')  OR  T0.""WddStatus"" = ('P')  OR  T0.""WddStatus"" = ('A') )  ORDER BY T0.""DocNum"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ListOfInventoryCountin With {
                        .DocEntry = oRs.Fields.Item("DocEntry").Value,
                        .DocNum = oRs.Fields.Item("DocNum").Value,
                        .CountDate = oRs.Fields.Item("CountDate").Value,
                        .Time = oRs.Fields.Item("Time").Value,
                        .CountType = oRs.Fields.Item("CountType").Value,
                        .Remark = oRs.Fields.Item("Remarks").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnListOfInventoryCountin With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnListOfInventoryCountin With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnListOfInventoryCountin With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class
