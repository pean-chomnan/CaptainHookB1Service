Public Class ReturnBatchByBoxNumber
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of BatchNumber)
End Class

Public Class CReturnGetBatchNumber
    Public Function FGetReturnBatchByBoxNumber(ByVal WhsCode As String, ByVal ItemCode As String, ByVal BoxNumber As String) As ReturnBatchByBoxNumber
        Try
            Dim ls As New List(Of BatchNumber)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_GetBatchByBoxNumber""('" & WhsCode & "','" & ItemCode & "','" & BoxNumber & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New BatchNumber With {
                        .BatchNo = oRs.Fields.Item("DistNumber").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBatchByBoxNumber With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnBatchByBoxNumber With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBatchByBoxNumber With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class
