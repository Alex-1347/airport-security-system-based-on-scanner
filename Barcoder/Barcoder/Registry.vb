Public Class Registry

    Public Event FirstStart()
    Public Event Err(Msg As String)
    Public ReadOnly Property SoftwareName As String 'LawInfo

    Public Sub New(SoftwareName1 As String)
        SoftwareName = SoftwareName1
        Try
            Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
                If CurrentKey Is Nothing Then
                    Using SOFTWARE As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE", True)
                        Using MyKey As Microsoft.Win32.RegistryKey = SOFTWARE.CreateSubKey(SoftwareName)
                            RaiseEvent FirstStart()
                        End Using
                    End Using
                End If
            End Using
        Catch ex As Exception
            RaiseEvent Err(ex.Message)
        End Try
    End Sub

    Public Sub SetValue(Name As String, Value As Object, RegistryValueKind As Microsoft.Win32.RegistryValueKind)
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, RegistryValueKind)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Sub SetValue(Name As String, Value As String)
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Sub SetValue(Name As String, Value As Byte())
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.Binary)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Sub SetValue(Name As String, Value As UInteger)
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.DWord)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Sub SetValue(Name As String, Value As ULong)
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.QWord)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Sub SetValue(Name As String, Value As Date)
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value.ToString, Microsoft.Win32.RegistryValueKind.String)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Sub SetValue(Name As String, Value As Boolean)
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value.ToString, Microsoft.Win32.RegistryValueKind.String)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Sub SetValue(Name As String, Value As Decimal)
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                CurrentKey.SetValue(Name, Value.ToString, Microsoft.Win32.RegistryValueKind.String)
            Else
                RaiseEvent Err("RegistryKey SOFTWARE\" & SoftwareName & " missing")
            End If
        End Using
    End Sub

    Public Function GetValue(Of T)(Name As String) As Object
        Using CurrentKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & SoftwareName, True)
            If CurrentKey IsNot Nothing Then
                Dim ValueType As Microsoft.Win32.RegistryValueKind
                Dim Ret As Object = CurrentKey.GetValue(Name, Nothing, ValueType)
                If Ret IsNot Nothing Then
                    If GetType(T) = GetType(String) Then
                        Return Ret.ToString
                    ElseIf GetType(T) = GetType(Decimal) Then
                        Return Decimal.Parse(Ret.ToString)
                    ElseIf GetType(T) = GetType(Boolean) Then
                        Return Boolean.Parse(Ret.ToString)
                    ElseIf GetType(T) = GetType(Date) Then
                        Return Date.Parse(Ret.ToString)
                    ElseIf GetType(T) = GetType(Byte()) Then
                        Return TryCast(Ret, Byte())
                    ElseIf GetType(T) = GetType(UInteger) Then
                        Return DirectCast(Ret, UInteger)
                    ElseIf GetType(T) = GetType(ULong) Then
                        Return Convert.ToUInt64(Ret)
                    Else
                        Return Ret
                    End If
                End If
            End If
        End Using
    End Function

End Class
