Public Class LoginServiceWebRef
    'Protected Shared ReadOnly _Log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

#Region "<Local Variable>"
    Private _Company As SAPbobsCOM.Company
    Private _lErrCode As Integer
    Private _sErrMsg As String
#End Region

#Region "<Property In Class>"
    Public ReadOnly Property sErrMsg As String
        Get
            Return _sErrMsg
        End Get
    End Property

    Public ReadOnly Property lErrCode As Integer
        Get
            Return _lErrCode
        End Get
    End Property

    Public ReadOnly Property Company As SAPbobsCOM.Company
        Get
            Return _Company
        End Get
    End Property
#End Region

#Region "<Method In Class>"

    Public Sub New()
        LogIn()
    End Sub

    Private Function Decrypt(ByVal Str As String) As String
        Dim i As Integer = 1
        Dim Password As String = ""
        Dim S As String = ""

        Try
            For i = 1 To Len(Str)
                If Mid(Str, i, 1) <> "?" Then
                    S = S & Mid(Str, i, 1)
                Else
                    Password = Password & Chr(CInt(S) - 7)
                    S = ""
                End If
            Next

        Catch ex As Exception

        End Try

        Return Password

    End Function

    Private Sub LogIn()
        Dim oCompany As SAPbobsCOM.Company = Nothing
        Dim Server As String = ""
        Dim DbServerType As String = ""
        Dim LicenseServer As String = ""
        Dim DbUserName As String = ""
        Dim DbPassword As String = ""
        Dim CompanyDB As String = ""
        Dim UserName As String = ""
        Dim Password As String = ""
        Try
            'log4net.Config.XmlConfigurator.Configure()
            oCompany = New SAPbobsCOM.Company

            ' Set connection properties
            Select Case System.Configuration.ConfigurationManager.AppSettings("DbServerType")
                Case "dst_MSSQL" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL
                Case "dst_DB_2" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_DB_2
                Case "dst_SYBASE" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_SYBASE
                Case "dst_MSSQL2005" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005
                Case "dst_MAXDB" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MAXDB
                Case "dst_MSSQL2008" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008
                Case "dst_MSSQL2012" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012
                Case "dst_MSSQL2014" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014
                Case "dst_MSSQL2016" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016
                    'Case "dst_MSSQL2017" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2017
                Case "HANADB" : oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB

            End Select
            Dim tmpstr As String
            oCompany.Server = System.Configuration.ConfigurationManager.AppSettings("Server")
            tmpstr = oCompany.Server
            oCompany.LicenseServer = System.Configuration.ConfigurationManager.AppSettings("LicenseServer")
            tmpstr = oCompany.LicenseServer
            oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English ' change to your language
            oCompany.UseTrusted = True
            oCompany.DbUserName = System.Configuration.ConfigurationManager.AppSettings("DbUserName")
            tmpstr = oCompany.DbUserName
            oCompany.DbPassword = System.Configuration.ConfigurationManager.AppSettings("DbPassword")
            oCompany.CompanyDB = System.Configuration.ConfigurationManager.AppSettings("CompanyDB") ''"KOFIDB_Coltd" 
            oCompany.UserName = System.Configuration.ConfigurationManager.AppSettings("UserName")
            oCompany.Password = System.Configuration.ConfigurationManager.AppSettings("Password") ''"SAP123" 


            If oCompany.Connect() <> 0 Then 'Connection failed
                oCompany.GetLastError(_lErrCode, _sErrMsg)
                _Company = Nothing
                ''_Log.ErrorFormat("Error Code: {0}, Message: {1}", lErrCode, sErrMsg)
                '_Log.ErrorFormat("Error Code: {0}, Message: {1}", System.Configuration.ConfigurationManager.AppSettings("DbServerType"), System.Configuration.ConfigurationManager.AppSettings("Server"))
                '_Log.ErrorFormat("Error Code: {0}, Message: {1}", System.Configuration.ConfigurationManager.AppSettings("LicenseServer"), System.Configuration.ConfigurationManager.AppSettings("DbUserName"))
                '_Log.ErrorFormat("Error Code: {0}, Message: {1}", System.Configuration.ConfigurationManager.AppSettings("DbPassword"), System.Configuration.ConfigurationManager.AppSettings("CompanyDB"))
                '_Log.ErrorFormat("Error Code: {0}, Message: {1}", System.Configuration.ConfigurationManager.AppSettings("UserName"), System.Configuration.ConfigurationManager.AppSettings("Password"))
                'MsgBox("Error: " & sErrMsg)

            Else 'Connection OK
                _lErrCode = 0
                _sErrMsg = ""
                _Company = oCompany
                '_Log.Info("Company is connected..")
            End If
        Catch ex As Exception
            _lErrCode = ex.GetHashCode
            _sErrMsg = ex.Message
            _Company = Nothing
            '_Log.ErrorFormat("Error Code: {0}, Message: {1}", ex.GetHashCode, ex.Message)
        End Try

    End Sub


#End Region
End Class

