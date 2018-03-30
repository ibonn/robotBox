<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class settingsForm
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
        Me.yesButton = New System.Windows.Forms.Button()
        Me.noButton = New System.Windows.Forms.Button()
        Me.settingsTabs = New System.Windows.Forms.TabControl()
        Me.simulatorTab = New System.Windows.Forms.TabPage()
        Me.robotGroup = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.angleUpDown = New System.Windows.Forms.NumericUpDown()
        Me.angleLabel = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.startXTextBox = New System.Windows.Forms.TextBox()
        Me.startYTextBox = New System.Windows.Forms.TextBox()
        Me.startLocationLabel = New System.Windows.Forms.Label()
        Me.miscGroup = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.gridSizeUpDown = New System.Windows.Forms.NumericUpDown()
        Me.gridLabel = New System.Windows.Forms.Label()
        Me.translateLabel = New System.Windows.Forms.LinkLabel()
        Me.langLabel = New System.Windows.Forms.Label()
        Me.languageSelector = New System.Windows.Forms.ComboBox()
        Me.defaultPortTextBox = New System.Windows.Forms.TextBox()
        Me.defPortLabel = New System.Windows.Forms.Label()
        Me.framerateGroup = New System.Windows.Forms.GroupBox()
        Me.frameratePanel = New System.Windows.Forms.Panel()
        Me.frameRateTrackbar = New System.Windows.Forms.TrackBar()
        Me.fpsLabel = New System.Windows.Forms.Label()
        Me.frameRateUpDown = New System.Windows.Forms.NumericUpDown()
        Me.autoAdjustCheck = New System.Windows.Forms.CheckBox()
        Me.maxFPSLabel = New System.Windows.Forms.Label()
        Me.minFPSLabel = New System.Windows.Forms.Label()
        Me.minFrameRateUpDown = New System.Windows.Forms.NumericUpDown()
        Me.maxFrameRateUpDown = New System.Windows.Forms.NumericUpDown()
        Me.recorderTab = New System.Windows.Forms.TabPage()
        Me.ffmpegLabel = New System.Windows.Forms.Label()
        Me.browseButton = New System.Windows.Forms.Button()
        Me.ffmpegLocationTextBox = New System.Windows.Forms.TextBox()
        Me.noteLabel = New System.Windows.Forms.Label()
        Me.ffmpegFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.settingsTabs.SuspendLayout()
        Me.simulatorTab.SuspendLayout()
        Me.robotGroup.SuspendLayout()
        CType(Me.angleUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.miscGroup.SuspendLayout()
        CType(Me.gridSizeUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.framerateGroup.SuspendLayout()
        Me.frameratePanel.SuspendLayout()
        CType(Me.frameRateTrackbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.frameRateUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.minFrameRateUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.maxFrameRateUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.recorderTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'yesButton
        '
        Me.yesButton.Location = New System.Drawing.Point(433, 344)
        Me.yesButton.Name = "yesButton"
        Me.yesButton.Size = New System.Drawing.Size(75, 23)
        Me.yesButton.TabIndex = 3
        Me.yesButton.Text = "Ok"
        Me.yesButton.UseVisualStyleBackColor = True
        '
        'noButton
        '
        Me.noButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.noButton.Location = New System.Drawing.Point(514, 344)
        Me.noButton.Name = "noButton"
        Me.noButton.Size = New System.Drawing.Size(75, 23)
        Me.noButton.TabIndex = 4
        Me.noButton.Text = "Cancel"
        Me.noButton.UseVisualStyleBackColor = True
        '
        'settingsTabs
        '
        Me.settingsTabs.Controls.Add(Me.simulatorTab)
        Me.settingsTabs.Controls.Add(Me.recorderTab)
        Me.settingsTabs.Location = New System.Drawing.Point(12, 12)
        Me.settingsTabs.Name = "settingsTabs"
        Me.settingsTabs.SelectedIndex = 0
        Me.settingsTabs.Size = New System.Drawing.Size(577, 326)
        Me.settingsTabs.TabIndex = 5
        '
        'simulatorTab
        '
        Me.simulatorTab.Controls.Add(Me.robotGroup)
        Me.simulatorTab.Controls.Add(Me.miscGroup)
        Me.simulatorTab.Controls.Add(Me.framerateGroup)
        Me.simulatorTab.Location = New System.Drawing.Point(4, 22)
        Me.simulatorTab.Name = "simulatorTab"
        Me.simulatorTab.Padding = New System.Windows.Forms.Padding(3)
        Me.simulatorTab.Size = New System.Drawing.Size(569, 300)
        Me.simulatorTab.TabIndex = 0
        Me.simulatorTab.Text = "Simulator"
        Me.simulatorTab.UseVisualStyleBackColor = True
        '
        'robotGroup
        '
        Me.robotGroup.Controls.Add(Me.Label11)
        Me.robotGroup.Controls.Add(Me.angleUpDown)
        Me.robotGroup.Controls.Add(Me.angleLabel)
        Me.robotGroup.Controls.Add(Me.Label9)
        Me.robotGroup.Controls.Add(Me.Label8)
        Me.robotGroup.Controls.Add(Me.startXTextBox)
        Me.robotGroup.Controls.Add(Me.startYTextBox)
        Me.robotGroup.Controls.Add(Me.startLocationLabel)
        Me.robotGroup.Location = New System.Drawing.Point(296, 154)
        Me.robotGroup.Name = "robotGroup"
        Me.robotGroup.Size = New System.Drawing.Size(267, 140)
        Me.robotGroup.TabIndex = 17
        Me.robotGroup.TabStop = False
        Me.robotGroup.Text = "Robot"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(135, 68)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(25, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "deg"
        '
        'angleUpDown
        '
        Me.angleUpDown.Location = New System.Drawing.Point(49, 66)
        Me.angleUpDown.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.angleUpDown.Name = "angleUpDown"
        Me.angleUpDown.Size = New System.Drawing.Size(80, 20)
        Me.angleUpDown.TabIndex = 6
        '
        'angleLabel
        '
        Me.angleLabel.AutoSize = True
        Me.angleLabel.Location = New System.Drawing.Point(6, 68)
        Me.angleLabel.Name = "angleLabel"
        Me.angleLabel.Size = New System.Drawing.Size(37, 13)
        Me.angleLabel.TabIndex = 5
        Me.angleLabel.Text = "Angle:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(138, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(17, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Y:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(17, 13)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "X:"
        '
        'startXTextBox
        '
        Me.startXTextBox.Location = New System.Drawing.Point(29, 36)
        Me.startXTextBox.Name = "startXTextBox"
        Me.startXTextBox.Size = New System.Drawing.Size(100, 20)
        Me.startXTextBox.TabIndex = 2
        Me.startXTextBox.Text = "37"
        '
        'startYTextBox
        '
        Me.startYTextBox.Location = New System.Drawing.Point(161, 35)
        Me.startYTextBox.Name = "startYTextBox"
        Me.startYTextBox.Size = New System.Drawing.Size(100, 20)
        Me.startYTextBox.TabIndex = 1
        Me.startYTextBox.Text = "37"
        '
        'startLocationLabel
        '
        Me.startLocationLabel.AutoSize = True
        Me.startLocationLabel.Location = New System.Drawing.Point(6, 20)
        Me.startLocationLabel.Name = "startLocationLabel"
        Me.startLocationLabel.Size = New System.Drawing.Size(72, 13)
        Me.startLocationLabel.TabIndex = 0
        Me.startLocationLabel.Text = "Start location:"
        '
        'miscGroup
        '
        Me.miscGroup.Controls.Add(Me.Label14)
        Me.miscGroup.Controls.Add(Me.gridSizeUpDown)
        Me.miscGroup.Controls.Add(Me.gridLabel)
        Me.miscGroup.Controls.Add(Me.translateLabel)
        Me.miscGroup.Controls.Add(Me.langLabel)
        Me.miscGroup.Controls.Add(Me.languageSelector)
        Me.miscGroup.Controls.Add(Me.defaultPortTextBox)
        Me.miscGroup.Controls.Add(Me.defPortLabel)
        Me.miscGroup.Location = New System.Drawing.Point(6, 154)
        Me.miscGroup.Name = "miscGroup"
        Me.miscGroup.Size = New System.Drawing.Size(284, 140)
        Me.miscGroup.TabIndex = 16
        Me.miscGroup.TabStop = False
        Me.miscGroup.Text = "Misc"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(256, 98)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(23, 13)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "mm"
        '
        'gridSizeUpDown
        '
        Me.gridSizeUpDown.Location = New System.Drawing.Point(171, 96)
        Me.gridSizeUpDown.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.gridSizeUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.gridSizeUpDown.Name = "gridSizeUpDown"
        Me.gridSizeUpDown.Size = New System.Drawing.Size(79, 20)
        Me.gridSizeUpDown.TabIndex = 21
        Me.gridSizeUpDown.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'gridLabel
        '
        Me.gridLabel.AutoSize = True
        Me.gridLabel.Location = New System.Drawing.Point(12, 98)
        Me.gridLabel.Name = "gridLabel"
        Me.gridLabel.Size = New System.Drawing.Size(85, 13)
        Me.gridLabel.TabIndex = 20
        Me.gridLabel.Text = "Grid square size:"
        '
        'translateLabel
        '
        Me.translateLabel.AutoSize = True
        Me.translateLabel.Location = New System.Drawing.Point(188, 68)
        Me.translateLabel.Name = "translateLabel"
        Me.translateLabel.Size = New System.Drawing.Size(83, 13)
        Me.translateLabel.TabIndex = 19
        Me.translateLabel.TabStop = True
        Me.translateLabel.Text = "Help translating!"
        '
        'langLabel
        '
        Me.langLabel.AutoSize = True
        Me.langLabel.Location = New System.Drawing.Point(12, 68)
        Me.langLabel.Name = "langLabel"
        Me.langLabel.Size = New System.Drawing.Size(58, 13)
        Me.langLabel.TabIndex = 17
        Me.langLabel.Text = "Language:"
        '
        'languageSelector
        '
        Me.languageSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.languageSelector.FormattingEnabled = True
        Me.languageSelector.Location = New System.Drawing.Point(82, 65)
        Me.languageSelector.Name = "languageSelector"
        Me.languageSelector.Size = New System.Drawing.Size(100, 21)
        Me.languageSelector.TabIndex = 18
        '
        'defaultPortTextBox
        '
        Me.defaultPortTextBox.Location = New System.Drawing.Point(171, 39)
        Me.defaultPortTextBox.Name = "defaultPortTextBox"
        Me.defaultPortTextBox.Size = New System.Drawing.Size(100, 20)
        Me.defaultPortTextBox.TabIndex = 17
        Me.defaultPortTextBox.Text = "15000"
        '
        'defPortLabel
        '
        Me.defPortLabel.AutoSize = True
        Me.defPortLabel.Location = New System.Drawing.Point(12, 42)
        Me.defPortLabel.Name = "defPortLabel"
        Me.defPortLabel.Size = New System.Drawing.Size(65, 13)
        Me.defPortLabel.TabIndex = 18
        Me.defPortLabel.Text = "Default port:"
        '
        'framerateGroup
        '
        Me.framerateGroup.Controls.Add(Me.frameratePanel)
        Me.framerateGroup.Controls.Add(Me.autoAdjustCheck)
        Me.framerateGroup.Controls.Add(Me.maxFPSLabel)
        Me.framerateGroup.Controls.Add(Me.minFPSLabel)
        Me.framerateGroup.Controls.Add(Me.minFrameRateUpDown)
        Me.framerateGroup.Controls.Add(Me.maxFrameRateUpDown)
        Me.framerateGroup.Location = New System.Drawing.Point(6, 6)
        Me.framerateGroup.Name = "framerateGroup"
        Me.framerateGroup.Size = New System.Drawing.Size(557, 142)
        Me.framerateGroup.TabIndex = 13
        Me.framerateGroup.TabStop = False
        Me.framerateGroup.Text = "Frame rate"
        '
        'frameratePanel
        '
        Me.frameratePanel.Controls.Add(Me.frameRateTrackbar)
        Me.frameratePanel.Controls.Add(Me.fpsLabel)
        Me.frameratePanel.Controls.Add(Me.frameRateUpDown)
        Me.frameratePanel.Location = New System.Drawing.Point(6, 36)
        Me.frameratePanel.Name = "frameratePanel"
        Me.frameratePanel.Size = New System.Drawing.Size(545, 100)
        Me.frameratePanel.TabIndex = 12
        '
        'frameRateTrackbar
        '
        Me.frameRateTrackbar.Location = New System.Drawing.Point(68, 35)
        Me.frameRateTrackbar.Maximum = 100
        Me.frameRateTrackbar.Minimum = 1
        Me.frameRateTrackbar.Name = "frameRateTrackbar"
        Me.frameRateTrackbar.Size = New System.Drawing.Size(412, 45)
        Me.frameRateTrackbar.TabIndex = 6
        Me.frameRateTrackbar.Value = 25
        '
        'fpsLabel
        '
        Me.fpsLabel.AutoSize = True
        Me.fpsLabel.Location = New System.Drawing.Point(2, 44)
        Me.fpsLabel.Name = "fpsLabel"
        Me.fpsLabel.Size = New System.Drawing.Size(60, 13)
        Me.fpsLabel.TabIndex = 4
        Me.fpsLabel.Text = "Frame rate:"
        '
        'frameRateUpDown
        '
        Me.frameRateUpDown.Location = New System.Drawing.Point(486, 42)
        Me.frameRateUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.frameRateUpDown.Name = "frameRateUpDown"
        Me.frameRateUpDown.Size = New System.Drawing.Size(56, 20)
        Me.frameRateUpDown.TabIndex = 10
        Me.frameRateUpDown.Value = New Decimal(New Integer() {25, 0, 0, 0})
        '
        'autoAdjustCheck
        '
        Me.autoAdjustCheck.AutoSize = True
        Me.autoAdjustCheck.Checked = True
        Me.autoAdjustCheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.autoAdjustCheck.Location = New System.Drawing.Point(6, 19)
        Me.autoAdjustCheck.Name = "autoAdjustCheck"
        Me.autoAdjustCheck.Size = New System.Drawing.Size(129, 17)
        Me.autoAdjustCheck.TabIndex = 1
        Me.autoAdjustCheck.Text = "Auto adjust frame rate"
        Me.autoAdjustCheck.UseVisualStyleBackColor = True
        '
        'maxFPSLabel
        '
        Me.maxFPSLabel.AutoSize = True
        Me.maxFPSLabel.Location = New System.Drawing.Point(9, 52)
        Me.maxFPSLabel.Name = "maxFPSLabel"
        Me.maxFPSLabel.Size = New System.Drawing.Size(104, 13)
        Me.maxFPSLabel.TabIndex = 2
        Me.maxFPSLabel.Text = "Maximum frame rate:"
        '
        'minFPSLabel
        '
        Me.minFPSLabel.AutoSize = True
        Me.minFPSLabel.Location = New System.Drawing.Point(12, 103)
        Me.minFPSLabel.Name = "minFPSLabel"
        Me.minFPSLabel.Size = New System.Drawing.Size(101, 13)
        Me.minFPSLabel.TabIndex = 3
        Me.minFPSLabel.Text = "Minimum frame rate:"
        '
        'minFrameRateUpDown
        '
        Me.minFrameRateUpDown.Location = New System.Drawing.Point(154, 101)
        Me.minFrameRateUpDown.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.minFrameRateUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.minFrameRateUpDown.Name = "minFrameRateUpDown"
        Me.minFrameRateUpDown.Size = New System.Drawing.Size(397, 20)
        Me.minFrameRateUpDown.TabIndex = 9
        Me.minFrameRateUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'maxFrameRateUpDown
        '
        Me.maxFrameRateUpDown.Location = New System.Drawing.Point(154, 50)
        Me.maxFrameRateUpDown.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.maxFrameRateUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.maxFrameRateUpDown.Name = "maxFrameRateUpDown"
        Me.maxFrameRateUpDown.Size = New System.Drawing.Size(397, 20)
        Me.maxFrameRateUpDown.TabIndex = 8
        Me.maxFrameRateUpDown.Value = New Decimal(New Integer() {40, 0, 0, 0})
        '
        'recorderTab
        '
        Me.recorderTab.Controls.Add(Me.ffmpegLabel)
        Me.recorderTab.Controls.Add(Me.browseButton)
        Me.recorderTab.Controls.Add(Me.ffmpegLocationTextBox)
        Me.recorderTab.Location = New System.Drawing.Point(4, 22)
        Me.recorderTab.Name = "recorderTab"
        Me.recorderTab.Padding = New System.Windows.Forms.Padding(3)
        Me.recorderTab.Size = New System.Drawing.Size(569, 300)
        Me.recorderTab.TabIndex = 1
        Me.recorderTab.Text = "Recorder"
        Me.recorderTab.UseVisualStyleBackColor = True
        '
        'ffmpegLabel
        '
        Me.ffmpegLabel.AutoSize = True
        Me.ffmpegLabel.Location = New System.Drawing.Point(17, 15)
        Me.ffmpegLabel.Name = "ffmpegLabel"
        Me.ffmpegLabel.Size = New System.Drawing.Size(82, 13)
        Me.ffmpegLabel.TabIndex = 2
        Me.ffmpegLabel.Text = "ffmpeg location:"
        '
        'browseButton
        '
        Me.browseButton.Location = New System.Drawing.Point(484, 10)
        Me.browseButton.Name = "browseButton"
        Me.browseButton.Size = New System.Drawing.Size(75, 23)
        Me.browseButton.TabIndex = 1
        Me.browseButton.Text = "Browse"
        Me.browseButton.UseVisualStyleBackColor = True
        '
        'ffmpegLocationTextBox
        '
        Me.ffmpegLocationTextBox.Location = New System.Drawing.Point(174, 12)
        Me.ffmpegLocationTextBox.Name = "ffmpegLocationTextBox"
        Me.ffmpegLocationTextBox.ReadOnly = True
        Me.ffmpegLocationTextBox.Size = New System.Drawing.Size(304, 20)
        Me.ffmpegLocationTextBox.TabIndex = 0
        '
        'noteLabel
        '
        Me.noteLabel.AutoSize = True
        Me.noteLabel.Location = New System.Drawing.Point(13, 349)
        Me.noteLabel.Name = "noteLabel"
        Me.noteLabel.Size = New System.Drawing.Size(294, 13)
        Me.noteLabel.TabIndex = 6
        Me.noteLabel.Text = "Note: You must restart the simulator to apply the new settings"
        '
        'ffmpegFileDialog
        '
        Me.ffmpegFileDialog.FileName = "ffmpeg.exe"
        Me.ffmpegFileDialog.Filter = "Windows executable files|*.exe|All files|*.*"
        '
        'settingsForm
        '
        Me.AcceptButton = Me.yesButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.noButton
        Me.ClientSize = New System.Drawing.Size(607, 379)
        Me.Controls.Add(Me.noteLabel)
        Me.Controls.Add(Me.settingsTabs)
        Me.Controls.Add(Me.noButton)
        Me.Controls.Add(Me.yesButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "settingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "robotBox - Settings"
        Me.settingsTabs.ResumeLayout(False)
        Me.simulatorTab.ResumeLayout(False)
        Me.robotGroup.ResumeLayout(False)
        Me.robotGroup.PerformLayout()
        CType(Me.angleUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.miscGroup.ResumeLayout(False)
        Me.miscGroup.PerformLayout()
        CType(Me.gridSizeUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.framerateGroup.ResumeLayout(False)
        Me.framerateGroup.PerformLayout()
        Me.frameratePanel.ResumeLayout(False)
        Me.frameratePanel.PerformLayout()
        CType(Me.frameRateTrackbar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.frameRateUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.minFrameRateUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.maxFrameRateUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.recorderTab.ResumeLayout(False)
        Me.recorderTab.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents yesButton As Button
    Friend WithEvents noButton As Button
    Friend WithEvents settingsTabs As TabControl
    Friend WithEvents simulatorTab As TabPage
    Friend WithEvents minFrameRateUpDown As NumericUpDown
    Friend WithEvents maxFrameRateUpDown As NumericUpDown
    Friend WithEvents minFPSLabel As Label
    Friend WithEvents maxFPSLabel As Label
    Friend WithEvents autoAdjustCheck As CheckBox
    Friend WithEvents miscGroup As GroupBox
    Friend WithEvents framerateGroup As GroupBox
    Friend WithEvents translateLabel As LinkLabel
    Friend WithEvents langLabel As Label
    Friend WithEvents languageSelector As ComboBox
    Friend WithEvents defaultPortTextBox As TextBox
    Friend WithEvents defPortLabel As Label
    Friend WithEvents robotGroup As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents angleUpDown As NumericUpDown
    Friend WithEvents angleLabel As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents startXTextBox As TextBox
    Friend WithEvents startYTextBox As TextBox
    Friend WithEvents startLocationLabel As Label
    Friend WithEvents noteLabel As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents gridSizeUpDown As NumericUpDown
    Friend WithEvents gridLabel As Label
    Friend WithEvents recorderTab As TabPage
    Friend WithEvents ffmpegLabel As Label
    Friend WithEvents browseButton As Button
    Friend WithEvents ffmpegLocationTextBox As TextBox
    Friend WithEvents ffmpegFileDialog As OpenFileDialog
    Friend WithEvents frameratePanel As Panel
    Friend WithEvents frameRateTrackbar As TrackBar
    Friend WithEvents fpsLabel As Label
    Friend WithEvents frameRateUpDown As NumericUpDown
End Class
