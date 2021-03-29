Public Class NoScanForm

    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        Try
            Dim NewData = New InputData With {.crdate = Now, .type = 4, .comment = CommentTextBox.Text}
            StarForm_Instance.db1.InputDatas.Add(NewData)
            StarForm_Instance.db1.SaveChanges()
            CommentTextBox.Text = ""
            Me.Close()
        Catch ex As Exception
            Dim scanForm_Instance1 As ScanForm = ScanForm_Instance
            MsgBox(ex.Message & vbCrLf & StarForm_Instance.CurConnectionString, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class