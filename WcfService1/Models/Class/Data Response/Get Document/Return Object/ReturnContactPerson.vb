Public Class ReturnContactPerson
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ContactPerson)
End Class

Public Class ContactPerson
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnGetContactPerson
    Public Function FGetReturnContactPerson(ByVal CardCode As String) As ReturnContactPerson
        Try
            Dim ls As New List(Of ContactPerson)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""CntctCode"",""Name"" FROM " & _DBNAME & ".""OCPR"" WHERE ""CardCode""='" & CardCode & "'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ContactPerson With {
                        .Code = oRs.Fields.Item("CntctCode").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("Name").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnContactPerson With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnContactPerson With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnContactPerson With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class

