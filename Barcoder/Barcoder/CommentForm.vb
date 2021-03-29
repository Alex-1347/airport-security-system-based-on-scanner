Public Class CommentForm

    Private Sub CommentForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        TypeComboBox.SelectedIndex = 4
    End Sub

    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        ScanForm_Instance.Comment = CommentTextBox.Text
        ScanForm_Instance.Type = TypeComboBox.SelectedIndex
        Me.Close()
    End Sub
End Class