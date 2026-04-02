Imports System.Security.Cryptography
Imports System.Text
Public Class UserService

    Private _repo As IUserRepository

    ' ✅ Dependency Injection here
    Public Sub New(repo As IUserRepository)
        _repo = repo
    End Sub

    Public Function Login(username As String, password As String) As UserDTO

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            Throw New Exception("Username and Password are required.")
        End If

        Dim hashedPassword As String = HashPassword(password)

        Return _repo.Login(username, hashedPassword)
    End Function

    Public Function InsertLoginHistory(username As String, role As String) As Integer
        Return _repo.InsertLoginHistory(username, role)
    End Function

    Private Function HashPassword(ByVal password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()

            For i As Integer = 0 To bytes.Length - 1
                builder.Append(bytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function
    Public Sub CreateAdmin(fullname As String, username As String, password As String, question As String, answer As String)

        If String.IsNullOrWhiteSpace(fullname) OrElse
       String.IsNullOrWhiteSpace(username) OrElse
       String.IsNullOrWhiteSpace(password) OrElse
       String.IsNullOrWhiteSpace(question) OrElse
       String.IsNullOrWhiteSpace(answer) Then

            Throw New Exception("Please fill in all setup fields.")
        End If

        Dim hashedPassword As String = HashPassword(password)

        _repo.CreateAdmin(fullname, username, hashedPassword, question, answer.ToLower())
    End Sub

End Class

