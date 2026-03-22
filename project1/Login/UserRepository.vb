Imports MySql.Data.MySqlClient

Public Class UserRepository

    Private connectionString As String = "server=localhost;user id=root;password=;database=login_db"

    Public Function Login(username As String, password As String) As UserDTO
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

    Public Function InsertLoginHistory(username As String, role As String) As Integer
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