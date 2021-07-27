Public Class ItemMasterData
    Public Property ItemCode As String
    Public Property ItemName As String
    Public Property FrgName As String
    Public Property CodeBar As List(Of CodeBars)
    Public Property ItmsGrpCod As Integer
    Public Property ItmsGrpNam As String
    Public Property FirmCode As Integer
    Public Property PricingUnit As Integer

    Public Property ManBatchNum As String
    Public Property ManSerNum As String
    Public Property ManagmtMethod As String ' Use Serial & Batch
    Public Property IssuePrimarilyBy As Integer ' Use Serial & Batch
    Public Property WTLiable As String
    Public Property GLMethod As String

    Public Property PurchasingUoMName As String
    Public Property PurchasePackagingUoMName As String
    Public Property SalesUoMName As String
    Public Property SalePackagingUoMName As String

    Public Property PurchaseItemsPerUnit As Double
    Public Property PurchaseQtyPerPackUnit As Double
    Public Property SalesItemsPerUnit As Double
    Public Property SalesQtyPerPackUnit As Double

    Public Property SHeight As Double
    Public Property BHeight As Double
    Public Property SWidth As Double
    Public Property BWidth As Double
    Public Property SLength As Double
    Public Property BLength As Double
    Public Property SVolume As Double
    Public Property BVolume As Double
    Public Property Sweight As Double
    Public Property BWeight As Double

    Public Property PrchseItem As String
    Public Property SellItem As String
    Public Property InvntItem As String

    Public Property PlanningMethod As String
    Public Property ProcurementMethod As String
    Public Property ComponentWarehouse As String
    Public Property OrderInterval As String
    Public Property OrderMultiple As Double
    Public Property MinimumOrderQty As Double
    Public Property CheckingRule As String
    Public Property LeadTime As Integer
    Public Property ToleranceDays As Integer

    Public Property PhantomItem As String
    Public Property IssueMethod As String
    Public Property ProductionStdCost As Double
    Public Property IncludeInProductionRollup As String

    Public Property UserText As String
    Public Property UomGroup As Integer
    Public Property InventoryUOM As String
    Public Property U_Type As String
    Public Property U_ProductComposition As String
    Public Property U_StorageCondition As String
    Public Property U_HowToEat As String
    Public Property U_Certifiedcode As String
    Public Property WebDocNum As String

    Public Class CodeBars
        ' Public Property BcdEntry As Integer
        Public Property BcdUOMCode As Integer
        Public Property BcdCode As String
        Public Property BcdName As String
    End Class
End Class
