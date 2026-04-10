Public Class Resident
    Inherits Person 'INHERITANCE: Automatically gets the Id property

    Public Property LastName As String
    Public Property FirstName As String
    Public Property MiddleName As String
    Public Property Address As String
    Public Property Street As String
    Public Property District As String
    Public Property Sex As String
    Public Property BirthDate As Date
    Public Property Age As Integer

    'POLYMORPHISM: Overriding the base method
    Public Overrides Function GetDetails() As String
        Return "Resident: " & FirstName & " " & LastName & " (" & Street & ")"
    End Function
End Class