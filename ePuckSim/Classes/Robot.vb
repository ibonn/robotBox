Imports System.Math

Public Class Robot
#Region "Constants"
    Private Const TAU = 2 * PI                              ' Tau constant = 2 * PI
    Public Const radius As Integer = 37                     ' Robot radius in millimetres (1px = 1mm -> radius = 37mm)
    Public Const wheelRadius As Integer = 20                ' Wheel radius in millimetres (1px = 1mm -> radius = 40mm ~ 41mm)
    Public Const groundSensorSize As Single = 1.5           ' Ground sensor size (1px = 1mm -> size = 3 mm)
    Private Const sensorRange As Integer = 45               ' Proximity sensor range (1px = 1mm -> range = 45mm)
    Public Const sensorCount As Integer = 8                 ' Number of sensors
#End Region
#Region "Attributes"
#Region "Private"
    Private _location As PointF                             ' Robot location
    Private leds(7) As Boolean                              ' The status of the leds
    Private ledLocations(7) As Point                        ' Led locations
    Private proximitySensors(sensorCount - 1) As Short      ' Proximity sensor measurements
    Private proximitySensorRays(sensorCount - 1) As Vector  ' Proximity sensor ray vectors
    Private proxRayLengths(sensorCount - 1) As Double       ' Sensor ray lengths
    Private speeds(1) As Short                              ' Wheel speeds
    Private locations(1) As Integer                         ' Wheel locations
    Private _angle As Single                                ' Spin angle
    Private dirVector As Vector                             ' Direction vector
    Private _path As List(Of PointF)                        ' Robot path
    Private _startPosition As Point                         ' Start position
    Private _startAngle As Single                           ' Start angle
    Private groundSensors(2) As Short                       ' Ground sensors
    Private groundSensorLocations(2) As Point               ' Ground sensor locations

    Private ledSize As Size                                 ' Led size (Diameter)
#End Region
#End Region
#Region "Getters/setters"
#Region "Public"

    Public Property leftWheelLocation As Double
        Get
            Return locations(0)
        End Get
        Set(value As Double)
            locations(0) = value
        End Set
    End Property

    Public Property rightWheelLocation As Double
        Get
            Return locations(1)
        End Get
        Set(value As Double)
            locations(1) = value
        End Set
    End Property

    Public Property leftWheelSpeed As Double
        Get
            Return speeds(0)
        End Get
        Set(value As Double)
            speeds(0) = value
        End Set
    End Property

    Public Property rightWheelSpeed As Double
        Get
            Return speeds(1)
        End Get
        Set(value As Double)
            speeds(1) = value
        End Set
    End Property

    Public ReadOnly Property imageLocation As Point
        Get
            Return New Point(_location.X - radius, _location.Y - radius)
        End Get
    End Property

    Public ReadOnly Property location As PointF
        Get
            Return _location
        End Get
    End Property

    Public ReadOnly Property locationInt As Point
        Get
            Return New Point(_location.X, _location.Y)
        End Get
    End Property

    Public ReadOnly Property imageCenter As Point
        Get
            Return New Point(_location.X, _location.Y)
        End Get
    End Property

    Public ReadOnly Property directionVector As Vector
        Get
            Return dirVector
        End Get
    End Property

    Public ReadOnly Property path As PointF()
        Get
            Return _path.ToArray
        End Get
    End Property

    Public ReadOnly Property angle As Single
        Get
            ' Normalize angle
            Dim a As Single = _angle
            While a > TAU
                a -= TAU
            End While

            Return a
        End Get
    End Property
#End Region
#Region "Private"

