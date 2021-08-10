Public Class ReturnListOfSalesOrder
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ListOfSalesOrder)
End Class

Public Class ListOfSalesOrder

    Public Property Series As Integer
    Public Property SeriesName As String
    Public Property DocNum As Integer
    Public Property DocDate As Date
    Public Property DocDueDate As Date
    Public Property TaxDate As Date
    Public Property CardCode As String
    Public Property CardName As String
    Public Property ContactName As String
    Public Property NumAtCard As String
    Public Property CurSource As String
    Public Property TotalBFDiscount As Double
    Public Property DiscPrcnt As Double
    Public Property DiscSum As Double
    Public Property DocTotal As Double
    Public Property Remark As String

End Class

Public Class CReturnGetListOfSalesOrder
    Public Function FGetReturnListOfSalesOrder() As ReturnListOfSalesOrder
        Try
            Dim ls As New List(Of ListOfSalesOrder)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_LisOfGetSO""()"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ListOfSalesOrder With {
                        .Series = oRs.Fields.Item("Series").Value.ToString.Trim,
                        .SeriesName = oRs.Fields.Item("SeriesName").Value.ToString.Trim,
                        .DocNum = oRs.Fields.Item("DocNum").Value.ToString.Trim,
                        .DocDate = oRs.Fields.Item("DocDate").Value.ToString.Trim,
                        .DocDueDate = oRs.Fields.Item("DocDueDate").Value.ToString.Trim,
                        .TaxDate = oRs.Fields.Item("TaxDate").Value.ToString.Trim,
                        .CardCode = oRs.Fields.Item("CardCode").Value.ToString.Trim,
                        .CardName = oRs.Fields.Item("CardName").Value.ToString.Trim,
                        .ContactName = oRs.Fields.Item("ContactName").Value.ToString.Trim,
                        .NumAtCard = oRs.Fields.Item("NumAtCard").Value.ToString.Trim,
                        .CurSource = oRs.Fields.Item("CurSource").Value.ToString.Trim,
                        .TotalBFDiscount = oRs.Fields.Item("TotalBFDiscount").Value.ToString.Trim,
                        .DiscPrcnt = oRs.Fields.Item("DiscPrcnt").Value.ToString.Trim,
                        .DiscSum = oRs.Fields.Item("DiscSum").Value.ToString.Trim,
                        .DocTotal = oRs.Fields.Item("DocTotal").Value.ToString.Trim,
                        .Remark = oRs.Fields.Item("Comments").Value.ToString.Trim
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnListOfSalesOrder With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnListOfSalesOrder With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnListOfSalesOrder With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class

