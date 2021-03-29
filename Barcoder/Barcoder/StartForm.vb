Imports System.Configuration
Imports CoreScanner
Public Class StartForm

    Public Shared Property CurConnectionString As String
    Public Shared db1 As Sdb
    'Public Shared m_pCoreScanner As CCoreScannerClass
    Dim Reg As Registry
    Public TempDir As String

    Private Sub StartForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Text &= " (" & My.Application.Info.Version.ToString & ")"
        Reg = New Registry("BarcodeScanner")
#Region "Initialization and relocation app.config and db"
        Try
            Dim BarcodeDbLocation As String = Reg.GetValue(Of String)("Database")
            Dim EmptyDB As Byte() = My.Resources.Barcode
            TempDir = GetTempDirectory() 'C:\Users\khark\AppData\Local\Barcoder\
            ToolStripStatusLabel1.Text = TempDir
            'Locate Database
            '
            If BarcodeDbLocation Is Nothing Then
                If Not My.Computer.FileSystem.DirectoryExists(TempDir) Then My.Computer.FileSystem.CreateDirectory(TempDir)
                My.Computer.FileSystem.WriteAllBytes(IO.Path.Combine(TempDir, "ScannerDB.db"), EmptyDB, False)
                Reg.SetValue("Database", IO.Path.Combine(TempDir, "ScannerDB.db"))
                BarcodeDbLocation = Reg.GetValue(Of String)("Database") 'C:\Users\khark\AppData\Local\Barcoder\ScannerDB.db
            Else
                If Not My.Computer.FileSystem.FileExists(BarcodeDbLocation) Then
                    If Not My.Computer.FileSystem.DirectoryExists(TempDir) Then My.Computer.FileSystem.CreateDirectory(TempDir)
                    My.Computer.FileSystem.WriteAllBytes(IO.Path.Combine(TempDir, "ScannerDB.db"), EmptyDB, False)
                End If
            End If
            'create new AppDomain with new config
            Dim OldConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            'Path=G:\Projects\ZebraScanner\Barcoder\Barcoder\bin\Debug\Barcoder.exe.Config
            CurConnectionString = OldConfig.ConnectionStrings.ConnectionStrings("SDBconnectionString").ConnectionString
            'metadata=res://*/SDB.csdl|res://*/SDB.ssdl|res://*/SDB.msl;provider=System.Data.SQLite.EF6;provider connection string="data source=G:\Projects\ZebraScanner\Barcoder\Barcoder\ScannerDB.db"
            Using AppConfig.Change(IO.Path.Combine(TempDir, "App.config"))
                Dim NewConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                NewConfig = OldConfig
                NewConfig.ConnectionStrings.ConnectionStrings.Remove("SDBconnectionString")
                NewConfig.ConnectionStrings.ConnectionStrings.Add(New ConnectionStringSettings With {
                                                               .Name = "SDBconnectionString",
                                                               .ProviderName = "System.Data.EntityClient",
                                                               .ConnectionString = CurConnectionString.Replace("G:\Projects\ZebraScanner\Barcoder\Barcoder\ScannerDB.db", BarcodeDbLocation)})
                NewConfig.SaveAs(IO.Path.Combine(TempDir, "App.config"), ConfigurationSaveMode.Modified)
                'save to C:\Users\khark\AppData\Local\Barcoder\App.config
                ConfigurationManager.RefreshSection("connectionStrings")
                CurConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings("SDBconnectionString").ConnectionString
                'metadata=res://*/SDB.csdl|res://*/SDB.ssdl|res://*/SDB.msl;provider=System.Data.SQLite.EF6;provider connection string="data source=C:\Users\khark\AppData\Local\Barcoder\ScannerDB.db"
                db1 = New Sdb(CurConnectionString)
                If db1 IsNot Nothing Then
                    If db1.Database IsNot Nothing Then
                        If db1.Database.Connection IsNot Nothing Then
                            AutoClosingMessageBox.Show(db1.Database.Connection.GetType.Name & vbCrLf & db1.Database.Connection.ConnectionString & vbCrLf & IO.Path.Combine(TempDir, "App.config"), "Diagnostic message")
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            Dim ErrorMessage As String = ex.Message.ToString
            If ex.InnerException IsNot Nothing Then
                ErrorMessage = ErrorMessage & vbCrLf & ex.InnerException.ToString
            End If
            If db1 IsNot Nothing Then
                If db1.Database IsNot Nothing Then
                    If db1.Database.Connection IsNot Nothing Then
                        Dim Str1 As String = ""
                        Str1 = db1.Database.Connection.GetType.Name & vbCrLf & db1.Database.Connection.ConnectionString
                        ErrorMessage = ErrorMessage & vbCrLf & Str1
                    End If
                End If
            End If
            MsgBox(ErrorMessage)
            End
        End Try
#End Region
    End Sub

    Private Sub ScanButton_Click(sender As Object, e As EventArgs) Handles ScanButton.Click
        ScanForm_Instance.ShowDialog()
    End Sub

    Private Sub NoScanButton_Click(sender As Object, e As EventArgs) Handles NoScanButton.Click
        NoScanForm_Instance.ShowDialog()
    End Sub

    Private Sub AdminButton_Click(sender As Object, e As EventArgs) Handles AdminButton.Click
        Dim DF As New DataForm
        DF.Show()
    End Sub

    Private Sub FormatButton_Click(sender As Object, e As EventArgs) Handles FormatButton.Click
        Dim FF As New FormatForm
        FF.Show()
    End Sub

    Private Sub ConnectButton_Click(sender As Object, e As EventArgs) Handles ConnectButton.Click
        Dim Process As System.Diagnostics.Process
        Try
            Process = System.Diagnostics.Process.Start("ScannerSDK.exe")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


End Class