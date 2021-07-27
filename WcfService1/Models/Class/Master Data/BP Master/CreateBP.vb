Public Class CreateBP
    Dim oCompany As SAPbobsCOM.Company = Nothing
    Private _lErrCode As Integer
    Private _sErrMsg As String
    Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

    Public Function SendBPMaster(ByVal obj As List(Of BPMasterData)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim returnstatus As ReturnStatus
        Dim BP As SAPbobsCOM.BusinessPartners = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0
        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                BP = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners)
                Dim i As Integer
                i = 0
                Do While i < obj.Count
                    '     BP.Series = obj(i)
                    BP.CardCode = obj(i).CardCode
                    BP.Currency = obj(i).DocCurr
                    BP.CardName = obj(i).CardName
                    BP.CardForeignName = obj(i).ForeignName
                    BP.FederalTaxID = obj(i).TaxID
                    If obj(i).GroupID <> Nothing Then
                        BP.GroupCode = obj(i).GroupID
                    End If

                    If obj(i).DocType = "C" Then
                        BP.CardType = SAPbobsCOM.BoCardTypes.cCustomer
                    ElseIf obj(i).DocType = "V" Then
                        BP.CardType = SAPbobsCOM.BoCardTypes.cSupplier
                    End If

                    BP.DebitorAccount = obj(i).AcctReceivable
                    BP.DownPaymentClearAct = obj(i).AcctDownPayment

                    ' General
                    BP.Phone1 = obj(i).Tel1
                    BP.Phone2 = obj(i).Tel2
                    BP.Cellular = obj(i).MobilePhone
                    BP.Fax = obj(i).Fax
                    BP.EmailAddress = obj(i).EMail

                    'Contract Person
                    BP.ContactEmployees.SetCurrentLine(0)
                    BP.ContactEmployees.Name = obj(i).Name
                    BP.ContactEmployees.FirstName = obj(i).FristName
                    BP.ContactEmployees.LastName = obj(i).LastName
                    BP.ContactEmployees.Position = obj(i).Position
                    BP.ContactEmployees.Remarks1 = obj(i).Remarks1
                    BP.ContactEmployees.Address = obj(i).Address
                    BP.ContactEmployees.Phone1 = obj(i).Tel_1
                    BP.ContactEmployees.Phone2 = obj(i).Tel_2
                    BP.ContactEmployees.E_Mail = obj(i).E_mail
                    BP.ContactEmployees.Add()

                    ' Payment Treams
                    BP.PayTermsGrpCode = obj(i).PaymentTerms

                    ' Ship to 
                    BP.Addresses.SetCurrentLine(0)
                    BP.Addresses.AddressName = obj(i).Branch
                    BP.Addresses.AddressName2 = obj(i).Line1
                    BP.Addresses.AddressName3 = obj(i).Line2
                    BP.Addresses.Street = obj(i).Road
                    BP.Addresses.StreetNo = obj(i).HouseNo
                    BP.Addresses.BuildingFloorRoom = obj(i).Room
                    BP.Addresses.ZipCode = obj(i).ZipCode
                    BP.Addresses.Country = obj(i).Country
                    BP.Addresses.County = obj(i).Province
                    BP.Addresses.Block = obj(i).SubDistrict
                    BP.Addresses.City = obj(i).District
                    'BP.Addresses.UserFields.Fields.Item("U_MooNo").Value = obj(i).MooNo
                    'BP.Addresses.UserFields.Fields.Item("U_BuildingNo").Value = obj(i).Building
                    'BP.Addresses.UserFields.Fields.Item("U_FloorNo").Value = obj(i).Floor
                    ' update 05.10.2020
                    BP.Addresses.FederalTaxID = obj(i).TaxID
                    BP.Addresses.GlobalLocationNumber = obj(i).NameTitle
                    BP.Addresses.TaxOffice = obj(i).TaxOffice

                    BP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_ShipTo
                    BP.Addresses.Add()

                    ' Pay to 
                    If obj(i).DocType = "C" Then
                        BP.Addresses.AddressName = obj(i).Branch '
                    ElseIf obj(i).DocType = "V" Then
                        BP.Addresses.AddressName = obj(i).NamePayto '
                    End If

                    BP.Addresses.AddressName2 = obj(i).Line1
                    BP.Addresses.AddressName3 = obj(i).Line2
                    BP.Addresses.Street = obj(i).Road
                    BP.Addresses.StreetNo = obj(i).HouseNo
                    BP.Addresses.BuildingFloorRoom = obj(i).Room
                    BP.Addresses.ZipCode = obj(i).ZipCode
                    BP.Addresses.Country = obj(i).Country
                    BP.Addresses.County = obj(i).Province
                    BP.Addresses.Block = obj(i).SubDistrict
                    BP.Addresses.City = obj(i).District
                    'BP.Addresses.UserFields.Fields.Item("U_MooNo").Value = obj(i).MooNo
                    'BP.Addresses.UserFields.Fields.Item("U_BuildingNo").Value = obj(i).Building
                    'BP.Addresses.UserFields.Fields.Item("U_FloorNo").Value = obj(i).Floor

                    ' update 05.10.2020

                    BP.Addresses.FederalTaxID = obj(i).TaxID
                    BP.Addresses.GlobalLocationNumber = obj(i).NameTitle
                    BP.Addresses.TaxOffice = obj(i).TaxOffice

                    BP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo
                    BP.Addresses.Add()

                    ' Bank Setup
                    BP.BPBankAccounts.SetCurrentLine(0)
                    BP.BPBankAccounts.BankCode = obj(i).BankID
                    BP.BPBankAccounts.AccountNo = obj(i).BankAcct
                    BP.BPBankAccounts.AccountName = obj(i).BankName
                    BP.BPBankAccounts.Branch = obj(i).BranchAcct
                    BP.BPBankAccounts.IBAN = obj(i).IBAN

                    ' House Bank
                    BP.HouseBankCountry = obj(i).HouseBankCountry
                    BP.HouseBankAccount = obj(i).HouseBankAccount
                    BP.HouseBank = obj(i).HouseBank
                    BP.HouseBankBranch = obj(i).HouseBankBrand
                    '      BP.HouseBankIBAN = obj(i).HouseBankIBAN

                    'BP.UserFields.Fields.Item("U_PromptPayID").Value = obj(i).PromtpPayID
                    'BP.UserFields.Fields.Item("U_PromptType").Value = obj(i).PromtpPayType
                    'BP.UserFields.Fields.Item("U_CashCard").Value = obj(i).CardNumber
                    BP.BPBankAccounts.Add()

                    If obj(i).DocType = "V" Then
                        BP.BPPaymentMethods.PaymentMethodCode = obj(i).PaymentMethodsCode
                        BP.BPPaymentMethods.SetCurrentLine(0)
                        BP.BPPaymentMethods.Add()
                    End If

                    Dim subWHT As Boolean = False

                    Try
                        For Each W In obj(i).WHTCode

                            BP.BPWithholdingTax.WTCode = W.WHTCode
                            BP.BPWithholdingTax.SetCurrentLine(0)
                            BP.BPWithholdingTax.Add()
                            subWHT = True
                        Next
                        If subWHT = True Then
                            BP.SubjectToWithholdingTax = SAPbobsCOM.BoYesNoEnum.tYES
                        End If
                    Catch ex As Exception

                    End Try

                    RetVal = BP.Add()
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

    Public Function Update(ByVal obj As List(Of BPMasterData)) As List(Of ReturnStatus)
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim returnstatus As ReturnStatus
        Dim BP As SAPbobsCOM.BusinessPartners = Nothing
        Dim RetVal As Integer = 0
        Dim xDocEntry As Integer = 0

        Try

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                BP = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners)
                Dim i As Integer
                i = 0
                Do While i < obj.Count
                    RetVal = BP.GetByKey(obj(i).CardCode)
                    If RetVal > 0 Then
                        'Update Error
                        oCompany.GetLastError(_lErrCode, _sErrMsg)
                        returnstatus = New ReturnStatus With {
                            .ErrirMsg = _sErrMsg,
                            .ErrorCode = _lErrCode,
                            .WEBDocNum = obj(i).WebDocNum,
                            .DocEntry = obj(i).DocEntry,
                            .SAPDocNum = obj(i).CardCode
                        }
                        ls_returnstatus.Add(returnstatus)
                    Else

                        BP.Currency = obj(i).DocCurr
                        BP.CardName = obj(i).CardName
                        BP.CardForeignName = obj(i).ForeignName
                        BP.FederalTaxID = obj(i).TaxID
                        If obj(i).GroupID <> Nothing Then
                            BP.GroupCode = obj(i).GroupID
                        End If
                        ' General
                        BP.Phone1 = obj(i).Tel1
                        BP.Phone2 = obj(i).Tel2
                        BP.Cellular = obj(i).MobilePhone
                        BP.Fax = obj(i).Fax
                        BP.EmailAddress = obj(i).EMail

                        'Contact Person
                        BP.ContactEmployees.SetCurrentLine(0)
                        BP.ContactEmployees.Delete()

                        BP.ContactEmployees.Name = obj(i).Name
                        BP.ContactEmployees.FirstName = obj(i).FristName
                        BP.ContactEmployees.Position = obj(i).Position
                        BP.ContactEmployees.Remarks1 = obj(i).Remarks1
                        BP.ContactEmployees.Address = obj(i).Address
                        BP.ContactEmployees.Phone1 = obj(i).Tel_1
                        BP.ContactEmployees.E_Mail = obj(i).E_mail
                        BP.ContactEmployees.Add()

                        ' Payment Treams
                        BP.PayTermsGrpCode = obj(i).PaymentTerms

                        ' Ship to 
                        BP.Addresses.SetCurrentLine(0)
                        BP.Addresses.Delete()

                        BP.Addresses.AddressName = obj(i).Branch
                        BP.Addresses.AddressName2 = obj(i).Line1
                        BP.Addresses.AddressName3 = obj(i).Line2
                        BP.Addresses.Street = obj(i).Road
                        BP.Addresses.StreetNo = obj(i).HouseNo
                        BP.Addresses.BuildingFloorRoom = obj(i).Room
                        BP.Addresses.ZipCode = obj(i).ZipCode
                        BP.Addresses.Country = obj(i).Country
                        BP.Addresses.County = obj(i).Province
                        BP.Addresses.Block = obj(i).SubDistrict
                        BP.Addresses.City = obj(i).District
                        BP.Addresses.FederalTaxID = obj(i).TaxID
                        BP.Addresses.GlobalLocationNumber = obj(i).NameTitle
                        BP.Addresses.TaxOffice = obj(i).TaxOffice
                        BP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_ShipTo
                        BP.Addresses.Add()

                        ' Pay to 
                        If obj(i).DocType = "C" Then
                            BP.Addresses.AddressName = obj(i).Branch '
                        ElseIf obj(i).DocType = "V" Then
                            BP.Addresses.AddressName = obj(i).NamePayto '
                        End If

                        BP.Addresses.AddressName2 = obj(i).Line1
                        BP.Addresses.AddressName3 = obj(i).Line2
                        BP.Addresses.Street = obj(i).Road
                        BP.Addresses.StreetNo = obj(i).HouseNo
                        BP.Addresses.BuildingFloorRoom = obj(i).Room
                        BP.Addresses.ZipCode = obj(i).ZipCode
                        BP.Addresses.Country = obj(i).Country
                        BP.Addresses.County = obj(i).Province
                        BP.Addresses.Block = obj(i).SubDistrict
                        BP.Addresses.City = obj(i).District
                        BP.Addresses.FederalTaxID = obj(i).TaxID
                        BP.Addresses.GlobalLocationNumber = obj(i).NameTitle
                        BP.Addresses.TaxOffice = obj(i).TaxOffice
                        BP.Addresses.AddressType = SAPbobsCOM.BoAddressType.bo_BillTo
                        BP.Addresses.Add()

                        ' Bank Setup
                        BP.BPBankAccounts.SetCurrentLine(0)
                        BP.BPBankAccounts.Delete()

                        BP.BPBankAccounts.BankCode = obj(i).BankID
                        BP.BPBankAccounts.AccountNo = obj(i).BankAcct
                        BP.BPBankAccounts.AccountName = obj(i).BankName
                        BP.BPBankAccounts.Branch = obj(i).BranchAcct
                        BP.BPBankAccounts.IBAN = obj(i).IBAN
                        BP.BPBankAccounts.Add()

                        'House Bank
                        BP.HouseBankCountry = obj(i).HouseBankCountry
                        BP.HouseBankAccount = obj(i).HouseBankAccount
                        BP.HouseBank = obj(i).HouseBank
                        BP.HouseBankBranch = obj(i).HouseBankBrand

                        'Update Payment Method
                        If obj(i).DocType = "V" Then
                            BP.BPPaymentMethods.SetCurrentLine(0)
                            BP.BPPaymentMethods.Delete()
                            BP.BPPaymentMethods.PaymentMethodCode = obj(i).PaymentMethodsCode
                            BP.BPPaymentMethods.Add()
                        End If

                        RetVal = BP.Update
                        If RetVal <> 0 Then
                            'Update Error
                            oCompany.GetLastError(_lErrCode, _sErrMsg)
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = _sErrMsg,
                                .ErrorCode = _lErrCode,
                                .WEBDocNum = obj(i).WebDocNum,
                                .DocEntry = obj(i).DocEntry,
                                .SAPDocNum = obj(i).CardCode
                            }
                            ls_returnstatus.Add(returnstatus)
                        Else
                            returnstatus = New ReturnStatus With {
                                .ErrirMsg = "Updated Successfully",
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
End Class
