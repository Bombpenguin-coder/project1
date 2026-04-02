Module Module1
    Sub Main()
        Dim repo As IUserRepository = New UserRepository()
        Dim service As New UserService(repo)
        Dim form As New Form1(service)

        Application.Run(form)
    End Sub
End Module