Imports System.Text
Imports System.Text.RegularExpressions

Public Class DataForm

    Dim ShowRows As List(Of InputData)
    Dim Style1 = New DataGridViewCellStyle()
    Dim Style2 = New DataGridViewCellStyle()
    Dim Style3 = New DataGridViewCellStyle()
    Dim Style4 = New DataGridViewCellStyle()
    Dim FilteredType As Integer = -1
    Public FromDateFilter As Date
    Public ToDateFilter As Date
    Dim DateFilterSet As Boolean = False
    Dim FormLoaded As Boolean = False
    Dim TotalDbRows As Integer

    Private Sub DataForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        Style1.BackColor = Color.FromArgb(200, 200, 220)
        Style2.BackColor = Color.FromArgb(200, 220, 200)
        Style3.BackColor = Color.FromArgb(200, 200, 200)
        Style4.BackColor = Color.FromArgb(200, 220, 220)
        PageSizeComboBox.SelectedIndex = 0
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.ReadOnly = True
        DataGridView1.Columns.Clear()
        For I As Integer = 0 To 15
            If I = 3 Then
                Dim IconColumn As DataGridViewImageColumn = New DataGridViewImageColumn()
                IconColumn.Width = 25
                DataGridView1.Columns.Add(IconColumn)
            Else
                DataGridView1.Columns.Add("Field" & I.ToString("D2"), I.ToString("D2"))
            End If
        Next
        DataGridView1.Columns(0).HeaderText = "#"
        DataGridView1.Columns(0).Width = 40
        DataGridView1.Columns(1).HeaderText = "Date"
        DataGridView1.Columns(2).HeaderText = "Type"
        DataGridView1.Columns(3).HeaderText = "Edit"
        DataGridView1.Columns(4).HeaderText = "Comment"
        For I As Integer = 5 To 15
            DataGridView1.Columns(I).HeaderText = "..."
        Next
        RefreshToolStripButton1_Click(Nothing, Nothing)
        TypeToolStripComboBox1.SelectedIndex = 0
        FormLoaded = True
    End Sub

    Private Sub RefreshToolStripButton1_Click(sender As Object, e As EventArgs) Handles RefreshToolStripButton1.Click
        Try
            TotalDbRows = StarForm_Instance.db1.InputDatas.Count
            ShowRows = StarForm_Instance.db1.InputDatas.
                Where(AddressOf WhereType).
                Where(AddressOf WhereDate).
                OrderBy(Function(X) X.id).Skip(CInt(PageSizeComboBox.Text) * CInt(FromPageToolStripTextBox.Text)).
                Take(PageSizeComboBox.Text).ToList
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
        ResList = SplitRows(ShowRows).ToList
        ToolStripStatusLabel1.Text = $"filtered {ShowRows.Count} rows start from record {CInt(PageSizeComboBox.Text) * CInt(FromPageToolStripTextBox.Text)} (total {TotalDbRows} rows)"
        StatusStrip1.Refresh()
        DataGridView1.DataSource = ResList
        DataGridView1.Refresh()
    End Sub

    Function WhereType(X As InputData)
        If FilteredType = -1 Then
            Return True
        ElseIf FilteredType = X.type Then
            Return True
        Else
            Return False
        End If
    End Function

    Function WhereDate(X As InputData)
        If Not DateFilterSet Then
            Return True
        ElseIf DateFilterSet And X.crdate >= FromDateFilter And X.crdate <= ToDateFilter Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub RightToolStripButton_Click(sender As Object, e As EventArgs) Handles RightToolStripButton.Click
        If CInt(FromPageToolStripTextBox.Text) >= 0 Then
            FromPageToolStripTextBox.Text = CInt(FromPageToolStripTextBox.Text) + 1
            RefreshToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub LeftToolStripButton_Click(sender As Object, e As EventArgs) Handles LeftToolStripButton.Click
        If CInt(FromPageToolStripTextBox.Text) > 0 Then
            FromPageToolStripTextBox.Text = CInt(FromPageToolStripTextBox.Text) - 1
            RefreshToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub

    Dim ResList As IEnumerable(Of List(Of String))
    Iterator Function SplitRows(ShowRows As List(Of InputData)) As IEnumerable(Of List(Of String))
        Dim CurRow As String()
        Dim Format As String()
        For RowIndex As Integer = 0 To ShowRows.Count - 1
            If Not String.IsNullOrEmpty(ShowRows(RowIndex).txt) Then
                '
                If String.IsNullOrEmpty(ShowRows(RowIndex).split) Then
                    ShowRows(RowIndex).split = GetWordsPosition(ShowRows(RowIndex).txt)
                End If
                If ShowRows(RowIndex).split = "0;" Then ' the same ShowRows(RowIndex).split="0;"
                    ReDim CurRow(0)
                    CurRow(0) = ShowRows(RowIndex).txt
                    Yield CurRow.ToList
                Else
                    Format = ShowRows(RowIndex).split.Split(";")
                    'CurRow = ShowRows(RowIndex).txt.Split(" ")
                    Dim IntFormat As New List(Of Integer)
                    For ColIndex As Integer = 0 To Format.Count - 1
                        If IsNumeric(Format(ColIndex)) Then
                            IntFormat.Add(Format(ColIndex))
                        End If
                    Next
                    If IntFormat.Count = 0 Then
                        ReDim CurRow(0)
                        CurRow(0) = ShowRows(RowIndex).txt
                        Yield CurRow.ToList
                        Return
                    Else
                        Try
                            ReDim CurRow(IntFormat.Count)
                            Dim I As Integer
                            For I = 0 To IntFormat.Count - 2
                                CurRow(I) = ShowRows(RowIndex).txt.ToString.Substring(IntFormat(I), IntFormat(I + 1) - IntFormat(I))
                            Next
                            CurRow(I + 1) = ShowRows(RowIndex).txt.ToString.Substring(IntFormat(I))
                            Yield CurRow.ToList
                        Catch ex As Exception
                            MsgBox($"Invalid data format in record {ShowRows(RowIndex).id}", MsgBoxStyle.Critical)
                        End Try
                        'ReDim CurRow(0)
                        'CurRow(0) = ShowRows(RowIndex).txt
                        'Yield CurRow.ToList
                    End If
                End If
            Else
                CurRow = Nothing
                Yield New List(Of String)
            End If
        Next
    End Function

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'Debug.Print(e.RowIndex & ":" & e.ColumnIndex)
        If ShowRows.Count > 0 And ShowRows.Count > e.RowIndex Then
            If e.ColumnIndex = 0 Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ShowRows(e.RowIndex).id
            ElseIf e.ColumnIndex = 1 Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ShowRows(e.RowIndex).crdate
            ElseIf e.ColumnIndex = 2 Then
                Select Case ShowRows(e.RowIndex).type
                    Case 0 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Airlines boarding pass"
                    Case 1 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Royalty passes for lounge"
                    Case 2 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Check-in/walk-in passengers"
                    Case 3 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Credit card holders"
                    Case Else : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "(unspecified)"
                End Select
            ElseIf e.ColumnIndex = 3 Then
                'blinking
                'DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = My.Resources.edit_1
            ElseIf e.ColumnIndex = 4 Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ShowRows(e.RowIndex).comment
            ElseIf e.ColumnIndex > 4 And e.ColumnIndex - 5 < ResList(e.RowIndex).Count Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ResList(e.RowIndex)(e.ColumnIndex - 5)
            End If
            Select Case ShowRows(e.RowIndex).type
                Case 0 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = Style1
                Case 1 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = Style2
                Case 2 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = Style3
                Case 3 : DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Style = Style4
            End Select
        End If
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For I As Integer = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(I).Cells(3).Value = My.Resources.edit_1
        Next
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 3 Then
            Dim X As New EditForm
            X.ParentDataForm = Me
            X.CurRecord = ShowRows(e.RowIndex)
            X.ShowDialog()
            RefreshToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub PageSizeComboBox_Click(sender As Object, e As EventArgs) Handles PageSizeComboBox.SelectedIndexChanged
        If FormLoaded Then
            RefreshToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub ExcelButton_Click(sender As Object, e As EventArgs) Handles ExcelButton.Click
        Dim Process As System.Diagnostics.Process
        Dim Str1 As New StringBuilder
        Dim CsvFileName As String = IO.Path.Combine(StarForm_Instance.TempDir, "Export." & Now.ToString.Replace(":", "-").Replace("/", "-").Replace("\", "-").Replace(" ", "-") & ".csv")
        Try
            For j As Integer = 0 To 15
                Str1.Append(DataGridView1.Columns(j).HeaderText & ";")
            Next
            For i As Integer = 0 To ResList.Count - 1
                Str1.AppendLine()
                Str1.Append(ShowRows(i).id & ";")
                Str1.Append(ShowRows(i).crdate & ";")
                Select Case ShowRows(i).type
                    Case 0 : Str1.Append("Airlines boarding pass;")
                    Case 1 : Str1.Append("Royalty passes for lounge;")
                    Case 2 : Str1.Append("Check-in/walk-in passengers;")
                    Case 3 : Str1.Append("Credit card holders;")
                    Case Else : Str1.Append("(unspecified);")
                End Select
                Str1.Append(ShowRows(i).comment & ";")
                For J As Integer = 4 To 15
                    If DataGridView1.Rows(i).Cells(J).Value IsNot Nothing Then
                        Str1.Append(DataGridView1.Rows(i).Cells(J).Value.ToString & ";")
                    Else
                        Str1.Append(";")
                    End If
                Next
            Next
            My.Computer.FileSystem.WriteAllText(CsvFileName, Str1.ToString, False, System.Text.Encoding.UTF8)
            Process = System.Diagnostics.Process.Start(CsvFileName)
        Catch ex As Exception
            MsgBox(CsvFileName & vbCrLf & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub TypeToolStripComboBox1_Click(sender As Object, e As EventArgs) Handles TypeToolStripComboBox1.SelectedIndexChanged
        If FormLoaded Then
            Select Case TypeToolStripComboBox1.Text
                Case "Airlines boarding pass" : FilteredType = 0
                Case "Royalty passes for lounge" : FilteredType = 1
                Case "Check-in/walk-in passengers" : FilteredType = 2
                Case "Credit card holders" : FilteredType = 3
                Case "(unspecified)" : FilteredType = 4
                Case Else : FilteredType = -1
            End Select
            RefreshColumnName(FilteredType)
            RefreshToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub


    Private Sub RefreshColumnName(FilteredType As Integer)
        If FilteredType = 0 Or FilteredType = 1 Or FilteredType = 2 Or FilteredType = 3 Then
            Dim Fmt As Format = StarForm_Instance.db1.Formats.Where(Function(z) z.Type = FilteredType).FirstOrDefault
            For I As Integer = 5 To 15
                Select Case I
                    Case 5 : DataGridView1.Columns(I).HeaderText = Fmt.F1comment
                    Case 6 : DataGridView1.Columns(I).HeaderText = Fmt.F2comment
                    Case 7 : DataGridView1.Columns(I).HeaderText = Fmt.F3comment
                    Case 8 : DataGridView1.Columns(I).HeaderText = Fmt.F4comment
                    Case 9 : DataGridView1.Columns(I).HeaderText = Fmt.F5comment
                    Case 10 : DataGridView1.Columns(I).HeaderText = Fmt.F6comment
                    Case 11 : DataGridView1.Columns(I).HeaderText = Fmt.F7comment
                    Case 12 : DataGridView1.Columns(I).HeaderText = Fmt.F8comment
                    Case 13 : DataGridView1.Columns(I).HeaderText = Fmt.F9comment
                    Case 14 : DataGridView1.Columns(I).HeaderText = Fmt.F10comment
                    Case 15 : DataGridView1.Columns(I).HeaderText = Fmt.F11comment
                End Select
            Next
        Else
            For I As Integer = 5 To 15
                DataGridView1.Columns(I).HeaderText = "..."
            Next
        End If

    End Sub

    Private Sub DateFilterToolStripTextBox_Click(sender As Object, e As EventArgs) Handles DateFilterToolStripTextBox.Click
        If FormLoaded Then
            DateFilterSet = True
            Dim X As New DateFilterForm
            X.ParentDataForm = Me
            X.ShowDialog()
            DateFilterToolStripTextBox.Text = $"{FromDateFilter.ToShortDateString}-{ToDateFilter.ToShortDateString}"
            RefreshToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub






End Class