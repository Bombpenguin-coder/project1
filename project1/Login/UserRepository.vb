Imports MySql.Data.MySqlClient

Public Class UserRepository
    Implements IUserRepository

    Private connectionString As String = "server=localhost;user id=root;password=;database=login_db"

    Public Function Login(username As String, password As String) As UserDTO Implements IUserRepository.Login
        Dim user As UserDTO = Nothing

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "SELECT * FROM users WHERE username=@username AND password=@password"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@password", password)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            user = New UserDTO With {
                                .Username = reader("username").ToString(),
                                .Fullname = reader("fullname").ToString(),
                                .Role = reader("role").ToString()
                            }
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Login Error: " & ex.Message)
        End Try

        Return user
    End Function

    Public Sub CreateAdmin(fullname As String, username As String, password As String, question As String, answer As String) Implements IUserRepository.CreateAdmin

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "INSERT INTO users (fullname, username, password, role, security_question, security_answer) VALUES (@Fullname, @Username, @Password, 'Superadmin', @Question, @Answer)"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Fullname", fullname)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Password", password)
                    cmd.Parameters.AddWithValue("@Question", question)
                    cmd.Parameters.AddWithValue("@Answer", answer)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception("Error creating admin: " & ex.Message)
        End Try

    End Sub

    Public Function InsertLoginHistory(username As String, role As String) As Integer Implements IUserRepository.InsertLoginHistory
        Dim historyId As Integer = 0

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Using cmd As New MySqlCommand("INSERT INTO login_history (username, role, login_time) VALUES (@username, @role, NOW())", conn)
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@role", role)
                    cmd.ExecuteNonQuery()
                End Using

                Using cmd2 As New MySqlCommand("SELECT LAST_INSERT_ID()", conn)
                    Dim result = cmd2.ExecuteScalar()
                    If result IsNot Nothing Then
                        historyId = Convert.ToInt32(result)
                    End If
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("History Error: " & ex.Message)
        End Try

        Return historyId
    End Function

End Class