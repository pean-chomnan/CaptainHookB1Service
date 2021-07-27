Public Class Class1

    '    oDeliveryHeader = New NscDeliveryHeader()
    '    oDeliveryLines = New List(Of NscDeliveryLine)

    ''Get data from UI to Model
    '    oDeliveryHeader.WebDocNum = "123"
    '    oDeliveryHeader.Series = "366"
    '    oDeliveryHeader.DocDate = "2021-07-22" 'Header_FinishDate = Format(Trim(Me.Range("N" & lRow).Value), "yyyy-mm-dd")
    '    oDeliveryHeader.DocDueDate = "2021-07-22"
    '    oDeliveryHeader.TaxDate = "2021-07-22"

    '    oDeliveryHeader.CardCode = "CLC10001"
    '    oDeliveryHeader.CardName = "The American Chamber of Commerce in Thailand"

    '    oDeliveryHeader.ContactPersonCode = 0
    '    oDeliveryHeader.NumAtCard = ""
    '    oDeliveryHeader.SalesPersonCode = -1 ' No Sales Employee
    '    oDeliveryHeader.PriceMode = "N"
    '    oDeliveryHeader.Comments = "Based On Sales Orders 212900001.(By Interface)"

    '    For i As Integer = 0 To 0 'frmDelivery.DataGridView1.Rows.Count
    'Dim tmpDeliveryLine As New NscDeliveryLine()

    ''tmpDeliveryLine.BarCode = ""
    '        tmpDeliveryLine.ItemCode = "2SANOCN08"
    '        tmpDeliveryLine.ItemName = "Atlantic Salmon Nature Cold Smoke 80G"

    '        tmpDeliveryLine.BaseType = "17"
    '        tmpDeliveryLine.BaseEntry = "16"
    '        tmpDeliveryLine.Baseline = "0"

    '        tmpDeliveryLine.Quantity = 1
    '        tmpDeliveryLine.Price = 100
    ''tmpDeliveryLine.U_PriceWeight = 100
    ''tmpDeliveryLine.U_Weight = 600

    '        tmpDeliveryLine.WhsCode = "10.CON20"
    '        tmpDeliveryLine.VatGroup = "S07"
    '        tmpDeliveryLine.CogsCode = "CSM"
    '        tmpDeliveryLine.CogsCode2 = "DP01"

    ''Add Batch Number 
    '        For y As Integer = 0 To 0 'frmDelivery.DataGridView1.Rows.Count
    'Dim tmpDeliveryBatch As New NscDeliveryBatch
    '            tmpDeliveryBatch.BatchCode = "19243071-00089"
    '            tmpDeliveryBatch.Quantity = 1
    '            tmpDeliveryLine.ListOfBatch.Add(tmpDeliveryBatch)
    '        Next

    ''Add Bin Location

    ''Add Document Line
    '        oDeliveryLines.Add(tmpDeliveryLine)
    '  Next
End Class
