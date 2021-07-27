Public Class ReturnBarCode
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of BarCode)
End Class

Public Class BarCode
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnGetBarCode
    Public Function FGetReturnBarCode(ByVal ItemCode As String) As ReturnBarCode
        Try
            Dim ls As New List(Of BarCode)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""BcdCode"",""BcdName"" FROM " & _DBNAME & ".""OBCD"" WHERE ""ItemCode""='" & ItemCode & "'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BarCode With {
                        .Code = oRs.Fields.Item("BcdCode").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("BcdName").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBarCode With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBarCode With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBarCode With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


