Public Class ReturnOrderInterval
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of OrderInterval)
End Class

Public Class OrderInterval
    Public Property Code As String
    Public Property Name As String
    Public Property Day As Double
End Class

Public Class CReturnOrderInterval
    Public Function FGetReturnOrderInterval() As ReturnOrderInterval
        Try
            Dim ls As New List(Of OrderInterval)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""Code"",""Name"",""Day"" FROM " & _DBNAME & ".""OCYC"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New OrderInterval With {
                        .Code = oRs.Fields.Item("Code").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("Name").Value.ToString.Trim,
                        .Day = oRs.Fields.Item("Day").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnOrderInterval With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnOrderInterval With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnOrderInterval With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function

End Class
