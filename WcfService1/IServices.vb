' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
<ServiceContract()>
Public Interface IServices

    <OperationContract()>
    Function GetData(ByVal value As Integer) As String

    <OperationContract()>
    Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType
    ' Create BP
    <OperationContract()>
    Function _CreateBP(ByVal obj As List(Of BPMasterData)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _UpdateBP(ByVal obj As List(Of BPMasterData)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _CreateItemMaster(ByVal obj As List(Of ItemMasterData)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _UpdateBarCodeItemMaster(ByVal obj As List(Of ItemMasterData)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _UpdateItemMaster(ByVal obj As List(Of ItemMasterData)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddGoodsReceiptPO(ByVal obj As List(Of GoodsReceiptPO.OPDN)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddDelivery(ByVal obj As List(Of ClassDelivery.ODLN)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddGoodsReceipt(ByVal obj As List(Of ClassGoodsReceipt.OIGN)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddGoodsIssue(ByVal obj As List(Of ClassGoodsIssue.OIGE)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddInventoryTransfer(ByVal obj As List(Of ClassInventoryTransfer.OWTR)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddInventoryCounting(ByVal obj As List(Of ClassInventoryCounting.OINC)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddIssueProduction(ByVal obj As List(Of ClassIssueProduction.OIGE)) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddReceiptFromProduction(ByVal obj As List(Of ClassReceiptFromProduction.OIGN), ByVal iCopyFrom As Integer) As List(Of ReturnStatus)
    <OperationContract()>
    Function _AddInventoryPosting(ByVal obj As List(Of ClassInventoryPosting.OIQR)) As List(Of ReturnStatus)



    <OperationContract()>
    Function _myClass() As Boolean
    <OperationContract()>
    Function _UpdateUDFBorCodeBoxNumber(ipaObj As List(Of PackingClass.ClassPacking)) As List(Of ReturnStatus)

    <OperationContract()>
    Function _GetPacking(BarCode As String) As PackingClass.ClassPackingResponse
    <OperationContract()>
    Function _GetBPGroupCode(ByVal Type As String) As ReturnBPGroup
    <OperationContract()>
    Function _GetBPCurrencyCode() As ReturnBPCurrency
    <OperationContract()>
    Function _GetBPAccountReceivable() As ReturnBPAccountReceivable
    <OperationContract()>
    Function _GetBPAccountDownPayment() As ReturnAccountDownPayment
    <OperationContract()>
    Function _GetBPPaymentTerms() As ReturnBPReturnPaymentTerms
    <OperationContract()>
    Function _GetBPCountry() As ReturnBPCountry
    <OperationContract()>
    Function _GetBankCode() As ReturnBankCode
    <OperationContract()>
    Function _GetHouseBankCountry() As ReturnHouseBankCountry
    <OperationContract()>
    Function _GetHouseBankAccountCode(ByVal BankAcctCode As String) As ReturnHouseBankAccount
    <OperationContract()>
    Function _GetPaymentMoethod(ByVal IorO As String) As ReturnPaymentMeothod
    <OperationContract()>
    Function _GetWithholdingTax() As ReturnWithholdingTax
    <OperationContract()>
    Function _GetItemCode(ByVal SearchingItem As String) As ReturnItemCode
    <OperationContract()>
    Function _GetUomGroup() As ReturnUomGroup
    <OperationContract()>
    Function _GetItemGroupCode() As ReturnItemGroupCode
    <OperationContract()>
    Function _GetFirmCode() As ReturnFirmCode
    <OperationContract()>
    Function _GetUDFType() As ReturnUDFType
    <OperationContract()>
    Function _GetSeries(ByVal ObjectType As String, ByVal PostingDate As Date) As ReturnSeries
    <OperationContract()>
    Function _GetCardCode(ByVal CardType As String, ByVal SearchingBP As String) As ReturnBP
    <OperationContract()>
    Function _GetContactPerson(ByVal CardCode As String) As ReturnContactPerson
    <OperationContract()>
    Function _GetSalesPersonCode() As ReturnSalesPersonCode
    <OperationContract()>
    Function _GetOwner() As ReturnOwner
    <OperationContract()>
    Function _GetbBarCode(ByVal ItemCode As String) As ReturnBarCode
    <OperationContract()>
    Function _GetTaxCode(ByVal IorO As String) As ReturnTaxCode
    <OperationContract()>
    Function _GetUomCode(ByVal ItemCode As String) As ReturnUomCode
    <OperationContract()>
    Function _GetWarehouse() As ReturnWarehouse
    <OperationContract()>
    Function _GetDimension(ByVal OneOrTwo As Integer) As ReturnDimension
    <OperationContract()>
    Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    <OperationContract()>
    Function _GetListOfPurchaseDocument() As ReturnListOfPurchaseDocument
    <OperationContract()>
    Function _GetBinCode(ByVal WarehouseCode As String) As ReturnBinCode
    <OperationContract()>
    Function _GetSalesOrder(ByVal DocNum As Integer) As ReturnSalesOrder


    <OperationContract()>
    Function _GetListOfSalesOrder() As ReturnListOfSalesOrder


    <OperationContract()>
    Function _GetDelivery(ByVal DocNum As Integer) As ReturnDelivery

    <OperationContract()>
    Function _GetListOfDelivery() As ReturnListOfDelivery



    <OperationContract()>
    Function _GetAvailableSerialBatch(ByVal ItemCode As String, ByVal WarehouseCode As String) As ReturnAvailableSerialBatch
    <OperationContract()>
    Function _GetPriceList() As ReturnPriceList
    <OperationContract()>
    Function _GetShipTo(ByVal CardCode As String) As ReturnShipTo
    <OperationContract()>
    Function _GetShipToAddress(ByVal CardCode As String, ByVal ShipTo As String) As ReturnShipToAddress
    <OperationContract()>
    Function _GetItemSetupBySerialOrBatch(ByVal ItemCode As String) As Integer
    <OperationContract()>
    Function _IsWarehouseManagerByBIN(ByVal WhsCode As String) As Boolean
    <OperationContract()>
    Function _GetOrderInterval() As ReturnOrderInterval
    <OperationContract()>
    Function _GetInventoryCountingUomCode(ByVal ItemCode As String) As ReturnInventoryCountingUomCode
    <OperationContract()>
    Function _GetGetItemOnHandPerWhsOrBIN(ByVal ItemCode As String, ByVal Warehouse As String, Optional BinCode As String = "") As ReturnItemOnHandPerWhsOrBIN
    <OperationContract()>
    Function _GetLisOfAvailableIssueLineFromProductionOrder(ByVal ProductionOrderDocEntry As List(Of Integer)) As ReturnGetListOfIssueComponent
    <OperationContract()>
    Function _GetLoadProductionOrderThatAvaibableForReceiptFromProduction() As ReturnGetListOfOWORforReceiptFromProduction
    <OperationContract()>
    Function _GetLoadIssueForProductionToReceiptFromProduction() As ReturnGetListOfOWORforReturnComponent
    <OperationContract()>
    Function _GetLoadIssueForProductionToReceiptFromProductionLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnGetListOfReturnComponent
    <OperationContract()>
    Function _GetProductionOrderListForIssueProduction() As ReturnGetListOfOWORforIssueForProduction
    <OperationContract()>
    Function _GetLoadProductionOrderToIssueLine(ByVal ListOfProductionOrderDocEntry As List(Of Integer)) As ReturnGetListOfIssueComponent
    <OperationContract()>
    Function _GetBatchByBatchNumber(ByVal WhsCode As String, ByVal ItemCode As String, ByVal BatchNo As String) As ReturnBatchNumber
    <OperationContract()>
    Function _GetBatchByBoxNumber(ByVal WhsCode As String, ByVal ItemCode As String, ByVal BoxNumber As String) As ReturnBatchByBoxNumber
    <OperationContract()>
    Function _GetBatchMaster(ByVal BatchNo As String) As ReturnBatchNumber
    <OperationContract()>
    Function _GetBatchMasterByBox(ByVal BoxNo As String) As ReturnBatchNumber
    <OperationContract()>
    Function _GetListOfInventoryCounting() As ReturnListOfInventoryCountin
    <OperationContract()>
    Function _GetListOfInventoryCountingLine(ByVal ls_InventoryCounting As List(Of Integer)) As ReturnListOfInventoryCountingLine
    <OperationContract()>
    Function _GetProjectCode() As ReturnProjectCode
    <OperationContract()>
    Function _GetManufacturer() As ReturnManufacturer
    '<OperationContract()>
    'Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    '<OperationContract()>
    'Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    '<OperationContract()>
    'Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    '<OperationContract()>
    'Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    '<OperationContract()>
    'Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    '<OperationContract()>
    'Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    '<OperationContract()>
    'Function _GetPurchaseOrder(ByVal DocNum As Integer) As ReturnPurchaseOrder
    'PurchaseOrder
End Interface

' Use a data contract as illustrated in the sample below to add composite types to service operations.

<DataContract()>
Public Class CompositeType

    <DataMember()>
    Public Property BoolValue() As Boolean

    <DataMember()>
    Public Property StringValue() As String

End Class
