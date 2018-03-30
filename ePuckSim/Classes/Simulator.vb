Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text.Encoding
Imports ePuckSim.Map

Public Class Simulator

#Region "Data types"
    Public Enum Tool
        Cursor      ' Cursor tool. Does nothing
        Move        ' Move tool. Moves the robot
        Rotate      ' Roatate tool. Rotates the robot
        Wall        ' Line tool. Draws walls
        Eraser      ' Eraser tool. Erase walls
        Path        ' Path tool. Create paths
    End Enum

    Public Structure simulatorSettings
        Public ffmpegPath As String
        Public autoAdjustFrameRate As Boolean
        Public maxFrameRate As Integer
        Public minFrameRate As Integer
        Public frameRate As Integer
        Public defaultPort As UShort
        Public gridSize As Integer
        Public startPosition As Point
        Public startAngle As Single
    End Structure
#End Region

#Region "Class attributes"
#Region "Constants"
    Private Const ePuckSimName As String = "robotBox"       ' Product name
    Private Const ePuckSimBuild As String = "2017120700"    ' Build
    Private Const ePuckSimVersion As String = "Beta"        ' Version
    Private Const isDev As Boolean = False                  ' Is it a developer preview?
#End Region

#Region "Private"
    Private screensize As Size                              ' The screen size
    Private recorder As Recorder                            ' The recorder
    Private recording As Boolean                            ' Is the recorder recording?
    Private running As Boolean                              ' Is the simulator running?
    Private robotViewer As Boolean                          ' Is the robot viewer enabled?
    Private sw As Stopwatch                                 ' Frame rate control stopwatch
    Private drawing As Boolean                              ' Is the user drawing a wall?
    Private drawingPath As Boolean                          ' Is the user drawing a path?
    Private lineStart As Point                              ' The start point of the wall currently being drawn
    Private lineEnd As Point                                ' The end point of the wall currently being drawn
    Private pathPoints As List(Of PointF)                   ' Points of the path currently being drawn
    Private mouseLocation As Point                          ' Mouse location
    Private currentTool As Tool                             ' Currently selected tool
    Private serverThread As Thread                          ' The server thread
    Private udpListener As UdpClient                        ' The server
    Private canvas As Control                               ' The canvas where the simulation will be rendered
    Private gr As Graphics                                  ' Canvas graphics object
    Private renderTimer As System.Windows.Forms.Timer       ' Timer
    Private map As Map                                      ' The current map
    Private rate As Integer                                 ' Render frame rate
    Private minRate As Integer                              ' Minimum frame rate
    Private maxRate As Integer                              ' Maximum frame rate
    Private isDebug As Boolean                              ' Is debug mode enabled?
    Private debugFont As Font                               ' Font used to show debug info
    Private _grid As Bitmap                                 ' Grid bitmap, to avoid redrawing it every iteration
    Private _gridSize As Integer                            ' Grid square size
    Private _cursor As Bitmap                               ' The cursor
    Private frame As Bitmap                                 ' The frame with grid, cursor, paths and walls being drawn...
    Private scene As Bitmap                                 ' The scene image
    Private recordFrame As Bitmap                           ' The frame to be sent with the recorder
    Private frameGraphics As Graphics                       ' The frame graphics object
    Private recordGraphics As Graphics                      ' The recordFrame graphics object
    Private sceneGraphics As Graphics                       ' The scene graphics object
#End Region

#Region "Public"
    Public autoAdjustFrameRate As Boolean                   ' Is the framerate auto-adjuested depending on the performance?
#End Region
#End Region

#Region "Events"
#Region "Public"
    Public Event exportFinished()
    Public Event stopped()                                  ' Simulation stopped
    Public Event started()                                  ' Simulation started
    Public Event tick(info As String)                       ' Timer tick elapsed
    Public Event log(message As String)                     ' Message logged
#End Region
#End Region

