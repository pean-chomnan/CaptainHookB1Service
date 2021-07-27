Public Class ReturnShipToAddress
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ShipToAddress)
End Class

Public Class ShipToAddress
    Public Property Address As String
End Class

Public Class CReturnGetShipToAddress
    Public Function FGetReturnShipToAddress(ByVal CardCode As String, ByVal ShipTo As String) As ReturnShipToAddress
        Try
            Dim ls As New List(Of ShipToAddress)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""Street"" ||' '|| ""City"" ||' '|| ""ZipCode"" ||' '|| B.""Name"" As ""Address"" FROM " & _DBNAME & ".""CRD1"" A LEFT OUTER JOIN " & _DBNAME & ".""OCRY"" B ON A.""Country""=B.""Code"" WHERE ""CardCode""='" & CardCode & "' AND ""AdresType""='S' AND ""Address""='" & ShipTo & "'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ShipToAddress With {
                        .Address = oRs.Fields.Item("Address").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnShipToAddress With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnShipToAddress With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnShipToAddress With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


