Public Class settingsForm
    Private Sub maxFrameRateUpDown_ValueChanged(sender As Object, e As EventArgs) Handles maxFrameRateUpDown.ValueChanged
        minFrameRateUpDown.Maximum = maxFrameRateUpDown.Value
        setSetting("maxFrameRate", maxFrameRateUpDown.Value)
    End Sub

    Private Sub minFrameRateUpDown_ValueChanged(sender As Object, e As EventArgs) Handles minFrameRateUpDown.ValueChanged
        maxFrameRateUpDown.Minimum = minFrameRateUpDown.Value
        setSetting("minFrameRate", minFrameRateUpDown.Value)
    End Sub

    Private Sub frameRateTrackbar_Scroll(sender As Object, e As EventArgs) Handles frameRateTrackbar.Scroll
        frameRateUpDown.Value = frameRateTrackbar.Value
        setSetting("frameRate", frameRateTrackbar.Value)
    End Sub

    Private Sub frameRateUpDown_ValueChanged(sender As Object, e As EventArgs) Handles frameRateUpDown.ValueChanged
        frameRateTrackbar.Value = frameRateUpDown.Value
        setSetting("frameRate", frameRateUpDown.Value)
    End Sub

    Private Sub settingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set language
        settingsTabs.TabPages(0).Text = getLang("simulator")
        framerateGroup.Text = getLang("framerate")
        autoAdjustCheck.Text = getLang("aaframerate")
        maxFPSLabel.Text = getLang("maxframerate")
        minFPSLabel.Text = getLang("minframerate")
        fpsLabel.Text = getLang("framerate")

        miscGroup.Text = getLang("misc")
        defPortLabel.Text = getLang("defport")
        langLabel.Text = getLang("lang")
        translateLabel.Text = getLang("helptrans")
        gridLabel.Text = getLang("gridsize")

        robotGroup.Text = getLang("robot")
        startLocationLabel.Text = getLang("startloc")
        angleLabel.Text = getLang("angle")

        noteLabel.Text = getLang("note")
        yesButton.Text = getLang("accept")
        noButton.Text = getLang("cancel")

        ffmpegLabel.Text = getLang("ffmpeglocation")
        browseButton.Text = getLang("browse")

        ' Fill controls
        languageSelector.Items.AddRange(getAvailableLanguages())
        maxFrameRateUpDown.Value = getSetting("maxFrameRate")
        minFrameRateUpDown.Value = getSetting("minFrameRate")
        frameRateUpDown.Value = getSetting("frameRate")
        frameRateTrackbar.Value = getSetting("frameRate")
        autoAdjustCheck.Checked = getSetting("autoAdjustFrameRate")
        defaultPortTextBox.Text = getSetting("defaultPort")
        gridSizeUpDown.Value = getSetting("gridSize")
        startXTextBox.Text = getSetting("startX")
        startYTextBox.Text = getSetting("startY")
        angleUpDown.Value = getSetting("startAngle")
        languageSelector.SelectedItem = langName
        ffmpegLocationTextBox.Text = getSetting("ffmpegPath")
    End Sub

    Private Sub yesButton_Click(sender As Object, e As EventArgs) Handles yesButton.Click
        saveSettings()
        Close()
    End Sub

    Private Sub autoAdjustCheck_CheckedChanged(sender As Object, e As EventArgs) Handles autoAdjustCheck.CheckedChanged
        frameratePanel.Visible = Not autoAdjustCheck.Checked

        setSetting("autoAdjustFrameRate", autoAdjustCheck.Checked)
    End Sub

    Private Sub defaultPortTextBox_TextChanged(sender As Object, e As EventArgs) Handles defaultPortTextBox.TextChanged
        setSetting("defaultPort", defaultPortTextBox.Text)
    End Sub

    Private Sub languageSelector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles languageSelector.SelectedIndexChanged
        setSetting("language", getlangCode(languageSelector.SelectedItem))
    End Sub

    Private Sub gridSizeUpDown_ValueChanged(sender As Object, e As EventArgs) Handles gridSizeUpDown.ValueChanged
        setSetting("gridSize", gridSizeUpDown.Value)
    End Sub

    Private Sub startXTextBox_TextChanged(sender As Object, e As EventArgs) Handles startXTextBox.TextChanged
        setSetting("startX", startXTextBox.Text)
    End Sub

    Private Sub startYTextBox_TextChanged(sender As Object, e As EventArgs) Handles startYTextBox.TextChanged
        setSetting("startY", startYTextBox.Text)
    End Sub

    Private Sub angleUpDown_ValueChanged(sender As Object, e As EventArgs) Handles angleUpDown.ValueChanged
        setSetting("startAngle", angleUpDown.Value)
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles ffmpegLocationTextBox.Click
        ffmpegFileDialog.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles browseButton.Click
        ffmpegFileDialog.ShowDialog()
    End Sub

    Private Sub ffmpegFileDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ffmpegFileDialog.FileOk
        setSetting("ffmpegPath", ffmpegLocationTextBox.Text)
    End Sub
End Class