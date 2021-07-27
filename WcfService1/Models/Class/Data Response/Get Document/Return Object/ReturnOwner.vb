
Public Class ReturnOwner
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of Owner)
End Class

Public Class Owner
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnGetOwner
    Public Function FGetReturnOwner() As ReturnOwner
        Try
            Dim ls As New List(Of Owner)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""empID"",""lastName"" ||' ,' || ""firstName"" As ""EmpName"" FROM " & _DBNAME & ".""OHEM"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New Owner With {
                        .Code = oRs.Fields.Item("empID").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("EmpName").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnOwner With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnOwner With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnOwner With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


