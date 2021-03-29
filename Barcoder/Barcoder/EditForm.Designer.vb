<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditForm))
        Me.CommentTextBox = New System.Windows.Forms.TextBox()
        Me.TypeComboBox = New System.Windows.Forms.ComboBox()
        Me.DataTextBox = New System.Windows.Forms.TextBox()
        Me.FormatTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CommentTextBox
        '
        Me.CommentTextBox.Location = New System.Drawing.Point(24, 73)
        Me.CommentTextBox.Multiline = True
        Me.CommentTextBox.Name = "CommentTextBox"
        Me.CommentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.CommentTextBox.Size = New System.Drawing.Size(415, 44)
        Me.CommentTextBox.TabIndex = 0
        '
        'TypeComboBox
        '
        Me.TypeComboBox.FormattingEnabled = True
        Me.TypeComboBox.Items.AddRange(New Object() {"Airlines boarding pass", "Royalty passes for lounge", "Check-in/walk-in passengers", "Credit card holders", "(unspecified)"})
        Me.TypeComboBox.Location = New System.Drawing.Point(24, 30)
        Me.TypeComboBox.Name = "TypeComboBox"
        Me.TypeComboBox.Size = New System.Drawing.Size(415, 21)
        Me.TypeComboBox.TabIndex = 1
        '
        'DataTextBox
        '
        Me.DataTextBox.Location = New System.Drawing.Point(24, 138)
        Me.DataTextBox.Multiline = True
        Me.DataTextBox.Name = "DataTextBox"
        Me.DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DataTextBox.Size = New System.Drawing.Size(415, 121)
        Me.DataTextBox.TabIndex = 2
        '
        'FormatTextBox
        '
        Me.FormatTextBox.Location = New System.Drawing.Point(24, 280)
        Me.FormatTextBox.Multiline = True
        Me.FormatTextBox.Name = "FormatTextBox"
        Me.FormatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.FormatTextBox.Size = New System.Drawing.Size(415, 79)
        Me.FormatTextBox.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(24, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label2.Location = New System.Drawing.Point(24, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Comment"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label3.Location = New System.Drawing.Point(21, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Data"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label4.Location = New System.Drawing.Point(21, 262)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Format"
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(175, 382)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 20
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'EditForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 421)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FormatTextBox)
        Me.Controls.Add(Me.DataTextBox)
        Me.Controls.Add(Me.TypeComboBox)
        Me.Controls.Add(Me.CommentTextBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EditForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CommentTextBox As TextBox
    Friend WithEvents TypeComboBox As ComboBox
    Friend WithEvents DataTextBox As TextBox
    Friend WithEvents FormatTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents SaveButton As Button
End Class
