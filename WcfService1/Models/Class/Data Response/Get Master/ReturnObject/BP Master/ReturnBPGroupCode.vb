Public Class ReturnBPGroup
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_BPGroupCode As List(Of GroupCode)
End Class

Public Class GroupCode
    Public Property GroupCode As Integer
    Public Property GroupName As String
End Class
Public Class GetBPGroupCode
    Public Function GetBPGroupCode(ByVal Type As String) As ReturnBPGroup
        Try
            Dim listGroupCode As New List(Of GroupCode)
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            Dim Table As String = ""
            Dim _type As Integer = 0
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim oLoginService As New LoginServiceWebRef
            '   Dim listItemCode As New List(Of GetItemMaster)

            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""GroupCode"",""GroupName"" FROM " & _DBNAME & ".""OCRG"" Where ""Locked""='N' AND ""GroupType""='" & Type.ToUpper & "' Order By ""GroupCode"""
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    listGroupCode.Add(New GroupCode With {
                        .GroupCode = oRs.Fields.Item("GroupCode").Value.ToString.Trim,
                        .GroupName = oRs.Fields.Item("GroupName").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New ReturnBPGroup With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_BPGroupCode = listGroupCode
                    })
            Else
                Return (New ReturnBPGroup With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_BPGroupCode = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnBPGroup With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_BPGroupCode = Nothing
                   })
        End Try
    End Function

End Class
