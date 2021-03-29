Public Class FormatForm
    Private Sub FormatForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        TypeToolStripComboBox.SelectedIndex = 0
        WordsPosComboBox.SelectedIndex = 0
    End Sub

    Dim CurRecType As Integer = -1
    Dim CurRec As Format

    Private Sub WordsPosComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WordsPosComboBox.SelectedIndexChanged
        If WordsPosComboBox.SelectedIndex = 0 Then
            'words format
            Me.Text = "Set number of words for each columns"
            DataGridView1.Columns(1).HeaderText = "Words"
        Else
            'fixed pos format
            Me.Text = "Set text position for each columns"
            DataGridView1.Columns(1).HeaderText = "Pos"
        End If
    End Sub

    Private Sub TypeToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeToolStripComboBox.SelectedIndexChanged
        If TypeToolStripComboBox.SelectedIndex = 0 Then
            DataGridView1.Rows.Clear()
            CurRecType = -1
            WordsPosComboBox.SelectedIndex = 0
        Else
            Select Case TypeToolStripComboBox.SelectedIndex
                Case 1
                    CurRecType = 0 ' "Airlines boarding pass"
                    WordsPosComboBox.SelectedIndex = 1
                Case 2
                    CurRecType = 1 ' "Royalty passes for lounge"
                    WordsPosComboBox.SelectedIndex = 0
                Case 3
                    CurRecType = 2 ' "Check-in/walk-in passengers"
                    WordsPosComboBox.SelectedIndex = 0
                Case 4
                    CurRecType = 3 ' "Credit card holders"
                    WordsPosComboBox.SelectedIndex = 0
            End Select
            If CurRecType <> -1 Then
                Dim FT As List(Of Format) = StarForm_Instance.db1.Formats.Where(Function(X) X.Type = CurRecType).ToList
                CurRec = FT(0)
                DataGridView1.Rows.Clear()
                For I As Integer = 1 To 15
                    Select Case I
                        Case 1 : DataGridView1.Rows.Add(I, CurRec.F1, CurRec.F1comment)
                        Case 2 : DataGridView1.Rows.Add(I, CurRec.F2, CurRec.F2comment)
                        Case 3 : DataGridView1.Rows.Add(I, CurRec.F3, CurRec.F3comment)
                        Case 4 : DataGridView1.Rows.Add(I, CurRec.F4, CurRec.F4comment)
                        Case 5 : DataGridView1.Rows.Add(I, CurRec.F5, CurRec.F5comment)
                        Case 6 : DataGridView1.Rows.Add(I, CurRec.F6, CurRec.F6comment)
                        Case 7 : DataGridView1.Rows.Add(I, CurRec.F7, CurRec.F7comment)
                        Case 8 : DataGridView1.Rows.Add(I, CurRec.F8, CurRec.F8comment)
                        Case 9 : DataGridView1.Rows.Add(I, CurRec.F9, CurRec.F9comment)
                        Case 10 : DataGridView1.Rows.Add(I, CurRec.F10, CurRec.F10comment)
                        Case 11 : DataGridView1.Rows.Add(I, CurRec.F11, CurRec.F11comment)
                        Case 12 : DataGridView1.Rows.Add(I, CurRec.F12, CurRec.F12comment)
                        Case 13 : DataGridView1.Rows.Add(I, CurRec.F13, CurRec.F13comment)
                        Case 14 : DataGridView1.Rows.Add(I, CurRec.F14, CurRec.F14comment)
                        Case 15 : DataGridView1.Rows.Add(I, CurRec.F15, CurRec.F15comment)
                    End Select
                Next
            End If
        End If
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        If TypeToolStripComboBox.SelectedIndex > 0 Then
            Try
                For I As Integer = 0 To 14
                    Select Case I
                        Case 0 : CurRec.F1 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F1comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 1 : CurRec.F2 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F2comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 2 : CurRec.F3 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F3comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 3 : CurRec.F4 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F4comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 4 : CurRec.F5 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F5comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 5 : CurRec.F6 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F6comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 6 : CurRec.F7 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F7comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 7 : CurRec.F8 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F8comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 8 : CurRec.F9 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F9comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 9 : CurRec.F10 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F10comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 10 : CurRec.F11 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F11comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 11 : CurRec.F12 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F12comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 12 : CurRec.F13 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F13comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 13 : CurRec.F14 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F14comment = DataGridView1.Rows(I).Cells(2).Value
                        Case 14 : CurRec.F15 = CInt(DataGridView1.Rows(I).Cells(1).Value) : CurRec.F15comment = DataGridView1.Rows(I).Cells(2).Value
                    End Select
                Next
                CurRec.FormatType = WordsPosComboBox.SelectedIndex
                StarForm_Instance.db1.SaveChanges()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub


End Class