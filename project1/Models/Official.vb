Public Class Official
    Inherits Person 'INHERITANCE: Automatically gets the Id property

    Public Property FullName As String
    Public Property Position As String
    Public Property ContactNumber As String

    'POLYMORPHISM: Overriding the base method
    Public Overrides Function GetDetails() As String
        Return "Official: " & FullName & " - " & Position
    End Function
End Class