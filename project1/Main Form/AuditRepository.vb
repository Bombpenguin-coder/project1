Imports MySql.Data.MySqlClient

Public Class AuditRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOG AN ACTION (We will call this from FormMain)
    Public Sub LogAction(username As String, role As String, action As String, modName As String, desc As String)
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
    End Sub

    ' 2. LOAD ALL LOGS (For the DataGridView)
    Public Function GetAuditHistory() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT action_date, username, role, action_type, module, description FROM audit_trail ORDER BY action_date DESC"
            Using cmd As New MySqlCommand(query, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function
End Class