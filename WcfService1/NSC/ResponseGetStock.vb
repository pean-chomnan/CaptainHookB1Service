Public Class ResponseGetStock
    Public Property ErrCode
    Public Property ErrMsg
    Public Property ls_data As List(Of Stock)

    Public Sub New()
        ErrCode = 0
        ErrMsg = ""
        ls_data = New List(Of Stock)
    End Sub



    Public Class GetStock
        Public Function Execute(WhsCode As String, BinCode As String) As ResponseGetStock
            Try
                Dim result As New ResponseGetStock
                Dim oCompany As SAPbobsCOM.Company = Nothing
                Dim oRs As SAPbobsCOM.Recordset = Nothing
                Dim strSql As String = ""
                Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
                Dim oLoginService As New LoginServiceWebRef
                Dim ls As New List(Of Stock)

                If oLoginService.lErrCode = 0 Then
                    oCompany = oLoginService.Company
                    oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    'strSql = "CALL " & _DBNAME & ".""USP_GetBatchMasterByBox""('" & BoxNumber & "');"
                    oRs.DoQuery(strSql)
                    Do While Not oRs.EoF
                        ls.Add(New Stock With {
                            .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                            .ItemName = oRs.Fields.Item("ItemName").Value.ToString.Trim,
                            .WhsCode = oRs.Fields.Item("WhsCode").Value.ToString.Trim,
                            .WhsName = oRs.Fields.Item("WhsName").Value.ToString.Trim,
                            .BinCode = oRs.Fields.Item("BinCode").Value.ToString.Trim,
                            .BinEntry = oRs.Fields.Item("BinEntry").Value.ToString.Trim,
                            .BatchNo = oRs.Fields.Item("BatchNo").Value.ToString.Trim,
                            .Quantity = oRs.Fields.Item("Quantity").Value.ToString.Trim,
                            .UOMEntry = oRs.Fields.Item("UOMEntry").Value.ToString.Trim,
                            .UOMCode = oRs.Fields.Item("UOMCode").Value.ToString.Trim
                        })
                        oRs.MoveNext()
                    Loop

                    result.ls_data = ls

                Else
                    result.ErrCode = oLoginService.lErrCode
                    result.ErrMsg = oLoginService.sErrMsg
                End If


                Return result
            Catch ex As Exception

                Dim result As New ResponseGetStock
                result.ErrMsg = ex.Message
                result.ErrCode = ex.GetHashCode
                Return result
            End Try
        End Function

    End Class

    Public Class Stock
        Public Property ItemCode As String
        Public Property ItemName As String
        Public Property WhsCode As String
        Public Property WhsName As String

        Public Property BinEntry As String
        Public Property BinCode As String

        Public Property BatchNo As String
        Public Property Quantity As Double

        Public Property UOMEntry As String
        Public Property UOMCode As String
    End Class

End Class
