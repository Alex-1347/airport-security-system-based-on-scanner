<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DateFilterForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DateFilterForm))
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.FromDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.ToDateTimePicker = New System.Windows.Forms.DateTimePicker()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(755, 188)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.FromDateTimePicker)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ToDateTimePicker)
        Me.SplitContainer1.Size = New System.Drawing.Size(401, 20)
        Me.SplitContainer1.SplitterDistance = 197
        Me.SplitContainer1.TabIndex = 1
        '
        'FromDateTimePicker
        '
        Me.FromDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FromDateTimePicker.Location = New System.Drawing.Point(0, 0)
        Me.FromDateTimePicker.MaxDate = New Date(2031, 2, 24, 0, 0, 0, 0)
        Me.FromDateTimePicker.MinDate = New Date(2021, 1, 1, 0, 0, 0, 0)
        Me.FromDateTimePicker.Name = "FromDateTimePicker"
        Me.FromDateTimePicker.Size = New System.Drawing.Size(197, 20)
        Me.FromDateTimePicker.TabIndex = 0
        '
        'ToDateTimePicker
        '
        Me.ToDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToDateTimePicker.Location = New System.Drawing.Point(0, 0)
        Me.ToDateTimePicker.MaxDate = New Date(2031, 2, 24, 0, 0, 0, 0)
        Me.ToDateTimePicker.MinDate = New Date(2021, 1, 1, 0, 0, 0, 0)
        Me.ToDateTimePicker.Name = "ToDateTimePicker"
        Me.ToDateTimePicker.Size = New System.Drawing.Size(200, 20)
        Me.ToDateTimePicker.TabIndex = 0
        '
        'DateFilterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 20)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DateFilterForm"
        Me.Text = "Select date (From - To)"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents FromDateTimePicker As DateTimePicker
    Friend WithEvents ToDateTimePicker As DateTimePicker
End Class