#Region "Getters/Setters"
#Region "Public"

    Public ReadOnly Property isRecording As Boolean
        Get
            Return recording
        End Get
    End Property

    ''' <summary>
    ''' Product name
    ''' </summary>
    ''' <returns>String containing the name</returns>
    Public Shared ReadOnly Property name As String
        Get
            Return ePuckSimName
        End Get
    End Property

    ''' <summary>
    ''' Product version
    ''' </summary>
    ''' <returns>String containing the version and build</returns>
    Public Shared ReadOnly Property version As String
        Get
            Return ePuckSimVersion & " (Build " & ePuckSimBuild & ")"
        End Get
    End Property
#End Region
#End Region

#Region "Methods"
#Region "Public"
    ''' <summary>
    ''' Constructor. Creates a new simulator
    ''' </summary>
    Public Sub New(settings As simulatorSettings)

        ' Set values
        screensize = Screen.PrimaryScreen.Bounds.Size

        ' Instantiate objects
        recorder = New Recorder(settings.ffmpegPath)
        debugFont = New Font("Courier New", 12)
        map = New Map(settings.startPosition, settings.startAngle)
        renderTimer = New System.Windows.Forms.Timer

        frame = New Bitmap(screensize.Width, screensize.Height)
        scene = New Bitmap(screensize.Width, screensize.Height)
        recordFrame = New Bitmap(screensize.Width, screensize.Height)

        frameGraphics = Graphics.FromImage(frame)
        sceneGraphics = Graphics.FromImage(scene)
        recordGraphics = Graphics.FromImage(recordFrame)

        frameGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        sceneGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        recordGraphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' Loading...
        RaiseEvent log(ePuckSimName & " " & version)
        RaiseEvent log("(C) 2017 BreachLabs - Some rights reserved")
        RaiseEvent log("Starting simulator...")

        ' Set default values
        recording = False
        running = False
        isDebug = False
        robotViewer = False
        minRate = settings.minFrameRate
        maxRate = settings.maxFrameRate
        If settings.autoAdjustFrameRate Then
            rate = settings.maxFrameRate
        Else
            rate = settings.frameRate
        End If
        renderTimer.Interval = 1000 / rate
        autoAdjustFrameRate = settings.autoAdjustFrameRate
        _cursor = My.Resources.cursor_default_outline
        currentTool = Tool.Cursor



        ' Handlers
        AddHandler renderTimer.Tick, AddressOf timerTick
        AddHandler recorder.exportFinished, AddressOf finishExport

        ' Start rendering
        'log("Maximum frame rate is " & rate & "FPS. Go to the settings menu to change it")
        RaiseEvent log("Simulator started. Create or load an environment and start the simulation.")
        Physics.setMap(map)
        Physics.start()
        renderTimer.Start()
    End Sub

    Public Sub startRecording()
        recording = True
    End Sub

    Public Sub pauseRecording()
        recording = False
    End Sub

    Public Sub resumeRecording()
        startRecording()
    End Sub

    Public Sub stopRecording()
        pauseRecording()
        recorder.reset()
    End Sub

    Public Sub exportRecording()
        If Not recording Then
            recorder.export(Application.StartupPath & "\" & Date.Now.ToString.Replace("/", "-").Replace(" ", "_").Replace(":", ".") & ".mp4")
        End If
    End Sub

    ''' <summary>
    ''' Resets the simulator to initial conditions
    ''' </summary>
    Public Sub reset()
        running = False
        [stop]()
        map.reset()
    End Sub

    ''' <summary>
    ''' Sets the canvas
    ''' </summary>
    ''' <param name="c">The control where the images will be rendered</param>
    Public Sub setCanvas(c As Control)
        canvas = c
        gr = canvas.CreateGraphics()
        gr.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
    End Sub

    'Public Sub resizeCanvas()
    '    If _gridSize <> 0 Then
    '        setGrid(_gridSize)
    '    End If
    'End Sub

    ''' <summary>
    ''' Allows keyboard interaction
    ''' </summary>
    ''' <param name="key">The pressed key</param>
    Public Sub keyboardInteract(key As Keys)
        Select Case key
            Case Keys.F3
                isDebug = Not isDebug
                If isDebug Then
                    RaiseEvent log("Debug mode enabled")
                Else
                    RaiseEvent log("Debug mode disabled")
                End If
            Case Keys.F1
                robotViewer = Not robotViewer

        End Select
    End Sub

    ''' <summary>
    ''' Allows mouse interaction
    ''' </summary>
    ''' <param name="location">The mouse location</param>
    ''' <param name="delta">The wheel scroll</param>
    ''' <param name="button">The pressed button</param>
    ''' <param name="tool">The selected tool</param>
    Public Sub mouseInteract(location As Point, delta As Integer, button As MouseButtons, tool As Tool)

        currentTool = tool
        mouseLocation = location

        If tool = Tool.Cursor Then
            _cursor = My.Resources.cursor_default_outline

        Else
            _cursor = My.Resources.cancel

            'If cursor moved over robot...
            If (location.X - map.robot.location.X) ^ 2 + (location.Y - map.robot.location.Y) ^ 2 < Robot.radius ^ 2 Then

                Select Case tool
                    Case Tool.Move
                        _cursor = My.Resources.cursor_move
                        drawing = False
                        drawingPath = False
                        'Drag drop robot
                        If button = MouseButtons.Left Then
                            map.robot.setLocation(location)
                        End If

                    Case Tool.Rotate
                        _cursor = My.Resources.rotate_3d
                        drawing = False
                        drawingPath = False
                        'Drag drop robot
                        If button = MouseButtons.Left Or delta > 0 Then
                            map.robot.rotate(0.1)
                        ElseIf button = MouseButtons.Right Or delta < 0 Then
                            map.robot.rotate(-0.1)
                        End If

                End Select
            Else
                Select Case tool
                    Case Tool.Wall
                        _cursor = My.Resources.cross_wall
                        drawingPath = False
                        If button = MouseButtons.Left Then
                            ' Draw line
                            If drawing Then
                                lineEnd = location
                                map.addWall(lineStart, lineEnd)
                            Else
                                lineStart = location
                            End If
                            drawing = Not drawing
                        ElseIf button = MouseButtons.Right Then
                            ' Cancel line
                            drawing = False
                        End If

                    Case Tool.Path
                        _cursor = My.Resources.cross_road
                        drawing = False

                        If drawingPath Then
                            If button = MouseButtons.Left Then
                                ' Add new point
                                pathPoints.Add(location)
                                lineStart = location
                            ElseIf button = MouseButtons.Right Then
                                ' Stop drawing path
                                If pathPoints.Count > 1 Then
                                    map.addPath(pathPoints.ToArray)
                                End If
                                drawingPath = False
                            End If
                        Else
                            ' Start drawing path
                            If button = MouseButtons.Left Then
                                pathPoints = New List(Of PointF)
                                pathPoints.Add(location)
                                lineStart = location
                                drawingPath = True
                            End If
                        End If

                    Case Tool.Eraser
                        _cursor = My.Resources.eraser
                        drawing = False
                        drawingPath = False
                        If button = MouseButtons.Left Then
                            ' Find the wall and remove it
                            map.removeWall(map.wallAt(location))

                            ' Find path point and remove it
                            map.removePathPoint(map.pathPointAt(location))
                        End If

                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Starts the simulator
    ''' </summary>
    ''' <param name="port">The port where the simuator will be listening</param>
    Public Sub start(port As Integer)
        running = True
        udpListener = New UdpClient(port)
        serverThread = New Thread(AddressOf serverLoop)
        serverThread.Start()
        RaiseEvent started()
        RaiseEvent log("Server started. Waiting for connection...")
    End Sub

    ''' <summary>
    ''' Stop the simulator
    ''' </summary>
    Public Sub [stop]()
        running = False
        udpListener.Close()
        serverThread.Abort()
        RaiseEvent stopped()
        RaiseEvent log("Server stopped.")
    End Sub

    Public Sub setMap(m As Map)
        map = m
        Physics.setMap(m)
    End Sub

    Public Function getMap() As Map
        Return map
    End Function

    ''' <summary>
    ''' Set grid size
    ''' </summary>
    ''' <param name="sz">The grid size</param>
    Public Sub setGrid(sz As Integer)

        _gridSize = sz

        'If Not canvas.Size = Size.Empty Then
        _grid = New Bitmap(screensize.Width, screensize.Height)
        Dim gridGraphics As Graphics = Graphics.FromImage(_grid)

        ' Draw grid
        Dim pn As New Pen(Color.LightGray)
        pn.DashStyle = Drawing2D.DashStyle.DashDot
        For i = 0 To screensize.Width - 1
            gridGraphics.DrawLine(pn, _gridSize * i, 0, _gridSize * i, screensize.Height)
        Next

        For i = 0 To screensize.Height - 1
            gridGraphics.DrawLine(pn, 0, _gridSize * i, screensize.Width, _gridSize * i)
        Next

        gridGraphics.Dispose()
        'End If
    End Sub

