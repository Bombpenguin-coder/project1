Imports MySql.Data.MySqlClient

Public Class BlotterRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOAD CASES
    Public Function GetAllCases() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Added the new columns to the SELECT query
            Dim query As String = "SELECT id, incident_date, complainant, complainant_cell, respondent, respondent_cell, incident_type, location, street, status, narrative, full_information FROM blotter_cases ORDER BY incident_date DESC"
            Using adapter As New MySqlDataAdapter(query, conn)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ' 2. ADD CASE
    Public Sub AddCase(bCase As BlotterCase)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Added new columns to INSERT query
            Dim query As String = "INSERT INTO blotter_cases (complainant, complainant_cell, respondent, respondent_cell, incident_type, location, street, incident_date, status, narrative, full_information) " &
                                  "VALUES (@Comp, @CompCell, @Resp, @RespCell, @Type, @Loc, @Street, @IncDate, @Stat, @Narr, @FullInfo)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Comp", bCase.Complainant)
                cmd.Parameters.AddWithValue("@CompCell", bCase.ComplainantCell)
                cmd.Parameters.AddWithValue("@Resp", bCase.Respondent)
                cmd.Parameters.AddWithValue("@RespCell", bCase.RespondentCell)
                cmd.Parameters.AddWithValue("@Type", bCase.IncidentType)
                cmd.Parameters.AddWithValue("@Loc", bCase.Location)
                cmd.Parameters.AddWithValue("@Street", bCase.Street)
                cmd.Parameters.AddWithValue("@IncDate", bCase.IncidentDate)
                cmd.Parameters.AddWithValue("@Stat", bCase.Status)
                cmd.Parameters.AddWithValue("@Narr", bCase.Narrative)
                cmd.Parameters.AddWithValue("@FullInfo", bCase.FullInformation)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 3. UPDATE CASE
    Public Sub UpdateCase(bCase As BlotterCase)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Added new columns to UPDATE query
            Dim query As String = "UPDATE blotter_cases SET complainant=@Comp, complainant_cell=@CompCell, respondent=@Resp, respondent_cell=@RespCell, " &
                                  "incident_type=@Type, location=@Loc, street=@Street, incident_date=@IncDate, status=@Stat, narrative=@Narr, full_information=@FullInfo WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Comp", bCase.Complainant)
                cmd.Parameters.AddWithValue("@CompCell", bCase.ComplainantCell)
                cmd.Parameters.AddWithValue("@Resp", bCase.Respondent)
                cmd.Parameters.AddWithValue("@RespCell", bCase.RespondentCell)
                cmd.Parameters.AddWithValue("@Type", bCase.IncidentType)
                cmd.Parameters.AddWithValue("@Loc", bCase.Location)
                cmd.Parameters.AddWithValue("@Street", bCase.Street)
                cmd.Parameters.AddWithValue("@IncDate", bCase.IncidentDate)
                cmd.Parameters.AddWithValue("@Stat", bCase.Status)
                cmd.Parameters.AddWithValue("@Narr", bCase.Narrative)
                cmd.Parameters.AddWithValue("@FullInfo", bCase.FullInformation)
                cmd.Parameters.AddWithValue("@id", bCase.Id)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class