Public Class GetDataResonse
    Public Function GetPacking(ByVal BarCode As String) As PackingClass.ClassPackingResponse
        Try
            Dim listItemCode As New List(Of PackingClass.ClassPacking)
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
            Dim Table As String = ""
            Dim _type As Integer = 0
            Dim oCompany As SAPbobsCOM.Company = Nothing
            Dim oRs As SAPbobsCOM.Recordset = Nothing
            Dim strSql As String = ""
            Dim oLoginService As New LoginServiceWebRef
            '   Dim listItemCode As New List(Of GetItemMaster)

            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                strSql = "SELECT ""ItemCode"",""DistNumber"" ,""MnfSerial"" ,""LotNumber"",""ExpDate"",""U_ACT_WeightOnBatch"" As ""WeightOnBatch"",CAST(""U_CompanyAddress"" AS NVARCHAR(100)) AS ""U_CompanyAddress"" ,CAST(""U_BarCodeBoxNumber"" AS NVARCHAR(100)) AS ""U_BarCodeBoxNumber"",CAST(""U_SmokingSystem"" AS NVARCHAR(100)) AS ""U_SmokingSystem"",'Batch' As ""Type"" FROM " & _DBNAME & ".""OBTN"" WHERE ""DistNumber""='" & BarCode & "' UNION SELECT ""ItemCode"",""DistNumber"" ,""MnfSerial"" ,""LotNumber"",""ExpDate"",""U_ACT_WeightOnBatch"" As ""WeightOnBatch"",CAST(""U_CompanyAddress"" AS NVARCHAR(100)) AS ""U_CompanyAddress"" ,CAST(""U_BarCodeBoxNumber"" AS NVARCHAR(100)) AS ""U_BarCodeBoxNumber"",CAST(""U_SmokingSystem"" AS NVARCHAR(100)) AS ""U_SmokingSystem"",'Serial' As ""Type"" FROM " & _DBNAME & ".""OSRN""  WHERE ""DistNumber""='" & BarCode & "'"
                oRs.DoQuery(strSql)
                Do While Not oRs.EoF
                    listItemCode.Add(New PackingClass.ClassPacking With {
                        .ItemCode = oRs.Fields.Item("ItemCode").Value.ToString.Trim,
                        .DistNumber = oRs.Fields.Item("DistNumber").Value.ToString.Trim,
                        .MnfSerial = oRs.Fields.Item("MnfSerial").Value.ToString.Trim,
                        .LotNumber = oRs.Fields.Item("LotNumber").Value.ToString.Trim,
                        .ExpiredDate = oRs.Fields.Item("ExpDate").Value,
                        .WeightOnBatch = oRs.Fields.Item("WeightOnBatch").Value.ToString.Trim,
                        .CompanyAddress = oRs.Fields.Item("U_CompanyAddress").Value.ToString.Trim,
                        .BarCodeBoxNumber = oRs.Fields.Item("U_BarCodeBoxNumber").Value.ToString.Trim,
                        .SmokingSystem = oRs.Fields.Item("U_SmokingSystem").Value.ToString.Trim,
                        .Type = oRs.Fields.Item("Type").Value.ToString.Trim
                        })
                    oRs.MoveNext()
                Loop
                Return (New PackingClass.ClassPackingResponse With {
                        .ErrCode = 0,
                        .ErrMsg = "",
                        .Obj = listItemCode
                    })
            Else
                Return (New PackingClass.ClassPackingResponse With {
                        .ErrCode = oLoginService.lErrCode,
                        .ErrMsg = oLoginService.sErrMsg,
                        .Obj = Nothing
                    })
            End If
        Catch ex As Exception
            Return (New PackingClass.ClassPackingResponse With {
                       .ErrCode = ex.HResult,
                       .ErrMsg = ex.Message.ToString(),
                       .Obj = Nothing
                   })
        End Try
    End Function

    Public Function UpdateUDFBorCodeBoxNumber(ipaObj As List(Of PackingClass.ClassPacking)) As List(Of ReturnStatus)
        Dim Table As String = ""
        Dim _type As Integer = 0
        Dim oCompany As SAPbobsCOM.Company = Nothing
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim strSql As String = ""
        Dim oLoginService As New LoginServiceWebRef
        Dim myclas As New myClassOfFuntion
        Dim ReturnStatus As ReturnStatus
        Dim ls_returnstatus As New List(Of ReturnStatus)
        Dim ls_Obj As New List(Of PackingClass.ClassPacking)

        Try
            Dim _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                For i As Integer = 0 To ipaObj.Count - 1
                    If ipaObj(i).Type.ToLower = "serial" Then
                        strSql = "UPDATE " & _DBNAME & ".""OSRN""  SET ""U_BarCodeBoxNumber""='" & ipaObj(i).BarCodeBoxNumber & "' WHERE ""ItemCode""='" & ipaObj(i).ItemCode & "' AND ""DistNumber""='" & ipaObj(i).DistNumber & "'"
                        myclas.AddUpdateQueryOCompany(strSql, oCompany)
                        ReturnStatus = New ReturnStatus With {
                            .ErrorCode = 0,
                            .ErrirMsg = "Update Serial " & ipaObj(i).DistNumber & " BarCode BoxNumber successfully!!"
                        }
                        ls_returnstatus.Add(ReturnStatus)
                    ElseIf ipaObj(i).Type.ToLower = "batch" Then
                        strSql = "UPDATE " & _DBNAME & ".""OBTN""  SET ""U_BarCodeBoxNumber""='" & ipaObj(i).BarCodeBoxNumber & "' WHERE ""ItemCode""='" & ipaObj(i).ItemCode & "' AND ""DistNumber""='" & ipaObj(i).DistNumber & "'"
                        myclas.AddUpdateQueryOCompany(strSql, oCompany)
                        ReturnStatus = New ReturnStatus With {
                            .ErrorCode = 0,
                            .ErrirMsg = "Update Batch " & ipaObj(i).DistNumber & " BarCode BoxNumber successfully!!"
                        }
                        ls_returnstatus.Add(ReturnStatus)
                    End If
                Next
            Else
                ReturnStatus = New ReturnStatus With {
                    .ErrorCode = oLoginService.lErrCode,
                    .ErrirMsg = oLoginService.sErrMsg
                }
                ls_returnstatus.Add(ReturnStatus)
            End If
        Catch ex As Exception
            ReturnStatus = New ReturnStatus With {
                    .ErrorCode = ex.HResult,
                       .ErrirMsg = ex.Message.ToString()
                }
            ls_returnstatus.Add(ReturnStatus)
        End Try

        Return ls_returnstatus

    End Function

End Class