#End Region

#Region "Private"

    Private Sub finishExport()
        RaiseEvent exportFinished()
    End Sub

    Private Function getPen(t As Tool, Optional drawing As Boolean = False) As Pen
        Select Case t
            Case Tool.Wall
                If drawing Then
                    Return New Pen(Color.FromArgb(127, Color.Gray), 3)
                Else
                    Return New Pen(Color.Gray, 3)
                End If

            Case Tool.Path
                If drawing Then
                    Return New Pen(Color.FromArgb(127, Color.Black), 20)
                Else
                    Return New Pen(Color.Black, 20)
                End If
        End Select
        Return Pens.Black
    End Function

    ''' <summary>
    ''' The timer tick handler
    ''' </summary>
    Private Sub timerTick()
        If canvas Is Nothing Then
            RaiseEvent log("Error: Canvas not found")
            renderTimer.Stop()
        Else
            'Start stopwatch
            sw = New Stopwatch
            sw.Start()

            Dim robotImg As Bitmap = map.robot.getImage

            ' Clear background
            frameGraphics.Clear(Color.White)
            sceneGraphics.Clear(Color.Transparent)  ' ??

            ' Draw grid
            If _grid IsNot Nothing Then
                frameGraphics.DrawImage(_grid, 0, 0)
            End If

            For Each path As PointF() In map.paths
                sceneGraphics.DrawCurve(getPen(Tool.Path), path)
                'frameGraphics.DrawCurve(New Pen(Color.White, 15), path)
            Next

            For Each line As Line In map.walls
                sceneGraphics.DrawLine(getPen(Tool.Wall), line.startPoint, line.endPoint)
            Next

            'Draw robot
            sceneGraphics.DrawImage(robotImg, map.robot.imageLocation.X, map.robot.imageLocation.Y, 2 * Robot.radius, 2 * Robot.radius)

            ' Print developer version
            If isDev Then
                sceneGraphics.DrawString("DEV " & ePuckSimBuild, New Font("Arial Black", 22), Brushes.Black, screensize.Width - 280, 5)
            End If

            frameGraphics.DrawImage(scene, 0, 0)

            ' Draw paths
            If drawingPath Then
                If pathPoints.Count > 1 Then
                    Dim points As PointF() = pathPoints.Union(New PointF() {mouseLocation}).ToArray
                    frameGraphics.DrawCurve(getPen(Tool.Path, True), points)
                Else
                    frameGraphics.DrawLine(getPen(Tool.Path, True), lineStart, mouseLocation)
                End If
            End If

            ' Draw walls 
            If drawing Then
                frameGraphics.DrawLine(getPen(Tool.Wall, True), lineStart, mouseLocation)
            End If

            'Draw debug info
            If isDebug Then

                Dim sensorCenter As Point
                Dim sensorRay As Point
                Dim intersectionPoints As Point() = Physics.sensorIntersectionPoints
                Dim collisionPoints As Point() = Physics.collisionPoints

                ' Draw sensor rays
                For i = 0 To Robot.sensorCount - 1
                    sensorRay = map.robot.getSensorRay(i)
                    frameGraphics.DrawLine(Pens.Red, map.robot.location, sensorRay)
                    frameGraphics.DrawString(i, debugFont, Brushes.Red, sensorRay)
                Next

                ' Draw floor sensors
                For i = 0 To 2
                    sensorCenter = map.robot.getGroundSensor(i)
                    frameGraphics.DrawEllipse(Pens.Red, circle(sensorCenter, Robot.groundSensorSize))
                Next

                'Draw robot path
                If map.robot.path.Count > 1 Then
                    frameGraphics.DrawCurve(Pens.Green, map.robot.path)
                End If

                'Draw direction vector
                Dim vector As Vector = Robot.radius * map.robot.directionVector.unitary
                Dim vectorArrow As PointF = New PointF(map.robot.location.X + vector.x, map.robot.location.Y + vector.y)
                frameGraphics.DrawLine(Pens.Green, map.robot.location, vectorArrow)

                'Draw collision lines
                frameGraphics.DrawEllipse(Pens.Blue, New Rectangle(map.robot.imageLocation.X, map.robot.imageLocation.Y, 2 * Robot.radius, 2 * Robot.radius))

                ' Draw intersection points (if any)
                If intersectionPoints.Count > 0 Then
                    For Each p As Point In intersectionPoints
                        frameGraphics.DrawString(map.robot.distance(p), debugFont, Brushes.Black, p)
                        frameGraphics.FillEllipse(Brushes.BlueViolet, New Rectangle(p.X - 2, p.Y - 2, 5, 5))
                    Next
                End If

                ' Draw collision points (if any)
                If collisionPoints.Count > 0 Then
                    For Each p As Point In collisionPoints
                        frameGraphics.DrawString(map.robot.distance(p), debugFont, Brushes.Black, p)
                        frameGraphics.FillEllipse(Brushes.Yellow, New Rectangle(p.X - 2, p.Y - 2, 5, 5))
                    Next
                End If

                'Draw version info, framerate...
                frameGraphics.DrawString(version & vbNewLine &
                                         "frameRate: " & rate & "FPS" & vbNewLine &
                                         "memoryUsage: " & getMemoryUsage() & vbNewLine &
                                         "deltaT: " & Physics.deltaT & "s" & vbNewLine &
                                         "leftSpeed: " & map.robot.leftWheelSpeed & vbNewLine &
                                         "rightSpeed: " & map.robot.rightWheelSpeed & vbNewLine &
                                         "proximitySensors: (" & String.Join(", ", map.robot.getProximity) & ")" & vbNewLine &
                                         "floorSensors: (" & String.Join(", ", map.robot.getFloorSensors) & ")" & vbNewLine &
                                         "direction: " & map.robot.directionVector.ToString & vbNewLine,
                                         debugFont, Brushes.Black, 0, 0)
            End If

            ' Draw robot viewer
            If robotViewer Then
                frameGraphics.DrawImage(robotImg, screensize.Width - robotImg.Width, 0)
            End If

            ' Draw cursor
            frameGraphics.DrawImage(_cursor, mouseLocation.X - _cursor.Width \ 2, mouseLocation.Y - _cursor.Height \ 2)

            'Draw frame
            gr.DrawImage(frame, 0, 0)

            ' Stop stopwatch and adjust rate
            sw.Stop()
            If autoAdjustFrameRate Then
                If sw.ElapsedMilliseconds > 100 Then
                    ' Decrease rate
                    rate -= 1
                    If rate < minRate Then
                        rate = minRate
                    End If
                Else
                    ' Increase rate
                    rate += 1
                    If rate > maxRate Then
                        rate = maxRate
                    End If
                End If
                renderTimer.Interval = 1000 / rate
            End If

            If recording Then
                'Dim recordFrame As New Bitmap(screensize.Width, screensize.Height)
                'Dim recordGraphics As Graphics = Graphics.FromImage(recordFrame)
                recordGraphics.Clear(Color.PapayaWhip)
                recordGraphics.DrawImage(scene, 0, 0)
                recorder.addFrame(recordFrame)
            End If

            RaiseEvent tick("frameRate: " & rate & "FPS; robotLocation: (" & map.robot.location.X & ", " & map.robot.location.Y & "); orientation: " & map.robot.angle & "rad/" & Physics.rad2deg(map.robot.angle) & "deg; cursorLocation: (" & mouseLocation.X & ", " & mouseLocation.Y & "); selectedTool: " & currentTool.ToString)
        End If
    End Sub

    Private Function getMemoryUsage() As String
        Dim currentProcess As Process = Process.GetCurrentProcess()
        Dim mem As Long = currentProcess.WorkingSet64
        If mem > 1024 Then
            mem = mem / 1024
            If mem > 1024 Then
                mem = mem / 1024
                Return mem & "MBytes"
            Else
                Return mem & "KBytes"
            End If
        Else
            Return mem & "Bytes"
        End If
    End Function

    ''' <summary>
    ''' The server thread handler
    ''' </summary>
    Private Sub serverLoop()
        Dim ep As New IPEndPoint(IPAddress.Any, 0)
        Dim bytes() As Byte
        Dim str As String
        Dim messages() As String
        Dim messageParts() As String
        Dim response As String
        While running
            bytes = udpListener.Receive(ep)
            response = ""
            str = ASCII.GetString(bytes)
            messages = str.Split(New Char() {vbLf})
            For Each message As String In messages
                messageParts = message.Split(",")
                Select Case messageParts(0)
