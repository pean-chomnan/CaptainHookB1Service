﻿
Public Class Form1
    Dim Client As New ServiceReference1.ServicesClient
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim ls_result As ServiceReference3.GetMasterResponse
        ' ls_result = Client._GetSimple("1")
        'Dim ls_result As ServiceReference3.ClassPackingResponse
        'Dim cl As ServiceReference3.classget

        'Client._TEST_GETFUNC("Welcome")
        'Client._GetSimple("1")
        ''ls_result = Client._GetPacking("SC-00022")
        'Dim st As String
        'st = ls_result.Obj(0).ItemCode
        'st = st
        'ls_result = Client._UpdateUDFBorCodeBoxNumber()

        'Dim ls_Res As New ServiceReference1.ReturnBPGroup
        'ls_Res = Client._GetBPGroupCode("C")
        'ls_Res = ls_Res

        'Dim ls_Res As New ServiceReference1.ReturnBPCurrency
        'ls_Res = Client._GetBPCurrencyCode()
        'ls_Res = ls_Res

        'Get CardCode Search
        'Dim ls_Res As New ServiceReference3.ReturnPriceList
        'ls_Res = Client._GetPriceList
        'Dim i As Integer
        'i = ls_Res.ls_data.Count
        'i = i

        ''Get ItemCode Like both(ItemCode,ItemName) 50 Rows
        'Dim ls_Res As New ServiceReference3.ReturnItemCode
        'ls_Res = Client._GetItemCode("Ch")
        'Dim i As Integer
        'i = ls_Res.ls_data.Count
        'i = i

        '======= GET DELIVERY AND DOCUMENT=========

        'Get 
        'Dim ls_Res1 As New ServiceReference1.ReturnLoadProductionOrderToIssueLine
        'ls_Res1 = Client._GetLisOfAvailableIssueLineFromProductionOrder()
        'Dim i As Integer
        'i = ls_Res1.ls_data.Count
        'i = i

        ' '' Get
        'Dim ls_Res As New ServiceReference1.ReturnLoadIssueForProductionToReceiptFromProductionLine
        'Dim ls_DocEntry As New List(Of Integer)

        'ls_DocEntry.Add(14)
        'ls_DocEntry.Add(15)
        'ls_DocEntry.Add(16)

        'ls_Res = Client._GetLoadIssueForProductionToReceiptFromProductionLine(ls_DocEntry.ToArray)

        'Dim i As Integer
        'i = ls_Res.ls_data.Count
        'i = i

        '' Get Load Production Order To Issue Line
        'Dim ls_Res As New ServiceReference1.ReturnLoadProductionOrderToIssueLine
        'Dim ls_DocEntry As New List(Of Integer)

        'ls_DocEntry.Add(14)
        'ls_DocEntry.Add(15)
        'ls_DocEntry.Add(16)

        'ls_Res = Client._GetLoadProductionOrderToIssueLine(ls_DocEntry.ToArray)
        'Dim i As Integer
        'i = ls_Res.ls_data.Count
        'i = i

        'Dim a As String

        '' =========== Add Delivery By Batch ========'

        'Dim DLNs As New List(Of ServiceReference1.ClassDeliveryODLN)
        'Dim DLN As New ServiceReference1.ClassDeliveryODLN ' List(Of ocrd)
        'Dim DLNLs As New List(Of ServiceReference1.ClassDeliveryLine)
        'Dim DLNL As New ServiceReference1.ClassDeliveryLine
        'Dim ls_Serials As New List(Of ServiceReference1.ClassDeliverySerialNumbers)
        'Dim Serial As New ServiceReference1.ClassDeliverySerialNumbers
        'Dim ls_result As List(Of ServiceReference1.ReturnStatus)
        'Dim ls_Batchs As New List(Of ServiceReference1.ClassDeliveryBatchNumbers)
        'Dim Batch As New ServiceReference1.ClassDeliveryBatchNumbers
        'Dim ibar As New ServiceReference1.ItemMasterData.CodeBars

        'Dim BIN As New ServiceReference1.ClassDeliveryIssueBIN
        'Dim ls_BIN As New List(Of ServiceReference1.ClassDeliveryIssueBIN)


        'DLN = New ServiceReference1.ClassDeliveryODLN

        'DLN.Series = 366
        'DLN.CardCode = "CLC10074"

        'DLN.DocDate = "2021-07-22"
        'DLN.DocDueDate = "2021-07-22"
        'DLN.TaxDate = "2021-07-22"
        ''DLN.RequestByBranch=
        'DLN.WebDocNum = "1116"
        'DLN.PriceMode = "N"
        'DLN.DiscountPercent = 0
        'DLN.ContactPersonCode = 14
        'DLN.SalesPersonCode = -1
        'DLN.DocumentsOwner = 2
        'DLN.NumAtCard = ""
        'DLN.Comments = "Based On Sales Orders 212900002.(By Interface)"

        'For i As Integer = 1 To 1
        '    If i = 1 Then

        '        DLNL.ItemCode = "1IGSTIOD"
        '        'DLNL.BarCode = "1IGS-B002"
        '        DLNL.Quantity = 3
        '        DLNL.Price = 3000
        '        DLNL.PriceWeight = 100
        '        DLNL.Weight = 600
        '        'DLNL.GrossPrice = 428
        '        DLNL.DiscPercent = 0
        '        DLNL.VatGroup = "S07"
        '        'DLNL.UomEntry = ""

        '        DLNL.WhsCode = "03.HKT05"
        '        DLNL.CogsCode = "CSM"
        '        DLNL.CogsCode2 = "DP01"

        '        If Client._GetItemSetupBySerialOrBatch("1IGSTIOD") = 2 Then
        '            ' Setup Batch Line 1
        '            Batch.Batch = "BY001"
        '            Batch.Quantity = 2

        '            If Client._IsWarehouseManagerByBIN("03.HKT05") = True Then
        '                'Multiple BIN 1
        '                BIN.BinAbsEntry = 52
        '                BIN.BinQuantity = 1
        '                ls_BIN.Add(BIN)
        '                BIN = Nothing
        '                BIN = New ServiceReference1.ClassDeliveryIssueBIN

        '                'Multiple BIN 2
        '                BIN.BinAbsEntry = 53
        '                BIN.BinQuantity = 1
        '                ls_BIN.Add(BIN)

        '                Batch.ls_BatchBIN = ls_BIN.ToArray()

        '                Batch = New ServiceReference1.ClassDeliveryBatchNumbers
        '                BIN = New ServiceReference1.ClassDeliveryIssueBIN
        '                ls_BIN = New List(Of ServiceReference1.ClassDeliveryIssueBIN)
        '            End If

        '            ls_Batchs.Add(Batch)
        '            Batch = New ServiceReference1.ClassDeliveryBatchNumbers

        '            ' Setup Batch Line 2
        '            Batch.Batch = "BY002"
        '            Batch.Quantity = 1

        '            If Client._IsWarehouseManagerByBIN("03.HKT05") = True Then

        '                'Multiple BIN 1
        '                BIN.BinAbsEntry = 52
        '                BIN.BinQuantity = 1
        '                ls_BIN.Add(BIN)

        '                Batch.ls_BatchBIN = ls_BIN.ToArray()
        '                ls_Batchs.Add(Batch)
        '                Batch = New ServiceReference1.ClassDeliveryBatchNumbers

        '                BIN = Nothing
        '                BIN = New ServiceReference1.ClassDeliveryIssueBIN
        '                ls_BIN = Nothing
        '                ls_BIN = New List(Of ServiceReference1.ClassDeliveryIssueBIN)
        '            End If

        '            DLNL.ls_Batch = ls_Batchs.ToArray

        '        End If

        '        DLNL.BaseEntry = 39
        '        DLNL.BaseType = "17"
        '        DLNL.Baseline = 0

        '        ls_Batchs.Clear()
        '        Batch = Nothing
        '        Batch = New ServiceReference1.ClassDeliveryBatchNumbers

        '    ElseIf i = 2 Then
        '        DLNL.ItemCode = "1RMSNFRWH"
        '        DLNL.Quantity = 2
        '        DLNL.Price = 200
        '        DLNL.PriceWeight = 100
        '        DLNL.Weight = 600

        '        '   DLNL.GrossPrice = 428
        '        DLNL.DiscPercent = 0
        '        DLNL.VatGroup = "S07"
        '        '   DLNL.UomEntry = ""

        '        DLNL.WhsCode = "03.HKT05"
        '        DLNL.CogsCode = "CSM"
        '        DLNL.CogsCode2 = "DP01"

        '        If Client._GetItemSetupBySerialOrBatch("1RMSNFRWH") = 3 Then
        '            If Client._IsWarehouseManagerByBIN("03.HKT05") = True Then
        '                'Multiple BIN 1
        '                BIN.BinAbsEntry = 52
        '                BIN.BinQuantity = 1
        '                ls_BIN.Add(BIN)
        '                BIN = Nothing
        '                BIN = New ServiceReference1.ClassDeliveryIssueBIN

        '                'Multiple BIN 2
        '                BIN.BinAbsEntry = 53
        '                BIN.BinQuantity = 1
        '                ls_BIN.Add(BIN)

        '                DLNL.ls_LineBIN = ls_BIN.ToArray()
        '                BIN = Nothing
        '                BIN = New ServiceReference1.ClassDeliveryIssueBIN
        '                ls_BIN.Clear()
        '            End If

        '            DLNL.BaseEntry = 39
        '            DLNL.BaseType = "17"
        '            DLNL.Baseline = 1

        '        End If
        '    End If

        '    DLNLs.Add(DLNL)
        '    DLNL = Nothing
        '    DLNL = New ServiceReference1.ClassDeliveryLine
        'Next

        'DLN.Lines = DLNLs.ToArray
        'DLNs.Add(DLN)
        'ls_result = Client._AddDelivery(DLNs.ToArray).ToList

        'a = ls_result(0).ErrirMsg



    End Sub

    Public Sub TestGetSO()
        'Get Sales Order
        Dim serviceClient As New ServiceReference1.ServicesClient
        Dim serviceResponse As New ServiceReference1.ReturnLoadProductionOrderThatAvaibableForReceiptFromProduction
        serviceResponse = serviceClient._GetLoadProductionOrderThatAvaibableForReceiptFromProduction()
        Dim listOfServiceSalesOrder As New List(Of ServiceReference1.SalesOrder)

        Dim dt_ReceiptFromProductionHeader As DataTable
        dt_ReceiptFromProductionHeader = New DataTable("Table")
        dt_ReceiptFromProductionHeader.Columns.Add("DocEntry", System.Type.GetType("System.Integer")) 'Item Code
        dt_ReceiptFromProductionHeader.Columns.Add("DocNum", System.Type.GetType("System.Integer")) 'Item Name
        dt_ReceiptFromProductionHeader.Columns.Add("SeriesName", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("Type", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("PostDate", System.Type.GetType("System.Date")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("DueDate", System.Type.GetType("System.Date")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("ProductNo", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("ProdName", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("Comments", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("StartDate", System.Type.GetType("System.Date")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("Priority", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("Status", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("Warehouse", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("OcrCode", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("OcrCode2", System.Type.GetType("System.String")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("PlannedQty", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("AvaibleReceipt", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("OnHand", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("IsCommited", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("OnOrder", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("StockAvaible", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("CmpltQty", System.Type.GetType("System.Decimal")) 'Quantity
        dt_ReceiptFromProductionHeader.Columns.Add("RjctQty", System.Type.GetType("System.Decimal")) 'Quantity

        If serviceResponse.ErrCode = 0 Then
            'Get Data Success
            listOfServiceSalesOrder = serviceResponse.ls_data.ToList
            Dim RowDetail As DataRow
            For Each tmpSO As ServiceReference1.SalesOrder In listOfServiceSalesOrder

                'Header Bind data to Form (Header)
                Dim txtCardCode As String
                Dim txtCarName As String
                Dim txtDocDate As String
                Dim txtDocDueDate As String
                Dim txtTaxDate As String
                txtCardCode = tmpSO.CardCode
                txtCarName = tmpSO.CardName
                txtDocDate = tmpSO.DocDate
                txtDocDueDate = tmpSO.DocDueDate
                txtTaxDate = tmpSO.TaxDate

                RowDetail = dt_DeliveryLine.NewRow
                RowDetail.Item("ItemCode") = tmpSO.ItemCode
                RowDetail.Item("ItemName") = ""
                RowDetail.Item("ItemName") = tmpSO.Quantity

            Next

            dt_DeliveryLine.AcceptChanges()

            ' Add Data Scource to Data Grid/List View
            ' DataGridView.DataSource = dt_DeliveryLine


            'Rename Field and and invisible some columns on data/List View

        Else
            'Get Data Error
            MessageBox.Show(serviceResponse.ErrMsg)

        End If

    End Sub
End Class
