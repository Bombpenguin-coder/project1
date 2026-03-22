Imports MySql.Data.MySqlClient

Public Class OfficialRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOAD OFFICIALS
    Public Function GetAllOfficials() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT id, fullname, position, contactnumber FROM officials"
            Using adapter As New MySqlDataAdapter(query, conn)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ' 2. ADD OFFICIAL
    Public Sub AddOfficial(off As Official)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO officials (fullname, position, contactnumber) VALUES (@Name, @Pos, @Contact)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Name", off.FullName)
                cmd.Parameters.AddWithValue("@Pos", off.Position)
                cmd.Parameters.AddWithValue("@Contact", off.ContactNumber)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 3. UPDATE OFFICIAL
    Public Sub UpdateOfficial(off As Official)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE officials SET fullname=@Name, position=@Pos, contactnumber=@Contact WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Name", off.FullName)
                cmd.Parameters.AddWithValue("@Pos", off.Position)
                cmd.Parameters.AddWithValue("@Contact", off.ContactNumber)
                cmd.Parameters.AddWithValue("@id", off.Id)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 4. DELETE OFFICIAL
    Public Sub DeleteOfficial(id As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "DELETE FROM officials WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class