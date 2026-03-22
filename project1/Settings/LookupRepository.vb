Imports MySql.Data.MySqlClient

Public Class LookupRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' Load items based on the category (e.g., "Street" or "Facility")
    ' 1. LOAD ITEMS (Now fetches the price too!)
    Public Function GetItemsByCategory(category As String) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT id, item_value, item_price FROM system_lookups WHERE category = @Cat ORDER BY item_value ASC"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Cat", category)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' 2. ADD ITEM (Now accepts an optional price)
    Public Sub AddItem(category As String, value As String, Optional price As Decimal = 0.00D)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO system_lookups (category, item_value, item_price) VALUES (@Cat, @Val, @Price)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Cat", category)
                cmd.Parameters.AddWithValue("@Val", value)
                cmd.Parameters.AddWithValue("@Price", price)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' Delete an item
    Public Sub DeleteItem(id As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "DELETE FROM system_lookups WHERE id = @Id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class