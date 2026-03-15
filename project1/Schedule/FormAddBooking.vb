Imports MySql.Data.MySqlClient

Public Class FormAddBooking
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    Public Property SelectedResidentId As Integer = 0

    ' --- NEW PROPERTIES ---
    Public ReadOnly Property IsResident As Boolean
        Get
            Return rdoResident.Checked
        End Get
    End Property

    Public ReadOnly Property ReserverName As String
        Get
            Return txtReserverName.Text.Trim()
        End Get
    End Property

    Public ReadOnly Property InCharge As String
        Get
            Return txtInCharge.Text.Trim()
        End Get
    End Property

    ' --- EXISTING PROPERTIES ---
    Public ReadOnly Property FacilityName As String
        Get
            Return cmbFacility.Text
        End Get
    End Property

    Public ReadOnly Property EventName As String
        Get
            Return txtEventName.Text.Trim()
        End Get
    End Property

    Public ReadOnly Property StartTime As DateTime
        Get
            Return dtpStartTime.Value
        End Get
    End Property

    Public ReadOnly Property EndTime As DateTime
        Get
            Return dtpEndTime.Value
        End Get
    End Property

    ' --- FORM EVENTS ---
    Private Sub FormAddBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- LOAD DYNAMIC FACILITIES FROM DATABASE ---
        Try
            Dim repo As New LookupRepository()
            cmbFacility.DataSource = repo.GetItemsByCategory("Facility")
            cmbFacility.DisplayMember = "item_value"
            cmbFacility.DropDownStyle = ComboBoxStyle.DropDownList
        Catch ex As Exception
            MessageBox.Show("Error loading facilities: " & ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Set up Date Time Pickers
        dtpStartTime.Format = DateTimePickerFormat.Custom
        dtpStartTime.CustomFormat = "MM/dd/yyyy hh:mm tt"
        dtpEndTime.Format = DateTimePickerFormat.Custom
        dtpEndTime.CustomFormat = "MM/dd/yyyy hh:mm tt"

        rdoResident.Checked = True ' Default to Resident
        LoadResidentsForLookup()
    End Sub

    ' --- SMART RADIO BUTTON LOGIC ---
    Private Sub rdoResident_CheckedChanged(sender As Object, e As EventArgs) Handles rdoResident.CheckedChanged, rdoNonResident.CheckedChanged
        If rdoResident.Checked Then
            ' Resident Mode: Enable search, disable manual name typing
            txtResidentSearch.Enabled = True
            dgvResidentLookup.Enabled = True
            txtReserverName.ReadOnly = True
            lblSelectedResident.Text = "Selected Resident: (None)"
        Else
            ' Non-Resident Mode: Disable search, enable manual name typing
            txtResidentSearch.Enabled = False
            dgvResidentLookup.Enabled = False
            txtReserverName.ReadOnly = False

            ' Reset resident info
            SelectedResidentId = 0
            txtResidentSearch.Clear()
            txtReserverName.Clear()
            lblSelectedResident.Text = "Selected Resident: N/A (Non-Resident)"
        End If
    End Sub

    ' --- RESIDENT SEARCH LOGIC ---
    Private Sub LoadResidentsForLookup(Optional searchTerm As String = "")
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT id, lastname, firstname, middlename FROM residents"
                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    query &= " WHERE CONCAT(lastname, ' ', firstname, ' ', middlename) LIKE @SearchTerm"
                End If
                query &= " ORDER BY lastname, firstname LIMIT 50"

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrWhiteSpace(searchTerm) Then
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" & searchTerm & "%")
                    End If
                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dtLookup As New DataTable()
                        adapter.Fill(dtLookup)
                        dgvResidentLookup.DataSource = dtLookup

                        If dgvResidentLookup.Columns.Contains("id") Then dgvResidentLookup.Columns("id").Visible = False
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message)
        End Try
    End Sub

    Private Sub txtResidentSearch_TextChanged(sender As Object, e As EventArgs) Handles txtResidentSearch.TextChanged
        LoadResidentsForLookup(txtResidentSearch.Text.Trim())
    End Sub

    Private Sub dgvResidentLookup_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidentLookup.CellClick
        If rdoResident.Checked AndAlso e.RowIndex >= 0 AndAlso dgvResidentLookup.CurrentRow IsNot Nothing Then
            Dim row = dgvResidentLookup.CurrentRow
            SelectedResidentId = CInt(row.Cells("id").Value)

            ' Format the name and AUTOFILL IT!
            Dim fullName As String = $"{row.Cells("firstname").Value} {row.Cells("lastname").Value}"
            txtReserverName.Text = fullName

            lblSelectedResident.Text = $"Selected Resident: {fullName} (ID: {SelectedResidentId})"
        End If
    End Sub

    ' --- BUTTONS & VALIDATION ---
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' New Validations!
        If IsResident AndAlso SelectedResidentId = 0 Then
            MessageBox.Show("Please search and select a resident from the grid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not IsResident AndAlso String.IsNullOrWhiteSpace(ReserverName) Then
            MessageBox.Show("Please manually enter the name of the Non-Resident reserver.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(InCharge) Then
            MessageBox.Show("Please indicate who is In-Charge of this reservation.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(FacilityName) OrElse String.IsNullOrWhiteSpace(EventName) Then
            MessageBox.Show("Please fill in the Facility and Event Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If StartTime >= EndTime Then
            MessageBox.Show("End time must be after the start time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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