Imports robotBox.Simulator

Public Class mainForm

#Region "Arritbutes"
    Private sim As Simulator
    Private started, hasRecorded As Boolean
    Private selectedTool As Tool
#End Region

#Region "Event handlers"
    Private Sub mainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load settings
        Dim settings As New simulatorSettings
        loadSettings()
        settings.ffmpegPath = getSetting("ffmpegPath")
        settings.autoAdjustFrameRate = getSetting("autoAdjustFrameRate")
        settings.defaultPort = getSetting("defaultPort")
        settings.frameRate = getSetting("frameRate")
        settings.gridSize = getSetting("gridSize")
        settings.maxFrameRate = getSetting("maxFrameRate")
        settings.minFrameRate = getSetting("minFrameRate")
        settings.startAngle = getSetting("startAngle")
        settings.startPosition = New Point(getSetting("startX"), getSetting("startY"))

        ' Load language
        fileMenu.Text = getLang("file")
        NewToolStripMenuItem.Text = getLang("new")
        SaveToolStripMenuItem.Text = getLang("save")
        OpenToolStripMenuItem.Text = getLang("open")

        simulatorMenu.Text = getLang("simulator")
        DebugModeToolStripMenuItem.Text = getLang("toggledebug")
        SettingsToolStripMenuItem.Text = getLang("settings")
        AboutToolStripMenuItem.Text = getLang("about")

        portLabel.Text = getLang("port")
        simStartStopButton.Text = getLang("start")

        cursorTool.Text = getLang("cursor")
        moveTool.Text = getLang("move")
        rotateTool.Text = getLang("rotate")
        wallTool.Text = getLang("wall")
        eraserTool.Text = getLang("eraser")
        pathTool.Text = getLang("path")

        recordButton.Text = getLang("record")
        exportButton.Text = getLang("export")
        exportStatusLabel.Text = getLang("exportmessage")

        ' Set values
        portSelector.Text = settings.defaultPort

        ' Initialize
        canvas.Select()
        started = False
        hasRecorded = False
        exportButton.Enabled = False
        Text = Simulator.name
        sim = New Simulator(settings)

        AddHandler sim.tick, AddressOf updateStatus
        AddHandler sim.stopped, AddressOf serverStopped
        AddHandler sim.exportFinished, AddressOf exportFinished

        sim.setCanvas(canvas)
        sim.setGrid(getSetting("gridSize"))
    End Sub

    Private Sub simStartStopButton_Click(sender As Object, e As EventArgs) Handles simStartStopButton.Click
        If started Then
            sim.stop()
            simStartStopButton.BackColor = Color.Green
            simStartStopButton.Text = getLang("start")
        Else
            sim.start(portSelector.Text)
            simStartStopButton.BackColor = Color.Red
            simStartStopButton.Text = getLang("stop")
        End If
        started = Not started
    End Sub

    Private Sub mainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        ' Stop the robot
        If started Then
            sim.stop()
        End If

        ' Delete temporary files
        If hasRecorded Or sim.isRecording Then
            sim.stopRecording()
        End If
    End Sub

    Private Sub moveTool_Click(sender As Object, e As EventArgs) Handles moveTool.Click
        selectedTool = Tool.Move
        resetToolBar()
        moveTool.Checked = True
    End Sub

    Private Sub canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles canvas.MouseMove
        'Send mouse location to the controller
        sim.mouseInteract(e.Location, e.Delta, e.Button, selectedTool)
    End Sub

    Private Sub canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles canvas.MouseDown
        'Send canvas click to controller and get response
        sim.mouseInteract(e.Location, e.Delta, e.Button, selectedTool)
    End Sub

    Private Sub cursorTool_Click(sender As Object, e As EventArgs) Handles cursorTool.Click
        selectedTool = Tool.Cursor
        resetToolBar()
        cursorTool.Checked = True
    End Sub

    Private Sub rotateTool_Click(sender As Object, e As EventArgs) Handles rotateTool.Click
        selectedTool = Tool.Rotate
        resetToolBar()
        rotateTool.Checked = True
    End Sub

    Private Sub canvas_MouseWheel(sender As Object, e As MouseEventArgs) Handles canvas.MouseWheel
        'Send mouse weel click to controller and get response
        sim.mouseInteract(e.Location, e.Delta, e.Button, selectedTool)
    End Sub

    Private Sub lineTool_Click(sender As Object, e As EventArgs) Handles wallTool.Click
        selectedTool = Tool.Wall
        resetToolBar()
        wallTool.Checked = True
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        sim.reset()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        aboutForm.ShowDialog()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        settingsForm.ShowDialog()
    End Sub

    Private Sub DebugModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugModeToolStripMenuItem.Click
        sim.keyboardInteract(Keys.F3)
    End Sub

    Private Sub ToggleRobotViewerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        sim.keyboardInteract(Keys.F1)
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        saveMapDialog.ShowDialog()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        openMapDialog.ShowDialog()
    End Sub

    Private Sub saveMapDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles saveMapDialog.FileOk
        sim.getMap.save(saveMapDialog.FileName)
    End Sub

    Private Sub openMapDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles openMapDialog.FileOk
        sim.setMap(New Map(openMapDialog.FileName))
    End Sub

    Private Sub eraserTool_Click(sender As Object, e As EventArgs) Handles eraserTool.Click
        selectedTool = Tool.Eraser
        resetToolBar()
        eraserTool.Checked = True
    End Sub

    Private Sub pathTool_Click(sender As Object, e As EventArgs) Handles pathTool.Click
        selectedTool = Tool.Path
        resetToolBar()
        pathTool.Checked = True
    End Sub

    Private Sub canvas_SizeChanged(sender As Object, e As EventArgs) Handles canvas.SizeChanged
        ' Set canvas with new size
        If sim IsNot Nothing Then
            sim.setCanvas(canvas)
            sim.setGrid(getSetting("gridSize"))
        End If
    End Sub

    Private Sub canvas_MouseLeave(sender As Object, e As EventArgs) Handles canvas.MouseLeave
        Cursor.Show()
    End Sub

    Private Sub canvas_MouseEnter(sender As Object, e As EventArgs) Handles canvas.MouseEnter
        Cursor.Hide()
    End Sub

    Private Sub recordButton_Click(sender As Object, e As EventArgs) Handles recordButton.Click
        If sim.isRecording Then
            recordButton.Image = My.Resources.record
            recordButton.Text = getLang("record")
            sim.pauseRecording()
            exportButton.Enabled = True
        Else
            If hasRecorded Then
                Dim response As MsgBoxResult = MsgBox(getLang("overwritemsg"), MsgBoxStyle.YesNo, getLang("overwritetitle"))
                If response = MsgBoxResult.Yes Then
                    sim.stopRecording() ' Reset recorder
                    record()
                End If
            Else
                record()
            End If
        End If
    End Sub

    Private Sub exportButton_Click(sender As Object, e As EventArgs) Handles exportButton.Click
        If Not sim.isRecording Then
            exportStatusLabel.Text = getLang("exportingvideo")

            ' Disable all controls
            recordButton.Enabled = False
            exportButton.Enabled = False

            ' Set progressbar value
            exportProgressbar.Style = ProgressBarStyle.Marquee

            sim.exportRecording()
            hasRecorded = False ' File exported
        End If
    End Sub
