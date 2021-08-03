
Public Class ReturnGetListOfReturnComponent
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ReturnComponent)
End Class

Public Class ReturnComponent
    Public Property Series As Integer
    Public Property SeriesName As String
    Public Property DocEntry As Integer
    Public Property DocNum As Integer
    Public Property LineNum As Integer
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property ItemType As String
    Public Property IssuedQty As Double
    Public Property wareHouse As String
    Public Property PlannedQty As Double
    Public Property AvaibleIssue As Double
    Public Property OnHand As Double
    Public Property IsCommited As Double
    Public Property OnOrder As Double
    Public Property StockAvaible As Double
    Public Property Type As String
    Public Property StartDate As Date
    Public Property EndDate As Date
    Public Property SeqNum As Integer
    Public Property Code As String
    Public Property Name As String
    Public Property OcrCode As String
    Public Property OcrCode2 As String
    Public Property OcrCode3 As String
    Public Property OcrCode4 As String
    Public Property OcrCode5 As String
    Public Property Expirydate As Integer
End Class

Public Class GetListOfReturnComponent
    Public Function Execute(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnGetListOfReturnComponent
        Try
            Dim ls As New List(Of ReturnComponent)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            Dim _type As Integer = 0
            Dim sPOREnt As String = ""

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                For i As Integer = 0 To ListOfProductionOrderDocEntry.Count - 1
                    If i = 0 Then
                        sPOREnt = ListOfProductionOrderDocEntry(i)
                    Else
                        sPOREnt = sPOREnt & "," & ListOfProductionOrderDocEntry(i)
                    End If
                Next

                strSql = "CALL " & _DBNAME & ".""USP_LoadIssueForProductionToReceiptFromProductionLine""('" & sPOREnt & "')"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ReturnComponent With {
                        .Series = oRs.Fields.Item("Series").Value,
                        .SeriesName = oRs.Fields.Item("SeriesName").Value,
                        .DocEntry = oRs.Fields.Item("DocEntry").Value,
                        .DocNum = oRs.Fields.Item("DocNum").Value,
                        .LineNum = oRs.Fields.Item("LineNum").Value,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value,
                        .ItemName = oRs.Fields.Item("ItemName").Value,
                        .ItemType = oRs.Fields.Item("ItemType").Value,
                        .IssuedQty = oRs.Fields.Item("IssuedQty").Value,
                        .wareHouse = oRs.Fields.Item("wareHouse").Value,
                        .PlannedQty = oRs.Fields.Item("PlannedQty").Value,
                        .AvaibleIssue = oRs.Fields.Item("AvaibleIssue").Value,
                        .OnHand = oRs.Fields.Item("OnHand").Value,
                        .IsCommited = oRs.Fields.Item("IsCommited").Value,
                        .OnOrder = oRs.Fields.Item("OnOrder").Value,
                        .StockAvaible = oRs.Fields.Item("StockAvaible").Value,
                        .Type = oRs.Fields.Item("Type").Value,
                        .StartDate = oRs.Fields.Item("StartDate").Value,
                        .EndDate = oRs.Fields.Item("EndDate").Value,
                        .SeqNum = oRs.Fields.Item("SeqNum").Value,
                        .Code = oRs.Fields.Item("Code").Value,
                        .Name = oRs.Fields.Item("Name").Value,
                        .OcrCode = oRs.Fields.Item("OcrCode").Value,
                        .OcrCode2 = oRs.Fields.Item("OcrCode2").Value,
                        .OcrCode3 = oRs.Fields.Item("OcrCode3").Value,
                        .OcrCode4 = oRs.Fields.Item("OcrCode4").Value,
                        .OcrCode5 = oRs.Fields.Item("OcrCode5").Value,
                        .Expirydate = oRs.Fields.Item("Expirydate").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnGetListOfReturnComponent With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnGetListOfReturnComponent With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnGetListOfReturnComponent With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class


