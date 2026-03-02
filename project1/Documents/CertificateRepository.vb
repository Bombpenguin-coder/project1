Imports MySql.Data.MySqlClient

Public Class CertificateRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOAD DOCUMENT HISTORY
    Public Function GetDocumentHistory() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            ' Join with residents table to get the full name
            Dim query As String = "SELECT c.id, c.control_number, c.certificate_type, c.purpose, c.date_issued, c.resident_id, CONCAT(r.lastname, ', ', r.firstname) AS resident_name 
                                   FROM certificates_issued AS c 
                                   JOIN residents AS r ON c.resident_id = r.id 
                                   ORDER BY c.date_issued DESC"
            Using adapter As New MySqlDataAdapter(query, conn)
                adapter.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    ' 2. GENERATE CONTROL NUMBER (Moved from Main Form)
    Public Function GenerateControlNumber() As String
        Dim currentYear As Integer = Date.Now.Year
        Dim nextId As Integer = 1

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT COUNT(id) FROM certificates_issued WHERE YEAR(date_issued) = @Year"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Year", currentYear)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                        nextId = Convert.ToInt32(result) + 1
                    End If
                End Using
            End Using
            Return $"BRGY-{currentYear}-{nextId.ToString().PadLeft(4, "0")}"
        Catch ex As Exception
            Return $"BRGY-ERR-{Date.Now.Ticks}"
        End Try
    End Function

    ' 3. ISSUE NEW CERTIFICATE
    Public Sub IssueCertificate(cert As Certificate)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO certificates_issued (resident_id, control_number, certificate_type, purpose, amount_paid, issued_by) 
                                   VALUES (@ResidentID, @ControlNumber, @Type, @Purpose, @Amount, @IssuedBy)"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@ResidentID", cert.ResidentId)
                cmd.Parameters.AddWithValue("@ControlNumber", cert.ControlNumber)
                cmd.Parameters.AddWithValue("@Type", cert.CertificateType)
                cmd.Parameters.AddWithValue("@Purpose", cert.Purpose)
                cmd.Parameters.AddWithValue("@Amount", cert.AmountPaid)
                cmd.Parameters.AddWithValue("@IssuedBy", cert.IssuedBy)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class