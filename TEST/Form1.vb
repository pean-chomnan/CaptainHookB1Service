
Public Class Form1
    Dim Client As New ServiceReference1.ServicesClient
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim ls_result As ServiceReference1.GetMasterResponse
        ' ls_result = Client._GetSimple("1")
        'Dim ls_result As ServiceReference1.ClassPackingResponse
        'Dim cl As ServiceReference1.classget

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
        'Dim ls_Res As New ServiceReference1.ReturnPriceList
        'ls_Res = Client._GetPriceList
        'Dim i As Integer
        'i = ls_Res.ls_data.Count
        'i = i

        ''Get ItemCode Like both(ItemCode,ItemName) 50 Rows
        'Dim ls_Res As New ServiceReference1.ReturnItemCode
        'ls_Res = Client._GetItemCode("Ch")
        'Dim i As Integer
        'i = ls_Res.ls_data.Count
        'i = i

        '======= GET DELIVERY AND DOCUMENT=========

        'Get 
        ' Dim ls_Res1 As New ServiceReference1.ReturnManufacturer
        'ls_Res1 = Client._GetManufacturer
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

        ' Get Load Production Order To Issue Line
        'Dim ls_Res As New ServiceReference1.ReturnListOfInventoryCountingLine
        'Dim ls_DocEntry As New List(Of Integer)

        'ls_DocEntry.Add(7)
        'ls_DocEntry.Add(37)
        'ls_DocEntry.Add(38)

        'ls_Res = Client._GetListOfInventoryCountingLine(ls_DocEntry.ToArray)
        'Dim i As Integer
        'i = ls_Res.ls_data.Count
        'i = i

        '''Get BatchNumber
        'Dim ls_Res As New ServiceReference1.ReturnGetListOfReturnComponent()
        'Dim arr As List(Of Integer)
        'arr.Add(1)
        'arr.Add(2)
        'arr.Add(3)
        'arr.Add(4)

        'ls_Res = Client._GetLoadIssueForProductionToReceiptFromProductionLine(arr.ToArray)
        'Dim i As Integer
        'i = ls_Res.ls_data.Count


        '======================== xxxx ========================='
        Dim ls_Packing As New List(Of ServiceReference1.PackingClassClassPacking)
        Dim Packing As New ServiceReference1.PackingClassClassPacking
        Dim ReturnSt As New List(Of ServiceReference1.ReturnStatus)

        'Batch PO20082008009-00003
        Packing.ItemCode = "4GT0100"
        Packing.DistNumber = "PO20082008009-00003"
        Packing.BarCodeBoxNumber = "BX004"
        ls_Packing.Add(Packing)
        Packing = New ServiceReference1.PackingClassClassPacking

        'Batch BX008
        Packing.ItemCode = "4GT0100"
        Packing.DistNumber = "BX008"
        Packing.BarCodeBoxNumber = "BX004"
        ls_Packing.Add(Packing)
        Packing = New ServiceReference1.PackingClassClassPacking

        'Batch SC-00022
        Packing.ItemCode = "4GT0100"
        Packing.DistNumber = "4B002"
        Packing.BarCodeBoxNumber = "BX004"
        ls_Packing.Add(Packing)
        Packing = New ServiceReference1.PackingClassClassPacking
        ReturnSt = Client._UpdateUDFBorCodeBoxNumber(ls_Packing.ToArray).ToList '(ls_Packing.ToList).

        MsgBox("Total Batch Updated:" & ReturnSt.Count & ReturnSt.Item(0).ErrirMsg)


    End Sub
End Class
