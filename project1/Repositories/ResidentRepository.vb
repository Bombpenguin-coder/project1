Imports MySql.Data.MySqlClient

Public Class ResidentRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    Public Function GetAllResidents(Optional searchTerm As String = "") As DataTable
        Dim dt As New DataTable()

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Added street here!
            Dim query As String = "SELECT id, lastname, firstname, middlename, birthdate, age, sex, address, street, district FROM residents"

            If Not String.IsNullOrWhiteSpace(searchTerm) Then
                query &= " WHERE CONCAT(lastname, ' ', firstname, ' ', middlename) LIKE @SearchTerm "
                query &= " OR address LIKE @SearchTerm OR street LIKE @SearchTerm OR district LIKE @SearchTerm"
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

    Public Sub AddResident(res As Resident)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            ' Fixed: Added birthdate and street
            Dim query As String = "INSERT INTO residents 
                        (lastname, firstname, middlename, birthdate, age, sex, address, street, district)
                        VALUES
                        (@LastName, @FirstName, @MiddleName, @BirthDate, @Age, @Sex, @Address, @Street, @District)"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@LastName", res.LastName)
                cmd.Parameters.AddWithValue("@FirstName", res.FirstName)
                cmd.Parameters.AddWithValue("@MiddleName", res.MiddleName)
                cmd.Parameters.AddWithValue("@BirthDate", res.BirthDate) ' Added BirthDate
                cmd.Parameters.AddWithValue("@Age", res.Age)
                cmd.Parameters.AddWithValue("@Sex", res.Sex)
                cmd.Parameters.AddWithValue("@Address", res.Address)
                cmd.Parameters.AddWithValue("@Street", res.Street)       ' Added Street
                cmd.Parameters.AddWithValue("@District", res.District)

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub UpdateResident(res As Resident)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            ' Fixed: Added street
            Dim query As String = "UPDATE residents SET 
                    lastname=@LastName, firstname=@FirstName, middlename=@MiddleName, 
                    birthdate=@BirthDate, age=@Age, sex=@Sex, address=@Address, 
                    street=@Street, district=@District 
                    WHERE id=@id"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@LastName", res.LastName)
                cmd.Parameters.AddWithValue("@FirstName", res.FirstName)
                cmd.Parameters.AddWithValue("@MiddleName", res.MiddleName)
                cmd.Parameters.AddWithValue("@BirthDate", res.BirthDate)
                cmd.Parameters.AddWithValue("@Age", res.Age)
                cmd.Parameters.AddWithValue("@Sex", res.Sex)
                cmd.Parameters.AddWithValue("@Address", res.Address)
                cmd.Parameters.AddWithValue("@Street", res.Street)       ' Added Street
                cmd.Parameters.AddWithValue("@District", res.District)
                cmd.Parameters.AddWithValue("@id", res.ID)

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

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