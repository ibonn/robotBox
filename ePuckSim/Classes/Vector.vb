Imports System.Math

Public Class Vector
    Private _x As Double
    Private _y As Double

    Public Shared ReadOnly Property nullVector As Vector
        Get
            Return New Vector(0, 0)
        End Get
    End Property

    Public Shared ReadOnly Property e1 As Vector
        Get
            Return New Vector(1, 0)
        End Get
    End Property

    Public Shared ReadOnly Property e2 As Vector
        Get
            Return New Vector(0, 1)
        End Get
    End Property

    Public Property x As Double
        Get
            Return _x
        End Get
        Set(value As Double)
            _x = x
        End Set
    End Property

    Public Property y As Double
        Get
            Return _y
        End Get
        Set(value As Double)
            _y = y
        End Set
    End Property

    Public ReadOnly Property modulus As Double
        Get
            Return sqrt(_x ^ 2 + _y ^ 2)
        End Get
    End Property

    Public ReadOnly Property isUnitary As Boolean
        Get
            Return modulus = 1
        End Get
    End Property

    Public ReadOnly Property unitary As Vector
        Get
            If modulus = 0 Then
                Return New Vector(0, 0)
            Else
                Return New Vector(x / modulus, y / modulus)
            End If
        End Get
    End Property

    Public Sub New(x As Double, y As Double)
        _x = x
        _y = y
    End Sub

    Public Overrides Function ToString() As String
        Return "(" & _x & ", " & _y & ")"
    End Function

    ''' <summary>
    ''' Addition
    ''' </summary>
    ''' <param name="v1"></param>
    ''' <param name="v2"></param>
    ''' <returns></returns>
    Public Shared Operator +(v1 As Vector, v2 As Vector) As Vector
        Return New Vector(v1.x + v2.x, v1.y + v2.y)
    End Operator

    ''' <summary>
    ''' Substraction
    ''' </summary>
    ''' <param name="v1"></param>
    ''' <param name="v2"></param>
    ''' <returns></returns>
    Public Shared Operator -(v1 As Vector, v2 As Vector) As Vector
        Return New Vector(v1.x - v2.x, v1.y - v2.y)
    End Operator

    ''' <summary>
    ''' Dot product
    ''' </summary>
    ''' <param name="v1"></param>
    ''' <param name="v2"></param>
    ''' <returns></returns>
    Public Shared Operator *(v1 As Vector, v2 As Vector) As Double
        Return v1.x * v2.x + v1.y * v2.y
    End Operator

    ''' <summary>
    ''' Constant prodict
    ''' </summary>
    ''' <param name="k"></param>
    ''' <param name="v"></param>
    ''' <returns></returns>
    Public Shared Operator *(k As Double, v As Vector) As Vector
        Return New Vector(k * v.x, k * v.y)
    End Operator

    ''' <summary>
    ''' Constant product
    ''' </summary>
    ''' <param name="v"></param>
    ''' <param name="k"></param>
    ''' <returns></returns>
    Public Shared Operator *(v As Vector, k As Double) As Vector
        Return k * v
    End Operator

    ''' <summary>
    ''' Opposite direction vector
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    Public Shared Operator Not(v As Vector) As Vector
        Return New Vector(-v.x, -v.y)
    End Operator

    Public Shared Operator =(v1 As Vector, v2 As Vector) As Boolean
        Return v1.x = v2.x And v1.y = v2.y
    End Operator

    Public Shared Operator <>(v1 As Vector, v2 As Vector) As Boolean
        Return Not v1 = v2
    End Operator

    Public Shared Operator <(v1 As Vector, v2 As Vector) As Boolean
        Return v1.modulus < v2.modulus
    End Operator

    Public Shared Operator <=(v1 As Vector, v2 As Vector) As Boolean
        Return v1.modulus <= v2.modulus
    End Operator

    Public Shared Operator >(v1 As Vector, v2 As Vector) As Boolean
        Return v1.modulus > v2.modulus
    End Operator

    Public Shared Operator >=(v1 As Vector, v2 As Vector) As Boolean
        Return v1.modulus >= v2.modulus
    End Operator
End Class
