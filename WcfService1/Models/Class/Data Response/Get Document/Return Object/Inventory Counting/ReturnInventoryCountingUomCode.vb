Public Class ReturnInventoryCountingUomCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of InventoryCountingUomCode)
End Class

Public Class InventoryCountingUomCode
    Public Property UomEntry As Integer
    Public Property UomCode As String
End Class

Public Class CReturnGetInventoryCountingUomCode
    Public Function FGetReturnInventoryCountingUomCode(ByVal ItemCode As String) As ReturnInventoryCountingUomCode
        Try
            Dim ls As New List(Of InventoryCountingUomCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim myClasss As New myClassOfFuntion
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            Dim UgpEntry As Integer = 0
            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                UgpEntry = myClasss.GetValFromQueryReturnNumberOCompany("SELECT ""UgpEntry"" FROM " & _DBNAME & ".""OITM"" WHERE ""ItemCode""='" & ItemCode & "'", oCompany)
                strSql = "SELECT C.""UomEntry"",C.""UomCode"" FROM " & _DBNAME & ".""OUGP"" A INNER JOIN " & _DBNAME & ".""UGP1"" B ON A.""UgpEntry""=B.""UgpEntry"" INNER JOIN " & _DBNAME & ".""OUOM"" C ON B.""UomEntry""=C.""UomEntry"" WHERE A.""UgpEntry""=" & UgpEntry & ";"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New InventoryCountingUomCode With {
                        .UomEntry = oRs.Fields.Item("UomEntry").Value.ToString.Trim,
                        .UomCode = oRs.Fields.Item("UomCode").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnInventoryCountingUomCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnInventoryCountingUomCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnInventoryCountingUomCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class




