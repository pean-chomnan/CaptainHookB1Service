Public Class ReturnGetListOfOWORforReturnComponent
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of OWORforReturnComponent)
End Class

Public Class OWORforReturnComponent
    Public Property DocEntry As Integer
    Public Property DocNum As Integer
    Public Property Series As Integer
    Public Property SeriesName As String
    Public Property Type As String
    Public Property PostDate As Date
    Public Property DueDate As Date
    Public Property ProductNo As String
    Public Property ProductName As String
    Public Property Comments As String
    Public Property Expirydate As Integer
End Class

Public Class GetListOfOWORforReturnComponent
    Public Function Execute() As ReturnGetListOfOWORforReturnComponent
        Try
            Dim ls As New List(Of OWORforReturnComponent)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_LoadIssueForProductionToReceiptFromProduction""()"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New OWORforReturnComponent With {
                        .DocEntry = oRs.Fields.Item("DocEntry").Value,
                        .DocNum = oRs.Fields.Item("DocNum").Value,
                        .Series = oRs.Fields.Item("Series").Value,
                        .SeriesName = oRs.Fields.Item("SeriesName").Value.ToString.Trim,
                        .Type = oRs.Fields.Item("Type").Value.ToString.Trim,
                        .PostDate = oRs.Fields.Item("PostDate").Value,
                        .DueDate = oRs.Fields.Item("DueDate").Value,
                        .ProductNo = oRs.Fields.Item("ProductNo").Value.ToString.Trim,
                        .ProductName = oRs.Fields.Item("ProdName").Value.ToString.Trim,
                        .Comments = oRs.Fields.Item("Comments").Value.ToString.Trim,
                        .Expirydate = oRs.Fields.Item("Expirydate").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnGetListOfOWORforReturnComponent With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnGetListOfOWORforReturnComponent With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnGetListOfOWORforReturnComponent With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


