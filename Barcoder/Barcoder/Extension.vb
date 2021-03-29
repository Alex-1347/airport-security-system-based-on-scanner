Imports System.Runtime.CompilerServices

Public Module Extension

    <Extension()>
    Public Function ToByteArray(ByVal stream As IO.Stream) As Byte()
        stream.Position = 0
        Dim buffer As Byte() = New Byte(stream.Length - 1) {}
        Dim totalBytesCopied As Integer = 0

        While totalBytesCopied < stream.Length
            totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied)
        End While

        Return buffer
    End Function

    <Extension()>
    Sub InvokeOnUiThreadIfRequired(ByVal GuiForm As Form, ByVal action As Action)
        If GuiForm.InvokeRequired Then
            GuiForm.BeginInvoke(action)
        Else
            action.Invoke()
        End If
    End Sub

    <Extension()>
    Sub EditEndRow(Grid As DataGridView)
        Grid.ClearSelection()
        Grid.Rows(Grid.Rows.Count - 1).Selected = True
        Grid.CurrentCell = Grid(0, Grid.Rows.Count - 1)
        Grid.CurrentCell.Selected = True
        Grid.FirstDisplayedScrollingRowIndex = Grid.Rows.Count - 1
        Grid.Focus()
        Grid.BeginEdit(False)
    End Sub

    <Extension()>
    Function GetTempDirectory(F As Form) As String
        Dim LocalTmpPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim TempAppDirectory As String = IO.Path.Combine(LocalTmpPath, Application.ProductName)
        If Not My.Computer.FileSystem.DirectoryExists(TempAppDirectory) Then
            My.Computer.FileSystem.CreateDirectory(TempAppDirectory)
        End If
        Return TempAppDirectory & "\"
    End Function

    <Extension()>
    Public Function ToHexString(ByVal Input As String) As String
        Dim InputBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(Input)
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        For i As Integer = 0 To InputBytes.Length - 1
            sb.Append(InputBytes(i).ToString("X2"))
        Next
        Return sb.ToString()
    End Function

    <Extension()>
    Public Function BytesToHex(ByVal InputBytes As Byte()) As String
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        For i As Integer = 0 To InputBytes.Length - 1
            sb.Append(InputBytes(i).ToString("X2"))
        Next
        Return sb.ToString()
    End Function

    <Extension()>
    Public Function HexStringToBytes(ByVal InputStr As String) As Byte()
        Dim ButeArrLength As Integer = (InputStr.Length + 1) / 2 'размер буфера (примерный)
        Dim OutArr(ButeArrLength) As Byte, I As Integer, OneByte As Byte
        For I = 0 To InputStr.Length / 2 - 1
            OneByte = Byte.Parse(InputStr.Substring(I * 2, 2), Globalization.NumberStyles.AllowHexSpecifier)
            OutArr(I) = OneByte
        Next
        ReDim Preserve OutArr(I - 1)
        Return OutArr
    End Function


    <Extension()>
    Public Function ClearTmpDirectory(ByVal TmpDir As String) As String
        If My.Computer.FileSystem.DirectoryExists(TmpDir) Then
            Dim TmpDirInfo = My.Computer.FileSystem.GetDirectoryInfo(TmpDir)
            For Each One In TmpDirInfo.GetFiles
                My.Computer.FileSystem.DeleteFile(One.FullName)
            Next
            My.Computer.FileSystem.DeleteDirectory(TmpDir, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
    End Function

    Public Function GetRandomInteger(MaxValue As Integer) As Integer
        Dim byte_count As Byte() = New Byte(3) {}
        Dim random_number As New System.Security.Cryptography.RNGCryptoServiceProvider()
        random_number.GetBytes(byte_count)
        Return (Math.Abs(BitConverter.ToInt32(byte_count, 0)) / Int32.MaxValue) * MaxValue
    End Function

    Public Function CreateRandomPassword(Len As String, Optional FromChagCode As UInt32 = &H33, Optional ToCharCode As UInt32 = &H7E, Optional ExcludeChars As String = "<>") As String
        Dim Ret1 As New System.Text.StringBuilder
        While Ret1.Length < Len
            Dim RandomNum As Integer = FromChagCode + GetRandomInteger(ToCharCode - FromChagCode)
            Dim OneChar As Char = ChrW(RandomNum)
            If Not ExcludeChars.Contains(OneChar) Then
                Ret1.Append(OneChar)
            End If
        End While
        Return Ret1.ToString
    End Function

    <Extension()>
    Public Function FindRecusriveByName(ByVal control As Control, ControlName As String) As IEnumerable(Of Control)
        Dim controls = control.Controls.Cast(Of Control)()
        Return controls.SelectMany(Function(ctrl) FindRecusriveByName(ctrl, ControlName)).Concat(controls).Where(Function(c) c.Name = ControlName)
    End Function

    <Extension()>
    Public Function FindRecusriveByType(ByVal control As Control, ByVal type As Type) As IEnumerable(Of Control)
        Dim controls = control.Controls.Cast(Of Control)()
        Return controls.SelectMany(Function(ctrl) FindRecusriveByType(ctrl, type)).Concat(controls).Where(Function(c) c.[GetType]() = type)
    End Function


End Module
