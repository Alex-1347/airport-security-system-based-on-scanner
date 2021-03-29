Imports System.Text
Imports System.Text.RegularExpressions

Public Class EditForm

    Public ParentDataForm As DataForm
    Public CurRecord As InputData
    Private Sub EditForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        If CurRecord IsNot Nothing Then
            Select Case CurRecord.type
                Case 0 : TypeComboBox.Text = "Airlines boarding pass"
                Case 1 : TypeComboBox.Text = "Royalty passes for lounge"
                Case 2 : TypeComboBox.Text = "Check-in/walk-in passengers"
                Case 3 : TypeComboBox.Text = "Credit card holders"
                Case Else : TypeComboBox.Text = "(unspecified)"
            End Select
            Text = $"#{CurRecord.id} - {CurRecord.crdate.ToString}"
            CommentTextBox.Text = CurRecord.comment
            DataTextBox.Text = CurRecord.txt
            If String.IsNullOrEmpty(CurRecord.split) Then
                If Not String.IsNullOrEmpty(CurRecord.txt) Then
                    FormatTextBox.Text = GetWordsPosition(CurRecord.txt)
                End If
            Else
                FormatTextBox.Text = CurRecord.split
            End If
        End If
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Select Case TypeComboBox.Text
            Case "Airlines boarding pass" : CurRecord.type = 0
            Case "Royalty passes for lounge" : CurRecord.type = 1
            Case "Check-in/walk-in passengers" : CurRecord.type = 2
            Case "Credit card holders" : CurRecord.type = 3
            Case "(unspecified)" : CurRecord.type = 4
        End Select
        CurRecord.comment = CommentTextBox.Text
        CurRecord.txt = DataTextBox.Text
        CurRecord.split = FormatTextBox.Text
        Try
            StarForm_Instance.db1.SaveChanges()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Me.Close()
    End Sub
End Class