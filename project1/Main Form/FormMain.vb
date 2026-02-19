Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class FormMain

    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"
    Public ResidentsTable As New DataTable()
    Private _selectedResidentIdForIssuance As Integer = 0
    Private _currentUserRole As String = "" ' Stores the logged-in user's role
    Private _selectedResidentIdForSchedule As Integer = 0
    Private _currentUserFullname As String = ""
    Private _currentHistoryId As Integer = 0
    Private _currentUsername As String = ""

    ' This is the Constructor. It runs BEFORE FormMain_Load.
    ' It receives the role from Form1.

    ' Add 'ByVal historyId As Integer' to the end
    Public Sub New(ByVal role As String, ByVal fullname As String, ByVal historyId As Integer, ByVal username As String)
        InitializeComponent()

        _currentUserRole = role
        _currentUserFullname = fullname
        _currentHistoryId = historyId
        _currentUsername = username
        lblUserInfo.Text = $"{_currentUserFullname} ({_currentUserRole})"
    End Sub


    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyRolePermissions()
        HideAllPanels()
        pnlDashboard.Visible = True
        LoadDashboardData()
    End Sub

    Private Sub HideAllPanels()
        pnlDashboard.Visible = False
        pnlResidents.Visible = False
        pnlLoginHistory.Visible = False
        pnlAddUsers.Visible = False
        pnlDocuments.Visible = False
        pnlSchedule.Visible = False
        pnlOfficials.Visible = False
        pnlBlotter.Visible = False
    End Sub
    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        HideAllPanels()
        pnlDashboard.Visible = True

        ' Call the new function to load all dashboard stats
        LoadDashboardData()
    End Sub

    Private Sub btnResidents_Click(sender As Object, e As EventArgs) Handles btnResidents.Click
        HideAllPanels()
        pnlResidents.Visible = True
        ' Initial Load: The optional parameter defaults to an empty string, loading ALL residents.
        LoadResidentsFromDatabase()
    End Sub

    Private Sub btnLoginHistory_Click(sender As Object, e As EventArgs) Handles btnLoginHistory.Click
        HideAllPanels()
        pnlLoginHistory.Visible = True
    End Sub

    Private Sub btnAddUsers_Click(sender As Object, e As EventArgs) Handles btnAddUsers.Click
        HideAllPanels()
        pnlAddUsers.Visible = True

        ' Populate the Role ComboBox
        cmbUserRole.Items.Clear()
        cmbUserRole.Items.AddRange(New String() {"Admin", "Technician", "Staff"})
        cmbUserRole.SelectedIndex = 0 ' Default to "Admin"

        ' Load all users into the grid
        LoadUsers()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim result As DialogResult = MessageBox.Show(
        "Are you sure you want to log out?",
        "Confirm Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    )

        If result <> DialogResult.Yes Then
            Return
        End If


        Try
            ' Only update logout time if we have a valid history ID
            If _currentHistoryId > 0 Then
                Using logoutConn As New MySqlConnection(loginHistoryConnectionString)
                    logoutConn.Open()
                    ' Update the record with the current time
                    Dim query As String = "UPDATE login_history SET logout_time = NOW() WHERE id = @id"
                    Using cmd As New MySqlCommand(query, logoutConn)
                        cmd.Parameters.AddWithValue("@id", _currentHistoryId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If
        Catch ex As Exception
            ' Show an error, but don't stop the user from logging out
            MessageBox.Show("Could not update logout time: " & ex.Message, "Logout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        Dim loginForm As Form1 = Nothing
        For Each f As Form In Application.OpenForms
            If TypeOf f Is Form1 Then
                loginForm = CType(f, Form1)
                Exit For
            End If
        Next

        If loginForm Is Nothing Then
            loginForm = New Form1()
            loginForm.Show()
        Else
            loginForm.Show()
            loginForm.PrepareForLogin()
        End If

        Me.Close()
    End Sub
    Public Sub UpdateResidentCount()
        lblTotalResidents.Text = ResidentsTable.Rows.Count.ToString()
    End Sub

    Private Sub btnAddResident_Click(sender As Object, e As EventArgs) Handles btnAddResident.Click
        Dim addForm As New FormAddResidents()

        ' Inside btnAddResident_Click...
        If addForm.ShowDialog() = DialogResult.OK Then
            Try
                ' 1. Bundle the data into our new Model
                Dim newResident As New Resident()
                newResident.LastName = addForm.LastName
                newResident.FirstName = addForm.FirstName
                newResident.MiddleName = addForm.MiddleName
                newResident.Age = addForm.Age
                newResident.Gender = addForm.Gender
                newResident.Address = addForm.Address
                newResident.District = addForm.District
                newResident.Barangay = addForm.Barangay
                newResident.City = addForm.City

                ' 2. Send it to the Repository
                Dim repo As New ResidentRepository()
                repo.AddResident(newResident)

                ' 3. Success!
                LoadResidentsFromDatabase()
                MessageBox.Show("Resident added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Error adding resident: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnDeleteResident_Click(sender As Object, e As EventArgs) Handles btnDeleteResident.Click
        If dgvResidents.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a resident to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedId As Integer = dgvResidents.CurrentRow.Cells("id").Value
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this resident?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Try
                ' 1. Call the Repository
                Dim repo As New ResidentRepository()
                repo.DeleteResident(selectedId)

                ' 2. Refresh UI
                MessageBox.Show("Resident deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadResidentsFromDatabase()

            Catch ex As Exception
                MessageBox.Show("Error deleting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnEditResident_Click(sender As Object, e As EventArgs) Handles btnEditResident.Click
        If dgvResidents.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a resident to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedId As Integer = CInt(dgvResidents.CurrentRow.Cells("id").Value)
        Dim editForm As New FormAddResidents()

        ' 1. CRITICAL: Set the ResidentID (to put the form in EDIT mode)
        editForm.ResidentID = selectedId

        ' 2. Populate non-date fields
        editForm.txtLastName.Text = dgvResidents.CurrentRow.Cells("lastname").Value.ToString()
        editForm.txtFirstName.Text = dgvResidents.CurrentRow.Cells("firstname").Value.ToString()
        editForm.txtMiddleName.Text = dgvResidents.CurrentRow.Cells("middlename").Value.ToString()

        editForm.cmbGender.Text = dgvResidents.CurrentRow.Cells("gender").Value.ToString()
        editForm.txtAddress.Text = dgvResidents.CurrentRow.Cells("address").Value.ToString()
        editForm.txtDistrict.Text = dgvResidents.CurrentRow.Cells("district").Value.ToString()
        editForm.cmbBarangay.Text = dgvResidents.CurrentRow.Cells("barangay").Value.ToString()
        editForm.txtCity.Text = dgvResidents.CurrentRow.Cells("city").Value.ToString()

        ' 3. ROBUST DATE POPULATION (Fixes the ArgumentOutOfRangeException)
        Dim dgvCellValue = dgvResidents.CurrentRow.Cells("birthdate").Value
        Dim defaultSafeDate As Date = New Date(2000, 1, 1) ' A safe, historically distant default
        Dim residentBirthDate As Date

        ' Attempt to retrieve the date from the DataGridView
        If dgvCellValue IsNot DBNull.Value AndAlso IsDate(dgvCellValue) Then
            residentBirthDate = CDate(dgvCellValue)
        Else
            residentBirthDate = defaultSafeDate
        End If

        ' Validate the date against the DateTimePicker's MaxDate and MinDate
        If residentBirthDate > editForm.dtpBirthDate.MaxDate Then
            ' If the date is in the future or beyond MaxDate, set it to the maximum allowed (usually Date.Today)
            editForm.dtpBirthDate.Value = editForm.dtpBirthDate.MaxDate
        ElseIf residentBirthDate < editForm.dtpBirthDate.MinDate Then
            ' If the date is too old (before 1753), set it to the minimum allowed
            editForm.dtpBirthDate.Value = editForm.dtpBirthDate.MinDate
        Else
            ' The date is valid, set it directly
            editForm.dtpBirthDate.Value = residentBirthDate
        End If
        ' ---------------------------------------------------------------------

        ' 4. Show the form and execute update
        If editForm.ShowDialog() = DialogResult.OK Then
            Try
                ' 1. Pack the data
                Dim updatedRes As New Resident()
                updatedRes.Id = selectedId
                updatedRes.LastName = editForm.LastName
                updatedRes.FirstName = editForm.FirstName
                updatedRes.MiddleName = editForm.MiddleName
                updatedRes.BirthDate = editForm.BirthDate
                updatedRes.Age = editForm.Age
                updatedRes.Gender = editForm.Gender
                updatedRes.Address = editForm.Address
                updatedRes.District = editForm.District
                updatedRes.Barangay = editForm.Barangay
                updatedRes.City = editForm.City

                ' 2. Call the Repository
                Dim repo As New ResidentRepository()
                repo.UpdateResident(updatedRes)

                ' 3. Refresh UI
                MessageBox.Show("Resident updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadResidentsFromDatabase()

            Catch ex As Exception
                MessageBox.Show("Error updating: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub LoadResidentsFromDatabase(Optional searchTerm As String = "")
        Try
            ' 1. Create the worker
            Dim repo As New ResidentRepository()

            ' 2. Ask the worker for data (No SQL here!)
            ResidentsTable = repo.GetAllResidents(searchTerm)

            ' 3. Update the UI
            dgvResidents.DataSource = ResidentsTable

            ' Re-apply column formatting after binding the new filtered data
            If dgvResidents.Columns.Contains("id") Then dgvResidents.Columns("id").Visible = False
            If dgvResidents.Columns.Contains("lastname") Then dgvResidents.Columns("lastname").HeaderText = "Last Name"
            If dgvResidents.Columns.Contains("firstname") Then dgvResidents.Columns("firstname").HeaderText = "First Name"
            If dgvResidents.Columns.Contains("middlename") Then dgvResidents.Columns("middlename").HeaderText = "Middle Name"
            If dgvResidents.Columns.Contains("age") Then dgvResidents.Columns("age").HeaderText = "Age"
            If dgvResidents.Columns.Contains("gender") Then dgvResidents.Columns("gender").HeaderText = "Gender"
            If dgvResidents.Columns.Contains("address") Then dgvResidents.Columns("address").HeaderText = "Address"
            If dgvResidents.Columns.Contains("district") Then dgvResidents.Columns("district").HeaderText = "District"
            If dgvResidents.Columns.Contains("barangay") Then dgvResidents.Columns("barangay").HeaderText = "Barangay"
            If dgvResidents.Columns.Contains("city") Then dgvResidents.Columns("city").HeaderText = "City"

            UpdateResidentCount()
        Catch ex As Exception
            MessageBox.Show("Error loading residents: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormOfficials_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadOfficialsData()
    End Sub

    Private Sub LoadOfficialsData()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()


                Dim query As String = "SELECT fullname, position, contactnumber FROM officials"


                Using cmd As New MySqlCommand(query, conn)
                    Dim adapter As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    adapter.Fill(table)


                    dgvOfficials.DataSource = table
                End Using
            End Using


            If dgvOfficials.Columns.Contains("fullname") Then dgvOfficials.Columns("fullname").HeaderText = "Full Name"
            If dgvOfficials.Columns.Contains("position") Then dgvOfficials.Columns("position").HeaderText = "Position"
            If dgvOfficials.Columns.Contains("contactnumber") Then dgvOfficials.Columns("contactnumber").HeaderText = "Contact"

            dgvOfficials.ReadOnly = True
            dgvOfficials.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgvOfficials.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Catch ex As Exception
            MessageBox.Show("Error loading dashboard officials: " & ex.Message)
        End Try
    End Sub

    Private usersConnectionString As String = "server=localhost;user id=root;password=;database=login_db;"
    Private loginHistoryConnectionString As String = "server=localhost;user id=root;password=;database=login_db;"

    Private Sub FormLoginHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLoginHistoryData()
    End Sub

    Private Sub LoadLoginHistoryData()
        Try
            Dim dt As New DataTable()

            ' 1. Start building the SQL Query
            ' We join login_history (aliased 'lh') with users (aliased 'u')
            Dim query As String = "
            SELECT 
                u.fullname, 
                lh.login_time, 
                lh.logout_time 
            FROM login_history AS lh
            JOIN users AS u ON lh.username = u.username
        "

            Using conn As New MySqlConnection(loginHistoryConnectionString) ' Use login_db for both
                conn.Open()

                Using cmd As New MySqlCommand() ' Create an empty command

                    ' 2. Apply Role-Based Filtering
                    Select Case _currentUserRole
                        Case "Admin", "Technician"
                        ' These roles see everyone. No filter needed.
                        ' The query is complete as is.
                        Case "Staff", "Users"
                            ' These roles only see themselves.
                            ' We add a WHERE clause to filter by their username.
                            query &= " WHERE lh.username = @Username"
                            cmd.Parameters.AddWithValue("@Username", _currentUsername)
                        Case Else
                            ' Unknown role sees nothing
                            query &= " WHERE 1 = 0" ' (This is a trick to return no rows)
                    End Select

                    ' 3. Add ordering
                    query &= " ORDER BY lh.login_time DESC"

                    ' 4. Set command properties and fill the table
                    cmd.Connection = conn
                    cmd.CommandText = query
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            ' 5. Format the data for the grid
            ' (This part handles the "(Still Logged In)" text)
            Dim formattedTable As New DataTable()
            formattedTable.Columns.Add("Fullname")
            formattedTable.Columns.Add("Login Time")
            formattedTable.Columns.Add("Logout Time")

            For Each row As DataRow In dt.Rows
                Dim fullname As String = row("fullname").ToString()
                Dim loginTime As String = CDate(row("login_time")).ToString() ' Format to local
                Dim logoutTimeObject As Object = row("logout_time")
                Dim formattedLogoutTime As String = ""

                If logoutTimeObject Is DBNull.Value Then
                    formattedLogoutTime = "(Still Logged In)"
                Else
                    formattedLogoutTime = CDate(logoutTimeObject).ToString()
                End If

                formattedTable.Rows.Add(fullname, loginTime, formattedLogoutTime)
            Next

            ' 6. Bind the final formatted table to the grid
            dgvLoginHistory.DataSource = formattedTable

            ' (Your existing formatting code...)
            dgvLoginHistory.ReadOnly = True
            dgvLoginHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgvLoginHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Catch ex As Exception
            MessageBox.Show("Error loading login history: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvResidents_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidents.CellContentClick

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Pass the current text in the search box to the loading function.
        ' This updates the DataGridView instantly as the user types.
        LoadResidentsFromDatabase(txtSearch.Text.Trim())
    End Sub

    Private Sub btnDocuments_Click(sender As Object, e As EventArgs) Handles btnDocuments.Click
        HideAllPanels()
        pnlDocuments.Visible = True
        LoadDocumentHistory()


        ' 1. Define the array of certificate types
        Dim certificateTypes As String() = {
        "Barangay Clearance",
        "Certificate of Indigency",
        "Certificate of Residency"
    }

        ' 2. Clear the ComboBox and add the array
        cmbCertificateType.Items.Clear()
        cmbCertificateType.Items.AddRange(certificateTypes)

        ' 3. (Most Important) Set the style to prevent typing
        ' This forces the user to pick from the list
        cmbCertificateType.DropDownStyle = ComboBoxStyle.DropDownList

        ' 4. Set a default selection so it's not blank
        If cmbCertificateType.Items.Count > 0 Then
            cmbCertificateType.SelectedIndex = 0 ' Selects "Barangay Clearance"
        End If

        ' Call the load function to populate the grid initially
        LoadResidentsForLookup()

        ' Reset selection when panel opens
        _selectedResidentIdForIssuance = 0
        lblSelectedResident.Text = "Selected Resident: (None)"
    End Sub

    Private Sub txtAmountPaid_TextChanged(sender As Object, e As EventArgs) Handles txtAmountPaid.TextChanged

    End Sub

    Private Sub txtResidentSearch_TextChanged(sender As Object, e As EventArgs) Handles txtResidentSearch.TextChanged
        ' Call a new function to load residents into our lookup grid, passing the search term
        LoadResidentsForLookup(txtResidentSearch.Text.Trim())
    End Sub

    Private Sub LoadResidentsForLookup(Optional searchTerm As String = "")
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' 1. Select only essential columns for lookup
                Dim query As String = "SELECT id, lastname, firstname, middlename, address, barangay FROM residents"

                ' 2. Apply search filter
                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    query &= " WHERE CONCAT(lastname, ' ', firstname, ' ', middlename) LIKE @SearchTerm "
                    query &= " OR address LIKE @SearchTerm "
                End If

                query &= " ORDER BY lastname, firstname LIMIT 50" ' Limit results for speed

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrWhiteSpace(searchTerm) Then
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" & searchTerm & "%")
                    End If

                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dtLookup As New DataTable()
                        adapter.Fill(dtLookup)

                        ' Bind to the NEW DataGridView in pnlDocuments
                        dgvResidentLookup.DataSource = dtLookup

                        ' Formatting the lookup grid
                        If dgvResidentLookup.Columns.Contains("id") Then dgvResidentLookup.Columns("id").Visible = False
                        If dgvResidentLookup.Columns.Contains("lastname") Then dgvResidentLookup.Columns("lastname").HeaderText = "Last Name"
                        If dgvResidentLookup.Columns.Contains("firstname") Then dgvResidentLookup.Columns("firstname").HeaderText = "First Name"
                        If dgvResidentLookup.Columns.Contains("middlename") Then dgvResidentLookup.Columns("middlename").Visible = False
                        If dgvResidentLookup.Columns.Contains("address") Then dgvResidentLookup.Columns("address").Width = 200
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading resident lookup: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvResidentLookup_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResidentLookup.CellClick
        If e.RowIndex >= 0 AndAlso dgvResidentLookup.CurrentRow IsNot Nothing Then
            Dim selectedRow = dgvResidentLookup.CurrentRow

            ' 1. Store the selected resident's ID
            _selectedResidentIdForIssuance = CInt(selectedRow.Cells("id").Value)

            ' 2. Update the label to confirm selection
            Dim fullName As String = $"{selectedRow.Cells("lastname").Value}, {selectedRow.Cells("firstname").Value}"
            lblSelectedResident.Text = $"Selected Resident: {fullName} (ID: {_selectedResidentIdForIssuance})"

            ' 3. (Optional) Clear other fields to prepare for new issuance
            ' txtPurpose.Clear()
            ' txtAmountPaid.Text = "0.00"
            ' cmbCertificateType.SelectedIndex = 0
        End If
    End Sub
    Private Function GenerateControlNumber() As String
        Try
            Dim currentYear As Integer = Date.Now.Year
            Dim nextId As Integer = 1 ' Default to 1

            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Query to count how many certificates were issued THIS YEAR
                ' We add 1 to get the next sequential number
                Dim query As String = "SELECT COUNT(id) FROM certificates_issued WHERE YEAR(date_issued) = @Year"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Year", currentYear)

                    ' ExecuteScalar retrieves the single count value
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                        ' Add 1 to the current count for the new ID
                        nextId = Convert.ToInt32(result) + 1
                    End If
                End Using
            End Using

            ' Format: BRGY-YYYY-NNNN (e.g., BRGY-2025-0001)
            ' .PadLeft(4, "0") ensures the number is 4 digits (0001, 0002, ... 0010, ... 0100)
            Return $"BRGY-{currentYear}-{nextId.ToString().PadLeft(4, "0")}"

        Catch ex As Exception
            MessageBox.Show("Error generating control number: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' Return a basic timestamp as a fallback to avoid errors
            Return $"BRGY-ERR-{Date.Now.Ticks}"
        End Try
    End Function

    Private Sub btnIssueSave_Click(sender As Object, e As EventArgs) Handles btnIssueSave.Click

        ' --- 1. VALIDATION ---
        If _selectedResidentIdForIssuance = 0 Then
            MessageBox.Show("Please search and select a resident first.", "No Resident Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(cmbCertificateType.Text) Then
            MessageBox.Show("Please select a certificate type.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbCertificateType.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtPurpose.Text) Then
            MessageBox.Show("Please enter the purpose for this document.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPurpose.Focus()
            Return
        End If

        ' Validate amount (must be a valid number)
        Dim amount As Decimal = 0.0
        If Not Decimal.TryParse(txtAmountPaid.Text, amount) Then
            MessageBox.Show("Please enter a valid amount (e.g., 50.00 or 0).", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmountPaid.Focus()
            Return
        End If

        ' --- 2. GENERATE CONTROL NUMBER ---
        Dim newControlNumber As String = GenerateControlNumber()
        txtControlNumber.Text = newControlNumber ' Display it on the form

        ' --- 3. SAVE TO DATABASE ---
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' Note: date_issued defaults to CURRENT_TIMESTAMP in the DB, so we don't need to specify it.
                ' We assume 'issued_by' is the current logged-in user (we'll add this later)
                Dim query As String = "
                INSERT INTO certificates_issued 
                (resident_id, control_number, certificate_type, purpose, amount_paid, issued_by) 
                VALUES 
                (@ResidentID, @ControlNumber, @CertificateType, @Purpose, @AmountPaid, @IssuedBy)
            "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ResidentID", _selectedResidentIdForIssuance)
                    cmd.Parameters.AddWithValue("@ControlNumber", newControlNumber)
                    cmd.Parameters.AddWithValue("@CertificateType", cmbCertificateType.Text)
                    cmd.Parameters.AddWithValue("@Purpose", txtPurpose.Text.Trim())
                    cmd.Parameters.AddWithValue("@AmountPaid", amount)

                    ' TODO: Replace "admin" with the actual logged-in user's name
                    cmd.Parameters.AddWithValue("@IssuedBy", "admin")

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show($"Certificate issued successfully!{vbCrLf}Control Number: {newControlNumber}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear fields for the next issuance
            ResetDocumentPanel()
            LoadDocumentHistory()

        Catch ex As Exception
            MessageBox.Show("Error saving certificate: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper function to clear the form
    Private Sub ResetDocumentPanel()
        _selectedResidentIdForIssuance = 0
        lblSelectedResident.Text = "Selected Resident: (None)"
        txtResidentSearch.Clear()
        txtPurpose.Clear()
        txtAmountPaid.Text = "0.00"
        txtControlNumber.Clear()
        cmbCertificateType.SelectedIndex = -1 ' Clear selection
        LoadResidentsForLookup() ' Refresh the lookup grid
    End Sub



    Private Sub LoadDashboardData()
        Dim residentCount As Integer = 0
        Dim officialCount As Integer = 0
        Dim reportsCount As Integer = 0

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' 1. Get Total Residents
                Dim queryResidents As String = "SELECT COUNT(id) FROM residents"
                Using cmd As New MySqlCommand(queryResidents, conn)
                    residentCount = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                ' 2. Get Total Officials
                Dim queryOfficials As String = "SELECT COUNT(id) FROM officials" ' Assumes 'officials' table has an 'id'
                Using cmd As New MySqlCommand(queryOfficials, conn)
                    officialCount = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

                ' 3. Get Total Reports Generated (from certificates_issued table)
                Dim queryReports As String = "SELECT COUNT(id) FROM certificates_issued"
                Using cmd As New MySqlCommand(queryReports, conn)
                    reportsCount = Convert.ToInt32(cmd.ExecuteScalar())
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading dashboard data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' 4. Update the Dashboard Labels

        lblTotalResidents.Text = residentCount.ToString()
        lblTotalOfficials.Text = officialCount.ToString()
        lblReportsGenerated.Text = reportsCount.ToString()

        ' 5. Load the Officials DataGridView

        LoadOfficialsData()
    End Sub

    Private Sub ApplyRolePermissions()
        ' -----------------------------------------------------------------
        ' First, set the "default" state:
        ' Enable all buttons, then we will disable them based on role.
        ' -----------------------------------------------------------------

        ' Resident Panel buttons
        btnAddResident.Enabled = True
        btnEditResident.Enabled = True
        btnDeleteResident.Enabled = True

        ' Document Panel buttons
        btnIssueSave.Enabled = True
        btnPrintPreview.Enabled = True

        ' Schedule Panel buttons
        btnSaveBooking.Enabled = True

        ' Panel Menu buttons (we use .Visible for these)
        btnResidents.Visible = True
        btnDocuments.Visible = True
        btnSchedule.Visible = True
        btnOfficials.Visible = True ' (Your officials management button)
        pnlAddUsers.Visible = True  ' (Your user management button)

        ' -----------------------------------------------------------------
        ' Now, apply restrictions based on the logged-in user's role
        ' -----------------------------------------------------------------
        Select Case _currentUserRole

            Case "Admin", "Technician"
                ' --- ADMIN / TECHNICIAN ---
                ' They can do everything. No restrictions needed.
                ' We'll just rename the User Management button for clarity
                btnAddUsers.Text = "User Maintenance" ' (If you're still using btnReports)

            Case "Staff"
                ' --- STAFF ---
                ' Can do daily work, but no admin tasks.

                ' 1. Hide Admin-Only Panels:
                btnOfficials.Visible = False  ' Cannot manage officials
                pnlAddUsers.Visible = False ' Cannot manage users

                ' 2. (Optional) Disable delete buttons (safer for staff)
                btnDeleteResident.Enabled = False

            Case Else
                ' --- UNKNOWN ROLE ---
                ' Default to "View Only" for security
                btnAddResident.Enabled = False
                btnEditResident.Enabled = False
                btnDeleteResident.Enabled = False
                btnIssueSave.Enabled = False
                btnPrintPreview.Enabled = False
                btnSaveBooking.Enabled = False

                btnOfficials.Visible = False
                pnlAddUsers.Visible = False

                btnResidents.Visible = False
                btnDocuments.Visible = False
                btnSchedule.Visible = False
                btnBlotter.Visible = False

        End Select

        ' This renames your user panel button if it's still named btnReports
        ' This line should be *after* the Select Case
        If pnlAddUsers.Visible = True Then
            btnAddUsers.Text = "User Maintenance"
        Else
            ' If the panel is hidden, we might as well hide the button too
            btnAddUsers.Visible = False
        End If

    End Sub

    Private Sub LoadUsers()
        Try
            Using conn As New MySqlConnection(usersConnectionString)
                conn.Open()

                ' Select all users BUT hide the password column from the grid
                Dim query As String = "SELECT id, fullname, username, role FROM users"
                Using adapter As New MySqlDataAdapter(query, conn)
                    Dim dtUsers As New DataTable()
                    adapter.Fill(dtUsers)
                    dgvUsers.DataSource = dtUsers

                    ' Format the grid
                    If dgvUsers.Columns.Contains("id") Then dgvUsers.Columns("id").Visible = False
                    If dgvUsers.Columns.Contains("fullname") Then dgvUsers.Columns("fullname").HeaderText = "Full Name"
                    If dgvUsers.Columns.Contains("username") Then dgvUsers.Columns("username").HeaderText = "Username"
                    If dgvUsers.Columns.Contains("role") Then dgvUsers.Columns("role").HeaderText = "Role"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading users: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellClick
        If e.RowIndex >= 0 AndAlso dgvUsers.CurrentRow IsNot Nothing Then
            ' Get the selected row
            Dim selectedRow = dgvUsers.CurrentRow

            ' Populate the textboxes
            txtUserFullname.Text = selectedRow.Cells("fullname").Value.ToString()
            txtUserUsername.Text = selectedRow.Cells("username").Value.ToString()
            cmbUserRole.Text = selectedRow.Cells("role").Value.ToString()

            ' We don't load the password from the DB for security. 
            ' Set it to blank, implying "leave unchanged" or "set new password".
            txtUserPassword.Clear()
            txtUserPassword.PlaceholderText = "Enter new password to change"
        End If
    End Sub

    Private Function HashPassword(ByVal password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            ' Compute the hash from the password bytes
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))

            ' Convert the byte array to a hexadecimal string
            Dim builder As New StringBuilder()


            For i As Integer = 0 To bytes.Length - 1
                builder.Append(bytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click

        ' 1. VALIDATION (Check for empty fields)
        If String.IsNullOrWhiteSpace(txtUserFullname.Text) OrElse
       String.IsNullOrWhiteSpace(txtUserUsername.Text) OrElse
       String.IsNullOrWhiteSpace(txtUserPassword.Text) OrElse
       String.IsNullOrWhiteSpace(cmbUserRole.Text) Then

            MessageBox.Show("Please fill in all fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. PREPARE DATA
        Dim fullname = txtUserFullname.Text.Trim()
        Dim username = txtUserUsername.Text.Trim()
        Dim role = cmbUserRole.Text

        ' 3. HASH THE PASSWORD
        Dim hashedPassword As String = HashPassword(txtUserPassword.Text)

        ' 4. SAVE TO DATABASE
        Try
            Using conn As New MySqlConnection(usersConnectionString)
                conn.Open()

                ' A. Check for duplicate username first
                Dim queryCheck As String = "SELECT COUNT(*) FROM users WHERE username = @Username"
                Using cmdCheck As New MySqlCommand(queryCheck, conn)
                    cmdCheck.Parameters.AddWithValue("@Username", username)
                    Dim userCount As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                    If userCount > 0 Then
                        MessageBox.Show("This username already exists. Please choose another one.", "Duplicate User", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End Using

                ' B. Insert the new user
                Dim queryInsert As String = "
                INSERT INTO users (fullname, username, password, role) 
                VALUES (@Fullname, @Username, @Password, @Role)
            "
                Using cmd As New MySqlCommand(queryInsert, conn)
                    cmd.Parameters.AddWithValue("@Fullname", fullname)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Password", hashedPassword) ' Save the hashed password
                    cmd.Parameters.AddWithValue("@Role", role)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 5. FEEDBACK AND CLEANUP
            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadUsers()     ' Refresh the grid
            ClearUserForm() ' Clear the textboxes

        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub ClearUserForm()
        txtUserFullname.Clear()
        txtUserUsername.Clear()
        txtUserPassword.Clear()
        txtUserPassword.PlaceholderText = ""
        cmbUserRole.SelectedIndex = 0
        dgvUsers.ClearSelection()
    End Sub

    Private Sub btnUpdateUser_Click(sender As Object, e As EventArgs) Handles btnUpdateUser.Click

        ' 1. VALIDATION (Check for selection)
        If dgvUsers.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a user from the grid to update.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Get the ID of the selected user
        Dim selectedId As Integer = CInt(dgvUsers.CurrentRow.Cells("id").Value)

        ' 2. VALIDATION (Check for empty fields)
        If String.IsNullOrWhiteSpace(txtUserFullname.Text) OrElse
       String.IsNullOrWhiteSpace(txtUserUsername.Text) OrElse
       String.IsNullOrWhiteSpace(cmbUserRole.Text) Then

            MessageBox.Show("Please fill in all fields (Fullname, Username, and Role).", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 3. PREPARE DATA
        Dim fullname = txtUserFullname.Text.Trim()
        Dim username = txtUserUsername.Text.Trim()
        Dim role = cmbUserRole.Text

        ' 4. PREPARE DATABASE COMMAND
        Try
            Using conn As New MySqlConnection(usersConnectionString)
                conn.Open()

                Dim query As String = ""
                Dim passwordWasUpdated As Boolean = False


                If String.IsNullOrWhiteSpace(txtUserPassword.Text) Then
                    ' A. Password box is EMPTY. Do NOT update the password.
                    query = "
                    UPDATE users SET 
                        fullname = @Fullname, 
                        username = @Username, 
                        role = @Role 
                    WHERE id = @id
                "
                    passwordWasUpdated = False
                Else
                    ' B. Password box has text. Update the password.
                    query = "
                    UPDATE users SET 
                        fullname = @Fullname, 
                        username = @Username, 
                        role = @Role, 
                        password = @Password 
                    WHERE id = @id
                "
                    passwordWasUpdated = True
                End If


                Using cmd As New MySqlCommand(query, conn)
                    ' Add the parameters that are common to both queries
                    cmd.Parameters.AddWithValue("@Fullname", fullname)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Role", role)
                    cmd.Parameters.AddWithValue("@id", selectedId)

                    ' Add the password parameter ONLY if it was updated
                    If passwordWasUpdated Then
                        Dim hashedPassword As String = HashPassword(txtUserPassword.Text)
                        cmd.Parameters.AddWithValue("@Password", hashedPassword)
                    End If

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 5. FEEDBACK AND CLEANUP
            MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadUsers()     ' Refresh the grid
            ClearUserForm() ' Clear the textboxes

        Catch ex As Exception
            MessageBox.Show("Error updating user: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click

        ' 1. VALIDATION (Check for selection)
        If dgvUsers.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a user from the grid to delete.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Get the data from the selected row
        Dim selectedId As Integer = CInt(dgvUsers.CurrentRow.Cells("id").Value)
        Dim selectedUsername As String = dgvUsers.CurrentRow.Cells("username").Value.ToString()

        ' --- NEW: Get the role of the user we are trying to delete ---
        Dim selectedRole As String = dgvUsers.CurrentRow.Cells("role").Value.ToString()

        ' 2. SAFEGUARD #1 (Prevent deleting the main 'admin' account)
        If selectedUsername.ToLower() = "admin" Then
            MessageBox.Show("You cannot delete the primary 'admin' account.", "Action Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        ' --- NEW SAFEGUARD #2 (Protect High-Level Accounts) ---
        ' If the user being deleted is an Admin or Technician, BLOCK IT.
        If selectedRole = "Admin" OrElse selectedRole = "Technician" Then
            MessageBox.Show($"For security reasons, you cannot delete {selectedRole} accounts.",
                        "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If
        ' --------------------------------------------------------

        ' 3. CONFIRMATION DIALOG
        Dim confirmResult As DialogResult
        confirmResult = MessageBox.Show($"Are you sure you want to permanently delete this user?{vbCrLf}{vbCrLf}User: {selectedUsername}",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question)

        If confirmResult = DialogResult.No Then
            Return
        End If

        ' 4. EXECUTE DELETE
        Try
            Using conn As New MySqlConnection(usersConnectionString)
                conn.Open()
                Dim query As String = "DELETE FROM users WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", selectedId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsers()
            ClearUserForm()

        Catch ex As Exception
            MessageBox.Show("Error deleting user: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New()
        InitializeComponent()

        ' Set the bypass info
        _currentUserRole = "Admin"
        _currentUserFullname = "Bypass Admin"
        _currentHistoryId = 0 ' It's 0 because we didn't log in
        _currentUsername = "admin"

        lblUserInfo.Text = "Bypass Admin (Admin)"
    End Sub

    Private Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click
        HideAllPanels()
        pnlSchedule.Visible = True

        cmbFacility.Items.Clear()
        cmbFacility.Items.AddRange(New String() {
            "Barangay Basketball Court",
            "Multi-Purpose Hall",
            "Daycare Center",
            "Barangay Conference Room"
            })

        ' Load schedule for today
        LoadScheduleData(calSchedule.SelectionStart.Date)
    End Sub

    Private Sub calSchedule_DateChanged(sender As Object, e As DateRangeEventArgs) Handles calSchedule.DateChanged
        ' Get the date the user clicked
        Dim selectedDate As Date = calSchedule.SelectionStart.Date

        ' Call our new function to load the grid for that date
        LoadScheduleData(selectedDate)
    End Sub

    Private Sub LoadScheduleData(ByVal selectedDate As Date)
        Try
            Using conn As New MySqlConnection(connectionString) ' Uses your main barangay_db connection
                conn.Open()

                ' Query to select reservations that START on the selected date.
                ' The MySQL DATE() function ignores the time part.
                Dim query As String = "
                SELECT facility_name, event_name, start_datetime, end_datetime, resident_id 
                FROM reservations 
                WHERE DATE(start_datetime) = @SelectedDate 
                ORDER BY start_datetime
            "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate)

                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dtSchedule As New DataTable()
                        adapter.Fill(dtSchedule)

                        ' Bind to the reservations grid
                        dgvReservations.DataSource = dtSchedule


                        If dgvReservations.Columns.Contains("facility_name") Then
                            dgvReservations.Columns("facility_name").HeaderText = "Facility"
                        End If
                        If dgvReservations.Columns.Contains("event_name") Then
                            dgvReservations.Columns("event_name").HeaderText = "Event"
                        End If
                        If dgvReservations.Columns.Contains("start_datetime") Then
                            dgvReservations.Columns("start_datetime").HeaderText = "Start Time"
                            dgvReservations.Columns("start_datetime").DefaultCellStyle.Format = "hh:mm tt"
                        End If
                        If dgvReservations.Columns.Contains("end_datetime") Then
                            dgvReservations.Columns("end_datetime").HeaderText = "End Time"
                            dgvReservations.Columns("end_datetime").DefaultCellStyle.Format = "hh:mm tt"
                        End If
                        If dgvReservations.Columns.Contains("resident_id") Then
                            dgvReservations.Columns("resident_id").Visible = False ' Hide the ID
                        End If

                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading schedule: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSaveBooking_Click(sender As Object, e As EventArgs) Handles btnSaveBooking.Click

        ' --- 1. VALIDATION ---
        If _selectedResidentIdForSchedule = 0 Then
            MessageBox.Show("Please search for and select a resident first.", "Missing Resident", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If cmbFacility.SelectedIndex = -1 Then
            MessageBox.Show("Please select a facility.", "Missing Facility", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If dtpEndTime.Value <= dtpStartTime.Value Then
            MessageBox.Show("The booking's End Time must be after the Start Time.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' --- 2. GET DATA FROM FORM ---
        Dim facility As String = cmbFacility.Text
        Dim eventName As String = txtEventName.Text.Trim()
        Dim startTime As DateTime = dtpStartTime.Value
        Dim endTime As DateTime = dtpEndTime.Value
        Dim residentId As Integer = _selectedResidentIdForSchedule

        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' --- 3. CONFLICT CHECKING ---
                Dim queryCheck As String = "
                SELECT COUNT(*) FROM reservations
                WHERE facility_name = @Facility
                AND (@NewStart < end_datetime) 
                AND (@NewEnd > start_datetime)
            "

                Dim conflictCount As Integer = 0
                Using cmdCheck As New MySqlCommand(queryCheck, conn)
                    cmdCheck.Parameters.AddWithValue("@Facility", facility)
                    cmdCheck.Parameters.AddWithValue("@NewStart", startTime)
                    cmdCheck.Parameters.AddWithValue("@NewEnd", endTime)

                    conflictCount = Convert.ToInt32(cmdCheck.ExecuteScalar())
                End Using

                ' If count is > 0, we have a conflict
                If conflictCount > 0 Then
                    MessageBox.Show("Booking conflict! This facility is already reserved during that time slot.", "Conflict Detected", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return ' Stop before saving
                End If

                ' --- 4. NO CONFLICT - SAVE THE BOOKING ---
                Dim queryInsert As String = "
                INSERT INTO reservations (facility_name, resident_id, event_name, start_datetime, end_datetime) 
                VALUES (@Facility, @ResidentID, @Event, @Start, @End)
            "

                Using cmdInsert As New MySqlCommand(queryInsert, conn)
                    cmdInsert.Parameters.AddWithValue("@Facility", facility)
                    cmdInsert.Parameters.AddWithValue("@ResidentID", residentId)
                    cmdInsert.Parameters.AddWithValue("@Event", eventName)
                    cmdInsert.Parameters.AddWithValue("@Start", startTime)
                    cmdInsert.Parameters.AddWithValue("@End", endTime)

                    cmdInsert.ExecuteNonQuery()
                End Using
            End Using

            ' --- 5. FEEDBACK AND CLEANUP ---
            MessageBox.Show("Booking saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the schedule grid
            LoadScheduleData(calSchedule.SelectionStart.Date)

            ' Clear the booking fields
            ClearBookingForm()

        Catch ex As Exception
            MessageBox.Show("Error saving booking: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub ClearBookingForm()
        cmbFacility.SelectedIndex = -1
        txtEventName.Clear()

        lblScheduleSelectedResident.Text = "(No Resident Selected)"
        txtScheduleResidentSearch.Clear()
        _selectedResidentIdForSchedule = 0

        dtpStartTime.Value = DateTime.Now
        dtpEndTime.Value = DateTime.Now.AddHours(1)
    End Sub

    Private Sub txtScheduleResidentSearch_TextChanged(sender As Object, e As EventArgs) Handles txtScheduleResidentSearch.TextChanged
        ' Call a new function to load residents into our schedule lookup grid
        LoadResidentsForSchedule(txtScheduleResidentSearch.Text.Trim())
    End Sub

    Private Sub LoadResidentsForSchedule(Optional searchTerm As String = "")
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' 1. Select only essential columns
                Dim query As String = "SELECT id, lastname, firstname, middlename FROM residents"

                ' 2. Apply search filter
                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    query &= " WHERE CONCAT(lastname, ' ', firstname) LIKE @SearchTerm "
                End If

                query &= " ORDER BY lastname, firstname LIMIT 20" ' Limit results for a small grid

                Using cmd As New MySqlCommand(query, conn)
                    If Not String.IsNullOrWhiteSpace(searchTerm) Then
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" & searchTerm & "%")
                    End If

                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dtLookup As New DataTable()
                        adapter.Fill(dtLookup)

                        ' Bind to the NEW DataGridView in pnlSchedule
                        dgvScheduleResidentSearch.DataSource = dtLookup

                        ' Formatting the lookup grid
                        If dgvScheduleResidentSearch.Columns.Contains("id") Then dgvScheduleResidentSearch.Columns("id").Visible = False
                        If dgvScheduleResidentSearch.Columns.Contains("lastname") Then dgvScheduleResidentSearch.Columns("lastname").HeaderText = "Last Name"
                        If dgvScheduleResidentSearch.Columns.Contains("firstname") Then dgvScheduleResidentSearch.Columns("firstname").HeaderText = "First Name"
                        If dgvScheduleResidentSearch.Columns.Contains("middlename") Then dgvScheduleResidentSearch.Columns("middlename").Visible = False
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading resident lookup: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvScheduleResidentSearch_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvScheduleResidentSearch.CellClick
        If e.RowIndex >= 0 AndAlso dgvScheduleResidentSearch.CurrentRow IsNot Nothing Then
            Dim selectedRow = dgvScheduleResidentSearch.CurrentRow

            ' 1. Store the selected resident's ID
            _selectedResidentIdForSchedule = CInt(selectedRow.Cells("id").Value)

            ' 2. Update the label to confirm selection
            Dim fullName As String = $"{selectedRow.Cells("lastname").Value}, {selectedRow.Cells("firstname").Value}"
            lblScheduleSelectedResident.Text = $"Selected: {fullName}"

            ' 3. Clear the search box and hide the grid (optional, but clean)
            txtScheduleResidentSearch.Clear()
            dgvScheduleResidentSearch.DataSource = Nothing
        End If
    End Sub

    Private Sub btnOfficials_Click(sender As Object, e As EventArgs) Handles btnOfficials.Click
        HideAllPanels()
        pnlOfficials.Visible = True

        btnAddOfficial.Enabled = True
        btnUpdateOfficial.Enabled = False
        btnDeleteOfficial.Enabled = False

        ' Call the new function to load all officials
        LoadOfficials()
    End Sub

    Private Sub LoadOfficials()
        Try
            Using conn As New MySqlConnection(connectionString) ' Using your barangay_db
                conn.Open()


                Dim query As String = "SELECT id, fullname, position, contactnumber FROM officials"
                Using adapter As New MySqlDataAdapter(query, conn)
                    Dim dtOfficials As New DataTable()
                    adapter.Fill(dtOfficials)
                    dgvOfficialsList.DataSource = dtOfficials

                    ' Format the grid
                    If dgvOfficialsList.Columns.Contains("id") Then dgvOfficialsList.Columns("id").Visible = False
                    If dgvOfficialsList.Columns.Contains("fullname") Then dgvOfficialsList.Columns("fullname").HeaderText = "Full Name"
                    If dgvOfficialsList.Columns.Contains("position") Then dgvOfficialsList.Columns("position").HeaderText = "Position"
                    If dgvOfficialsList.Columns.Contains("contactnumber") Then dgvOfficialsList.Columns("contactnumber").HeaderText = "Contact"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading officials: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvOfficialsList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOfficialsList.CellClick
        If e.RowIndex >= 0 AndAlso dgvOfficialsList.CurrentRow IsNot Nothing Then
            Dim selectedRow = dgvOfficialsList.CurrentRow

            ' Populate the textboxes
            txtOfficialName.Text = selectedRow.Cells("fullname").Value.ToString()
            txtOfficialPosition.Text = selectedRow.Cells("position").Value.ToString()
            txtOfficialContact.Text = selectedRow.Cells("contactnumber").Value.ToString()

            btnAddOfficial.Enabled = False
            btnUpdateOfficial.Enabled = True
            btnDeleteOfficial.Enabled = True
        End If
    End Sub

    Private Sub btnClearOfficial_Click(sender As Object, e As EventArgs) Handles btnClearOfficial.Click
        txtOfficialName.Clear()
        txtOfficialPosition.Clear()
        txtOfficialContact.Clear()
        dgvOfficialsList.ClearSelection() ' Deselect the grid

        btnAddOfficial.Enabled = True
        btnUpdateOfficial.Enabled = False
        btnDeleteOfficial.Enabled = False
    End Sub

    Private Sub btnAddOfficial_Click(sender As Object, e As EventArgs) Handles btnAddOfficial.Click

        ' 1. VALIDATION
        ' Check if the essential fields are filled
        If String.IsNullOrWhiteSpace(txtOfficialName.Text) OrElse
           String.IsNullOrWhiteSpace(txtOfficialPosition.Text) Then

            MessageBox.Show("Please fill in at least the Name and Position fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if contact number is exactly 11 digits
        If txtOfficialContact.Text.Length <> 11 Then
            MessageBox.Show("Contact number must be exactly 11 digits (e.g., 09xxxxxxxxx).", "Invalid Contact", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if it starts with "09" (Optional, but good for PH context)
        If Not txtOfficialContact.Text.StartsWith("09") Then
            MessageBox.Show("Contact number must start with '09'.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. PREPARE DATA
        Dim fullname As String = txtOfficialName.Text.Trim()
        Dim position As String = txtOfficialPosition.Text.Trim()
        Dim contact As String = txtOfficialContact.Text.Trim()

        ' 3. SAVE TO DATABASE
        Try
            Using conn As New MySqlConnection(connectionString) ' Using your barangay_db
                conn.Open()

                Dim query As String = "
                INSERT INTO officials (fullname, position, contactnumber) 
                VALUES (@Fullname, @Position, @Contact)
            "

                Using cmd As New MySqlCommand(query, conn)
                    ' Add parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@Fullname", fullname)
                    cmd.Parameters.AddWithValue("@Position", position)
                    cmd.Parameters.AddWithValue("@Contact", contact)

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 4. FEEDBACK AND CLEANUP
            MessageBox.Show("Official added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadOfficials() ' Refresh the grid to show the new data
            btnClearOfficial_Click(Nothing, Nothing) ' Call your clear button's code to reset the textboxes

        Catch ex As Exception
            MessageBox.Show("Error adding official: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdateOfficial_Click(sender As Object, e As EventArgs) Handles btnUpdateOfficial.Click

        ' 1. VALIDATION (Check for selection)
        If dgvOfficialsList.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an official from the grid to update.", "No Official Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. GET THE ID
        ' Get the ID from the selected grid row
        Dim selectedId As Integer = CInt(dgvOfficialsList.CurrentRow.Cells("id").Value)

        ' 3. VALIDATION (Check for empty fields)
        If String.IsNullOrWhiteSpace(txtOfficialName.Text) OrElse
           String.IsNullOrWhiteSpace(txtOfficialPosition.Text) Then

            MessageBox.Show("Please fill in at least the Name and Position fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 4. PREPARE DATA
        Dim fullname As String = txtOfficialName.Text.Trim()
        Dim position As String = txtOfficialPosition.Text.Trim()
        Dim contact As String = txtOfficialContact.Text.Trim()

        ' 5. UPDATE DATABASE
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "
                UPDATE officials SET 
                    fullname = @Fullname, 
                    position = @Position, 
                    contactnumber = @Contact 
                WHERE id = @id
            "

                Using cmd As New MySqlCommand(query, conn)
                    ' Add parameters
                    cmd.Parameters.AddWithValue("@Fullname", fullname)
                    cmd.Parameters.AddWithValue("@Position", position)
                    cmd.Parameters.AddWithValue("@Contact", contact)
                    cmd.Parameters.AddWithValue("@id", selectedId) ' This is the most important one!

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 6. FEEDBACK AND CLEANUP
            MessageBox.Show("Official updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadOfficials() ' Refresh the grid
            btnClearOfficial_Click(Nothing, Nothing) ' Clear the textboxes

        Catch ex As Exception
            MessageBox.Show("Error updating official: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteOfficial_Click(sender As Object, e As EventArgs) Handles btnDeleteOfficial.Click

        ' 1. VALIDATION (Check for selection)
        If dgvOfficialsList.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an official from the grid to delete.", "No Official Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. GET THE ID AND NAME
        Dim selectedId As Integer = CInt(dgvOfficialsList.CurrentRow.Cells("id").Value)
        Dim selectedName As String = dgvOfficialsList.CurrentRow.Cells("fullname").Value.ToString()

        ' 3. CONFIRMATION DIALOG
        Dim confirmResult As DialogResult
        confirmResult = MessageBox.Show($"Are you sure you want to permanently delete this official?{vbCrLf}{vbCrLf}Name: {selectedName}",
                                        "Confirm Delete",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question)

        ' Stop if the user clicks "No"
        If confirmResult = DialogResult.No Then
            Return
        End If

        ' 4. EXECUTE DELETE
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                Dim query As String = "DELETE FROM officials WHERE id = @id"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", selectedId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 5. FEEDBACK AND CLEANUP
            MessageBox.Show("Official deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadOfficials() ' Refresh the grid
            btnClearOfficial_Click(Nothing, Nothing) ' Clear the textboxes

        Catch ex As Exception
            MessageBox.Show("Error deleting official: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub pnlResidentStat_Click(sender As Object, e As EventArgs) Handles pnlResidentStat.Click
        ' Clicks the main "Residents" menu button
        btnResidents.PerformClick()
    End Sub

    Private Sub pnlOfficialStat_Click(sender As Object, e As EventArgs) Handles pnlOfficialStat.Click
        ' Clicks the main "Officials" menu button
        btnOfficials.PerformClick()
    End Sub

    Private Sub pnlReportStat_Click(sender As Object, e As EventArgs) Handles pnlReportStat.Click
        ' Clicks the main "Documents" menu button
        btnDocuments.PerformClick()
    End Sub
    ' --- REPLACE your old btnPrintPreview_Click with this ---

    Private Sub btnPrintPreview_Click(sender As Object, e As EventArgs) Handles btnPrintPreview.Click

        ' 1. VALIDATE
        If dgvDocumentHistory.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a document from the history grid below to preview.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. GATHER DATA FROM THE GRID ROW
        Dim selectedRow = dgvDocumentHistory.CurrentRow

        ' --- NEW: Get the Certificate Type ---
        Dim certType As String = selectedRow.Cells("certificate_type").Value.ToString()

        Dim residentId As Integer = CInt(selectedRow.Cells("resident_id").Value)
        Dim certPurpose As String = selectedRow.Cells("purpose").Value.ToString()
        Dim certControlNum As String = selectedRow.Cells("control_number").Value.ToString()
        Dim certDate As String = CDate(selectedRow.Cells("date_issued").Value).ToString("MMMM dd, yyyy")
        Dim residentFullName As String = selectedRow.Cells("resident_name").Value.ToString()
        Dim certCaptain As String = "Hon. Merry Jean Ariola"

        ' 3. GET MISSING DATA (ADDRESS)
        Dim residentAddress As String = ""
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim query As String = "SELECT address FROM residents WHERE id = @ResidentID"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ResidentID", residentId)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                        residentAddress = result.ToString()
                    Else
                        residentAddress = "(Address not found)"
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching resident's address: " & ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' 4. CREATE AND SHOW THE PREVIEW FORM
        Dim previewForm As New FormCertificatePreview()

        ' --- NEW: Pass the certType to the function ---
        previewForm.PopulateCertificate(certType, residentFullName, residentAddress, certPurpose, certControlNum, certDate, certCaptain)

        previewForm.ShowDialog()
    End Sub

    Private Sub LoadDocumentHistory()
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' This query JOINS the certificates table with the residents table
                ' to get the resident's name.
                Dim query As String = "
                SELECT 
                    c.id, 
                    c.control_number, 
                    c.certificate_type, 
                    c.purpose, 
                    c.date_issued, 
                    c.resident_id,
                    CONCAT(r.lastname, ', ', r.firstname) AS resident_name

                FROM certificates_issued AS c
                JOIN residents AS r ON c.resident_id = r.id
                ORDER BY c.date_issued DESC
            "

                Using adapter As New MySqlDataAdapter(query, conn)
                    Dim dtHistory As New DataTable()
                    adapter.Fill(dtHistory)

                    ' Bind the data to our new grid
                    dgvDocumentHistory.DataSource = dtHistory

                    ' Format the columns
                    If dgvDocumentHistory.Columns.Contains("id") Then dgvDocumentHistory.Columns("id").Visible = False
                    If dgvDocumentHistory.Columns.Contains("control_number") Then dgvDocumentHistory.Columns("control_number").HeaderText = "Control #"
                    If dgvDocumentHistory.Columns.Contains("certificate_type") Then dgvDocumentHistory.Columns("certificate_type").HeaderText = "Type"

                    ' --- TYPO FIX ---
                    If dgvDocumentHistory.Columns.Contains("purpose") Then dgvDocumentHistory.Columns("purpose").HeaderText = "Purpose"
                    ' --- END OF FIX ---

                    If dgvDocumentHistory.Columns.Contains("date_issued") Then dgvDocumentHistory.Columns("date_issued").HeaderText = "Date Issued"
                    If dgvDocumentHistory.Columns.Contains("resident_name") Then dgvDocumentHistory.Columns("resident_name").HeaderText = "Resident"
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading document history: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBlotter_Click(sender As Object, e As EventArgs) Handles btnBlotter.Click
        HideAllPanels()
        pnlBlotter.Visible = True
        LoadBlotterCases()
    End Sub

    Private Sub LoadBlotterCases()
        Try
            Dim repo As New BlotterRepository()
            Dim dt As DataTable = repo.GetAllCases()

            dgvBlotter.DataSource = dt

            ' Formatting
            If dgvBlotter.Columns.Contains("id") Then dgvBlotter.Columns("id").Visible = False
            If dgvBlotter.Columns.Contains("incident_date") Then dgvBlotter.Columns("incident_date").HeaderText = "Date"
            If dgvBlotter.Columns.Contains("complainant") Then dgvBlotter.Columns("complainant").HeaderText = "Complainant"
            If dgvBlotter.Columns.Contains("respondent") Then dgvBlotter.Columns("respondent").HeaderText = "Respondent"
            If dgvBlotter.Columns.Contains("incident_type") Then dgvBlotter.Columns("incident_type").HeaderText = "Type"
            If dgvBlotter.Columns.Contains("status") Then dgvBlotter.Columns("status").HeaderText = "Status"
            ' Hide extra columns if needed
            If dgvBlotter.Columns.Contains("location") Then dgvBlotter.Columns("location").Visible = False
            If dgvBlotter.Columns.Contains("narrative") Then dgvBlotter.Columns("narrative").Visible = False

        Catch ex As Exception
            MessageBox.Show("Error loading blotter: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSaveCase_Click(sender As Object, e As EventArgs) Handles btnSaveCase.Click
        ' Open the new Pop-up Form
        Using addForm As New FormAddBlotter()
            If addForm.ShowDialog() = DialogResult.OK Then
                Try
                    ' 1. Pack data into the Class
                    Dim newCase As New BlotterCase()
                    newCase.Complainant = addForm.Complainant
                    newCase.Respondent = addForm.Respondent
                    newCase.IncidentType = addForm.IncidentType
                    newCase.Location = addForm.IncidentLocation ' Mapping the name change
                    newCase.IncidentDate = addForm.IncidentDate
                    newCase.Status = addForm.Status
                    newCase.Narrative = addForm.Narrative

                    ' 2. Send to Repository
                    Dim repo As New BlotterRepository()
                    repo.AddCase(newCase)

                    ' 3. Refresh
                    MessageBox.Show("Case recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadBlotterCases()

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnUpdateCase_Click(sender As Object, e As EventArgs) Handles btnUpdateCase.Click
        ' 1. Check if a row is selected
        If dgvBlotter.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a case to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim id As Integer = CInt(dgvBlotter.CurrentRow.Cells("id").Value)

        ' 2. Open the form and fill it with Grid data
        Using editForm As New FormAddBlotter()
            editForm.CaseID = id

            ' Safely grab text from the grid cells
            editForm.Complainant = dgvBlotter.CurrentRow.Cells("complainant").Value.ToString()
            editForm.Respondent = dgvBlotter.CurrentRow.Cells("respondent").Value.ToString()
            editForm.IncidentType = dgvBlotter.CurrentRow.Cells("incident_type").Value.ToString()
            editForm.Status = dgvBlotter.CurrentRow.Cells("status").Value.ToString()

            ' Note: If you hid the 'Location' or 'Narrative' columns in the grid, 
            ' you might need to fetch them from the DB here. 
            ' But for now, we assume they are in the grid (even if hidden).
            If dgvBlotter.Columns.Contains("location") Then
                editForm.IncidentLocation = dgvBlotter.CurrentRow.Cells("location").Value.ToString()
            End If
            If dgvBlotter.Columns.Contains("narrative") Then
                editForm.Narrative = dgvBlotter.CurrentRow.Cells("narrative").Value.ToString()
            End If

            Dim dDate = dgvBlotter.CurrentRow.Cells("incident_date").Value
            If IsDate(dDate) Then editForm.IncidentDate = CDate(dDate)

            ' 3. Show Form & Save
            If editForm.ShowDialog() = DialogResult.OK Then
                Try
                    Dim upCase As New BlotterCase()
                    upCase.Id = id
                    upCase.Complainant = editForm.Complainant
                    upCase.Respondent = editForm.Respondent
                    upCase.IncidentType = editForm.IncidentType
                    upCase.Location = editForm.IncidentLocation
                    upCase.IncidentDate = editForm.IncidentDate
                    upCase.Status = editForm.Status
                    upCase.Narrative = editForm.Narrative

                    Dim repo As New BlotterRepository()
                    repo.UpdateCase(upCase)

                    MessageBox.Show("Case updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadBlotterCases()

                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Sub RestrictToNumbers(sender As Object, e As KeyPressEventArgs) Handles txtAmountPaid.KeyPress, txtOfficialContact.KeyPress
        ' Allow digits (0-9) and the Backspace key
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then
            ' If it's NOT a digit and NOT backspace, ignore the input
            e.Handled = True
        End If
    End Sub

    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click

    End Sub
End Class


