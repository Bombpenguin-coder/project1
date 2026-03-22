Imports MySql.Data.MySqlClient

Public Class ReservationRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOAD RESERVATIONS BY DATE
    Public Function GetReservationsByDate(selectedDate As Date) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Added new columns to the SELECT query
            Dim query As String = "SELECT id, resident_id, is_resident, reserver_name, facility_name, event_name, start_datetime, end_datetime, in_charge " &
                                  "FROM reservations WHERE DATE(start_datetime) = @SelDate ORDER BY start_datetime ASC"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@SelDate", selectedDate.ToString("yyyy-MM-dd"))
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' 2. ADD RESERVATION
    Public Sub AddReservation(res As Reservation)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Added new columns to the INSERT query
            Dim query As String = "INSERT INTO reservations (resident_id, is_resident, reserver_name, facility_name, event_name, start_datetime, end_datetime, in_charge) " &
                                  "VALUES (@ResId, @IsRes, @ResName, @Facility, @Event, @Start, @End, @InCharge)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ResId", res.ResidentId)
                cmd.Parameters.AddWithValue("@IsRes", res.IsResident)
                cmd.Parameters.AddWithValue("@ResName", res.ReserverName)
                cmd.Parameters.AddWithValue("@Facility", res.FacilityName)
                cmd.Parameters.AddWithValue("@Event", res.EventName)
                cmd.Parameters.AddWithValue("@Start", res.StartDateTime)
                cmd.Parameters.AddWithValue("@End", res.EndDateTime)
                cmd.Parameters.AddWithValue("@InCharge", res.InCharge)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 3. CONFLICT CHECKER (Remains exactly the same)
    Public Function HasConflict(facility As String, proposedStart As DateTime, proposedEnd As DateTime) As Boolean
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT COUNT(*) FROM reservations WHERE facility_name = @Facility " &
                                  "AND ((start_datetime < @End AND end_datetime > @Start))"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Facility", facility)
                cmd.Parameters.AddWithValue("@Start", proposedStart)
                cmd.Parameters.AddWithValue("@End", proposedEnd)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function
End Class