#End Region
#End Region
#Region "Methods"
#Region "Public"

    Public Function getMotorPositions() As Integer()
        Return locations
    End Function

    ' Server/communication related functions
    Public Sub [stop]()
        speeds(0) = 0
        speeds(1) = 0
        For i = 0 To leds.Count - 1
            leds(i) = False
        Next
    End Sub

    Public Sub setSound(number As Integer)
        If number >= 1 And number <= 5 Then
            ' Select an audio esource from my.resources to play and play it
            Select Case number
                Case 1
                    'My.Computer.Audio.Play("", AudioPlayMode.BackgroundLoop)
                Case 2
                    'My.Computer.Audio.Play("", AudioPlayMode.BackgroundLoop)
                Case 3
                    'My.Computer.Audio.Play("", AudioPlayMode.BackgroundLoop)
                Case 4
                    'My.Computer.Audio.Play("", AudioPlayMode.BackgroundLoop)
                Case 5
                    'My.Computer.Audio.Play("", AudioPlayMode.BackgroundLoop)
            End Select
            'My.Computer.Audio.Play("", AudioPlayMode.BackgroundLoop)
        Else
            My.Computer.Audio.Stop()
        End If
    End Sub

    Public Function getProximity() As Short()
        Return proximitySensors
    End Function

    Public Function getFloorSensors() As Short()
        Return groundSensors
    End Function

    Public Function getMotorSpeeds() As Short()
        Return speeds
    End Function

    Public Sub setMotorsSpeed(left As Short, right As Short)
        If left < -1000 Then
            left = -1000
        End If
        If right < -1000 Then
            right = -1000
        End If
        If left > 1000 Then
            left = 1000
        End If
        If right > 1000 Then
            right = 1000
        End If

        speeds(0) = right
        speeds(1) = left
    End Sub

    Public Sub setMotorPosition(left As Integer, right As Integer)
        locations(0) = left
        locations(1) = right
    End Sub

    Public Sub setLed(number As Integer, value As Integer)
        If number < 9 Then
            If value = 0 Then
                leds(number) = False
            ElseIf value = 1 Then
                leds(number) = True
            Else
                leds(number) = Not leds(number)
            End If
        End If
    End Sub
    Public Sub update(data As XDocument)
        For Each n As XElement In data.Root.Elements("leds").Elements("led")
            leds(n.@number) = CType(n.@value.ToString, Boolean)
        Next
        For Each n As XElement In data.Root.Elements("proximity").Elements("proximitySensor")
            proximitySensors(n.@number) = CInt(n.@value)
        Next
        For Each n As XElement In data.Root.Elements("motors").Elements("motor")
            speeds(n.@number) = CShort(n.@speed)
            locations(n.@number) = CInt(n.@location)
        Next

        ' Fix values
        For i = 0 To speeds.Count - 1
            If speeds(i) > 1000 Then
                speeds(i) = 1000
            End If
            If speeds(i) < -1000 Then
                speeds(i) = -1000
            End If
        Next
    End Sub

    Public Function collides(pt As Point) As Boolean
        Return (pt.X - _location.X) ^ 2 + (pt.Y - _location.Y) ^ 2 <= radius ^ 2 Or pt.IsEmpty
    End Function

    Public Function distance(pt As Point) As Double
        If Not collides(pt) Then
            Dim units() As Integer = {3750, 2200, 676, 306, 245, 120}
            Dim r As New Random
            Dim dist As Integer
            Dim i As Integer
            Dim [error] As Integer
            Dim prev As Integer
            dist = Sqrt((_location.X - pt.X) ^ 2 + (_location.Y - pt.Y) ^ 2) - radius    ' True distance in mm

            ' Linear regression
            i = 0
            While i < 5 AndAlso dist / 10 > i
                i += 1
            End While


            If i = 0 Then
                prev = 5
            Else
                prev = i - 1
            End If

            dist = (-(units(prev) - units(i))) * (dist / 10 - i) + units(i)

            ' Add some error (The greater the distance, the bigger the error)
            If dist < 0 Then
                [error] = r.Next(20 * dist / sensorRange, 0)
            Else
                [error] = r.Next(0, 20 * dist / sensorRange)
            End If

            Return dist + [error]
        End If
        Return -1
    End Function

    Public Sub reset()
        initialize()
    End Sub

    Public Function getLed(number As Integer) As Boolean
        Return leds(number)
    End Function

    Public Sub setLocation(location As PointF)
        _location = location
    End Sub

    Public Sub setGroundSensor(number As Integer, value As Short)
        groundSensors(number) = value
    End Sub

    Public Sub setProximitySensor(number As Integer, value As Integer)
        proximitySensors(number) = value
    End Sub

    Public Sub rotate(a As Single)
        _angle += a
    End Sub

    Public Function getImage() As Bitmap
        Dim bmp As New Bitmap(255, 255)
        Dim gr As Graphics = Graphics.FromImage(bmp)

        'Draw robot
        gr.DrawImage(My.Resources.ePuck, 0, 0, 255, 255)

        'Draw leds
        For i = 0 To 7
            If leds(i) Then
                gr.FillEllipse(Brushes.Red, New Rectangle(ledLocations(i), ledSize))
            End If
        Next

        'Rotate
        bmp = rotateImage(bmp, angle)

        'Return resulting image
        Return bmp
    End Function

    Public Sub addPathPoint(pt As PointF)
        _path.Add(pt)
    End Sub

    Public Function getGroundSensor(n As Integer) As Point
        Dim p As New Point(groundSensorLocations(n).X * Cos(angle) - groundSensorLocations(n).Y * Sin(angle), groundSensorLocations(n).X * Sin(angle) + groundSensorLocations(n).Y * Cos(angle))
        Return New Point(location.X + p.X, location.Y + p.Y)
    End Function

    Public Function getSensorRay(n As Integer) As Point
        Dim p As New Point(proxRayLengths(n) * (proximitySensorRays(n).x * Cos(angle) - proximitySensorRays(n).y * Sin(angle)), proxRayLengths(n) * (proximitySensorRays(n).x * Sin(angle) + proximitySensorRays(n).y * Cos(angle)))
        Return New Point(location.X + p.X, location.Y + p.Y)
    End Function

    Public Sub setDirectionVector(v As Vector)
        dirVector = v
    End Sub

    Public Sub setRayLength(number As Integer, len As Double)
        proxRayLengths(number) = len
    End Sub

    Public Sub resetSensors()
        For i = 0 To sensorCount - 1
            proxRayLengths(i) = radius + sensorRange
        Next
    End Sub

    Public Sub New(startPosition As Point, startAngle As Single)
        _startPosition = startPosition
        _startAngle = startAngle

        ' Led size (constant)
        ledSize = New Size(10, 10)

        ' Led coordinates (constant)
        ledLocations(0) = New Point(117, 0)
        ledLocations(1) = New Point(217, 50)
        ledLocations(2) = New Point(245, 117)
        ledLocations(3) = New Point(190, 227)
        ledLocations(4) = New Point(117, 245)
        ledLocations(5) = New Point(49, 227)
        ledLocations(6) = New Point(0, 117)
        ledLocations(7) = New Point(23, 50)



        ' Proximity sensor ray coordinates (constant)
        Dim sensorAngles() As Integer = {-100, -130, -180, -250, -290, 0, -50, -80}
        Dim t As Double

        For i = 0 To 7
            t = PI * sensorAngles(i) / 180
            proximitySensorRays(7 - i) = New Vector(Cos(t), Sin(t))    ' Unitary vector
        Next

        ' Ground sensors
        groundSensorLocations(0) = New Point(-8 - groundSensorSize \ 2, 7 - Robot.radius)
        groundSensorLocations(1) = New Point(-groundSensorSize \ 2, 7 - Robot.radius)
        groundSensorLocations(2) = New Point(8 - groundSensorSize \ 2, 7 - Robot.radius)

        initialize()
    End Sub