#End Region

    Private Sub updateStatus(message As String)
        statusLabel.Text = message
    End Sub

    Private Sub resetToolBar()
        For Each e As ToolStripButton In toolBar.Items
            e.Checked = False
        Next
    End Sub

    Delegate Sub serverStoppedDelegate()
    Private Sub serverStopped()
        If applicationBar.InvokeRequired Then
            Dim d As New serverStoppedDelegate(AddressOf serverStopped)
            applicationBar.Invoke(d)
        Else
            Dim i As ToolStripButton = applicationBar.Items(4)
            i.BackColor = Color.Green
            i.Text = "Start"
        End If
    End Sub

    Private Sub record()
        recordButton.Image = My.Resources.record_rec
        recordButton.Text = getLang("stoprecording")
        sim.startRecording()
        hasRecorded = True
    End Sub

    Delegate Sub exportFinishedDelegate()
    Private Sub exportFinished()
        If recordToolbar.InvokeRequired Then
            Dim d As New exportFinishedDelegate(AddressOf exportFinished)
            recordToolbar.Invoke(d)
        Else
            exportProgressbar.Style = ProgressBarStyle.Continuous

            recordButton.Enabled = True
            exportButton.Enabled = False
            exportStatusLabel.Text = getLang("videoexported")
        End If
    End Sub
End Class
