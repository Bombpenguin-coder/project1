Imports MySql.Data.MySqlClient

Public Class ReservationRepository
    ' Update this connection string if needed
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOAD SCHEDULE BY DATE
    Public Function GetReservationsByDate(selectedDate As Date) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' DATE() function ensures we only match the day, ignoring the exact time
            Dim query As String = "SELECT id, facility_name, event_name, start_datetime, end_datetime, resident_id 
                                   FROM reservations 
                                   WHERE DATE(start_datetime) = @SelectedDate 
                                   ORDER BY start_datetime"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@SelectedDate", selectedDate.Date)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' 2. CHECK FOR CONFLICTS (Returns True if there is a conflict)
    Public Function HasConflict(facility As String, newStart As DateTime, newEnd As DateTime) As Boolean
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM reservations 
                                   WHERE facility_name = @Facility 
                                   AND (@NewStart < end_datetime) 
                                   AND (@NewEnd > start_datetime)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Facility", facility)
                cmd.Parameters.AddWithValue("@NewStart", newStart)
                cmd.Parameters.AddWithValue("@NewEnd", newEnd)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0 ' If count > 0, conflict exists!
            End Using
        End Using
    End Function

    ' 3. ADD RESERVATION
    Public Sub AddReservation(res As Reservation)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO reservations (facility_name, resident_id, event_name, start_datetime, end_datetime) 
                                   VALUES (@Facility, @ResidentID, @Event, @Start, @End)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Facility", res.FacilityName)
                cmd.Parameters.AddWithValue("@ResidentID", res.ResidentId)
                cmd.Parameters.AddWithValue("@Event", res.EventName)
                cmd.Parameters.AddWithValue("@Start", res.StartDateTime)
                cmd.Parameters.AddWithValue("@End", res.EndDateTime)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class