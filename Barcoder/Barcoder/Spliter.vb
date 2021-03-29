Imports System.Text
Imports System.Text.RegularExpressions

Module Spliter
    Public Function GetWordsPosition(InputString As String) As String
        Dim Reg1 As New Regex("[^\s]+ +")
        Dim Words As MatchCollection = Reg1.Matches(InputString)
        If Words IsNot Nothing Then
            If Words.Count > 0 Then
                Dim Str1 As New StringBuilder
                For i As Integer = 0 To Words.Count - 1
                    Str1.Append(Words(i).Index & ";")
                Next
                Return Str1.ToString
            Else
                Return "0;"
            End If
        Else
            Return "0;"
        End If
    End Function
End Module
