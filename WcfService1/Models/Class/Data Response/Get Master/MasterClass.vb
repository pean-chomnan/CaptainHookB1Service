Public Class MasterClass
    Public Class OCRG  ' BP Group Code
        Public Property GroupCode As Integer
        Public Property Groupname As String
    End Class



    Public Class OITM
        Public Property ItemCode As String
        Public Property Descrition As String
        Public Property ItemGroupID As String
        Public Property UserText As String
        Public Property UomGroup As Integer
        Public Property U_Category As String
        Public Property ItemCategory As String
        Public Property ActiveStatus As String

    End Class
    Public Class OITB
        Public Property ItmGroupCode As String
        Public Property itmGroupName As String

    End Class
    Public Class OUBR
        Public Property Code As String
        Public Property Name As String
        Public Property Remarks As String

    End Class
    Public Class OPRC
        Public Property PrcCode As String
        Public Property PrcName As String
        Public Property DimCode As Integer
        Public Property CCTypeCode As String

    End Class
    Public Class OHPS
        Public Property PosID As String
        Public Property PosName As String
        Public Property PosDescr As String
    End Class
    Public Class OUDP
        Public Property Code As String
        Public Property Name As String
        Public Property Remarks As String
    End Class
    Public Class OHEM
        Public Property EmployeeID As String
        Public Property Password As String
        Public Property Title As String
        Public Property F_Name As String
        Public Property L_Name As String
        Public Property DivisionID As String
        Public Property PositionID As String
        Public Property DepartmentID As String
        Public Property BranchID As String
        Public Property Email As String
        Public Property Active As String

    End Class
    Public Class OCRD
        Public Property CardCode As String
        Public Property CardName As String
        Public Property Address As String
        Public Property Branch As String
        Public Property TaxID As String
    End Class
End Class
