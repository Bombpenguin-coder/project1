Imports MySql.Data.MySqlClient

Public Class BlotterRepository
    ' Update this connection string if needed
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOAD ALL CASES
    Public Function GetAllCases() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT id, incident_date, complainant, respondent, incident_type, status, location, narrative FROM blotter_cases ORDER BY incident_date DESC"
            Using adapter As New MySqlDataAdapter(query, conn)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ' 2. ADD NEW CASE
    Public Sub AddCase(bCase As BlotterCase)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO blotter_cases (complainant, respondent, incident_type, location, incident_date, status, narrative) VALUES (@Comp, @Resp, @Type, @Loc, @Date, @Status, @Narr)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Comp", bCase.Complainant)
                cmd.Parameters.AddWithValue("@Resp", bCase.Respondent)
                cmd.Parameters.AddWithValue("@Type", bCase.IncidentType)
                cmd.Parameters.AddWithValue("@Loc", bCase.Location)
                cmd.Parameters.AddWithValue("@Date", bCase.IncidentDate)
                cmd.Parameters.AddWithValue("@Status", bCase.Status)
                cmd.Parameters.AddWithValue("@Narr", bCase.Narrative)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 3. UPDATE CASE
    Public Sub UpdateCase(bCase As BlotterCase)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE blotter_cases SET complainant=@Comp, respondent=@Resp, incident_type=@Type, location=@Loc, incident_date=@Date, status=@Status, narrative=@Narr WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Comp", bCase.Complainant)
                cmd.Parameters.AddWithValue("@Resp", bCase.Respondent)
                cmd.Parameters.AddWithValue("@Type", bCase.IncidentType)
                cmd.Parameters.AddWithValue("@Loc", bCase.Location)
                cmd.Parameters.AddWithValue("@Date", bCase.IncidentDate)
                cmd.Parameters.AddWithValue("@Status", bCase.Status)
                cmd.Parameters.AddWithValue("@Narr", bCase.Narrative)
                cmd.Parameters.AddWithValue("@id", bCase.Id)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 4. DELETE CASE (Optional, but good to have)
    Public Sub DeleteCase(id As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "DELETE FROM blotter_cases WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class