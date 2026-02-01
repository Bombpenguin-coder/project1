Imports MySql.Data.MySqlClient

Public Class ResidentRepository
    ' Copy the connection string from your FormMain
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' ---------------------------------------------------------
    ' 1. MOVED FROM: LoadResidentsFromDatabase
    ' ---------------------------------------------------------
    Public Function GetAllResidents(Optional searchTerm As String = "") As DataTable
        Dim dt As New DataTable()

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT id, lastname, firstname, middlename, birthdate, age, gender, address, district, barangay, city FROM residents"

            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                query &= " WHERE CONCAT(lastname, ' ', firstname, ' ', middlename) LIKE @SearchTerm "
                query &= " OR address LIKE @SearchTerm OR barangay LIKE @SearchTerm "
            End If

            query &= " ORDER BY lastname, firstname"

            Using cmd As New MySqlCommand(query, conn)
                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" & searchTerm & "%")
                End If

                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    ' ---------------------------------------------------------
    ' 2. MOVED FROM: btnAddResident_Click
    ' ---------------------------------------------------------
    Public Sub AddResident(res As Resident)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            ' (Optional: You can add the duplicate check logic here too)

            Dim query As String = "INSERT INTO residents 
                        (lastname, firstname, middlename, age, gender, address, district, barangay, city)
                        VALUES
                        (@LastName, @FirstName, @MiddleName, @Age, @Gender, @Address, @District, @Barangay, @City)"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@LastName", res.LastName)
                cmd.Parameters.AddWithValue("@FirstName", res.FirstName)
                cmd.Parameters.AddWithValue("@MiddleName", res.MiddleName)
                cmd.Parameters.AddWithValue("@Age", res.Age)
                cmd.Parameters.AddWithValue("@Gender", res.Gender)
                cmd.Parameters.AddWithValue("@Address", res.Address)
                cmd.Parameters.AddWithValue("@District", res.District)
                cmd.Parameters.AddWithValue("@Barangay", res.Barangay)
                cmd.Parameters.AddWithValue("@City", res.City)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub UpdateResident(res As Resident)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE residents SET 
                    lastname=@LastName, firstname=@FirstName, middlename=@MiddleName, 
                    birthdate=@BirthDate, age=@Age, gender=@Gender, address=@Address, 
                    district=@District, barangay=@Barangay, city=@City 
                    WHERE id=@id"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@LastName", res.LastName)
                cmd.Parameters.AddWithValue("@FirstName", res.FirstName)
                cmd.Parameters.AddWithValue("@MiddleName", res.MiddleName)
                cmd.Parameters.AddWithValue("@BirthDate", res.BirthDate)
                cmd.Parameters.AddWithValue("@Age", res.Age)
                cmd.Parameters.AddWithValue("@Gender", res.Gender)
                cmd.Parameters.AddWithValue("@Address", res.Address)
                cmd.Parameters.AddWithValue("@District", res.District)
                cmd.Parameters.AddWithValue("@Barangay", res.Barangay)
                cmd.Parameters.AddWithValue("@City", res.City)
                cmd.Parameters.AddWithValue("@id", res.Id) ' Important!

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 4. MOVED FROM: btnDeleteResident_Click
    Public Sub DeleteResident(id As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "DELETE FROM residents WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class