' MISC
                    Case ""     ' Null message
                        ' Ignore the message
                    Case "R"    ' Reset robot
                        response &= "z,Command not found\r\nz,Command not found\r\n"
                    Case "S"    ' Stop the robot
                        map.robot.stop()
                        response &= "s\r\n"

                    Case "v"    ' Get sercom version (epuck firmware version)
                        ' Ignore the message
' GET
                    Case "a"    ' Get nonfiltered accelerometer
                        ' Ignore the message

                    Case "A"    ' Get filtered accelerometer
                        ' Ignore the message
                    Case "N"    ' Get proximity sensors
                        response = String.Join(",", map.robot.getProximity()) & "\r\n"

                    Case "M"    ' Get floor sensors
                        response = String.Join(",", map.robot.getFloorSensors()) & "\r\n"

                    Case "Q"    ' Get motor positions
                        ' Ignore the message
                        response = String.Join(",", map.robot.getMotorPositions()) & "\r\n"

                    Case "O"    ' Get light sensors
                        ' Ignore the message

                    Case "u"    ' Get micorophones
                        ' Ignore the message

                    Case "E"    ' Get motor speeds
                        response = String.Join(",", map.robot.getMotorSpeeds()) & "\r\n"

                    Case "I"    ' Get camera
                        ' Ignore the message
' SET
                    Case "D"    ' Set motors speed
                        map.robot.setMotorsSpeed(messageParts(1), messageParts(2))

                    Case "P"    ' Set motors position
                        map.robot.setMotorPosition(messageParts(1), messageParts(2))

                    Case "L"    ' Set led
                        map.robot.setLed(messageParts(1), messageParts(2))

                    Case "T"    ' Set sound
                        map.robot.setSound(messageParts(1))

                    Case "J"    ' Set camera parameters
                        ' Ignore the message

                    Case Else
                        response &= "z,Command not found\r\n"
                End Select
            Next
            response = response.Replace("\r\n", vbCr & vbLf)
            bytes = ASCII.GetBytes(response)

            udpListener.Send(bytes, bytes.Count, ep)

            'Delay
            Thread.Sleep(30)
        End While
    End Sub

    Private Function circle(center As Point, radius As Double) As Rectangle
        Return New Rectangle(center.X - radius, center.Y - radius, 2 * radius, 2 * radius)
    End Function
#End Region
#End Region
End Class