Public Class ReturnItemOnHandPerWhsOrBIN
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ItemOnHandPerWhsOrBIN)
End Class

Public Class ItemOnHandPerWhsOrBIN
    Public Property OnHand As Double
End Class

Public Class CReturnGetItemOnHandPerWhsOrBIN
    Public Function FGetReturnItemOnHandPerWhsOrBIN(ByVal ItemCode As String, ByVal Warehouse As String, Optional BinCode As String = "") As ReturnItemOnHandPerWhsOrBIN
        Try
            Dim ls As New List(Of ItemOnHandPerWhsOrBIN)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim myClasss As New myClassOfFuntion
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            Dim _type As Integer = 0
            Dim BinEntry As Integer = 0
            Dim WhsManagByBin As String = ""
            Dim oLoginService As New LoginServiceWebRef

            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                WhsManagByBin = myClasss.GetValFromQueryOCompany("SELECT ""BinActivat"" FROM " & _DBNAME & ".""OWHS"" WHERE ""WhsCode""='" & Warehouse & "'", oCompany)

                If WhsManagByBin = "Y" Then
                    BinEntry = myClasss.GetValFromQueryReturnNumberOCompany("SELECT ""AbsEntry"" FROM " & _DBNAME & ".""OBIN"" WHERE ""BinCode""='" & BinCode & "'", oCompany)
                    strSql = "SELECT T0.""OnHandQty"" FROM  " & _DBNAME & ".""OIBQ"" T0  WHERE T0.""ItemCode"" = ('" & ItemCode & "')  AND  T0.""WhsCode"" = ('" & Warehouse & "')  AND  T0.""BinAbs"" = (" & BinEntry & ")"
                Else
                    strSql = "SELECT T0.""OnHandQty"" FROM  " & _DBNAME & ".""OIBQ"" T0  WHERE T0.""ItemCode"" = ('" & ItemCode & "')  AND  T0.""WhsCode"" = ('" & Warehouse & "')"
                End If
                
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ItemOnHandPerWhsOrBIN With {
                        .OnHand = oRs.Fields.Item("OnHandQty").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnItemOnHandPerWhsOrBIN With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnItemOnHandPerWhsOrBIN With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnItemOnHandPerWhsOrBIN With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class






