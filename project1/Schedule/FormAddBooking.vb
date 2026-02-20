Imports MySql.Data.MySqlClient

Public Class FormAddBooking
    ' Tracker for the resident making the booking
    Public Property SelectedResidentId As Integer = 0
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' --- PROPERTIES (Bridges for Main Form) ---
    Public Property FacilityName As String
        Get
            Return cmbFacility.Text
        End Get
        Set(value As String)
            cmbFacility.Text = value
        End Set
    End Property

    Public Property EventName As String
        Get
            Return txtEventName.Text.Trim()
        End Get
        Set(value As String)
            txtEventName.Text = value
        End Set
    End Property

    Public Property StartTime As DateTime
        Get
            Return dtpStartTime.Value
        End Get
        Set(value As DateTime)
            dtpStartTime.Value = value
        End Set
    End Property

    Public Property EndTime As DateTime
        Get
            Return dtpEndTime.Value
        End Get
        Set(value As DateTime)
            dtpEndTime.Value = value
        End Set
    End Property

    ' --- FORM EVENTS ---
    Private Sub FormAddBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If cmbFacility.Items.Count = 0 Then
            cmbFacility.Items.AddRange(New String() {"Barangay Basketball Court", "Multi-Purpose Hall", "Daycare Center", "Barangay Conference Room"})
        End If

        ' This disables past dates on the calendar picker visually!
        dtpStartTime.MinDate = DateTime.Now
        dtpEndTime.MinDate = DateTime.Now

        ' Set default times
        dtpStartTime.Value = DateTime.Now
        dtpEndTime.Value = DateTime.Now.AddHours(1)
        lblSelectedResident.Text = "Selected: (None)"
    End Sub

    ' --- RESIDENT SEARCH LOGIC (Moved here from Main Form) ---
    Private Sub txtResidentSearch_TextChanged(sender As Object, e As EventArgs) Handles txtResidentSearch.TextChanged
        Dim searchTerm As String = txtResidentSearch.Text.Trim()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT id, lastname, firstname FROM residents"
                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    query &= " WHERE CONCAT(lastname, ' ', firstname) LIKE @SearchTerm "
                End If
                query &= " ORDER BY lastname LIMIT 20"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrWhiteSpace(searchTerm) Then
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" & searchTerm & "%")
                    End If
                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dtLookup As New DataTable()
                        adapter.Fill(dtLookup)
                        dgvResidentSearch.DataSource = dtLookup
                        If dgvResidentSearch.Columns.Contains("id") Then dgvResidentSearch.Columns("id").Visible = False
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvResidentSearch_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidentSearch.CellClick
        If e.RowIndex >= 0 AndAlso dgvResidentSearch.CurrentRow IsNot Nothing Then
            SelectedResidentId = CInt(dgvResidentSearch.CurrentRow.Cells("id").Value)
            Dim fullName As String = $"{dgvResidentSearch.CurrentRow.Cells("lastname").Value}, {dgvResidentSearch.CurrentRow.Cells("firstname").Value}"
            lblSelectedResident.Text = $"Selected: {fullName}"
        End If
    End Sub

    ' --- SAVE BUTTON ---
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Basic Validation
        If SelectedResidentId = 0 Then
            MessageBox.Show("Please select a resident.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If cmbFacility.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(EventName) Then
            MessageBox.Show("Please fill in facility and event name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If EndTime <= StartTime Then
            MessageBox.Show("End Time must be after Start Time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If StartTime < DateTime.Now Then
            MessageBox.Show("You cannot book a facility in the past.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If EndTime <= StartTime Then
            MessageBox.Show("End Time must be after Start Time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class