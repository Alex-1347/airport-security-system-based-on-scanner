Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Module Starter

    <DllImport("user32.dll")>
    Private Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    End Function

    <DllImport("Kernel32")>
    Private Function GetConsoleWindow() As IntPtr
    End Function

    Const SW_HIDE As Integer = 0
    Const SW_SHOW As Integer = 5

    Public StarForm_Instance As StartForm
    Public ScanForm_Instance As ScanForm
    Public NoScanForm_Instance As NoScanForm

    <STAThread>
    Public Sub Main()


        Dim hwnd As IntPtr
        hwnd = GetConsoleWindow()
        ShowWindow(hwnd, SW_HIDE)

        AddHandler Application.ThreadException, New Threading.ThreadExceptionEventHandler(AddressOf Form1_UIThreadException)
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        AddHandler AppDomain.CurrentDomain.UnhandledException, New UnhandledExceptionEventHandler(AddressOf CurrentDomain_UnhandledException)

        StarForm_Instance = New StartForm
        ScanForm_Instance = New ScanForm
        NoScanForm_Instance = New NoScanForm

        'BrowserForm_Instance = New BrowserForm

        'BrowserForm_Instance.FormBorderStyle = FormBorderStyle.FixedDialog
        'BrowserForm_Instance.MaximumSize = New Size(500, 700)
        'BrowserForm_Instance.Show()

        'BrowserForm_Instance.Hide()
        Application.Run(StarForm_Instance)


    End Sub



    Private Sub Form1_UIThreadException(ByVal sender As Object, ByVal t As Threading.ThreadExceptionEventArgs)
        Dim result As DialogResult = DialogResult.Cancel

        Try
            result = ShowThreadExceptionDialog("Fatal Error", t.Exception)
            If result = DialogResult.Abort Then Application.[Exit]()
        Catch
            Try
                MessageBox.Show("Fatal Windows Forms Error", "UIThreadException", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.[Stop])
            Finally
                Application.[Exit]()
            End Try
        End Try

    End Sub


    Private Sub CurrentDomain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Dim errorMsg As String = "Fatal Non-UI Error:" & vbLf & vbLf
        Dim ex As Exception = CType(e.ExceptionObject, Exception)

        Try
            Dim myLog As EventLog = New EventLog()
            myLog.Source = "ThreadException"
            myLog.WriteEntry(errorMsg & ex.Message & vbLf & vbLf & "Stack Trace:" & vbLf + ex.StackTrace)
        Catch exc As Exception

            Try
                MessageBox.Show("Could not write the error to the event log. Reason: " & exc.Message, "UnhandledException", MessageBoxButtons.OK, MessageBoxIcon.[Stop])
            Finally
                Application.[Exit]()
            End Try
        End Try
    End Sub

    Public Function ShowThreadExceptionDialog(ByVal title As String, ByVal e As Exception) As DialogResult
        Dim errorMsg As String = "An application error occurred. Please contact the adminstrator " & "with the following information:" & vbLf & vbLf
        errorMsg = errorMsg & e.Message & vbLf & vbLf & "Stack Trace:" & vbLf + e.StackTrace
        Return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.[Stop])
    End Function

End Module
