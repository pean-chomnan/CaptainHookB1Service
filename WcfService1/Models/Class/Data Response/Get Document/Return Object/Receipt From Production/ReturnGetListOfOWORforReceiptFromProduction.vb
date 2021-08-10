Public Class ReturnGetListOfOWORforReceiptFromProduction
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of OWORforReceiptFromProduction)
End Class

Public Class OWORforReceiptFromProduction
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
    Public Property StartDate As Date
    Public Property Priority As Double
    Public Property Status As String

    Public Property Warehouse As String
    Public Property OcrCode As String
    Public Property OcrCode2 As String
    Public Property OcrCode3 As String
    Public Property OcrCode4 As String
    Public Property OcrCode5 As String
    Public Property PlannedQty As Double
    Public Property AvaibleReceipt As Double
    Public Property OnHand As Double
    Public Property IsCommited As Double
    Public Property OnOrder As Double
    Public Property StockAvaible As Double
    Public Property CmpltQty As Double
    Public Property RjctQty As Double
    Public Property Expirydate As Integer
    Public Property UOM As String
End Class

Public Class GetListOfOWORforReceiptFromProduction
    Public Function Execute() As ReturnGetListOfOWORforReceiptFromProduction
        Try
            Dim ls As New List(Of OWORforReceiptFromProduction)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            Dim _type As Integer = 0
            Dim oLoginService As New LoginServiceWebRef

            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "CALL " & _DBNAME & ".""USP_LoadProductionOrderAvaibableReceiptFromProduction""()"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New OWORforReceiptFromProduction With {
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
                        .StartDate = oRs.Fields.Item("StartDate").Value,
                        .Priority = oRs.Fields.Item("Priority").Value,
                        .Status = oRs.Fields.Item("Status").Value,
                        .Warehouse = oRs.Fields.Item("Warehouse").Value,
                        .OcrCode = oRs.Fields.Item("OcrCode").Value,
                        .OcrCode2 = oRs.Fields.Item("OcrCode2").Value,
                        .OcrCode3 = oRs.Fields.Item("OcrCode3").Value,
                        .OcrCode4 = oRs.Fields.Item("OcrCode4").Value,
                        .OcrCode5 = oRs.Fields.Item("OcrCode5").Value,
                        .PlannedQty = oRs.Fields.Item("PlannedQty").Value,
                        .AvaibleReceipt = oRs.Fields.Item("AvaibleReceipt").Value,
                        .OnHand = oRs.Fields.Item("OnHand").Value,
                        .IsCommited = oRs.Fields.Item("IsCommited").Value,
                        .OnOrder = oRs.Fields.Item("OnOrder").Value,
                        .StockAvaible = oRs.Fields.Item("StockAvaible").Value,
                        .CmpltQty = oRs.Fields.Item("CmpltQty").Value,
                        .RjctQty = oRs.Fields.Item("RjctQty").Value,
                        .Expirydate = oRs.Fields.Item("Expirydate").Value,
                        .UOM = oRs.Fields.Item("Uom").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnGetListOfOWORforReceiptFromProduction With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnGetListOfOWORforReceiptFromProduction With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnGetListOfOWORforReceiptFromProduction With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


