Imports System.Math
Imports ePuckSim.Map

Public Class Physics

    Private Const autoAdjustDeltaT As Boolean = False

    Private Shared _map As Map
    Private Shared _intersectionPoints As New List(Of Point)
    Private Shared _collisionPoints As New List(Of Point)
    Private Shared _deltaT As Integer
    Private Shared refreshTimer As New Timer
    Private Shared sw As Stopwatch
    Private Shared elapsed As Integer
    Private Shared screenSize As Size

    Public Shared ReadOnly Property sensorIntersectionPoints As Point()
        Get
            Return _intersectionPoints.ToArray
        End Get
    End Property

    Public Shared ReadOnly Property collisionPoints As Point()
        Get
            Return _collisionPoints.ToArray
        End Get
    End Property

    Public Shared ReadOnly Property deltaT As Double
        Get
            Return _deltaT / 1000
        End Get
    End Property

    Public Shared Sub setMap(m As Map)
        _map = m
    End Sub

    Public Shared Sub start()
        screenSize = Screen.PrimaryScreen.Bounds.Size
        AddHandler refreshTimer.Tick, AddressOf doPhysics
        _deltaT = 100
        refreshTimer.Interval = _deltaT
        refreshTimer.Start()
    End Sub

    Public Shared Sub [stop]()
        refreshTimer.Stop()
    End Sub

    Public Shared Sub doPhysics()
        sw = New Stopwatch
        sw.Start()
        ' Update robot
        Dim robot As Robot = _map.robot
        Dim r As New Random
        Dim wheel1Location As Double = robot.leftWheelLocation
        Dim wheel2Location As Double = robot.rightWheelLocation
        Dim wheel1 As Double = tick2rad(robot.leftWheelSpeed)  ' rad/s  
        Dim wheel2 As Double = tick2rad(robot.rightWheelSpeed)  ' rad/s 
        Dim linearSpeed As Double = Robot.wheelRadius * (wheel1 + wheel2) / 2 ' mm/s    
        Dim angularSpeed As Double = Robot.wheelRadius * (wheel2 - wheel1) / (2 * Robot.radius) ' 1/s ~ rad/s

        ' Reseteamos los valores de los sensores
        robot.resetSensors()

        ' Calculamos nuevo angulo y desplazamiento
        Dim s As Single = angularSpeed * deltaT  ' [rad/s] * [s] = [rad]
        'Dim d As Single = linearSpeed * time  ' [mm/s] * [s] = [mm]

        ' 118 has been experimentally calculated from robot rotation. It may be a wrong constant
        wheel1Location += 118 * wheel1 * deltaT + r.Next(-5, 6)
        wheel2Location += 118 * wheel2 * deltaT + r.Next(-5, 6)

        ' Update wheel locations
        ' Take a look (may change 118): http://www.e-puck.org/index.php?option=com_content&view=article&id=7&Itemid=9
        robot.leftWheelLocation = wheel1Location
        robot.rightWheelLocation = wheel2Location

        Dim dirVector As Vector = New Vector(linearSpeed * Sin(robot.angle), linearSpeed * -Cos(robot.angle))                   ' ([mm/s], [mm/s])
        Dim location As PointF = New Point(robot.location.X + dirVector.x * deltaT, robot.location.Y + dirVector.y * deltaT)  ' ([mm], [mm])

        ' Update robot angle
        robot.rotate(s)

        ' Update robot direction vector
        robot.setDirectionVector(dirVector)

        ' Update robot location
        robot.setLocation(location)

        ' Add point to path
        robot.addPathPoint(robot.location)

        ' Set random sensor data
        For i = 0 To Robot.sensorCount - 1
            robot.setProximitySensor(i, r.Next(0, 200))    ' Nothing near the sensors, values between 0 and 200
        Next

        'For j = 0 To 2
        '    'robot.setGroundSensor(j, r.Next(500, 1000))
        '    robot.setGroundSensor(j, 0)
        'Next

        ' No intersection points
        _intersectionPoints.Clear()

        ' No collision points
        _collisionPoints.Clear()

        ' Calculate new intersection points
        Dim sensorRay As Point
        Dim ip As Point

        ' Interact with walls
        For Each line As Line In _map.walls

            ' Measure distances
            For i = 0 To 7
                sensorRay = robot.getSensorRay(i)
                If intersect(robot.imageCenter, sensorRay, line.startPoint, line.endPoint) Then
                    ip = intersectionPoint(robot.imageCenter, sensorRay, line.startPoint, line.endPoint)
                    ' If the intersection point is inside the robot ignore it
                    If Not isInsideCircle(ip, robot.location, Robot.radius) Then
                        robot.setProximitySensor(i, robot.distance(ip))
                        robot.setRayLength(i, Sqrt((robot.location.X - ip.X) ^ 2 + (robot.location.Y - ip.Y) ^ 2))
                        _intersectionPoints.Add(ip)
                    End If
                End If
            Next

            ' Check collision (https://stackoverflow.com/questions/1073336/circle-line-segment-collision-detection-algorithm)
            ' We do not need to check the condition, if no points exist an empty array is returned
            'If intersect(line.startPoint, line.endPoint, robot.location, Robot.radius) Then
            _collisionPoints.AddRange(intersectionPoints(line.startPoint, line.endPoint, robot.location, Robot.radius))
            'End If
        Next

        ' Measure floor sensors
        '<TEMP> ' Should not use a bitmap to measure the sensors. Try to replace with some math. See TODO
        Dim pathBmp As New Bitmap(screenSize.Width, screenSize.Height)
        Dim gr As Graphics = Graphics.FromImage(pathBmp)
        gr.Clear(Color.White)
        For Each path As PointF() In _map.paths
            gr.DrawCurve(New Pen(Color.Black, 20), path)
            'gr.DrawCurve(New Pen(Color.White, 15), path)
            ' TODO:
            ' Calculate path and sensor circle intersection
            ' In case of intersection, activate the sensor
        Next

        Dim sensorLocation As Point
        Dim pixelColor As Color
        For i = 0 To 2
            sensorLocation = robot.getGroundSensor(i)
            If sensorLocation.X > 0 And sensorLocation.Y > 0 And sensorLocation.X < screenSize.Width And sensorLocation.Y < screenSize.Height Then
                pixelColor = pathBmp.GetPixel(sensorLocation.X, sensorLocation.Y)
                ' See graph at: https://www.cyberbotics.com/e-puck_ground_sensors.pdf
                If pixelColor.R = 0 And pixelColor.G = 0 And pixelColor.B = 0 Then
                    'Black
                    'robot.setGroundSensor(i, r.Next(0, 500))   ' Another sercom version?
                    If i = 0 Then
                        robot.setGroundSensor(i, r.Next(280, 290))
                    ElseIf i = 1 Then
                        robot.setGroundSensor(i, r.Next(270, 280))
                    Else
                        robot.setGroundSensor(i, r.Next(260, 270))
                    End If
                Else
                    'White
                    'robot.setGroundSensor(i, r.Next(500, 1000))   ' Another sercom version?
                    robot.setGroundSensor(i, r.Next(-60, -55))
                End If
            End If
        Next
        '</TEMP>

        ' Update robot depending on collisions (Improve?)
        dirVector = robot.directionVector
        If Not dirVector = Vector.nullVector Then
            Dim v As New Vector(0, 0)
            Dim w As Vector
            For Each p As Point In _collisionPoints
                w = New Vector(robot.location.X - p.X, robot.location.Y - p.Y)
                w = w * Abs(w * dirVector / (w.modulus * dirVector.modulus))
                v += w
            Next

            ' Normalize
            v = v.unitary * linearSpeed

            ' Update location
            location = New PointF(robot.location.X + v.x * deltaT, robot.location.Y + v.y * deltaT)
            robot.setLocation(location)
        End If

        sw.Stop()
        elapsed = sw.ElapsedMilliseconds  ' Update delta t
        If autoAdjustDeltaT AndAlso elapsed > 0 Then
            _deltaT = elapsed
            refreshTimer.Interval = _deltaT
        End If
    End Sub

    Private Shared Function isInsideCircle(pt As PointF, c As PointF, r As Double) As Boolean
        Return (pt.X - c.X) ^ 2 + (pt.Y - c.Y) ^ 2 <= r ^ 2
    End Function

    ''' <summary>
    ''' Check wether a line segment and a circle intersect
    ''' </summary>
    ''' <param name="p">The line segment starting point</param>
    ''' <param name="q">The line segment ending point</param>
    ''' <param name="ct">The center of the circle</param>
    ''' <param name="r">The radius of the circle</param>
    ''' <returns></returns>
    Private Shared Function intersect(p As PointF, q As PointF, ct As PointF, r As Double) As Boolean
        Dim d As New Vector(q.X - p.X, q.Y - p.Y)
        Dim f As New Vector(p.X - ct.X, p.Y - ct.Y)

        Dim a As Double = d * d
        Dim b As Double = 2 * f * d
        Dim c As Double = f * f - r ^ 2

        Dim discriminant As Double = b ^ 2 - 4 * a * c

        ' If discriminant >= 0, there is a collision
        Return discriminant >= 0
    End Function

    ''' <summary>
    ''' Get the intersection points between a line segment and a circle
    ''' </summary>
    ''' <param name="p">The line segment starting point</param>
    ''' <param name="q">The line segment ending point</param>
    ''' <param name="ct">The center of the circle</param>
    ''' <param name="r">The radius of the circle</param>
    ''' <returns></returns>
    Private Shared Function intersectionPoints(p As PointF, q As PointF, ct As PointF, r As Double) As Point()
        Dim d As New Vector(q.X - p.X, q.Y - p.Y)
        Dim f As New Vector(p.X - ct.X, p.Y - ct.Y)

        Dim a As Double = d * d
        Dim b As Double = 2 * f * d
        Dim c As Double = f * f - r ^ 2

        Dim discriminant As Double = b ^ 2 - 4 * a * c

        Dim points As New List(Of Point)

        ' If discriminant >= 0, there is a collision
        If discriminant >= 0 Then
            discriminant = Sqrt(discriminant)

            Dim t1 As Double = (-b - discriminant) / (2 * a)
            Dim t2 As Double = (-b + discriminant) / (2 * a)

            ' 2 intersection points
            If t1 >= 0 And t1 <= 1 And t2 >= 0 And t2 <= 1 Then
                points.Add(New Point(p.X + t1 * d.x, p.Y + t1 * d.y))
                points.Add(New Point(p.X + t2 * d.x, p.Y + t2 * d.y))

            ElseIf t1 >= 0 And t1 <= 1 Then    ' 1 intersection point (t1)
                points.Add(New Point(p.X + t1 * d.x, p.Y + t1 * d.y))

            ElseIf t2 >= 0 And t2 <= 1 Then     ' 1 intersection point (t2)
                points.Add(New Point(p.X + t2 * d.x, p.Y + t2 * d.y))

            End If
        End If

        Return points.ToArray
    End Function

    ''' <summary>
    ''' Check if 2 lines intersect
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <param name="q1"></param>
    ''' <param name="p2"></param>
    ''' <param name="q2"></param>
    ''' <returns></returns>
    Private Shared Function intersect(p1 As PointF, q1 As PointF, p2 As PointF, q2 As PointF) As Boolean
        ' Find the four orientations needed for general And
        ' special cases
        Dim o1 As Integer = orientation(p1, q1, p2)
        Dim o2 As Integer = orientation(p1, q1, q2)
        Dim o3 As Integer = orientation(p2, q2, p1)
        Dim o4 As Integer = orientation(p2, q2, q1)

        ' General case
        If o1 <> o2 And o3 <> o4 Then
            Return True
        End If

        ' Special Cases
        ' p1, q1 And p2 are colinear And p2 lies on segment p1q1
        If o1 = 0 And onSegment(p1, p2, q1) Then Return True

        ' p1, q1 And p2 are colinear And q2 lies on segment p1q1
        If o2 = 0 And onSegment(p1, q2, q1) Then Return True

        ' p2, q2 And p1 are colinear And p1 lies on segment p2q2
        If o3 = 0 And onSegment(p2, p1, q2) Then Return True

        ' p2, q2 And q1 are colinear And q1 lies on segment p2q2
        If o4 = 0 And onSegment(p2, q1, q2) Then Return True

        Return False ' Doesn't fall in any of the above cases
    End Function

    ''' <summary>
    ''' Gets the intersection point between 2 lines
    ''' </summary>
    ''' <param name="p1">The first line start point</param>
    ''' <param name="p2">The first line end point</param>
    ''' <param name="q1">The second line start point</param>
    ''' <param name="q2">The second line end point</param>
    ''' <returns>The intersection point between the lines</returns>
    Private Shared Function intersectionPoint(p1 As PointF, p2 As PointF, q1 As PointF, q2 As PointF) As Point
        Dim dy1 As Double = p2.Y - p1.Y
        Dim dx1 As Double = p2.X - p1.X
        Dim dy2 As Double = q2.Y - q1.Y
        Dim dx2 As Double = q2.X - q1.X
        Dim p As New Point
        'check whether the two line parallel
        If dy1 * dx2 = dy2 * dx1 Or dx1 = 0 Then    ' or dx1 = 0 -> y = ... / dx1
            'Return P with a specific data
            Return Point.Empty  ' No intersection point
        Else
            Dim x As Double = ((q1.Y - p1.Y) * dx1 * dx2 + dy1 * dx2 * p1.X - dy2 * dx1 * q1.X) / (dy1 * dx2 - dy2 * dx1)
            Dim y As Double = p1.Y + (dy1 / dx1) * (x - p1.X)
            Return New Point(x, y)
        End If
    End Function

    Private Shared Function onSegment(p As PointF, q As PointF, r As PointF) As Boolean
        Return q.X <= Max(p.X, r.X) And q.X >= Min(p.X, r.X) And q.Y <= Max(p.Y, r.Y) And q.Y >= Min(p.Y, r.Y)
    End Function

    Private Shared Function orientation(p As PointF, q As PointF, r As PointF) As Integer
        ' See http://www.geeksforgeeks.org/orientation-3-ordered-points/
        ' for details of below formula.
        Dim val As Integer = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y)

        If val = 0 Then
            Return 0  ' colinear
        End If

        Return If(val > 0, 1, 2) ' clock or counterclock wise
    End Function

    Private Shared Function tick2rad(ticks As Integer) As Double
        Return PI * ticks / 500
    End Function

    Private Shared Function rad2tick(rads As Double) As Integer
        Return rads * 500 / PI
    End Function

    Public Shared Function deg2rad(deg As Double) As Double
        Return PI * deg / 180
    End Function

    Public Shared Function rad2deg(rad As Double) As Double
        Return 180 * rad / PI
    End Function

End Class