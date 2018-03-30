Imports System.IO
Imports System.Threading

Public Class Recorder
    Private ffmpegPath As String
    Private tempPath As String
    Private frameCount As Integer
    Private ffmpeg As Process
    Private backThread As Thread

    Public Event exportFinished()


    Public Sub New(ffmpeg As String)
        ffmpegPath = ffmpeg
        tempPath = Application.StartupPath & "\temp\"
        frameCount = 0

        Dim dir As New DirectoryInfo(tempPath)
        dir.Create()
        dir.Attributes = FileAttributes.Hidden
    End Sub

    Public Sub reset()
        For i = 0 To frameCount - 1
            File.Delete(tempPath & "frame" & i & ".jpg")
        Next
        frameCount = 0
    End Sub

    Public Sub addFrame(frame As Bitmap)
        frame.Save(tempPath & "frame" & frameCount & ".jpg", Imaging.ImageFormat.Jpeg)
        frameCount += 1
    End Sub

    Public Sub export(filename As String)

        ' Add credits frames
        Dim screenSize As Size = Screen.PrimaryScreen.Bounds.Size
        Dim logoSize As Size = My.Resources.logo_text.Size
        Dim creditFrame As New Bitmap(screenSize.Width, screenSize.Height)
        Dim gr As Graphics = Graphics.FromImage(creditFrame)

        gr.Clear(Color.AliceBlue)
        gr.DrawImage(My.Resources.logo_text, (screenSize.Width - logoSize.Width) \ 2, (screenSize.Height - logoSize.Height) \ 2)

        For i = 1 To 20
            addFrame(creditFrame)
        Next

        backThread = New Thread(AddressOf backgroundConverter)

        ffmpeg = Process.Start(ffmpegPath)
        ffmpeg.StartInfo.FileName = ffmpegPath
        ffmpeg.StartInfo.Arguments = "-i " & tempPath & "frame%d.jpg " & filename
        ffmpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        ffmpeg.StartInfo.UseShellExecute = False
        ffmpeg.StartInfo.RedirectStandardOutput = False
        ffmpeg.StartInfo.CreateNoWindow = True

        backThread.Start()
    End Sub

    Private Sub backgroundConverter()
        ffmpeg.Start()
        ffmpeg.WaitForExit()

        ' Delete all files
        reset()

        RaiseEvent exportFinished()
    End Sub
End Class
