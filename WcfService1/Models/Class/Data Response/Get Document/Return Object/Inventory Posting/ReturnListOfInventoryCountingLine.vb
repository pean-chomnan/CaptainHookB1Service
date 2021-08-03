Public Class ReturnListOfInventoryCountingLine
    Public Property ErrCode As Integer
    Public Property ErrMsg As String
    Public Property ls_data As List(Of ListOfInventoryCountingLine)
End Class

Public Class ListOfInventoryCountingLine
    Public Property DocEntry As Integer
    Public Property LineNum As Integer
    Public Property ItemCode As String
    Public Property ItemDesc As String
    Public Property Freeze As String
    Public Property WhsCode As String
    Public Property InWhsQty As Double
    Public Property Counted As String
    Public Property CountQty As Double
    Public Property Remark As String
    Public Property BarCode As String
    Public Property InvUoM As String
    Public Property Difference As Double
    Public Property DiffPercen As Double
    Public Property CountDate As Date
    Public Property CountTime As String
    Public Property ProjCode As String
    Public Property OcrCode As String
    Public Property LineStatus As String
    Public Property BinEntry As Integer
    Public Property VisOrder As Integer
    Public Property OcrCode2 As String
    Public Property OcrCode3 As String
    Public Property OcrCode4 As String
    Public Property OcrCode5 As String
    Public Property FirmCode As Integer
    Public Property SuppCatNum As String
    Public Property PrefVendor As String
    Public Property CountDiff As Double
    Public Property CountDiffP As Double
    Public Property UomCode As String
    Public Property UomQty As Double
End Class

Public Class CReturnGetListOfInventoryCountingLine
    Public Function FGetListOfInventoryCountingLine(ByVal ls_Entry As List(Of Integer)) As ReturnListOfInventoryCountingLine
        Try
            Dim ls As New List(Of ListOfInventoryCountingLine)
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            Dim DocEntry As String = ""
            Dim _type As Integer = 0

            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                For i As Integer = 0 To ls_Entry.Count - 1
                    If i = 0 Then
                        DocEntry = ls_Entry(i)
                    Else
                        DocEntry = DocEntry & "," & ls_Entry(i)
                    End If
                Next

                strSql = "CALL " & _DBNAME & ".""USP_ListOfInventoryCoutingLine""('" & DocEntry & "');"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    ls.Add(New ListOfInventoryCountingLine With {
                        .DocEntry = oRs.Fields.Item("DocEntry").Value,
                        .LineNum = oRs.Fields.Item("LineNum").Value,
                        .ItemCode = oRs.Fields.Item("ItemCode").Value,
                        .ItemDesc = oRs.Fields.Item("ItemDesc").Value,
                        .Freeze = oRs.Fields.Item("Freeze").Value,
                        .WhsCode = oRs.Fields.Item("WhsCode").Value,
                        .InWhsQty = oRs.Fields.Item("InWhsQty").Value,
                        .Counted = oRs.Fields.Item("Counted").Value,
                        .CountQty = oRs.Fields.Item("CountQty").Value,
                        .Remark = oRs.Fields.Item("Remark").Value,
                        .BarCode = oRs.Fields.Item("BarCode").Value,
                        .InvUoM = oRs.Fields.Item("InvUoM").Value,
                        .Difference = oRs.Fields.Item("Difference").Value,
                        .DiffPercen = oRs.Fields.Item("DiffPercen").Value,
                        .CountDate = oRs.Fields.Item("CountDate").Value,
                        .CountTime = oRs.Fields.Item("CountTime").Value,
                        .ProjCode = oRs.Fields.Item("ProjCode").Value,
                        .OcrCode = oRs.Fields.Item("OcrCode").Value,
                        .LineStatus = oRs.Fields.Item("LineStatus").Value,
                        .BinEntry = oRs.Fields.Item("BinEntry").Value,
                        .VisOrder = oRs.Fields.Item("VisOrder").Value,
                        .OcrCode2 = oRs.Fields.Item("OcrCode2").Value,
                        .OcrCode3 = oRs.Fields.Item("OcrCode3").Value,
                        .OcrCode4 = oRs.Fields.Item("OcrCode4").Value,
                        .OcrCode5 = oRs.Fields.Item("OcrCode5").Value,
                        .FirmCode = oRs.Fields.Item("FirmCode").Value,
                        .SuppCatNum = oRs.Fields.Item("SuppCatNum").Value,
                        .PrefVendor = oRs.Fields.Item("PrefVendor").Value,
                        .CountDiff = oRs.Fields.Item("CountDiff").Value,
                        .CountDiffP = oRs.Fields.Item("CountDiffP").Value,
                        .UomCode = oRs.Fields.Item("UomCode").Value,
                        .UomQty = oRs.Fields.Item("UomQty").Value
                    })
                    oRs.MoveNext()
                Loop
                Return (New ReturnListOfInventoryCountingLine With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .ls_data = ls
                    })
            Else
                Return (New ReturnListOfInventoryCountingLine With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .ls_data = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New ReturnListOfInventoryCountingLine With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .ls_data = Nothing
                   })
        End Try
    End Function
End Class