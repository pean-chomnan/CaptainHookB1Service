Public Class ReturnShipTo
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ShipTo)
End Class

Public Class ShipTo
    Public Property Code As String
End Class

Public Class CReturnGetShipTo
    Public Function FGetReturnShipTo(ByVal CardCode As String) As ReturnShipTo
        Try
            Dim ls As New List(Of ShipTo)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT '' AS ""Address"" FROM DUMMY UNION SELECT ""Address"" FROM " & _DBNAME & ".""CRD1"" WHERE ""CardCode""='" & CardCode & "' AND ""AdresType""='S'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ShipTo With {
                        .Code = oRs.Fields.Item("Address").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnShipTo With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnShipTo With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnShipTo With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


