' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.vb at the Solution Explorer and start debugging.

'<Assembly: log4net.Config.XmlConfigurator(ConfigFile:="log4net.config", Watch:=True)>
Imports WcfService1

Public Class Service1
    Implements IServices

    Public Sub New()
    End Sub

    Public Function GetData(ByVal value As Integer) As String Implements IServices.GetData
        Return String.Format("You entered: {0}", value)
    End Function

    Public Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType Implements IServices.GetDataUsingDataContract
        If composite Is Nothing Then
            Throw New ArgumentNullException("composite")
        End If
        If composite.BoolValue Then
            composite.StringValue &= "Suffix"
        End If
        Return composite
    End Function

    Public Function _CreateBP(obj As List(Of BPMasterData)) As List(Of ReturnStatus) Implements IServices._CreateBP
        Dim ob As CreateBP = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateBP
            ls_result = ob.SendBPMaster(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _myClass() As Boolean Implements IServices._myClass
        Dim mycc As myClassOfFuntion = Nothing
        mycc = New myClassOfFuntion
        Return True
    End Function

    ''Public Function _GetSimple(ByVal obj As String) As GetMasterResponse Implements IServices._GetSimple
    ''    Return GetSample.GetData("1")
    ''End Function

    Public Function _UpdateBP(obj As List(Of BPMasterData)) As List(Of ReturnStatus) Implements IServices._UpdateBP
        Dim ob As CreateBP = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateBP
            ls_result = ob.Update(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _CreateItemMaster(obj As List(Of ItemMasterData)) As List(Of ReturnStatus) Implements IServices._CreateItemMaster
        Dim ob As CreatItem = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreatItem
            ls_result = ob.SendItemMasterData(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _UpdateItemMaster(obj As List(Of ItemMasterData)) As List(Of ReturnStatus) Implements IServices._UpdateItemMaster
        Dim ob As CreatItem = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreatItem
            ls_result = ob.UpdateItemMaster(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _UpdateBarCodeItemMaster(obj As List(Of ItemMasterData)) As List(Of ReturnStatus) Implements IServices._UpdateBarCodeItemMaster
        Dim ob As CreatItem = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreatItem
            ls_result = ob.AddOrUpdateBarCodeOfItemMaster(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddGoodsReceiptPO(obj As List(Of GoodsReceiptPO.OPDN)) As List(Of ReturnStatus) Implements IServices._AddGoodsReceiptPO
        Dim ob As CreateGoodsReceiptPO = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateGoodsReceiptPO
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddDelivery(obj As List(Of ClassDelivery.ODLN)) As List(Of ReturnStatus) Implements IServices._AddDelivery
        Dim ob As CreateDelivery = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateDelivery
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddGoodsReceipt(obj As List(Of ClassGoodsReceipt.OIGN)) As List(Of ReturnStatus) Implements IServices._AddGoodsReceipt
        Dim ob As CreateGoodsReceipt = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateGoodsReceipt
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddGoodsIssue(obj As List(Of ClassGoodsIssue.OIGE)) As List(Of ReturnStatus) Implements IServices._AddGoodsIssue
        Dim ob As CreateGoodsIssue = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateGoodsIssue
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddInventoryTransfer(obj As List(Of ClassInventoryTransfer.OWTR)) As List(Of ReturnStatus) Implements IServices._AddInventoryTransfer
        Dim ob As CreateInventoryTransfer = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateInventoryTransfer
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddInventoryCounting(ByVal obj As List(Of ClassInventoryCounting.OINC)) As List(Of ReturnStatus) Implements IServices._AddInventoryCounting
        Dim ob As CreateInventoryCounting = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateInventoryCounting
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddIssueProduction(ByVal obj As List(Of ClassIssueProduction.OIGE)) As List(Of ReturnStatus) Implements IServices._AddIssueProduction
        Dim ob As CreateIssueProduction = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateIssueProduction
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddReceiptFromProduction(ByVal obj As List(Of ClassReceiptFromProduction.OIGN), ByVal iCopyFrom As Integer) As List(Of ReturnStatus) Implements IServices._AddReceiptFromProduction
        Dim ob As CreateReceiptFromProduction = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateReceiptFromProduction
            ls_result = ob.Send(obj, iCopyFrom)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _AddInventoryPosting(ByVal obj As List(Of ClassInventoryPosting.OIQR)) As List(Of ReturnStatus) Implements IServices._AddInventoryPosting
        Dim ob As CreateInventoryPosting = Nothing
        Dim ls_result As List(Of ReturnStatus) = Nothing
        Try
            ob = New CreateInventoryPosting
            ls_result = ob.Send(obj)
        Catch ex As Exception
        End Try
        Return ls_result
    End Function

    Public Function _GetPacking(ByVal BarCode As String) As PackingClass.ClassPackingResponse Implements IServices._GetPacking
        Dim obj As New GetDataResonse
        Return obj.GetPacking(BarCode)
    End Function

    Public Function _UpdateUDFBorCodeBoxNumber(ipaObj As List(Of PackingClass.ClassPacking)) As List(Of ReturnStatus) Implements IServices._UpdateUDFBorCodeBoxNumber
        Dim obb As New GetDataResonse
        Dim ls_result As List(Of ReturnStatus) = Nothing
        ls_result = obb.UpdateUDFBorCodeBoxNumber(ipaObj)
        Return ls_result
    End Function

    Public Function _GetBPGroupCode(ByVal Type As String) As ReturnBPGroup Implements IServices._GetBPGroupCode
        Dim obj As New GetBPGroupCode
        Return obj.GetBPGroupCode(Type)
    End Function

    Public Function _GetBPCurrencyCode() As ReturnBPCurrency Implements IServices._GetBPCurrencyCode
        Dim obj As New CGetBPCurrency
        Return obj.FGetCurrency()
    End Function

    Public Function _GetBPAccountReceivable() As ReturnBPAccountReceivable Implements IServices._GetBPAccountReceivable
        Dim obj As New CGetBPAcctReceivable
        Return obj.FGetAccountReceivable()
    End Function

    Public Function _GetBPAccountDownPayment() As ReturnAccountDownPayment Implements IServices._GetBPAccountDownPayment
        Dim obj As New CGetBPAcctDownPayment
        Return obj.FGetAccountDownPayment()
    End Function

    Public Function _GetBPPaymentTerms() As ReturnBPReturnPaymentTerms Implements IServices._GetBPPaymentTerms
        Dim obj As New CGetBPReturnPaymentTerms
        Return obj.FGetAccountDownPayment
    End Function

    Public Function _GetBPCountry() As ReturnBPCountry Implements IServices._GetBPCountry
        Dim obj As New CGetBPReturnCountry
        Return obj.FGetCountry
    End Function
    Public Function _GetBankCode() As ReturnBankCode Implements IServices._GetBankCode
        Dim obj As New CGetReturnBankCode
        Return obj.FGetBankCode
    End Function

    Public Function _GetHouseBankCountry() As ReturnHouseBankCountry Implements IServices._GetHouseBankCountry
        Dim obj As New CGetReturnHouseBankCountry
        Return obj.FGetHouseBankCountry
    End Function

    Public Function _GetHouseBankAccountCode(ByVal BankAcctCode As String) As ReturnHouseBankAccount Implements IServices._GetHouseBankAccountCode
        Dim obj As New CGetReturnHouseBankAccount
        Return obj.FGetHouseBankAccount(BankAcctCode)
    End Function

    Public Function _GetPaymentMoethod(ByVal IorO As String) As ReturnPaymentMeothod Implements IServices._GetPaymentMoethod
        Dim obj As New CGetReturnPaymentMeothod
        Return obj.FGetPaymentMoethod(IorO)
    End Function
    Public Function _GetWithholdingTax() As ReturnWithholdingTax Implements IServices._GetWithholdingTax
        Dim obj As New CGetReturnWithholdingTax
        Return obj.FGetWithholdingTax()
    End Function

    Public Function _GetItemCode(ByVal SearchingItem As String) As ReturnItemCode Implements IServices._GetItemCode
        Dim obj As New CReturnGetItemCode
        Return obj.FGetReturnItem(SearchingItem)
    End Function

    Public Function _GetUomGroup() As ReturnUomGroup Implements IServices._GetUomGroup
        Dim obj As New CReturnUomGroup
        Return obj.FGetReturnUomGroup
    End Function

    Public Function _GetItemGroupCode() As ReturnItemGroupCode Implements IServices._GetItemGroupCode
        Dim obj As New CReturnReturnItemGroupCode
        Return obj.FGetReturnItemGroupCode
    End Function

    Public Function _GetFirmCode() As ReturnFirmCode Implements IServices._GetFirmCode
        Dim obj As New CReturnFirmCode
        Return obj.FGetReturnFirmCode
    End Function

    Public Function _GetUDFType() As ReturnUDFType Implements IServices._GetUDFType
        Dim obj As New CReturnUDFType
        Return obj.FGetReturnUDFType
    End Function

    Public Function _GetSeries(ByVal ObjectType As String, ByVal PostingDate As Date) As ReturnSeries Implements IServices._GetSeries
        Dim obj As New CReturnSeries
        Return obj.FGetSeries(ObjectType, PostingDate)
    End Function

    Public Function _GetCardCode(ByVal CardType As String, ByVal SearchingBP As String) As ReturnBP Implements IServices._GetCardCode
        Dim obj As New CReturnGetBP
        Return obj.FGetReturnBP(CardType, SearchingBP)
    End Function

    Public Function _GetContactPerson(ByVal CardCode As String) As ReturnContactPerson Implements IServices._GetContactPerson
        Dim obj As New CReturnGetContactPerson
        Return obj.FGetReturnContactPerson(CardCode)
    End Function

    Public Function _GetOwner() As ReturnOwner Implements IServices._GetOwner
        Dim obj As New CReturnGetOwner
        Return obj.FGetReturnOwner
    End Function

    Public Function _GetSalesPersonCode() As ReturnSalesPersonCode Implements IServices._GetSalesPersonCode
        Dim obj As New CReturnGetSalesPersonCode
        Return obj.FGetReturnSalesPersonCode
    End Function

    Public Function _GetbBarCode(ByVal ItemCode As String) As ReturnBarCode Implements IServices._GetbBarCode
        Dim obj As New CReturnGetBarCode
        Return obj.FGetReturnBarCode(ItemCode)
    End Function

    Public Function _GetTaxCode(ByVal IorO As String) As ReturnTaxCode Implements IServices._GetTaxCode
        Dim obj As New CReturnGetTaxCode
        Return obj.FGetReturnTaxCode(IorO)
    End Function

    Public Function _GetUomCode(ByVal ItemCode As String) As ReturnUomCode Implements IServices._GetUomCode
        Dim obj As New CReturnGetUomCode
        Return obj.FGetReturnUomCode(ItemCode)
    End Function

    Public Function _GetWarehouse() As ReturnWarehouse Implements IServices._GetWarehouse
        Dim obj As New CReturnGetWarehouse
        Return obj.FGetReturnWarehouse
    End Function

    Public Function _GetDimension(ByVal OneOrTwo As Integer) As ReturnDimension Implements IServices._GetDimension
        Dim obj As New CReturnGetDimension
        Return obj.FGetReturnDimension(OneOrTwo)
    End Function

    Public Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder Implements IServices._GetPurchaseOrder
        Dim obj As New CReturnGetPurchaseOrder
        Return obj.FGetReturnPurchaseOrder(DocNum)
    End Function

    Public Function _GetListOfPurchaseDocument() As ReturnListOfPurchaseDocument Implements IServices._GetListOfPurchaseDocument
        Dim obj As New CReturnGetListOfPurchaseDocument
        Return obj.FGetReturnListOfPurchaseDocument()
    End Function

    Public Function _GetBinCode(ByVal WarehouseCode As String) As ReturnBinCode Implements IServices._GetBinCode
        Dim obj As New CReturnGetBinCode
        Return obj.FGetReturnBinCode(WarehouseCode)
    End Function

    Public Function _GetSalesOrder(ByVal DocNum As Integer) As ReturnSalesOrder Implements IServices._GetSalesOrder
        Dim obj As New CReturnGetSalesOrder
        Return obj.FGetReturnSalesOrder(DocNum)
    End Function

    Public Function _GetListOfSalesOrder() As ReturnListOfSalesOrder Implements IServices._GetListOfSalesOrder
        Dim obj As New CReturnGetListOfSalesOrder
        Return obj.FGetReturnListOfSalesOrder
    End Function

    Public Function _GetAvailableSerialBatch(ByVal ItemCode As String, ByVal WarehouseCode As String) As ReturnAvailableSerialBatch Implements IServices._GetAvailableSerialBatch
        Dim obj As New CReturnGetAvailableSerialBatch
        Return obj.FGetReturnAvailableSerialBatch(ItemCode, WarehouseCode)
    End Function

    Public Function _GetPriceList() As ReturnPriceList Implements IServices._GetPriceList
        Dim obj As New CReturnGetPriceList
        Return obj.FGetReturnPriceList
    End Function

    Public Function _GetShipTo(ByVal CardCode As String) As ReturnShipTo Implements IServices._GetShipTo
        Dim obj As New CReturnGetShipTo
        Return obj.FGetReturnShipTo(CardCode)
    End Function

    Public Function _GetShipToAddress(ByVal CardCode As String, ByVal ShipTo As String) As ReturnShipToAddress Implements IServices._GetShipToAddress
        Dim obj As New CReturnGetShipToAddress
        Return obj.FGetReturnShipToAddress(CardCode, ShipTo)
    End Function

    Public Function _GetItemSetupBySerialOrBatch(ByVal ItemCode As String) As Integer Implements IServices._GetItemSetupBySerialOrBatch
        Dim obj As New myClassOfFuntion
        Return obj.ItemSetupBy(ItemCode)
    End Function

    Public Function _IsWarehouseManagerByBIN(ByVal WhsCode As String) As Boolean Implements IServices._IsWarehouseManagerByBIN
        Dim obj As New myClassOfFuntion
        Return obj.isManagByBIN(WhsCode)
    End Function

    Public Function _GetOrderInterval() As ReturnOrderInterval Implements IServices._GetOrderInterval
        Dim obj As New CReturnOrderInterval
        Return obj.FGetReturnOrderInterval
    End Function

    Public Function _GetInventoryCountingUomCode(ByVal ItemCode As String) As ReturnInventoryCountingUomCode Implements IServices._GetInventoryCountingUomCode
        Dim obj As New CReturnGetInventoryCountingUomCode
        Return obj.FGetReturnInventoryCountingUomCode(ItemCode)
    End Function

    Public Function _GetGetItemOnHandPerWhsOrBIN(ByVal ItemCode As String, ByVal Warehouse As String, Optional BinCode As String = "") As ReturnItemOnHandPerWhsOrBIN Implements IServices._GetGetItemOnHandPerWhsOrBIN
        Dim obj As New CReturnGetItemOnHandPerWhsOrBIN
        Return obj.FGetReturnItemOnHandPerWhsOrBIN(ItemCode, Warehouse, BinCode)
    End Function

    Public Function _GetLisOfAvailableIssueLineFromProductionOrder(ByVal ProductionOrderDocEntry As List(Of Integer)) As ReturnGetListOfIssueComponent Implements IServices._GetLisOfAvailableIssueLineFromProductionOrder
        Dim obj As New GetListOfIssueComponent
        Return obj.Execute(ProductionOrderDocEntry)
    End Function

    Public Function _GetLoadProductionOrderThatAvaibableForReceiptFromProduction() As ReturnGetListOfOWORforReceiptFromProduction Implements IServices._GetLoadProductionOrderThatAvaibableForReceiptFromProduction
        Dim obj As New GetListOfOWORforReceiptFromProduction
        Return obj.Execute
    End Function
    Public Function _GetLoadIssueForProductionToReceiptFromProduction() As ReturnGetListOfOWORforReturnComponent Implements IServices._GetLoadIssueForProductionToReceiptFromProduction
        Dim obj As New GetListOfOWORforReturnComponent
        Return obj.Execute
    End Function
    Public Function _GetLoadIssueForProductionToReceiptFromProductionLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnGetListOfReturnComponent Implements IServices._GetLoadIssueForProductionToReceiptFromProductionLine
        Dim obj As New GetListOfReturnComponent
        Return obj.Execute(ListOfProductionOrderDocEntry)
    End Function
    Public Function _GetProductionOrderListForIssueProduction() As ReturnGetListOfOWORforIssueForProduction Implements IServices._GetProductionOrderListForIssueProduction
        Dim obj As New GetListOfOWORforIssueForProduction
        Return obj.Execute
    End Function
    Public Function _GetLoadProductionOrderToIssueLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnGetListOfIssueComponent Implements IServices._GetLoadProductionOrderToIssueLine
        Dim obj As New GetListOfIssueComponent
        Return obj.Execute(ListOfProductionOrderDocEntry)
    End Function
    Public Function _GetBatchByBatchNumber(ByVal WhsCode As String, ByVal ItemCode As String, ByVal BatchNo As String) As ReturnBatchNumber Implements IServices._GetBatchByBatchNumber
        Dim obj As New CReturnGetBatchNo
        Return obj.FGetReturnBatchByBatchNumber(WhsCode, ItemCode, BatchNo)
    End Function

    Public Function _GetBatchByBoxNumber(ByVal WhsCode As String, ByVal ItemCode As String, ByVal BoxNumber As String) As ReturnBatchByBoxNumber Implements IServices._GetBatchByBoxNumber
        Dim obj As New CReturnGetBatchNumber
        Return obj.FGetReturnBatchByBoxNumber(WhsCode, ItemCode, BoxNumber)
    End Function

    Public Function _GetBatchMaster(BatchNo As String) As ReturnBatchNumber Implements IServices._GetBatchMaster
        Dim obj As New CReturnGetBatchNo
        Return obj.GetBatchMaster(BatchNo)
    End Function

    Public Function _GetBatchMasterByBox(BoxNo As String) As ReturnBatchNumber Implements IServices._GetBatchMasterByBox
        Dim obj As New CReturnGetBatchNo
        Return obj.GetBatchMasterByBox(BoxNo)
    End Function

    'Public Function _GetBatchMaster(BatchNumber As String)

    Public Function _GetListOfInventoryCounting() As ReturnListOfInventoryCountin Implements IServices._GetListOfInventoryCounting
        Dim obj As New CReturnGetListOfInventoryCountin
        Return obj.FGetReturnListOfInventoryCountin()
    End Function

    Public Function _GetListOfInventoryCountingLine(ByVal ls_InventoryCounting As List(Of Integer)) As ReturnListOfInventoryCountingLine Implements IServices._GetListOfInventoryCountingLine
        Dim obj As New CReturnGetListOfInventoryCountingLine
        Return obj.FGetListOfInventoryCountingLine(ls_InventoryCounting)
    End Function

    Public Function _GetProjectCode() As ReturnProjectCode Implements IServices._GetProjectCode
        Dim obj As New CReturnProjectCode
        Return obj.FGetProjectCode()
    End Function

    Public Function _GetManufacturer() As ReturnManufacturer Implements IServices._GetManufacturer
        Dim obj As New CReturnManufacturer
        Return obj.FGetManufacturer
    End Function

    'Public Function _GetLoadIssueForProductionToReceiptFromProductionLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnLoadIssueForProductionToReceiptFromProductionLine Implements IServices._GetLoadIssueForProductionToReceiptFromProductionLine
    '    Dim obj As New CReturnGetLoadIssueForProductionToReceiptFromProductionLine
    '    Return obj.FGetReturnLoadIssueForProductionToReceiptFromProductionLine(ListOfProductionOrderDocEntry)
    'End Function

    'Public Function _GetLoadIssueForProductionToReceiptFromProductionLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnLoadIssueForProductionToReceiptFromProductionLine Implements IServices._GetLoadIssueForProductionToReceiptFromProductionLine
    '    Dim obj As New CReturnGetLoadIssueForProductionToReceiptFromProductionLine
    '    Return obj.FGetReturnLoadIssueForProductionToReceiptFromProductionLine(ListOfProductionOrderDocEntry)
    'End Function

    'Public Function _GetLoadIssueForProductionToReceiptFromProductionLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnLoadIssueForProductionToReceiptFromProductionLine Implements IServices._GetLoadIssueForProductionToReceiptFromProductionLine
    '    Dim obj As New CReturnGetLoadIssueForProductionToReceiptFromProductionLine
    '    Return obj.FGetReturnLoadIssueForProductionToReceiptFromProductionLine(ListOfProductionOrderDocEntry)
    'End Function

    'Public Function _GetLoadIssueForProductionToReceiptFromProductionLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnLoadIssueForProductionToReceiptFromProductionLine Implements IServices._GetLoadIssueForProductionToReceiptFromProductionLine
    '    Dim obj As New CReturnGetLoadIssueForProductionToReceiptFromProductionLine
    '    Return obj.FGetReturnLoadIssueForProductionToReceiptFromProductionLine(ListOfProductionOrderDocEntry)
    'End Function


    'Function _GetLoadProductionOrderToIssueLine(ByVal DocNum As Integer) As ReturnLoadProductionOrderToIssueLine
    'End Function
    '  Function _GetUomCode(ByVal ItemCode As String) As ReturnUomCode
End Class
