Public Interface IUserRepository
    Function Login(username As String, password As String) As UserDTO
    Function InsertLoginHistory(username As String, role As String) As Integer

    Sub CreateAdmin(fullname As String, username As String, password As String, question As String, answer As String)

End Interface