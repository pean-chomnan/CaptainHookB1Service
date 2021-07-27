Imports SAPbobsCOM

Public Class CreatItem
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function SendItemMasterData(ByVal obj As List(Of ItemMasterData)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim returnstatus As ReturnStatus
        Dim IMaster As SAPbobsCOM.Items = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim myClasss As New myClassOfFuntion

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                IMaster = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
                Dim i As Integer
                i = 0

                Do While i < obj.Count
                    IMaster.ItemCode = obj(i).ItemCode
                    IMaster.ItemName = obj(i).ItemName
                    IMaster.ForeignName = obj(i).FrgName

                    If obj(i).UomGroup <> 0 Then
                        IMaster.UoMGroupEntry = obj(i).UomGroup
                    End If

                    If myClasss.ICaseNumber(obj(i).CodeBar) <> 0 Then
                        For Each l In obj(i).CodeBar ' Create Code Bar
                            If myClasss.ICaseNumber(obj(i).UomGroup) = 0 Or myClasss.ICaseNumber(obj(i).UomGroup) = -1 Then  ' IF UOM GROUP =-1 UOM GROUP IN BARCODE ALSO -1
                                IMaster.BarCodes.UoMEntry = -1
                                ' IMaster.PricingUnit = -1
                            Else
                                IMaster.BarCodes.UoMEntry = l.BcdUOMCode
                                ' IMaster.PricingUnit = l.BcdUOMCode
                            End If

                            IMaster.BarCodes.BarCode = l.BcdCode
                            IMaster.BarCodes.FreeText = l.BcdName
                            IMaster.BarCodes.Add()
                        Next
                    End If

                    IMaster.ItemsGroupCode = obj(i).ItmsGrpCod
                    IMaster.Manufacturer = obj(i).FirmCode
                    'IMaster.PricingUnit = Obj(i).PricingUnit

                    'Batch Number
                    If obj(i).ManBatchNum = "Y" Then
                        IMaster.ManageBatchNumbers = SAPbobsCOM.BoYesNoEnum.tYES

                        If obj(i).ManagmtMethod = "R" Then
                            IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnReleaseOnly
                        Else
                            IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction
                        End If

                        If obj(i).IssuePrimarilyBy = 1 Then
                            IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbBinLocations
                        Else
                            IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbSerialAndBatchNumbers
                        End If
                    End If

                    'Serial Number
                    If obj(i).ManSerNum = "Y" Then
                        IMaster.ManageSerialNumbers = SAPbobsCOM.BoYesNoEnum.tYES

                        If obj(i).ManagmtMethod = "R" Then
                            IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnReleaseOnly
                        Else
                            IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction
                        End If

                        If obj(i).IssuePrimarilyBy = 1 Then
                            IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbBinLocations
                        Else
                            IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbSerialAndBatchNumbers
                        End If
                    End If

                    If obj(i).GLMethod = "W" Then ' Item Manager
                        IMaster.GLMethod = SAPbobsCOM.BoGLMethods.glm_WH
                    ElseIf obj(i).GLMethod = "C" Then
                        IMaster.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass
                    ElseIf obj(i).GLMethod = "L" Then
                        IMaster.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemLevel
                    End If

                    If obj(i).WTLiable = "Y" Then 'Widthholding Tax
                        IMaster.WTLiable = SAPbobsCOM.BoYesNoEnum.tYES
                    ElseIf obj(i).WTLiable = "N" Then
                        IMaster.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO
                    End If

                    ' Purchase & Sales (Packing, Unit Qty, Qty Per Unit)
                    IMaster.PurchasePackagingUnit = myClasss.ICaseString(obj(i).PurchasePackagingUoMName)
                    IMaster.SalesPackagingUnit = myClasss.ICaseString(obj(i).SalePackagingUoMName)
                    IMaster.SalesUnit = myClasss.ICaseString(obj(i).SalesUoMName)
                    IMaster.PurchaseUnit = myClasss.ICaseString(obj(i).PurchasingUoMName)

                    If myClasss.ICaseNumber(obj(i).PurchaseItemsPerUnit) <> 0 Then
                        IMaster.PurchaseItemsPerUnit = obj(i).PurchaseItemsPerUnit
                    End If

                    If myClasss.ICaseNumber(obj(i).PurchaseQtyPerPackUnit) <> 0 Then
                        IMaster.PurchaseQtyPerPackUnit = obj(i).PurchaseQtyPerPackUnit
                    End If

                    If myClasss.ICaseNumber(obj(i).SalesItemsPerUnit) <> 0 Then
                        IMaster.SalesItemsPerUnit = obj(i).SalesItemsPerUnit
                    End If

                    If myClasss.ICaseNumber(obj(i).SalesQtyPerPackUnit) <> 0 Then
                        IMaster.SalesQtyPerPackUnit = obj(i).SalesQtyPerPackUnit
                    End If

                    'Purchase & Sales (Masure)
                    IMaster.SalesUnitHeight = obj(i).SHeight
                    IMaster.SalesUnitWidth = obj(i).SWidth
                    IMaster.SalesUnitLength = obj(i).SLength
                    IMaster.SalesUnitWeight = obj(i).Sweight
                    If myClasss.ICaseNumber(obj(i).SVolume) <> 0 Then
                        IMaster.SalesVolumeUnit = obj(i).SVolume
                    End If

                    IMaster.PurchaseUnitHeight = obj(i).BHeight
                    IMaster.PurchaseUnitWidth = obj(i).BWidth
                    IMaster.PurchaseUnitLength = obj(i).BLength
                    IMaster.PurchaseUnitWeight = obj(i).BWeight
                    If myClasss.ICaseNumber(obj(i).BVolume) <> 0 Then
                        IMaster.PurchaseVolumeUnit = obj(i).BVolume
                    End If

                    ' Item For
                    If obj(i).PrchseItem = "N" Then
                        IMaster.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tNO
                    ElseIf obj(i).PrchseItem = "Y" Then
                        IMaster.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES
                    End If

                    If obj(i).SellItem = "N" Then
                        IMaster.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO
                    ElseIf obj(i).SellItem = "Y" Then
                        IMaster.SalesItem = SAPbobsCOM.BoYesNoEnum.tYES
                    End If

                    If obj(i).InvntItem = "N" Then
                        IMaster.InventoryItem = SAPbobsCOM.BoYesNoEnum.tNO
                    ElseIf obj(i).InvntItem = "Y" Then
                        IMaster.InventoryItem = SAPbobsCOM.BoYesNoEnum.tYES
                    End If

                    'Tab Planning Data
                    If myClasss.ICaseString(obj(i).PlanningMethod) = "" Then
                        IMaster.PlanningSystem = BoPlanningSystem.bop_MRP
                    ElseIf myClasss.ICaseString(obj(i).PlanningMethod) = "N" Then
                        IMaster.PlanningSystem = BoPlanningSystem.bop_None
                    ElseIf myClasss.ICaseString(obj(i).PlanningMethod) = "M" Then
                        IMaster.PlanningSystem = BoPlanningSystem.bop_MRP
                    End If

                    'ProcurementMethod & Component Whs
                    If myClasss.ICaseString(obj(i).ProcurementMethod) = "" Then
                        IMaster.ProcurementMethod = BoProcurementMethod.bom_Buy
                    ElseIf myClasss.ICaseString(obj(i).ProcurementMethod) = "B" Then
                        IMaster.ProcurementMethod = BoProcurementMethod.bom_Buy
                    ElseIf myClasss.ICaseString(obj(i).ProcurementMethod) = "M" Then
                        IMaster.ProcurementMethod = BoProcurementMethod.bom_Make
                        If myClasss.ICaseString(obj(i).ComponentWarehouse) = "" Or myClasss.ICaseString(obj(i).ComponentWarehouse) = "B" Then
                            IMaster.ComponentWarehouse = BoMRPComponentWarehouse.bomcw_BOM
                        ElseIf myClasss.ICaseString(obj(i).ComponentWarehouse) = "P" Then
                            IMaster.ComponentWarehouse = BoMRPComponentWarehouse.bomcw_Parent
                        End If
                    End If

                    If myClasss.ICaseString(obj(i).OrderInterval) <> "" Then
                        IMaster.OrderIntervals = obj(i).OrderInterval
                    End If

                    IMaster.MinOrderQuantity = myClasss.ICaseNumber(obj(i).MinimumOrderQty)
                    IMaster.LeadTime = obj(i).LeadTime
                    IMaster.ToleranceDays = obj(i).ToleranceDays

                    'Production Data
                    If myClasss.ICaseString(obj(i).PhantomItem) = "Y" Then
                        IMaster.IsPhantom = BoYesNoEnum.tYES
                    ElseIf myClasss.ICaseString(obj(i).PhantomItem) = "N" Then
                        IMaster.IsPhantom = BoYesNoEnum.tNO
                    End If
                    'If myClasss.ICaseString(obj(i).IssueMethod) = "" Or myClasss.ICaseString(obj(i).IssueMethod) = "B" Then
                    '    'IMaster.IssueMethod = BoIssueMethod.im_Backflush
                    'ElseIf myClasss.ICaseString(obj(i).IssueMethod) = "M" Then
                    '    IMaster.IssueMethod = BoIssueMethod.im_Manual
                    'End If
                    IMaster.ProdStdCost = obj(i).ProductionStdCost
                    If myClasss.ICaseString(obj(i).IssueMethod) = "" Or myClasss.ICaseString(obj(i).IssueMethod) = "N" Then
                        IMaster.InCostRollup = BoYesNoEnum.tNO
                    ElseIf myClasss.ICaseString(obj(i).IssueMethod) = "Y" Then
                        IMaster.InCostRollup = BoYesNoEnum.tYES
                    End If



                    IMaster.User_Text = obj(i).UserText

                    'UOM & UDF assign
                    IMaster.InventoryUOM = obj(i).InventoryUOM
                    IMaster.UserFields.Fields.Item("U_Type").Value = myClasss.ICaseString(obj(i).U_Type)
                    IMaster.UserFields.Fields.Item("U_ProductComposition").Value = myClasss.ICaseString(obj(i).U_ProductComposition)
                    IMaster.UserFields.Fields.Item("U_StorageCondition").Value = myClasss.ICaseString(obj(i).U_StorageCondition)
                    IMaster.UserFields.Fields.Item("U_HowToEat").Value = myClasss.ICaseString(obj(i).U_HowToEat)
                    IMaster.UserFields.Fields.Item("U_CertifiedCode").Value = myClasss.ICaseString(obj(i).U_Certifiedcode)

                    RetVal = IMaster.Add()
                    If (RetVal <> 0) Then
                        oCompany.GetLastError(_lErrCode, _sErrMsg)
                        returnstatus = New ReturnStatus With {
                            .ErrirMsg = _sErrMsg,
                            .ErrorCode = _lErrCode,
                            .WEBDocNum = obj(i).WebDocNum
                        }
                        ls_returnstatus.Add(returnstatus)
                    Else
                        returnstatus = New ReturnStatus With {
                            .ErrirMsg = "Add Successfully",
                            .ErrorCode = 0,
                            .WEBDocNum = obj(i).WebDocNum,
                            .DocEntry = oCompany.GetNewObjectKey()
                        }
                        ls_returnstatus.Add(returnstatus)
                    End If
                    i = i + 1
                Loop
            Else
                returnstatus = New ReturnStatus With {
                    .ErrirMsg = oLoginService.sErrMsg,
                    .ErrorCode = oLoginService.lErrCode,
                    .SAPDocNum = "",
                    .WEBDocNum = "",
                    .DocEntry = ""
                }
                ls_returnstatus.Add(returnstatus)
            End If
        Catch ex As Exception
            returnstatus = New ReturnStatus With {
                .ErrirMsg = ex.Message,
                .ErrorCode = ex.HResult,
                .SAPDocNum = "",
                .WEBDocNum = "",
                .DocEntry = ""
            }
            ls_returnstatus.Add(returnstatus)
        End Try
        Return ls_returnstatus
    End Function

    Public Function UpdateItemMaster(ByVal obj As List(Of ItemMasterData)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim returnstatus As ReturnStatus
        Dim IMaster As SAPbobsCOM.Items = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim myClasss As New myClassOfFuntion

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                IMaster = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
                Dim i As Integer
                i = 0
                Do While i < obj.Count
                    RetVal = IMaster.GetByKey(obj(i).ItemCode)

                    If RetVal > 0 Then
                        'Update Error
                        oCompany.GetLastError(_lErrCode, _sErrMsg)
                        returnstatus = New ReturnStatus With {
                            .ErrirMsg = _sErrMsg,
                            .ErrorCode = _lErrCode,
                            .WEBDocNum = obj(i).WebDocNum,
                            .DocEntry = 0,
                            .SAPDocNum = obj(i).ItemCode
                        }
                        ls_returnstatus.Add(returnstatus)
                    Else
                        'Update Item Master Data
                        ' IMaster.ItemCode = obj(i).ItemCode
                        IMaster.ItemName = obj(i).ItemName
                        IMaster.ForeignName = obj(i).FrgName
                        If obj(i).UomGroup <> 0 Then
                            IMaster.UoMGroupEntry = obj(i).UomGroup
                        End If

                        IMaster.BarCodes.Delete()
                        If myClasss.ICaseNumber(obj(i).CodeBar) <> 0 Then
                            For Each l In obj(i).CodeBar ' Create Code Bar
                                If myClasss.ICaseNumber(obj(i).UomGroup) = 0 Or myClasss.ICaseNumber(obj(i).UomGroup) = -1 Then  ' IF UOM GROUP =-1 UOM GROUP IN BARCODE ALSO -1
                                    IMaster.BarCodes.UoMEntry = -1
                                Else
                                    IMaster.BarCodes.UoMEntry = l.BcdUOMCode
                                End If

                                IMaster.BarCodes.BarCode = l.BcdCode
                                IMaster.BarCodes.FreeText = l.BcdName
                                IMaster.BarCodes.Add()
                            Next
                        End If

                        IMaster.ItemsGroupCode = obj(i).ItmsGrpCod
                        IMaster.Manufacturer = obj(i).FirmCode
                        IMaster.PricingUnit = obj(i).PricingUnit

                        'Batch Number
                        If obj(i).ManBatchNum = "Y" Then
                            IMaster.ManageBatchNumbers = SAPbobsCOM.BoYesNoEnum.tYES

                            If obj(i).ManagmtMethod = "R" Then
                                IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnReleaseOnly
                            Else
                                IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction
                            End If

                            If obj(i).IssuePrimarilyBy = 1 Then
                                IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbBinLocations
                            Else
                                IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbSerialAndBatchNumbers
                            End If
                        End If

                        'Serial Number
                        If obj(i).ManSerNum = "Y" Then
                            IMaster.ManageSerialNumbers = SAPbobsCOM.BoYesNoEnum.tYES

                            If obj(i).ManagmtMethod = "R" Then
                                IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnReleaseOnly
                            Else
                                IMaster.SRIAndBatchManageMethod = SAPbobsCOM.BoManageMethod.bomm_OnEveryTransaction
                            End If

                            If obj(i).IssuePrimarilyBy = 1 Then
                                IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbBinLocations
                            Else
                                IMaster.IssuePrimarilyBy = SAPbobsCOM.IssuePrimarilyByEnum.ipbSerialAndBatchNumbers
                            End If
                        End If

                        If obj(i).GLMethod = "W" Then ' Item Manager
                            IMaster.GLMethod = SAPbobsCOM.BoGLMethods.glm_WH
                        ElseIf obj(i).GLMethod = "C" Then
                            IMaster.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass
                        ElseIf obj(i).GLMethod = "L" Then
                            IMaster.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemLevel
                        End If

                        If obj(i).WTLiable = "Y" Then 'Widthholding Tax
                            IMaster.WTLiable = SAPbobsCOM.BoYesNoEnum.tYES
                        ElseIf obj(i).WTLiable = "N" Then
                            IMaster.WTLiable = SAPbobsCOM.BoYesNoEnum.tNO
                        End If

                        ' Purchase & Sales (Packing, Unit Qty, Qty Per Unit)
                        IMaster.PurchasePackagingUnit = myClasss.ICaseString(obj(i).PurchasePackagingUoMName)
                        IMaster.SalesPackagingUnit = myClasss.ICaseString(obj(i).SalePackagingUoMName)
                        IMaster.SalesUnit = myClasss.ICaseString(obj(i).SalesUoMName)
                        IMaster.PurchaseUnit = myClasss.ICaseString(obj(i).PurchasingUoMName)

                        If myClasss.ICaseNumber(obj(i).PurchaseItemsPerUnit) <> 0 Then
                            IMaster.PurchaseItemsPerUnit = obj(i).PurchaseItemsPerUnit
                        End If

                        If myClasss.ICaseNumber(obj(i).PurchaseQtyPerPackUnit) <> 0 Then
                            IMaster.PurchaseQtyPerPackUnit = obj(i).PurchaseQtyPerPackUnit
                        End If

                        If myClasss.ICaseNumber(obj(i).SalesItemsPerUnit) <> 0 Then
                            IMaster.SalesItemsPerUnit = obj(i).SalesItemsPerUnit
                        End If

                        If myClasss.ICaseNumber(obj(i).SalesQtyPerPackUnit) <> 0 Then
                            IMaster.SalesQtyPerPackUnit = obj(i).SalesQtyPerPackUnit
                        End If

                        'Purchase & Sales (Masure)
                        IMaster.SalesUnitHeight = obj(i).SHeight
                        IMaster.SalesUnitWidth = obj(i).SWidth
                        IMaster.SalesUnitLength = obj(i).SLength
                        IMaster.SalesUnitWeight = obj(i).Sweight
                        If myClasss.ICaseNumber(obj(i).SVolume) <> 0 Then
                            IMaster.SalesVolumeUnit = obj(i).SVolume
                        End If

                        IMaster.PurchaseUnitHeight = obj(i).BHeight
                        IMaster.PurchaseUnitWidth = obj(i).BWidth
                        IMaster.PurchaseUnitLength = obj(i).BLength
                        IMaster.PurchaseUnitWeight = obj(i).BWeight
                        If myClasss.ICaseNumber(obj(i).BVolume) <> 0 Then
                            IMaster.PurchaseVolumeUnit = obj(i).BVolume
                        End If

                        ' Item For
                        If obj(i).PrchseItem = "N" Then
                            IMaster.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tNO
                        ElseIf obj(i).PrchseItem = "Y" Then
                            IMaster.PurchaseItem = SAPbobsCOM.BoYesNoEnum.tYES
                        End If

                        If obj(i).SellItem = "N" Then
                            IMaster.SalesItem = SAPbobsCOM.BoYesNoEnum.tNO
                        ElseIf obj(i).SellItem = "Y" Then
                            IMaster.SalesItem = SAPbobsCOM.BoYesNoEnum.tYES
                        End If

                        If obj(i).InvntItem = "N" Then
                            IMaster.InventoryItem = SAPbobsCOM.BoYesNoEnum.tNO
                        ElseIf obj(i).InvntItem = "Y" Then
                            IMaster.InventoryItem = SAPbobsCOM.BoYesNoEnum.tYES
                        End If

                        'Tab Planning Data
                        If myClasss.ICaseString(obj(i).PlanningMethod) = "" Then
                            IMaster.PlanningSystem = BoPlanningSystem.bop_MRP
                        ElseIf myClasss.ICaseString(obj(i).PlanningMethod) = "N" Then
                            IMaster.PlanningSystem = BoPlanningSystem.bop_None
                        ElseIf myClasss.ICaseString(obj(i).PlanningMethod) = "M" Then
                            IMaster.PlanningSystem = BoPlanningSystem.bop_MRP
                        End If

                        'ProcurementMethod & Component Whs
                        If myClasss.ICaseString(obj(i).ProcurementMethod) = "" Then
                            IMaster.ProcurementMethod = BoProcurementMethod.bom_Buy
                        ElseIf myClasss.ICaseString(obj(i).ProcurementMethod) = "B" Then
                            IMaster.ProcurementMethod = BoProcurementMethod.bom_Buy
                        ElseIf myClasss.ICaseString(obj(i).ProcurementMethod) = "M" Then
                            IMaster.ProcurementMethod = BoProcurementMethod.bom_Make
                            If myClasss.ICaseString(obj(i).ComponentWarehouse) = "" Or myClasss.ICaseString(obj(i).ComponentWarehouse) = "B" Then
                                IMaster.ComponentWarehouse = BoMRPComponentWarehouse.bomcw_BOM
                            ElseIf myClasss.ICaseString(obj(i).ComponentWarehouse) = "P" Then
                                IMaster.ComponentWarehouse = BoMRPComponentWarehouse.bomcw_Parent
                            End If
                        End If

                        If myClasss.ICaseString(obj(i).OrderInterval) <> "" Then
                            IMaster.OrderIntervals = obj(i).OrderInterval
                        End If

                        IMaster.MinOrderQuantity = myClasss.ICaseNumber(obj(i).MinimumOrderQty)
                        IMaster.LeadTime = obj(i).LeadTime
                        IMaster.ToleranceDays = obj(i).ToleranceDays

                        'Production Data
                        If myClasss.ICaseString(obj(i).PhantomItem) = "Y" Then
                            IMaster.IsPhantom = BoYesNoEnum.tYES
                        ElseIf myClasss.ICaseString(obj(i).PhantomItem) = "N" Then
                            IMaster.IsPhantom = BoYesNoEnum.tNO
                        End If
                        'If myClasss.ICaseString(obj(i).IssueMethod) = "" Or myClasss.ICaseString(obj(i).IssueMethod) = "B" Then
                        '    'IMaster.IssueMethod = BoIssueMethod.im_Backflush
                        'ElseIf myClasss.ICaseString(obj(i).IssueMethod) = "M" Then
                        '    IMaster.IssueMethod = BoIssueMethod.im_Manual
                        'End If
                        IMaster.ProdStdCost = obj(i).ProductionStdCost
                        If myClasss.ICaseString(obj(i).IssueMethod) = "" Or myClasss.ICaseString(obj(i).IssueMethod) = "N" Then
                            IMaster.InCostRollup = BoYesNoEnum.tNO
                        ElseIf myClasss.ICaseString(obj(i).IssueMethod) = "Y" Then
                            IMaster.InCostRollup = BoYesNoEnum.tYES
                        End If

                        IMaster.User_Text = obj(i).UserText

                        'UOM & UDF assign
                        IMaster.InventoryUOM = obj(i).InventoryUOM
                        IMaster.UserFields.Fields.Item("U_Type").Value = myClasss.ICaseString(obj(i).U_Type)
                        IMaster.UserFields.Fields.Item("U_ProductComposition").Value = myClasss.ICaseString(obj(i).U_ProductComposition)
                        IMaster.UserFields.Fields.Item("U_StorageCondition").Value = myClasss.ICaseString(obj(i).U_StorageCondition)
                        IMaster.UserFields.Fields.Item("U_HowToEat").Value = myClasss.ICaseString(obj(i).U_HowToEat)
                        IMaster.UserFields.Fields.Item("U_CertifiedCode").Value = myClasss.ICaseString(obj(i).U_Certifiedcode)

                        RetVal = IMaster.Update()
                        If (RetVal <> 0) Then
                            oCompany.GetLastError(_lErrCode, _sErrMsg)
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = _sErrMsg,
                                .ErrorCode = _lErrCode,
                                .WEBDocNum = obj(i).WebDocNum
                            }
                            ls_returnstatus.Add(returnstatus)
                        Else
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = "Add Successfully",
                                .ErrorCode = 0,
                                .WEBDocNum = obj(i).WebDocNum,
                                .DocEntry = oCompany.GetNewObjectKey()
                            }
                            ls_returnstatus.Add(returnstatus)
                        End If
                    End If
                    i = i + 1
                Loop
            Else
                returnstatus = New ReturnStatus With {
                    .ErrirMsg = oLoginService.sErrMsg,
                    .ErrorCode = oLoginService.lErrCode,
                    .SAPDocNum = "",
                    .WEBDocNum = "",
                    .DocEntry = ""
                }
                ls_returnstatus.Add(returnstatus)
            End If
        Catch ex As Exception
            returnstatus = New ReturnStatus With {
                .ErrirMsg = ex.Message,
                .ErrorCode = ex.HResult,
                .SAPDocNum = "",
                .WEBDocNum = "",
                .DocEntry = ""
            }
            ls_returnstatus.Add(returnstatus)
        End Try
        Return ls_returnstatus
    End Function

    Public Function AddOrUpdateBarCodeOfItemMaster(ByVal obj As List(Of ItemMasterData)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim returnstatus As ReturnStatus
        Dim IMaster As SAPbobsCOM.Items = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Dim myClasss As New myClassOfFuntion
        Dim UomEntry As Integer = 0, UgpEntry As Integer = 0
        Dim Sql As String = ""

        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                IMaster = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
                Dim i As Integer
                i = 0
                Do While i < obj.Count

                    RetVal = IMaster.GetByKey(obj(i).ItemCode)
                    Sql = "SELECT T0.""UgpEntry"" FROM " & _DBNAME & ".""OITM"" T0 LEFT JOIN " & _DBNAME & ".""OUOM"" A ON T0.""BuyUnitMsr""=A.""UomCode"" LEFT JOIN " & _DBNAME & ".""UGP1"" B ON A.""UomEntry""=B.""UomEntry"" LEFT JOIN " & _DBNAME & ".""OUGP"" C ON B.""UgpEntry""=C.""UgpEntry"" WHERE T0.""ItemCode""='" & obj(i).ItemCode & "'"
                    UgpEntry = myClasss.GetValFromQueryReturnNumberOCompany(Sql, oCompany) 'GetUomEntry
                    If UgpEntry = -1 Then
                        UomEntry = -1
                    Else
                        Sql = "SELECT B.""UomEntry"" FROM " & _DBNAME & ".""OITM"" T0 LEFT JOIN " & _DBNAME & ".""OUOM"" A ON T0.""BuyUnitMsr""=A.""UomCode"" LEFT JOIN " & _DBNAME & ".""UGP1"" B ON A.""UomEntry""=B.""UomEntry"" LEFT JOIN " & _DBNAME & ".""OUGP"" C ON B.""UgpEntry""=C.""UgpEntry"" WHERE T0.""ItemCode""='" & obj(i).ItemCode & "'"
                        UomEntry = myClasss.GetValFromQueryReturnNumberOCompany(Sql, oCompany) 'GetUomEntry
                    End If
                    If RetVal > 0 Then
                        'Update Error
                        oCompany.GetLastError(_lErrCode, _sErrMsg)
                        returnstatus = New ReturnStatus With {
                            .ErrirMsg = _sErrMsg,
                            .ErrorCode = _lErrCode,
                            .WEBDocNum = obj(i).WebDocNum,
                            .DocEntry = 0,
                            .SAPDocNum = obj(i).ItemCode
                        }
                        ls_returnstatus.Add(returnstatus)
                    Else
                        'Update Item Master Data BarCode

                        If myClasss.ICaseNumber(obj(i).CodeBar) <> 0 Then
                            For Each l In obj(i).CodeBar ' Create Code Bar
                                If myClasss.GetValFromQueryReturnNumberOCompany("SELECT COUNT(*) FROM " & _DBNAME & ".""OBCD"" WHERE ""ItemCode""='" & obj(i).ItemCode & "' AND ""UomEntry""=" & UomEntry & "  AND ""BcdCode""='" & l.BcdCode & "'", oCompany) = 0 Then
                                    IMaster.BarCodes.UoMEntry = UomEntry
                                    IMaster.BarCodes.BarCode = l.BcdCode
                                    IMaster.BarCodes.FreeText = l.BcdName
                                    IMaster.BarCodes.Add()
                                End If

                            Next
                        End If

                        RetVal = IMaster.Update()
                        If (RetVal <> 0) Then
                            oCompany.GetLastError(_lErrCode, _sErrMsg)
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = _sErrMsg,
                                .ErrorCode = _lErrCode,
                                .WEBDocNum = obj(i).WebDocNum
                            }
                            ls_returnstatus.Add(returnstatus)
                        Else
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = "Add Successfully",
                                .ErrorCode = 0,
                                .WEBDocNum = obj(i).WebDocNum,
                                .DocEntry = oCompany.GetNewObjectKey()
                            }
                            ls_returnstatus.Add(returnstatus)
                        End If
                    End If
                    i = i + 1
                Loop
            Else
                returnstatus = New ReturnStatus With {
                    .ErrirMsg = oLoginService.sErrMsg,
                    .ErrorCode = oLoginService.lErrCode,
                    .SAPDocNum = "",
                    .WEBDocNum = "",
                    .DocEntry = ""
                }
                ls_returnstatus.Add(returnstatus)
            End If
        Catch ex As Exception
            returnstatus = New ReturnStatus With {
                .ErrirMsg = ex.Message,
                .ErrorCode = ex.HResult,
                .SAPDocNum = "",
                .WEBDocNum = "",
                .DocEntry = ""
            }
            ls_returnstatus.Add(returnstatus)
        End Try
        Return ls_returnstatus
    End Function

    'Public Function AddOrUpdateBarCodeOfItemMasterInGooodsReceipt(ByVal oItemCode As String, ByVal oBarCode As String) As List(Of ReturnStatus)
    '    Dim ls_returnstatus As New List(Of ReturnStatus)
    '    Dim returnstatus As ReturnStatus
    '    Dim IMaster As SAPbobsCOM.Items = Nothing
    '    Dim RetVal As Integer = 0
    '    Dim xDocEntry As Integer = 0
    '    Dim myClasss As New myClassOfFuntion
    '    Dim UomEntry As Integer = 0, UgpEntry As Integer = 0
    '    Dim Sql As String = ""

    '    'Dim oBarCodeParams As SAPbobsCOM.BarCodeParams

    '    Try
    '        Dim oLoginService As New LoginServiceWebRef
    '        If oLoginService.lErrCode = 0 Then
    '            oCompany = oLoginService.Company
    '            IMaster = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
    '            RetVal = IMaster.GetByKey(oItemCode)

    '            Dim oCompanyService As SAPbobsCOM.ICompanyService = CType(oCompany.GetCompanyService(), SAPbobsCOM.ICompanyService)
    '            Dim oBarCodesService As SAPbobsCOM.BarCodesService = CType(oCompanyService.GetBusinessServiceSAPbobsCOM.ServiceTypes.BarCodesService, SAPbobsCOM.BarCodesService)
    '            Dim objBarCode As SAPbobsCOM.BarCode = CType(oBarCodesService.GetDataInterface(BarCodesServiceDataInterfaces.bsBarCode), SAPbobsCOM.BarCode)

    '            Sql = "SELECT T0.""UgpEntry"" FROM " & _DBNAME & ".""OITM"" T0 LEFT JOIN " & _DBNAME & ".""OUOM"" A ON T0.""BuyUnitMsr""=A.""UomCode"" LEFT JOIN " & _DBNAME & ".""UGP1"" B ON A.""UomEntry""=B.""UomEntry"" LEFT JOIN " & _DBNAME & ".""OUGP"" C ON B.""UgpEntry""=C.""UgpEntry"" WHERE T0.""ItemCode""='" & oItemCode & "'"
    '            UgpEntry = myClasss.GetValFromQueryReturnNumberOCompany(Sql, oCompany) 'GetUomEntry
    '            If UgpEntry = -1 Then ' If UomGroup = Manual UomEntry will automatic -1 too
    '                UomEntry = -1
    '            Else
    '                Sql = "SELECT B.""UomEntry"" FROM " & _DBNAME & ".""OITM"" T0 LEFT JOIN " & _DBNAME & ".""OUOM"" A ON T0.""BuyUnitMsr""=A.""UomCode"" LEFT JOIN " & _DBNAME & ".""UGP1"" B ON A.""UomEntry""=B.""UomEntry"" LEFT JOIN " & _DBNAME & ".""OUGP"" C ON B.""UgpEntry""=C.""UgpEntry"" WHERE T0.""ItemCode""='" & oItemCode & "'"
    '                UomEntry = myClasss.GetValFromQueryReturnNumberOCompany(Sql, oCompany) 'GetUomEntry
    '            End If

    '            If RetVal > 0 Then
    '                'Update Error
    '                oCompany.GetLastError(_lErrCode, _sErrMsg)
    '                returnstatus = New ReturnStatus With {
    '                    .ErrirMsg = _sErrMsg,
    '                    .ErrorCode = _lErrCode,
    '                    .WEBDocNum = "",
    '                    .DocEntry = 0,
    '                    .SAPDocNum = oItemCode
    '                }
    '                ls_returnstatus.Add(returnstatus)
    '            Else
    '                'Update Item Master Data BarCode

    '                If myClasss.ICaseString(oBarCode) <> "" Then
    '                    If myClasss.GetValFromQueryReturnNumberOCompany("SELECT COUNT(*) FROM " & _DBNAME & ".""OBCD"" WHERE ""ItemCode""='" & oItemCode & "' AND ""UomEntry""=" & UomEntry & "  AND ""BcdCode""='" & oBarCode & "'", oCompany) = 0 Then
    '                        objBarCode.ItemNo = oItemCode
    '                        objBarCode.BarCode = oBarCode
    '                        objBarCode.UoMEntry = UomEntry
    '                        oBarCodesService.Add(objBarCode)
    '                    End If
    '                End If

    '                RetVal = IMaster.Update()
    '                If 1 = 1 Then
    '                    returnstatus = New ReturnStatus With {
    '                        .ErrirMsg = "Add Successfully",
    '                        .ErrorCode = 0,
    '                        .WEBDocNum = "",
    '                        .DocEntry = oCompany.GetNewObjectKey()
    '                    }
    '                    ls_returnstatus.Add(returnstatus)
    '                End If
    '            End If
    '        Else
    '            returnstatus = New ReturnStatus With {
    '                .ErrirMsg = oLoginService.sErrMsg,
    '                .ErrorCode = oLoginService.lErrCode,
    '                .SAPDocNum = "",
    '                .WEBDocNum = "",
    '                .DocEntry = ""
    '            }
    '            ls_returnstatus.Add(returnstatus)
    '        End If
    '    Catch ex As Exception
    '        returnstatus = New ReturnStatus With {
    '            .ErrirMsg = ex.Message,
    '            .ErrorCode = ex.HResult,
    '            .SAPDocNum = "",
    '            .WEBDocNum = "",
    '            .DocEntry = ""
    '        }
    '        ls_returnstatus.Add(returnstatus)
    '    End Try



    '    'Try
    '    '    Dim oLoginService As New LoginServiceWebRef
    '    '    If oLoginService.lErrCode = 0 Then
    '    '        oCompany = oLoginService.Company
    '    '        IMaster = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
    '    '        RetVal = IMaster.GetByKey(oItemCode)
    '    '        Sql = "SELECT T0.""UgpEntry"" FROM " & _DBNAME & ".""OITM"" T0 LEFT JOIN " & _DBNAME & ".""OUOM"" A ON T0.""BuyUnitMsr""=A.""UomCode"" LEFT JOIN " & _DBNAME & ".""UGP1"" B ON A.""UomEntry""=B.""UomEntry"" LEFT JOIN " & _DBNAME & ".""OUGP"" C ON B.""UgpEntry""=C.""UgpEntry"" WHERE T0.""ItemCode""='" & oItemCode & "'"
    '    '        UgpEntry = myClasss.GetValFromQueryReturnNumberOCompany(Sql, oCompany) 'GetUomEntry
    '    '        If UgpEntry = -1 Then ' If UomGroup = Manual UomEntry will automatic -1 too
    '    '            UomEntry = -1
    '    '        Else
    '    '            Sql = "SELECT B.""UomEntry"" FROM " & _DBNAME & ".""OITM"" T0 LEFT JOIN " & _DBNAME & ".""OUOM"" A ON T0.""BuyUnitMsr""=A.""UomCode"" LEFT JOIN " & _DBNAME & ".""UGP1"" B ON A.""UomEntry""=B.""UomEntry"" LEFT JOIN " & _DBNAME & ".""OUGP"" C ON B.""UgpEntry""=C.""UgpEntry"" WHERE T0.""ItemCode""='" & oItemCode & "'"
    '    '            UomEntry = myClasss.GetValFromQueryReturnNumberOCompany(Sql, oCompany) 'GetUomEntry
    '    '        End If

    '    '        If RetVal > 0 Then
    '    '            'Update Error
    '    '            oCompany.GetLastError(_lErrCode, _sErrMsg)
    '    '            returnstatus = New ReturnStatus With {
    '    '                .ErrirMsg = _sErrMsg,
    '    '                .ErrorCode = _lErrCode,
    '    '                .WEBDocNum = "",
    '    '                .DocEntry = 0,
    '    '                .SAPDocNum = oItemCode
    '    '            }
    '    '            ls_returnstatus.Add(returnstatus)
    '    '        Else
    '    '            'Update Item Master Data BarCode

    '    '            If myClasss.ICaseString(oBarCode) <> "" Then
    '    '                If myClasss.GetValFromQueryReturnNumberOCompany("SELECT COUNT(*) FROM " & _DBNAME & ".""OBCD"" WHERE ""ItemCode""='" & oItemCode & "' AND ""UomEntry""=" & UomEntry & "  AND ""BcdCode""='" & oBarCode & "'", oCompany) = 0 Then
    '    '                    IMaster.BarCodes.UoMEntry = UomEntry
    '    '                    IMaster.BarCodes.BarCode = oBarCode
    '    '                    IMaster.BarCodes.FreeText = oBarCode
    '    '                    IMaster.BarCodes.Add()
    '    '                End If
    '    '            End If

    '    '            RetVal = IMaster.Update()
    '    '            If (RetVal <> 0) Then
    '    '                oCompany.GetLastError(_lErrCode, _sErrMsg)
    '    '                returnstatus = New ReturnStatus With {
    '    '                    .ErrirMsg = _sErrMsg,
    '    '                    .ErrorCode = _lErrCode,
    '    '                    .WEBDocNum = ""
    '    '                }
    '    '                ls_returnstatus.Add(returnstatus)
    '    '            Else
    '    '                returnstatus = New ReturnStatus With {
    '    '                    .ErrirMsg = "Add Successfully",
    '    '                    .ErrorCode = 0,
    '    '                    .WEBDocNum = "",
    '    '                    .DocEntry = oCompany.GetNewObjectKey()
    '    '                }
    '    '                ls_returnstatus.Add(returnstatus)
    '    '            End If
    '    '        End If
    '    '    Else
    '    '        returnstatus = New ReturnStatus With {
    '    '            .ErrirMsg = oLoginService.sErrMsg,
    '    '            .ErrorCode = oLoginService.lErrCode,
    '    '            .SAPDocNum = "",
    '    '            .WEBDocNum = "",
    '    '            .DocEntry = ""
    '    '        }
    '    '        ls_returnstatus.Add(returnstatus)
    '    '    End If
    '    'Catch ex As Exception
    '    '    returnstatus = New ReturnStatus With {
    '    '        .ErrirMsg = ex.Message,
    '    '        .ErrorCode = ex.HResult,
    '    '        .SAPDocNum = "",
    '    '        .WEBDocNum = "",
    '    '        .DocEntry = ""
    '    '    }
    '    '    ls_returnstatus.Add(returnstatus)
    '    'End Try
    '    Return ls_returnstatus
    'End Function



End Class
