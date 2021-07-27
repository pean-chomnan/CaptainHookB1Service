Public Class BPMasterData
    Public Property DocEntry As Integer
    Public Property DocType As String
    Public Property GroupID As Integer
    Public Property GroupName As String
    Public Property CardName As String
    Public Property CardCode As String
    Public Property Code As Integer
    Public Property ForeignName As String
    Public Property DocCurr As String
    Public Property TaxID As String
    Public Property WebDocNum As String
    Public Property PaidType As String
    Public Property BPType As String

    'Ship to
    Public Property Branch As String
    Public Property Line1 As String
    Public Property Line2 As String
    Public Property HouseNo As String
    Public Property Building As String
    Public Property Room As String
    Public Property Floor As String
    Public Property MooNo As String
    Public Property Road As String
    Public Property SubDistrict As String
    Public Property District As String
    Public Property ZipCode As String
    Public Property Province As String
    Public Property Country As String
    Public Property NameTitle As String
    Public Property TaxOffice As String

    'Pay to
    Public Property NamePayto As String

    'General
    Public Property Tel1 As String
    Public Property Tel2 As String
    Public Property MobilePhone As String
    Public Property Fax As String
    Public Property EMail As String

    'Contact Personal
    Public Property Name As String
    Public Property FristName As String
    Public Property LastName As String
    Public Property Position As String
    Public Property Address As String
    Public Property Tel_1 As String
    Public Property Tel_2 As String
    Public Property E_mail As String
    Public Property Remarks1 As String

    'Payment Treams
    Public Property PaymentTerms As String

    'Bank
    Public Property PaymentID As String
    Public Property BankID As String
    Public Property BankAcct As String
    Public Property BankName As String
    Public Property BranchAcct As String
    Public Property PromtpPayID As String
    Public Property PromtpPayType As String
    Public Property CardNumber As String
    Public Property IBAN As String

    ' House Bank
    Public Property HouseBankCountry As String
    Public Property HouseBankAccount As String
    Public Property HouseBank As String
    Public Property HouseBankBrand As String
    Public Property HouseBankIBAN As String   'HouseBankIBAN

    'Payment Methods Code
    Public Property PaymentMethodsCode As String
    Public Property WHTCode As List(Of ListWHTCode)
    ' Account Receivable 
    Public Property AcctReceivable As String
    ' Account Down Payment Clearing Account
    Public Property AcctDownPayment As String

End Class
Public Class ListWHTCode
    Public Property WHTCode As String
End Class