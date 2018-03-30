Imports System.IO
Imports System.Math

Public Class Map

    Private Const version As UInteger = 0
    Private _robot As Robot
    Private _sp As Point
    Private _angle As Single

    Public Structure Line
        Public startPoint As PointF
        Public endPoint As PointF
    End Structure

    Private _paths As List(Of PointF())
    Private _walls As List(Of Line)

    Public ReadOnly Property robot As Robot
        Get
            Return _robot
        End Get
    End Property

    Public ReadOnly Property walls As Line()
        Get
            Return _walls.ToArray
        End Get
    End Property

    Public ReadOnly Property paths As PointF()()
        Get
            Return _paths.ToArray
        End Get
    End Property

    Private Sub initialize()
        _walls = New List(Of Line)
        _paths = New List(Of PointF())
        _robot = New Robot(_sp, _angle)
    End Sub

    Public Sub New(sp As Point, a As Single)
        _sp = sp
        _angle = a
        initialize()
    End Sub

    Public Sub New(path As String)
        load(path)
    End Sub

    Public Sub reset()
        _walls.Clear()
        _paths.Clear()
        _robot.reset()
    End Sub

    Public Sub load(path As String)
        If File.Exists(path) Then

            initialize()
            Dim mapDoc As XDocument = XDocument.Load(path)

            Dim sp, ep As New Point
            Dim pathPoints As List(Of PointF)

            Select Case mapDoc.Root.@version.ToString
                Case "0"
                    _robot.setLocation(New Point(mapDoc.Root.<robot>.@x, mapDoc.Root.<robot>.@y))   ' Read robot location
                    _robot.rotate(mapDoc.Root.<robot>.@angle)                                       ' Read angle

                    ' Read walls
                    For Each ln As XElement In mapDoc.Root.<walls>.Elements("wall")
                        sp.X = ln.@sx
                        sp.Y = ln.@sy
                        ep.X = ln.@ex
                        ep.Y = ln.@ey
                        _walls.Add(New Line With {.startPoint = sp, .endPoint = ep})
                    Next

                    ' Read paths
                    For Each pth As XElement In mapDoc.Root.<paths>.Elements("path")
                        pathPoints = New List(Of PointF)
                        For Each pt As XElement In pth.Elements("point")
                            pathPoints.Add(New PointF(pt.@x, pt.@y))
                        Next
                        _paths.Add(pathPoints.ToArray)
                    Next
                Case Else
                    Throw New Exception("Unknown map version: " & mapDoc.Root.@version.ToString)
            End Select
        Else
            Throw New Exception("Cannot find " & path)
        End If
    End Sub

    Public Sub save(path As String)
        Dim mapDoc As New XDocument

        Dim wallsXml As New List(Of XElement)
        Dim pathsXml As New List(Of XElement)

        For Each w As Line In _walls
            wallsXml.Add(New XElement("wall", New XAttribute("sx", w.startPoint.X),
                                              New XAttribute("sy", w.startPoint.Y),
                                              New XAttribute("ex", w.endPoint.X),
                                              New XAttribute("ey", w.endPoint.Y)
                                      )
            )
        Next

        Dim pathXml As List(Of XElement)
        For Each p As PointF() In _paths
            pathXml = New List(Of XElement)
            For Each pt As PointF In p
                pathXml.Add(New XElement("point", New XAttribute("x", pt.X), New XAttribute("y", pt.Y))
                )
            Next
            pathsXml.Add(New XElement("path", pathXml))
        Next

        mapDoc.Add(
            New XComment("ePuck simulator map"),
            New XElement("map", New XAttribute("version", version),
                            New XElement("robot", New XAttribute("x", _robot.location.X), New XAttribute("y", _robot.location.Y), New XAttribute("angle", _robot.angle)),
                            New XElement("walls",
                                         wallsXml
                            ),
                            New XElement("paths",
                                         pathsXml
                            )
                        )
        )

        mapDoc.Save(path)
    End Sub

    Public Sub addWall(startPoint As Point, endPoint As Point)
        Dim w As New Line
        w.startPoint = startPoint
        w.endPoint = endPoint
        addWall(w)
    End Sub

    Public Sub addWall(wall As Line)
        _walls.Add(wall)
    End Sub

    Public Sub removeWall(index As Integer)
        If index <> -1 Then
            _walls.RemoveAt(index)
        End If
    End Sub

    Public Sub addPath(points() As PointF)
        _paths.Add(points)
    End Sub

    ' https://stackoverflow.com/questions/3574338/find-linesegment-that-contains-a-point
    Public Function wallAt(location As Point) As Integer
        Dim l As Line
        Dim v, w As Vector
        Dim c1, c2, b, distance As Double
        Dim pb As Point
        For i = 0 To _walls.Count - 1
            l = _walls(i)
            v = New Vector(l.endPoint.X - l.startPoint.X, l.endPoint.Y - l.startPoint.Y)
            w = New Vector(location.X - l.startPoint.X, location.Y - l.startPoint.Y)
            c1 = w * v
            c2 = v * v
            If c2 <> 0 Then
                b = c1 / c2
                pb = New Point(l.startPoint.X + b * v.x, l.startPoint.Y + b * v.y)
                distance = Sqrt((location.X - pb.X) ^ 2 + (location.Y - pb.Y) ^ 2)
                If distance < 3 Then
                    Return i
                End If
            End If
        Next
        Return -1
    End Function

    Public Sub removePathPoint(path As Integer, index As Integer)
        If path <> -1 And index <> -1 Then
            _paths(path) = _paths(path).Where(Function(s) s <> _paths(path)(index)).ToArray
            If _paths(path).Count = 1 Then
                _paths.RemoveAt(path)
            End If
        End If
    End Sub

    Public Sub removePathPoint(patpointpair() As Integer)
        removePathPoint(patpointpair(0), patpointpair(1))
    End Sub

    Public Function pathPointAt(location As Point) As Integer()
        Dim distances As New List(Of Integer())

        ' Compute distances
        For i = 0 To _paths.Count - 1
            For j = 0 To _paths(i).Count - 1
                distances.Add(New Integer() {i, j, (location.X - _paths(i)(j).X) ^ 2 + (location.Y - _paths(i)(j).Y) ^ 2})
            Next
        Next

        If distances.Count = 0 Then
            Return New Integer() {-1, -1}
        End If

        ' Find minimum distance
        Dim pathPos, pointPos As Integer
        Dim minDist As Integer = distances(0)(2)
        For Each d As Integer() In distances
            If d(2) < minDist Then
                pathPos = d(0)
                pointPos = d(1)
                minDist = d(2)
            End If
        Next

        ' If the distance is smaller than the threshold, return the point
        If minDist < 900 Then
            Return New Integer() {pathPos, pointPos}
        End If
        Return New Integer() {-1, -1}
    End Function
End Class