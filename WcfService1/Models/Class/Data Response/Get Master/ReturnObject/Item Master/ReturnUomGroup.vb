Public Class ReturnUomGroup
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of UomGroup)
End Class

Public Class UomGroup
    Public Property Code As String
    Public Property Name As String
End Class

Public Class CReturnUomGroup
    Public Function FGetReturnUomGroup() As ReturnUomGroup
        Try
            Dim ls As New List(Of UomGroup)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""UgpEntry"",""UgpName"" FROM " & _DBNAME & ".""OUGP"" WHERE ""Locked""='N'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New UomGroup With {
                        .Code = oRs.Fields.Item("UgpEntry").Value.ToString.Trim,
                        .Name = oRs.Fields.Item("UgpName").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnUomGroup With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnUomGroup With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnUomGroup With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class




