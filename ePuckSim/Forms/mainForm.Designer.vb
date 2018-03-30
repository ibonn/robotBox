<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class mainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainForm))
        Me.applicationBar = New System.Windows.Forms.ToolStrip()
        Me.fileMenu = New System.Windows.Forms.ToolStripSplitButton()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.simulatorMenu = New System.Windows.Forms.ToolStripSplitButton()
        Me.DebugModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.separatorLine = New System.Windows.Forms.ToolStripSeparator()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.portLabel = New System.Windows.Forms.ToolStripLabel()
        Me.portSelector = New System.Windows.Forms.ToolStripTextBox()
        Me.simStartStopButton = New System.Windows.Forms.ToolStripButton()
        Me.toolBar = New System.Windows.Forms.ToolStrip()
        Me.cursorTool = New System.Windows.Forms.ToolStripButton()
        Me.moveTool = New System.Windows.Forms.ToolStripButton()
        Me.rotateTool = New System.Windows.Forms.ToolStripButton()
        Me.wallTool = New System.Windows.Forms.ToolStripButton()
        Me.pathTool = New System.Windows.Forms.ToolStripButton()
        Me.eraserTool = New System.Windows.Forms.ToolStripButton()
        Me.saveMapDialog = New System.Windows.Forms.SaveFileDialog()
        Me.openMapDialog = New System.Windows.Forms.OpenFileDialog()
        Me.canvas = New System.Windows.Forms.Panel()
        Me.statusBar = New System.Windows.Forms.StatusStrip()
        Me.statusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.recordToolbar = New System.Windows.Forms.ToolStrip()
        Me.recordButton = New System.Windows.Forms.ToolStripButton()
        Me.exportButton = New System.Windows.Forms.ToolStripButton()
        Me.exportProgressbar = New System.Windows.Forms.ToolStripProgressBar()
        Me.exportStatusLabel = New System.Windows.Forms.ToolStripLabel()
        Me.applicationBar.SuspendLayout()
        Me.toolBar.SuspendLayout()
        Me.statusBar.SuspendLayout()
        Me.recordToolbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'applicationBar
        '
        Me.applicationBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.applicationBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileMenu, Me.simulatorMenu, Me.portLabel, Me.portSelector, Me.simStartStopButton})
        Me.applicationBar.Location = New System.Drawing.Point(0, 0)
        Me.applicationBar.Name = "applicationBar"
        Me.applicationBar.Size = New System.Drawing.Size(875, 25)
        Me.applicationBar.TabIndex = 0
        '
        'fileMenu
        '
        Me.fileMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.fileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.SaveToolStripMenuItem, Me.OpenToolStripMenuItem})
        Me.fileMenu.Image = CType(resources.GetObject("fileMenu.Image"), System.Drawing.Image)
        Me.fileMenu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.fileMenu.Name = "fileMenu"
        Me.fileMenu.Size = New System.Drawing.Size(41, 22)
        Me.fileMenu.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Image = Global.robotBox.My.Resources.Resources.file_outline
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = Global.robotBox.My.Resources.Resources.content_save
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Image = Global.robotBox.My.Resources.Resources.open_in_app
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'simulatorMenu
        '
        Me.simulatorMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.simulatorMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DebugModeToolStripMenuItem, Me.separatorLine, Me.SettingsToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.simulatorMenu.Image = CType(resources.GetObject("simulatorMenu.Image"), System.Drawing.Image)
        Me.simulatorMenu.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.simulatorMenu.Name = "simulatorMenu"
        Me.simulatorMenu.Size = New System.Drawing.Size(74, 22)
        Me.simulatorMenu.Text = "Simulator"
        '
        'DebugModeToolStripMenuItem
        '
        Me.DebugModeToolStripMenuItem.Image = Global.robotBox.My.Resources.Resources.bug
        Me.DebugModeToolStripMenuItem.Name = "DebugModeToolStripMenuItem"
        Me.DebugModeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.DebugModeToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.DebugModeToolStripMenuItem.Text = "Toggle debug mode"
        '
        'separatorLine
        '
        Me.separatorLine.Name = "separatorLine"
        Me.separatorLine.Size = New System.Drawing.Size(197, 6)
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Image = Global.robotBox.My.Resources.Resources.settings
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Image = Global.robotBox.My.Resources.Resources.account
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'portLabel
        '
        Me.portLabel.Name = "portLabel"
        Me.portLabel.Size = New System.Drawing.Size(29, 22)
        Me.portLabel.Text = "Port"
        '
        'portSelector
        '
        Me.portSelector.Name = "portSelector"
        Me.portSelector.Size = New System.Drawing.Size(100, 25)
        Me.portSelector.Text = "15000"
        '
        'simStartStopButton
        '
        Me.simStartStopButton.BackColor = System.Drawing.Color.Green
        Me.simStartStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.simStartStopButton.ForeColor = System.Drawing.Color.White
        Me.simStartStopButton.Image = CType(resources.GetObject("simStartStopButton.Image"), System.Drawing.Image)
        Me.simStartStopButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.simStartStopButton.Name = "simStartStopButton"
        Me.simStartStopButton.Size = New System.Drawing.Size(35, 22)
        Me.simStartStopButton.Text = "Start"
        '
        'toolBar
        '
        Me.toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.toolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cursorTool, Me.moveTool, Me.rotateTool, Me.wallTool, Me.pathTool, Me.eraserTool})
        Me.toolBar.Location = New System.Drawing.Point(0, 25)
        Me.toolBar.Name = "toolBar"
        Me.toolBar.Size = New System.Drawing.Size(875, 33)
        Me.toolBar.TabIndex = 1
        '
        'cursorTool
        '
        Me.cursorTool.Checked = True
        Me.cursorTool.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cursorTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cursorTool.Image = Global.robotBox.My.Resources.Resources.cursor_default_outline
        Me.cursorTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cursorTool.Name = "cursorTool"
        Me.cursorTool.Padding = New System.Windows.Forms.Padding(5)
        Me.cursorTool.Size = New System.Drawing.Size(30, 30)
        Me.cursorTool.Text = "Cursor"
        '
        'moveTool
        '
        Me.moveTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.moveTool.Image = Global.robotBox.My.Resources.Resources.cursor_move
        Me.moveTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.moveTool.Name = "moveTool"
        Me.moveTool.Padding = New System.Windows.Forms.Padding(5)
        Me.moveTool.Size = New System.Drawing.Size(30, 30)
        Me.moveTool.Text = "Move"
        '
        'rotateTool
        '
        Me.rotateTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.rotateTool.Image = Global.robotBox.My.Resources.Resources.rotate_3d
        Me.rotateTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.rotateTool.Name = "rotateTool"
        Me.rotateTool.Padding = New System.Windows.Forms.Padding(5)
        Me.rotateTool.Size = New System.Drawing.Size(30, 30)
        Me.rotateTool.Text = "Rotate"
        '
        'wallTool
        '
        Me.wallTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.wallTool.Image = CType(resources.GetObject("wallTool.Image"), System.Drawing.Image)
        Me.wallTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.wallTool.Name = "wallTool"
        Me.wallTool.Padding = New System.Windows.Forms.Padding(5)
        Me.wallTool.Size = New System.Drawing.Size(30, 30)
        Me.wallTool.Text = "Draw wall"
        '
        'pathTool
        '
        Me.pathTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.pathTool.Image = Global.robotBox.My.Resources.Resources.road
        Me.pathTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.pathTool.Name = "pathTool"
        Me.pathTool.Padding = New System.Windows.Forms.Padding(5)
        Me.pathTool.Size = New System.Drawing.Size(30, 30)
        Me.pathTool.Text = "Draw path"
        '
        'eraserTool
        '
        Me.eraserTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.eraserTool.Image = Global.robotBox.My.Resources.Resources.eraser
        Me.eraserTool.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.eraserTool.Name = "eraserTool"
        Me.eraserTool.Padding = New System.Windows.Forms.Padding(5)
        Me.eraserTool.Size = New System.Drawing.Size(30, 30)
        Me.eraserTool.Text = "Eraser"
        '
        'saveMapDialog
        '
        Me.saveMapDialog.Filter = "ePuck simulator map files|*.si|All files|*.*"
        '
        'openMapDialog
        '
        Me.openMapDialog.Filter = "ePuck simulator map files|*.si|All files|*.*"
        '
        'canvas
        '
        Me.canvas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.canvas.BackColor = System.Drawing.Color.Black
        Me.canvas.Location = New System.Drawing.Point(0, 86)
        Me.canvas.Name = "canvas"
        Me.canvas.Size = New System.Drawing.Size(875, 473)
        Me.canvas.TabIndex = 2
        '
        'statusBar
        '
        Me.statusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusLabel})
        Me.statusBar.Location = New System.Drawing.Point(0, 562)
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Size = New System.Drawing.Size(875, 22)
        Me.statusBar.TabIndex = 3
        Me.statusBar.Text = "StatusStrip1"
        '
        'statusLabel
        '
        Me.statusLabel.Name = "statusLabel"
        Me.statusLabel.Size = New System.Drawing.Size(112, 17)
        Me.statusLabel.Text = "Loading simulator..."
        '
        'recordToolbar
        '
        Me.recordToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.recordButton, Me.exportButton, Me.exportProgressbar, Me.exportStatusLabel})
        Me.recordToolbar.Location = New System.Drawing.Point(0, 58)
        Me.recordToolbar.Name = "recordToolbar"
        Me.recordToolbar.Size = New System.Drawing.Size(875, 25)
        Me.recordToolbar.TabIndex = 4
        Me.recordToolbar.Text = "ToolStrip1"
        '
        'recordButton
        '
        Me.recordButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.recordButton.Image = Global.robotBox.My.Resources.Resources.record
        Me.recordButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.recordButton.Name = "recordButton"
        Me.recordButton.Size = New System.Drawing.Size(23, 22)
        Me.recordButton.Text = "Record"
        '
        'exportButton
        '
        Me.exportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.exportButton.Image = Global.robotBox.My.Resources.Resources.file_video
        Me.exportButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.exportButton.Name = "exportButton"
        Me.exportButton.Size = New System.Drawing.Size(23, 22)
        Me.exportButton.Text = "Export to video"
        '
        'exportProgressbar
        '
        Me.exportProgressbar.Name = "exportProgressbar"
        Me.exportProgressbar.Size = New System.Drawing.Size(100, 22)
        '
        'exportStatusLabel
        '
        Me.exportStatusLabel.Name = "exportStatusLabel"
        Me.exportStatusLabel.Size = New System.Drawing.Size(35, 22)
        Me.exportStatusLabel.Text = "Idle..."
        '
        'mainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 584)
        Me.Controls.Add(Me.recordToolbar)
        Me.Controls.Add(Me.statusBar)
        Me.Controls.Add(Me.canvas)
        Me.Controls.Add(Me.toolBar)
        Me.Controls.Add(Me.applicationBar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "mainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "robotBox"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.applicationBar.ResumeLayout(False)
        Me.applicationBar.PerformLayout()
        Me.toolBar.ResumeLayout(False)
        Me.toolBar.PerformLayout()
        Me.statusBar.ResumeLayout(False)
        Me.statusBar.PerformLayout()
        Me.recordToolbar.ResumeLayout(False)
        Me.recordToolbar.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents applicationBar As ToolStrip
    Friend WithEvents fileMenu As ToolStripSplitButton
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents portLabel As ToolStripLabel
    Friend WithEvents portSelector As ToolStripTextBox
    Friend WithEvents simStartStopButton As ToolStripButton
    Friend WithEvents toolBar As ToolStrip
    Friend WithEvents moveTool As ToolStripButton
    Friend WithEvents cursorTool As ToolStripButton
    Friend WithEvents rotateTool As ToolStripButton
    Friend WithEvents wallTool As ToolStripButton
    Friend WithEvents simulatorMenu As ToolStripSplitButton
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DebugModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents separatorLine As ToolStripSeparator
    Friend WithEvents saveMapDialog As SaveFileDialog
    Friend WithEvents openMapDialog As OpenFileDialog
    Friend WithEvents eraserTool As ToolStripButton
    Friend WithEvents canvas As Panel
    Friend WithEvents statusBar As StatusStrip
    Friend WithEvents statusLabel As ToolStripStatusLabel
    Friend WithEvents pathTool As ToolStripButton
    Friend WithEvents recordToolbar As ToolStrip
    Friend WithEvents recordButton As ToolStripButton
    Friend WithEvents exportButton As ToolStripButton
    Friend WithEvents exportProgressbar As ToolStripProgressBar
    Friend WithEvents exportStatusLabel As ToolStripLabel
End Class
