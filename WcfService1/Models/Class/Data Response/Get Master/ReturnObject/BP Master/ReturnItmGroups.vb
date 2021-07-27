Public Class ReturnItmGroups
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_ItmGrp As List(Of MasterClass.OITB)
    Public Sub getItmGroup(ByVal Type As EnumClass.Type)
        Try
            Dim ItmGrp As WcfService1.MasterClass.OITB
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            ls_ItmGrp = New List(Of MasterClass.OITB)

            Dim _type As Integer = 0
            If Type = EnumClass.Type.AddNew Then
                _type = 1
            Else
                _type = 0
            End If
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & """" & _DBNAME & """" & "._USP_GETMASTERDATA('OITB'," & _type & ")"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ItmGrp = New MasterClass.OITB With {
                        .ItmGroupCode = oRs.Fields.Item(0).Value.ToString.Trim,
                        .itmGroupName = oRs.Fields.Item(1).Value.ToString.Trim
                    }
                    ls_ItmGrp.Add(ItmGrp)
                    oRs.MoveNext()
                Loop
                ErrCode = 0
                ErrMsg = ""
            Else
                ErrCode = oLoginService.lErrCode
                ErrMsg = oLoginService.sErrMsg
                ls_ItmGrp = Nothing
            End If
        Catch ex As Exception
            ErrCode = ex.HResult
            ErrMsg = ex.Message.ToString()
            ls_ItmGrp = Nothing
        End Try
    End Sub

End Class
