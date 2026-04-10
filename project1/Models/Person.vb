Public Class Person
    ' 1. ENCAPSULATION & INHERITANCE
    Public Property Id As Integer

    ' 2. POLYMORPHISM
    Public Overridable Function GetDetails() As String
        Return "Basic Person Record"
    End Function
End Class