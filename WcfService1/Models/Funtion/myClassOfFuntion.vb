Public Class myClassOfFuntion

    Public ioCompany As SAPbobsCOM.Company
    Public iolErrCode As Integer
    Public iosErrMsg As String
    Public ioDBName As String
    Public ioRs As SAPbobsCOM.Recordset = Nothing
    Public Sql As String = ""
    Public _DBNAME As String = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")
    Public _DBTYPE As String = System.Configuration.ConfigurationManager.AppSettings("DbServerType")

    Public Function Has(ByVal FieldName As String, ByVal Code As String, ByVal sTable As String) As Boolean
        Dim _result As Boolean = False
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim strSql As String
        Dim oCompany As SAPbobsCOM.Company = Nothing
        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                If _DBTYPE = "HANADB" Then
                    strSql = "SELECT " & """" & FieldName & """" & " FROM " & """" & _DBNAME & """" & "." & """" & sTable & """" & " WHERE " & """" & FieldName & """" & " = N'" & Code & "'"
                Else
                    strSql = "SELECT " & FieldName & " FROM " & sTable & " WHERE  " & FieldName & " = N'" & Code & "'"
                End If

                oRs.DoQuery(strSql)
                If oRs.RecordCount > 0 Then
                    _result = True
                End If
            End If
        Catch ex As Exception
            _result = False
        End Try
        Return _result

    End Function

    Public Function Get_DocNum(ByVal xDocEntry As String, ByVal sTable As String) As String
        Dim _result As String = ""
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim strSql As String
        Dim oCompany As SAPbobsCOM.Company = Nothing
        Try
            'log4net.Config.XmlConfigurator.Configure()
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                strSql = "SELECT DocNum FROM " & sTable & " WHERE DocEntry ='" & xDocEntry & "'"
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRs.DoQuery(strSql)

                If oRs.RecordCount > 0 Then
                    _result = oRs.Fields.Item(0).Value.ToString.Trim
                End If
            End If
        Catch ex As Exception
            '_Log.ErrorFormat("Error Code: {0}, Message: {1}", ex.GetHashCode, ex.Message)
        End Try
        Return _result
    End Function

    Public Function GetDocNumByDocEntry(ByVal xDocNum As String, ByVal sTable As String) As String
        Dim _result As String = ""
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim strSql As String
        Dim oCompany As SAPbobsCOM.Company = Nothing
        Try
            'log4net.Config.XmlConfigurator.Configure()
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                strSql = "SELECT DocEntry FROM " & sTable & " WHERE DocNum ='" & xDocNum & "'"
                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRs.DoQuery(strSql)

                If oRs.RecordCount > 0 Then
                    _result = oRs.Fields.Item(0).Value.ToString.Trim
                End If
            End If
        Catch ex As Exception
            '_Log.ErrorFormat("Error Code: {0}, Message: {1}", ex.GetHashCode, ex.Message)
        End Try
        Return _result
    End Function

    Public Function ItemSetupBy(ByVal ItemCode As String) As Integer
        Dim _result As Integer = 0
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim strSql As String
        Dim oCompany As SAPbobsCOM.Company = Nothing
        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                If _DBTYPE = "HANADB" Then
                    strSql = "SELECT ""ManSerNum"",""ManBtchNum"" FROM " & """" & _DBNAME & """" & "." & """OITM"" WHERE ""ItemCode"" = '" & ItemCode & "'"
                Else
                    strSql = "SELECT ManSerNum,ManBtchNum FROM OITM WHERE ItemCode = '" & ItemCode & "'"
                End If

                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRs.DoQuery(strSql)

                If oRs.RecordCount > 0 Then
                    If oRs.Fields.Item(0).Value.ToString.Trim = "Y" Then
                        _result = 1
                    ElseIf oRs.Fields.Item(1).Value.ToString.Trim = "Y" Then
                        _result = 2
                    Else
                        _result = 3
                    End If
                End If
            End If
        Catch ex As Exception
            _result = 0
        End Try
        Return _result
    End Function

    Public Function isManagByBIN(ByVal WhsCode As String) As Boolean
        Dim _result As Integer = 0
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim strSql As String
        Dim oCompany As SAPbobsCOM.Company = Nothing
        Try
            Dim oLoginService As New LoginServiceWebRef
            If oLoginService.lErrCode = 0 Then
                oCompany = oLoginService.Company
                If _DBTYPE = "HANADB" Then
                    strSql = "SELECT  ""BinActivat"" FROM " & _DBNAME & ".""OWHS"" WHERE ""WhsCode""='" & WhsCode & "'"
                Else
                    strSql = "SELECT  BinActivat FROM OWHS WHERE WhsCode='" & _DBNAME & "'"
                End If

                oRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                oRs.DoQuery(strSql)

                If oRs.RecordCount > 0 Then
                    If oRs.Fields.Item(0).Value.ToString.Trim = "Y" Then
                        _result = True
                    Else
                        _result = False
                    End If
                End If
            End If
        Catch ex As Exception
            _result = False
        End Try
        Return _result
    End Function

    Public Function iConnect()
        Dim oLoginService As New LoginServiceWebRef
        ioDBName = System.Configuration.ConfigurationManager.AppSettings("CompanyDB")

        If oLoginService.lErrCode = 0 Then
            ioCompany = oLoginService.Company
            Return ""
        Else
            Return iolErrCode = oLoginService.sErrMsg
        End If
    End Function

    Public Function ICaseNumber(ByRef obj) As Object
        Try
            If obj = Nothing Then
                Return 0
            Else
                Return obj
            End If
        Catch ex As Exception
            Return 1
        End Try
    End Function

    Public Function ICaseString(ByRef obj) As Object
        Try
            If obj = Nothing Then
                Return ""
            Else
                Return obj
            End If
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function ICaseList(ByRef obj As List(Of ClassInventoryCounting.InventoryCountingLineUoMs)) As Integer
        Try
            If IsNothing(obj) = False Then
                If obj.Count > 0 Then
                    Return obj.Count
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
            
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function ICaseListOfBIN(ByRef obj As List(Of GoodsReceiptPO.BINCode)) As Integer
        Try
            If IsNothing(obj) = False Then
                If obj.Count > 0 Then
                    Return obj.Count
                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function ICaseListOfIssueBIN(ByRef obj As List(Of ClassDelivery.IssueBIN)) As Integer
        Try
            If IsNothing(obj) = False Then
                If obj.Count > 0 Then
                    Return obj.Count
                Else
                    Return 0
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Function AddUpdateQueryOCompany(Sql As String, ByVal oCompany As SAPbobsCOM.Company) As ReturnStatus
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Try
            oRs = DoQueryWrapper(Sql, oCompany)
            Return (New ReturnStatus With {
                        .ErrorCode = 0,
                        .ErrirMsg = "Add Or Update BarCode BoxNumber successfully!!"
                    })
        Catch ex As Exception
            Return (New ReturnStatus With {
                        .ErrorCode = 999,
                        .ErrirMsg = "Error Add Or Update:" & ex.ToString
                    })
        End Try
    End Function

    Public Function AddUpdateQueryWidthoutOCompany(Sql As String) As ReturnStatus
        Dim oRs As SAPbobsCOM.Recordset = Nothing

        Try
            oRs = DoQueryWrapperWidthoutOCompany(Sql)
            Return (New ReturnStatus With {
                        .ErrorCode = 0,
                        .ErrirMsg = "Add Or Update BarCode BoxNumber successfully!!"
                    })
        Catch ex As Exception
            Return (New ReturnStatus With {
                        .ErrorCode = 999,
                        .ErrirMsg = "Error Add Or Update:" & ex.ToString
                    })
        End Try

    End Function

    Public Function GetValFromQuery(ByVal Sql As String)
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim st As String = ""

        oRs = DoQueryWrapperWidthoutOCompany(Sql)
        If oRs.RecordCount > 0 Then
            st = oRs.Fields.Item(0).Value
        Else
            st = ""
        End If
        Return st
    End Function

    Public Function GetValFromQueryOCompany(ByVal Sql As String, ByVal oCompany As SAPbobsCOM.Company)
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim st As String = ""

        oRs = DoQueryWrapper(Sql, oCompany)
        If oRs.RecordCount > 0 Then
            st = oRs.Fields.Item(0).Value
        Else
            st = ""
        End If
        Return st
    End Function


    Public Function GetValFromQueryReturnNumberOCompany(ByVal Sql As String, ByVal oCompany As SAPbobsCOM.Company)
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim st As Double = 0
        oRs = DoQueryWrapper(Sql, oCompany)
        If oRs.RecordCount > 0 Then
            st = oRs.Fields.Item(0).Value
        Else
            st = 0
        End If
        Return st
    End Function

    Public Function GetValFromQueryReturnNumberWidthout(ByVal Sql As String)
        Dim oRs As SAPbobsCOM.Recordset = Nothing
        Dim st As Double = 0
        oRs = DoQueryWrapperWidthoutOCompany(Sql)
        If oRs.RecordCount > 0 Then
            st = oRs.Fields.Item(0).Value
        Else
            st = 0
        End If
        Return st
    End Function

    Public Function DoQueryWrapperWidthoutOCompany(ByVal Query As String) As SAPbobsCOM.Recordset
        Dim iMsg As String = ""
        Try
            iMsg = iConnect()
            If iMsg = "" Then
                ioRs = ioCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                ioRs.DoQuery(Query)
            End If
        Catch ex As Exception
        End Try
        Return ioRs
    End Function

    Public Function DoQueryWrapper(ByVal Query As String, ByVal oCompany As SAPbobsCOM.Company) As SAPbobsCOM.Recordset
        Try
            If oCompany.Connected Then
                ioRs = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                ioRs.DoQuery(Query)
            End If
        Catch ex As Exception
        End Try
        Return ioRs
    End Function

    ', ByVal oCompany As SAPbobsCOM.Company

End Class
