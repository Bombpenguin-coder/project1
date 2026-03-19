Imports MySql.Data.MySqlClient

Public Class AuditRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOG AN ACTION
    Public Sub LogAction(username As String, role As String, action As String, modName As String, desc As String)
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "INSERT INTO audit_trail (username, role, action_type, module, description) " &
                                      "VALUES (@User, @Role, @Action, @Module, @Desc)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@User", username)
                    cmd.Parameters.AddWithValue("@Role", role)
                    cmd.Parameters.AddWithValue("@Action", action)
                    cmd.Parameters.AddWithValue("@Module", modName)
                    cmd.Parameters.AddWithValue("@Desc", desc)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ' If it fails, this will tell us exactly why!
            MessageBox.Show("Audit Log Error: " & ex.Message, "System Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ' 2. LOAD ALL LOGS 
    Public Function GetAuditHistory() As DataTable
        Dim dt As New DataTable()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT action_date, username, role, action_type, module, description FROM audit_trail ORDER BY action_date DESC"
                Using cmd As New MySqlCommand(query, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Failed to load audit history: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return dt
    End Function
End Class