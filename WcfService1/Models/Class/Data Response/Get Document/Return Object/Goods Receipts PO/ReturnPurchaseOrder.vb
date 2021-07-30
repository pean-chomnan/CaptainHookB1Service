Public Class ReturnPurchaseOrder
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of PurchaseOrder)
End Class

Public Class PurchaseOrder
    Public Property DocEntry As Integer
    Public Property CardCode As String
    Public Property CardName As String
    Public Property ContactName As String
    Public Property NumAtCard As String
    Public Property CurSource As String
    Public Property Series As Integer
    Public Property SeriesName As String
    Public Property DocNum As Integer
    Public Property DocDate As Date
    Public Property DocDueDate As Date
    Public Property TaxDate As Date
    Public Property TotalBFDiscount As Double
    Public Property DiscPrcnt As Double
    Public Property DiscSum As Double
    Public Property DocTotal As Double
    Public Property ItemCode As String
    Public Property CodeBars As String
    Public Property Quantity As Double
    Public Property Price As Double
    Public Property LineDiscPrcnt As Double
    Public Property VatGroup As String
    Public Property LineTotal As Double
    Public Property WhsCode As String
    Public Property OcrCode As String
    Public Property OcrCode2 As String
    Public Property UomCode As String
    Public Property LineNum As Integer
    Public Property ShipTo As String

End Class

Public Class CReturnGetPurchaseOrder
    Public Function FGetReturnPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
        Try
            Dim ls As New List(Of PurchaseOrder)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_GetPO""(" & DocNum & ")"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New PurchaseOrder With {
                        .DocEntry = oRs.Fields.Item("DocEntry").Value,
                        .CardCode = oRs.Fields.Item("CardCode").Value.ToString.Trim,
                        .CardName = oRs.Fields.Item("CardName").Value.ToString.Trim,
                        .ContactName = oRs.Fields.Item("ContactName").Value.ToString.Trim,
                        .NumAtCard = oRs.Fields.Item("NumAtCard").Value.ToString.Trim,
                        .CurSource = oRs.Fields.Item("CurSource").Value.ToString.Trim,
                        .Series = oRs.Fields.Item("Series").Value.ToString.Trim,
                        .SeriesName = oRs.Fields.Item("SeriesName").Value.ToString.Trim,
                        .DocNum = oRs.Fields.Item("DocNum").Value,
                        .DocDate = oRs.Fields.Item("DocDate").Value,
                        .DocDueDate = oRs.Fields.Item("DocDueDate").Value,
                        .TaxDate = oRs.Fields.Item("TaxDate").Value,
                        .TotalBFDiscount = oRs.Fields.Item("TotalBFDiscount").Value,
                        .DiscPrcnt = oRs.Fields.Item("DiscPrcnt").Value,
                        .DiscSum = oRs.Fields.Item("DiscSum").Value,
                        .DocTotal = oRs.Fields.Item("DocTotal").Value,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .CodeBars = oRs.Fields.Item("CodeBars").Value.ToString.Trim,
                        .Quantity = oRs.Fields.Item("Quantity").Value,
                        .Price = oRs.Fields.Item("Price").Value,
                        .LineDiscPrcnt = oRs.Fields.Item("LineDiscPrcnt").Value,
                        .VatGroup = oRs.Fields.Item("VatGroup").Value.ToString.Trim,
                        .LineTotal = oRs.Fields.Item("LineTotal").Value,
                        .WhsCode = oRs.Fields.Item("WhsCode").Value.ToString.Trim,
                        .OcrCode = oRs.Fields.Item("OcrCode").Value.ToString.Trim,
                        .OcrCode2 = oRs.Fields.Item("OcrCode2").Value.ToString.Trim,
                        .UomCode = oRs.Fields.Item("UomCode").Value.ToString.Trim,
                        .LineNum = oRs.Fields.Item("LineNum").Value,
                        .ShipTo = oRs.Fields.Item("ShipTo").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnPurchaseOrder With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnPurchaseOrder With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnPurchaseOrder With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class







