<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class aboutForm
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
        Me.okButton = New System.Windows.Forms.Button()
        Me.versionLabel = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.logoImage = New System.Windows.Forms.PictureBox()
        CType(Me.logoImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'okButton
        '
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.okButton.Location = New System.Drawing.Point(277, 133)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(108, 44)
        Me.okButton.TabIndex = 1
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'versionLabel
        '
        Me.versionLabel.AutoSize = True
        Me.versionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.versionLabel.Location = New System.Drawing.Point(133, 12)
        Me.versionLabel.Name = "versionLabel"
        Me.versionLabel.Size = New System.Drawing.Size(132, 18)
        Me.versionLabel.TabIndex = 2
        Me.versionLabel.Text = "robotBox - Version"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Location = New System.Drawing.Point(133, 33)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(252, 94)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "https://github.com/ibonn/robotBox"
        '
        'logoImage
        '
        Me.logoImage.Image = Global.robotBox.My.Resources.Resources.logo_text
        Me.logoImage.Location = New System.Drawing.Point(12, 12)
        Me.logoImage.Name = "logoImage"
        Me.logoImage.Size = New System.Drawing.Size(115, 115)
        Me.logoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.logoImage.TabIndex = 0
        Me.logoImage.TabStop = False
        '
        'aboutForm
        '
        Me.AcceptButton = Me.okButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.okButton
        Me.ClientSize = New System.Drawing.Size(397, 190)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.versionLabel)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.logoImage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "aboutForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "robotBox - About"
        CType(Me.logoImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents logoImage As PictureBox
    Friend WithEvents okButton As Button
    Friend WithEvents versionLabel As Label
    Friend WithEvents TextBox1 As TextBox
End Class
