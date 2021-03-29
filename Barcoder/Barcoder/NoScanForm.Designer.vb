<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NoScanForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NoScanForm))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CommentTextBox = New System.Windows.Forms.TextBox()
        Me.OkButton = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.CommentTextBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.OkButton)
        Me.SplitContainer1.Size = New System.Drawing.Size(422, 229)
        Me.SplitContainer1.SplitterDistance = 194
        Me.SplitContainer1.TabIndex = 0
        '
        'CommentTextBox
        '
        Me.CommentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CommentTextBox.Location = New System.Drawing.Point(0, 0)
        Me.CommentTextBox.Multiline = True
        Me.CommentTextBox.Name = "CommentTextBox"
        Me.CommentTextBox.Size = New System.Drawing.Size(194, 229)
        Me.CommentTextBox.TabIndex = 0
        '
        'OkButton
        '
        Me.OkButton.BackgroundImage = CType(resources.GetObject("OkButton.BackgroundImage"), System.Drawing.Image)
        Me.OkButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OkButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OkButton.Location = New System.Drawing.Point(0, 0)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(224, 229)
        Me.OkButton.TabIndex = 0
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'NoScanForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(422, 229)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NoScanForm"
        Me.Text = "Add comment"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents OkButton As Button
    Friend WithEvents CommentTextBox As TextBox
End Class
