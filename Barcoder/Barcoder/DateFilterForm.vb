Imports System.ComponentModel

Public Class DateFilterForm

    Public ParentDataForm As DataForm
    Private Sub DateFilterForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        FromDateTimePicker.Value = Now
        ToDateTimePicker.Value = Now
    End Sub

    Private Sub DateFilterForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ParentDataForm.FromDateFilter = FromDateTimePicker.Value
        ParentDataForm.ToDateFilter = ToDateTimePicker.Value
    End Sub
End Class