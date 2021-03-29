Imports System.Text.RegularExpressions

Public Class ConfirmForm

    Public ParentForm As ScanForm
    Public Type As Integer
    Public CrDate As Date
    Public Comment As String
    Public InputData As String
    Public SplitFormat As String
    Public FmtRecord As Format

    Private Sub ConfirmForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Select Case Type
            Case 0 : Ltype.Text = "Airlines boarding pass"
            Case 1 : Ltype.Text = "Royalty passes for lounge"
            Case 2 : Ltype.Text = "Check-in/walk-in passengers"
            Case 3 : Ltype.Text = "Credit card holders"
            Case 4 : Ltype.Text = "Raw"
        End Select
        lDate.Text = CrDate
        Lcomment.Text = Comment
        LFormat.Text = SplitFormat
        If Type = 4 Then
            Dim SplittedStr1 As String = Regex.Replace(InputData, "\s+", " ")
            Dim WordsPosStr() As String = SplittedStr1.Split(" ")
            For I As Integer = 0 To Math.Min(WordsPosStr.Count - 1, 14)
                Select Case I
                    Case 0 : F1D.Text = WordsPosStr(I)
                    Case 1 : F2D.Text = WordsPosStr(I)
                    Case 2 : F3D.Text = WordsPosStr(I)
                    Case 3 : F4D.Text = WordsPosStr(I)
                    Case 4 : F5D.Text = WordsPosStr(I)
                    Case 5 : F6D.Text = WordsPosStr(I)
                    Case 6 : F7D.Text = WordsPosStr(I)
                    Case 7 : F8D.Text = WordsPosStr(I)
                    Case 8 : F9D.Text = WordsPosStr(I)
                    Case 9 : F10D.Text = WordsPosStr(I)
                    Case 10 : F11D.Text = WordsPosStr(I)
                    Case 11 : F12D.Text = WordsPosStr(I)
                    Case 12 : F13D.Text = WordsPosStr(I)
                    Case 13 : F14D.Text = WordsPosStr(I)
                    Case 14 : F15D.Text = WordsPosStr(I)
                End Select
            Next
        Else
            Dim CurRow As String()
            Dim Format As String()
            Format = SplitFormat.Split(";")
            Dim IntFormat As New List(Of Integer)
            For ColIndex As Integer = 0 To Format.Count - 1
                If IsNumeric(Format(ColIndex)) Then
                    IntFormat.Add(Format(ColIndex))
                End If
            Next
            F1N.Text = FmtRecord.F1comment
            F2N.Text = FmtRecord.F2comment
            F3N.Text = FmtRecord.F3comment
            F4N.Text = FmtRecord.F4comment
            F5N.Text = FmtRecord.F5comment
            F6N.Text = FmtRecord.F6comment
            F7N.Text = FmtRecord.F7comment
            F8N.Text = FmtRecord.F8comment
            F9N.Text = FmtRecord.F9comment
            F10N.Text = FmtRecord.F10comment
            F11N.Text = FmtRecord.F11comment
            F12N.Text = FmtRecord.F12comment
            F13N.Text = FmtRecord.F13comment
            F14N.Text = FmtRecord.F14comment
            F15N.Text = FmtRecord.F15comment
            If IntFormat.Count = 0 Then
                F1D.Text = InputData
            Else
                Try
                    ReDim CurRow(IntFormat.Count)
                    Dim I As Integer
                    For I = 0 To IntFormat.Count - 2
                        CurRow(I) = InputData.Substring(IntFormat(I), IntFormat(I + 1) - IntFormat(I))
                    Next
                    CurRow(I + 1) = InputData.Substring(IntFormat(I))
                    For j As Integer = 0 To Math.Min(CurRow.Count - 1, 14)
                        Select Case j
                            Case 0 : F1D.Text = CurRow(j)
                            Case 1 : F2D.Text = CurRow(j)
                            Case 2 : F3D.Text = CurRow(j)
                            Case 3 : F4D.Text = CurRow(j)
                            Case 4 : F5D.Text = CurRow(j)
                            Case 5 : F6D.Text = CurRow(j)
                            Case 6 : F7D.Text = CurRow(j)
                            Case 7 : F8D.Text = CurRow(j)
                            Case 8 : F9D.Text = CurRow(j)
                            Case 9 : F10D.Text = CurRow(j)
                            Case 10 : F11D.Text = CurRow(j)
                            Case 11 : F12D.Text = CurRow(j)
                            Case 12 : F13D.Text = CurRow(j)
                            Case 13 : F14D.Text = CurRow(j)
                            Case 14 : F15D.Text = CurRow(j)
                        End Select
                    Next

                Catch ex As Exception
                    MsgBox($"Invalid data format: {SplitFormat}", MsgBoxStyle.Critical)
                End Try
            End If
        End If
    End Sub


    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        ParentForm.IsConfirm = False
        Me.Close()
    End Sub

    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        ParentForm.IsConfirm = True
        Me.Close()
    End Sub

    Private Sub FormatLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles FormatLinkLabel.LinkClicked
        Dim FF As New FormatForm
        FF.Show()
    End Sub

    Private Sub AdminLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles AdminLinkLabel.LinkClicked
        Dim DF As New DataForm
        DF.Show()
    End Sub


End Class