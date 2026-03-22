Imports MySql.Data.MySqlClient

Public Class BlotterRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' --- NEW: THE DUPLICATE SHIELD ---
    Public Function IsDuplicateCase(complainant As String, respondent As String, incidentDate As DateTime, currentId As Integer) As Boolean
        Dim isDupe As Boolean = False
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM blotter_cases WHERE complainant = @Comp AND respondent = @Resp AND incident_date = @IncDate AND id <> @CurrentId"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Comp", complainant)
                cmd.Parameters.AddWithValue("@Resp", respondent)
                cmd.Parameters.AddWithValue("@IncDate", incidentDate.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@CurrentId", currentId)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then isDupe = True
            End Using
        End Using
        Return isDupe
    End Function

    ' 1. LOAD CASES
    Public Function GetAllCases() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Added incident_time
            Dim query As String = "SELECT id, incident_date, incident_time, complainant, complainant_cell, respondent, respondent_cell, incident_type, location, street, status, narrative, full_information FROM blotter_cases ORDER BY incident_date DESC"
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
            ' Added incident_time to the insert query
            Dim query As String = "INSERT INTO blotter_cases (complainant, complainant_cell, respondent, respondent_cell, incident_type, incident_time, location, street, incident_date, status, narrative, full_information) " &
                                  "VALUES (@Comp, @CompCell, @Resp, @RespCell, @Type, @IncTime, @Loc, @Street, @IncDate, @Stat, @Narr, @FullInfo)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Comp", bCase.Complainant)
                cmd.Parameters.AddWithValue("@CompCell", bCase.ComplainantCell)
                cmd.Parameters.AddWithValue("@Resp", bCase.Respondent)
                cmd.Parameters.AddWithValue("@RespCell", bCase.RespondentCell)
                cmd.Parameters.AddWithValue("@Type", bCase.IncidentType)
                cmd.Parameters.AddWithValue("@IncTime", bCase.IncidentTime) ' NEW
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
            ' Added incident_time to update query
            Dim query As String = "UPDATE blotter_cases SET complainant=@Comp, complainant_cell=@CompCell, respondent=@Resp, respondent_cell=@RespCell, " &
                                  "incident_type=@Type, incident_time=@IncTime, location=@Loc, street=@Street, incident_date=@IncDate, status=@Stat, narrative=@Narr, full_information=@FullInfo WHERE id=@id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Comp", bCase.Complainant)
                cmd.Parameters.AddWithValue("@CompCell", bCase.ComplainantCell)
                cmd.Parameters.AddWithValue("@Resp", bCase.Respondent)
                cmd.Parameters.AddWithValue("@RespCell", bCase.RespondentCell)
                cmd.Parameters.AddWithValue("@Type", bCase.IncidentType)
                cmd.Parameters.AddWithValue("@IncTime", bCase.IncidentTime) ' NEW
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