#End Region
#Region "Private"
    Private Sub initialize()
        _path = New List(Of PointF)
        _location = _startPosition
        dirVector = New Vector(0, 0)

        speeds(0) = 0
        speeds(1) = 0

        locations(0) = 0
        locations(1) = 0

        _angle = _startAngle

        ' Set leds
        For i = 0 To 7
            leds(i) = False
        Next
    End Sub

    Private Function rotateImage(bmp As Bitmap, angle As Double) As Bitmap

        Dim bmp2 As New Bitmap(bmp.Width, bmp.Height)
        Dim gr As Graphics = Graphics.FromImage(bmp2)

        Dim upperLeftDrawPoint As Point = New Point(0, 0)

        'Calculate the center of the image
        Dim imageCenterOffset As Point = New Point(bmp.Width / 2, bmp.Height / 2)

        'Translate the graphics origin to the destination image center before rotating
        gr.TranslateTransform(upperLeftDrawPoint.X + imageCenterOffset.X, upperLeftDrawPoint.Y + imageCenterOffset.Y)

        'Rotate the matrix around the origin
        gr.RotateTransform(180 * angle / PI)

        'Translate the graphics origin back to the upper left image destination before drawing
        gr.TranslateTransform((upperLeftDrawPoint.X + imageCenterOffset.X) * -1, (upperLeftDrawPoint.Y + imageCenterOffset.Y) * -1)

        'Draw the image
        gr.DrawImage(bmp, upperLeftDrawPoint)

        'Reset all changes to the transformation matrix
        gr.ResetTransform()

        Return bmp2
    End Function

    Private Function tick2rad(ticks As Integer) As Double
        Return PI * ticks / 500
    End Function

    Private Function rad2tick(rads As Double) As Integer
        Return rads * 500 / PI
    End Function
#End Region
#End Region
End Class
