Public Class aboutForm
    Private Sub okButton_Click(sender As Object, e As EventArgs) Handles okButton.Click
        Me.Close()
    End Sub

    Private Sub aboutForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        versionLabel.Text = Simulator.name & " " & Simulator.version
    End Sub
End Class