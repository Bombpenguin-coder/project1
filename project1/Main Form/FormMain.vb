Imports System.Security.Cryptography
Imports System.Text
Imports MySql.Data.MySqlClient

Public Class FormMain

    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"
    Public ResidentsTable As New DataTable()
    Private _selectedResidentIdForIssuance As Integer = 0
    Private _currentUserRole As String = "" ' Stores the logged-in user's role
    Private _selectedResidentIdForSchedule As Integer = 0
    Private _currentUserFullname As String = ""
    Private _currentHistoryId As Integer = 0
    Private _currentUsername As String = ""

    Public Sub New(ByVal role As String, ByVal fullname As String, ByVal historyId As Integer, ByVal username As String)
        InitializeComponent()

        _currentUserRole = role
        _currentUserFullname = fullname
        _currentHistoryId = historyId
        _currentUsername = username
        lblUserInfo.Text = $"{_currentUserFullname} ({_currentUserRole})"
    End Sub


    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' WAKE UP THE CLOCK!
        TimerClock.Start()

        ApplyRolePermissions()
        HideAllPanels()
        pnlDashboard.Visible = True
        LoadDashboardData()
    End Sub

    Private Sub HideAllPanels()
        pnlDashboard.Visible = False
        pnlResidents.Visible = False
        pnlLoginHistory.Visible = False
        pnlDocuments.Visible = False
        pnlSchedule.Visible = False
        pnlOfficials.Visible = False
        pnlBlotter.Visible = False

        ' Add BOTH possible names to ensure it always hides!
        pnlAddUsers.Visible = False
        pnlUserMaintenance.Visible = False

        pnlAuditTrail.Visible = False
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

    Private Sub btnAddUsers_Click(sender As Object, e As EventArgs) Handles btnUserMaintenance.Click
        HideAllPanels()
        pnlUserMaintenance.Visible = True

        ' Populate the Role ComboBox (UPDATED ROLES)
        cmbUserRole.Items.Clear()
        cmbUserRole.Items.AddRange(New String() {"Superadmin", "Admin", "Staff"})
        cmbUserRole.SelectedIndex = 0 ' Default to Superadmin

        ' --- PREVENT MULTIPLE SUPERADMINS ---
        If cmbUserRole.Text = "Superadmin" Then
            MessageBox.Show("You cannot create additional Superadmin accounts. There can only be one system owner.", "Security Restriction", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

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
                newResident.Sex = addForm.Sex
                newResident.Address = addForm.Address
                newResident.District = addForm.District

                ' 2. Send it to the Repository
                Dim repo As New ResidentRepository()
                repo.AddResident(newResident)

                ' 3. Success!
                LoadResidentsFromDatabase()
                MessageBox.Show("Resident added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Dim audit As New AuditRepository()
                audit.LogAction(_currentUsername, _currentUserRole, "ADD", "Residents", $"Added new resident: {newResident.FirstName} {newResident.LastName}")

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

                Dim audit As New AuditRepository()
                audit.LogAction(_currentUsername, _currentUserRole, "DELETE", "Residents", $"Deleted resident ID: {selectedId}")

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

        editForm.cmbSex.Text = dgvResidents.CurrentRow.Cells("sex").Value.ToString()
        editForm.txtAddress.Text = dgvResidents.CurrentRow.Cells("address").Value.ToString()
        editForm.txtDistrict.Text = dgvResidents.CurrentRow.Cells("district").Value.ToString()

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
                updatedRes.ID = selectedId
                updatedRes.LastName = editForm.LastName
                updatedRes.FirstName = editForm.FirstName
                updatedRes.MiddleName = editForm.MiddleName
                updatedRes.BirthDate = editForm.BirthDate
                updatedRes.Age = editForm.Age
                updatedRes.Sex = editForm.Sex
                updatedRes.Address = editForm.Address
                updatedRes.District = editForm.District
                ' 2. Call the Repository
                Dim repo As New ResidentRepository()
                repo.UpdateResident(updatedRes)

                ' 3. Refresh UI
                MessageBox.Show("Resident updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadResidentsFromDatabase()

                Dim audit As New AuditRepository()
                audit.LogAction(_currentUsername, _currentUserRole, "UPDATE", "Residents", $"Updated resident ID: {selectedId}")

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
            If dgvResidents.Columns.Contains("sex") Then dgvResidents.Columns("sex").HeaderText = "Sex"
            If dgvResidents.Columns.Contains("address") Then dgvResidents.Columns("address").HeaderText = "Address"
            If dgvResidents.Columns.Contains("district") Then dgvResidents.Columns("district").HeaderText = "District"

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
                        Case "Superadmin", "Admin"
                            ' These roles see everyone. No filter needed.
                        Case "Staff"
                            ' These roles only see themselves.
                            query &= " WHERE lh.username = @Username"
                            cmd.Parameters.AddWithValue("@Username", _currentUsername)
                        Case Else
                            ' Unknown role sees nothing
                            query &= " WHERE 1 = 0"
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
    End Sub

    Private Sub btnIssueSave_Click(sender As Object, e As EventArgs) Handles btnIssueSave.Click
        Using issueForm As New FormIssueCertificate()
            If issueForm.ShowDialog() = DialogResult.OK Then
                Try
                    Dim repo As New CertificateRepository()

                    ' 1. Automatically generate the next control number
                    Dim nextControlNum As String = repo.GenerateControlNumber()

                    ' 2. Pack the data into the Class
                    Dim newCert As New Certificate()
                    newCert.ResidentId = issueForm.SelectedResidentId
                    newCert.ControlNumber = nextControlNum
                    newCert.CertificateType = issueForm.CertificateType
                    newCert.Purpose = issueForm.Purpose
                    newCert.AmountPaid = issueForm.AmountPaid

                    ' We automatically record who issued it based on who is logged in!
                    newCert.IssuedBy = _currentUserFullname

                    ' 3. Save to database
                    repo.IssueCertificate(newCert)

                    ' 4. Success and Refresh
                    MessageBox.Show($"Certificate issued successfully!{vbCrLf}Control Number: {nextControlNum}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoadDocumentHistory()
                    LoadDashboardData() ' Update the total reports generated on the dashboard!

                    Dim audit As New AuditRepository()
                    audit.LogAction(_currentUsername, _currentUserRole, "ISSUE", "Documents", $"Issued {issueForm.CertificateType} (Control #: {nextControlNum}) to Resident ID: {issueForm.SelectedResidentId}")

                Catch ex As Exception
                    MessageBox.Show("Error issuing certificate: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
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

        Select Case _currentUserRole.ToLower()

            Case "superadmin"
                ' Full access (no restrictions)
                btnResidents.Enabled = True
                btnUserMaintenance.Enabled = True
                btnOfficials.Enabled = True
                btnBlotter.Enabled = True
                btnSchedule.Enabled = True

            Case "admin"
                ' Limited access (no user management)
                btnResidents.Enabled = True
                btnUserMaintenance.Enabled = False ' ❌ cannot manage users
                btnOfficials.Enabled = True
                btnBlotter.Enabled = True
                btnSchedule.Enabled = True

            Case "staff"
                ' Very limited access
                btnResidents.Enabled = True
                btnUserMaintenance.Enabled = False
                btnOfficials.Enabled = False
                btnBlotter.Enabled = False
                btnSchedule.Enabled = True

            Case Else
                ' Unknown role = no access
                btnResidents.Enabled = False
                btnUserMaintenance.Enabled = False
                btnOfficials.Enabled = False
                btnBlotter.Enabled = False
                btnSchedule.Enabled = False

        End Select

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
            txtUserFullname.Text = selectedRow.Cells("fullname").Value.ToString
            txtUserUsername.Text = selectedRow.Cells("username").Value.ToString
            cmbUserRole.Text = selectedRow.Cells("role").Value.ToString

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
        Dim fullname = txtUserFullname.Text.Trim
        Dim username = txtUserUsername.Text.Trim
        Dim role = cmbUserRole.Text

        ' 3. HASH THE PASSWORD
        Dim hashedPassword = HashPassword(txtUserPassword.Text)

        ' 4. SAVE TO DATABASE
        Try
            Using conn As New MySqlConnection(usersConnectionString)
                conn.Open()

                ' A. Check for duplicate username first
                Dim queryCheck = "SELECT COUNT(*) FROM users WHERE username = @Username"
                Using cmdCheck As New MySqlCommand(queryCheck, conn)
                    cmdCheck.Parameters.AddWithValue("@Username", username)
                    Dim userCount = Convert.ToInt32(cmdCheck.ExecuteScalar)

                    If userCount > 0 Then
                        MessageBox.Show("This username already exists. Please choose another one.", "Duplicate User", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End Using

                ' B. Insert the new user with Security Question
                Dim queryInsert = "
                INSERT INTO users (fullname, username, password, role, security_question, security_answer) 
                VALUES (@Fullname, @Username, @Password, @Role, @Question, @Answer)
            "
                Using cmd As New MySqlCommand(queryInsert, conn)
                    cmd.Parameters.AddWithValue("@Fullname", fullname)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Password", hashedPassword)
                    cmd.Parameters.AddWithValue("@Role", role)
                    cmd.Parameters.AddWithValue("@Question", cmbUserQuestion.Text)
                    cmd.Parameters.AddWithValue("@Answer", txtUserAnswer.Text.Trim().ToLower())

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 5. FEEDBACK AND CLEANUP
            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadUsers()     ' Refresh the grid
            ClearUserForm() ' Clear the textboxes

            Dim audit As New AuditRepository()
            audit.LogAction(_currentUsername, _currentUserRole, "ADD", "Users", $"Added new user account: {username}")

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
        cmbUserQuestion.SelectedIndex = -1
        txtUserAnswer.Clear()
        dgvUsers.ClearSelection()
    End Sub

    Private Sub btnUpdateUser_Click(sender As Object, e As EventArgs) Handles btnUpdateUser.Click

        ' 1. VALIDATION (Check for selection)
        If dgvUsers.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a user from the grid to update.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Get the data from the selected row
        Dim selectedId As Integer = dgvUsers.CurrentRow.Cells("id").Value
        Dim selectedUsername As String = dgvUsers.CurrentRow.Cells("username").Value.ToString()
        Dim selectedRole As String = dgvUsers.CurrentRow.Cells("role").Value.ToString()

        ' --- NEW: ROLE-BASED SECURITY FOR UPDATING ---
        If _currentUserRole = "Admin" Then
            ' 1. Admins cannot touch Superadmins
            If selectedRole = "Superadmin" Then
                MessageBox.Show("For security reasons, Admins cannot update Superadmin accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return
            End If

            ' 2. Admins cannot touch OTHER Admins (But can update themselves)
            If selectedRole = "Admin" AndAlso selectedUsername <> _currentUsername Then
                MessageBox.Show("Admins cannot update other Admin accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return
            End If
        End If

        ' 3. Prevent ANYONE from accidentally demoting the primary 'admin' account
        If selectedUsername.ToLower() = "admin" AndAlso cmbUserRole.Text <> "Superadmin" Then
            MessageBox.Show("The primary 'admin' account must remain a Superadmin.", "Action Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        ' --- PREVENT PRIVILEGE ESCALATION ---
        ' If they are trying to save the role as Superadmin, but the user wasn't ALREADY the Superadmin...
        If cmbUserRole.Text = "Superadmin" AndAlso selectedRole <> "Superadmin" Then
            MessageBox.Show("Accounts cannot be promoted to Superadmin.", "Security Restriction", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        ' 2. VALIDATION (Check for empty fields)
        If String.IsNullOrWhiteSpace(txtUserFullname.Text) OrElse
       String.IsNullOrWhiteSpace(txtUserUsername.Text) OrElse
       String.IsNullOrWhiteSpace(cmbUserRole.Text) Then

            MessageBox.Show("Please fill in all fields (Fullname, Username, and Role).", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 3. PREPARE DATA
        Dim fullname = txtUserFullname.Text.Trim
        Dim username = txtUserUsername.Text.Trim
        Dim role = cmbUserRole.Text

        ' 4. PREPARE DATABASE COMMAND
        Try
            Using conn As New MySqlConnection(usersConnectionString)
                conn.Open()

                Dim query = ""
                Dim passwordWasUpdated = False


                If String.IsNullOrWhiteSpace(txtUserPassword.Text) Then
                    ' A. Password box is EMPTY. Do NOT update the password.
                    query = "
                    UPDATE users SET 
                        fullname = @Fullname, 
                        username = @Username, 
                        role = @Role,
                        security_question = @Question,
                        security_answer = @Answer
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
                        password = @Password,
                        security_question = @Question,
                        security_answer = @Answer
                    WHERE id = @id
                "
                    passwordWasUpdated = True
                End If

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Fullname", fullname)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Role", role)
                    cmd.Parameters.AddWithValue("@Question", cmbUserQuestion.Text)
                    cmd.Parameters.AddWithValue("@Answer", txtUserAnswer.Text.Trim().ToLower())
                    cmd.Parameters.AddWithValue("@id", selectedId)

                    If passwordWasUpdated Then
                        Dim hashedPassword = HashPassword(txtUserPassword.Text)
                        cmd.Parameters.AddWithValue("@Password", hashedPassword)
                    End If

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' 5. FEEDBACK AND CLEANUP
            MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadUsers()     ' Refresh the grid
            ClearUserForm() ' Clear the textboxes

            Dim audit As New AuditRepository()
            audit.LogAction(_currentUsername, _currentUserRole, "UPDATE", "Users", $"Updated user account: {username} (ID: {selectedId})")

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
        Dim selectedId As Integer = dgvUsers.CurrentRow.Cells("id").Value
        Dim selectedUsername = dgvUsers.CurrentRow.Cells("username").Value.ToString

        ' --- NEW: Get the role of the user we are trying to delete ---
        Dim selectedRole = dgvUsers.CurrentRow.Cells("role").Value.ToString

        ' 2. SAFEGUARD #1 (Prevent deleting the main 'admin' account)
        If selectedUsername.ToLower = "admin" Then
            MessageBox.Show("You cannot delete the primary 'admin' account.", "Action Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        ' --- NEW SAFEGUARD #2 (Protect High-Level Accounts) ---
        If selectedRole = "Superadmin" Then
            MessageBox.Show("For security reasons, you cannot delete Superadmin accounts.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return
        End If

        ' Prevent normal Admins from deleting other Admins
        If _currentUserRole = "Admin" AndAlso selectedRole = "Admin" Then
            MessageBox.Show("Admins cannot delete other Admin accounts. Only a Superadmin can do this.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop)
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
                Dim query = "DELETE FROM users WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", selectedId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadUsers()
            ClearUserForm()

            Dim audit As New AuditRepository()
            audit.LogAction(_currentUsername, _currentUserRole, "DELETE", "Users", $"Deleted user account: {selectedUsername}")

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
            Dim repo As New ReservationRepository()
            Dim dt As DataTable = repo.GetReservationsByDate(selectedDate)

            dgvReservations.DataSource = dt

            ' Formatting
            If dgvReservations.Columns.Contains("facility_name") Then dgvReservations.Columns("facility_name").HeaderText = "Facility"
            If dgvReservations.Columns.Contains("event_name") Then dgvReservations.Columns("event_name").HeaderText = "Event"
            If dgvReservations.Columns.Contains("reserver_name") Then dgvReservations.Columns("reserver_name").HeaderText = "Reserved By"
            If dgvReservations.Columns.Contains("is_resident") Then dgvReservations.Columns("is_resident").Visible = False
            If dgvReservations.Columns.Contains("in_charge") Then dgvReservations.Columns("in_charge").HeaderText = "In-Charge"

            If dgvReservations.Columns.Contains("start_datetime") Then
                dgvReservations.Columns("start_datetime").HeaderText = "Start Time"
                dgvReservations.Columns("start_datetime").DefaultCellStyle.Format = "hh:mm tt"
            End If

            If dgvReservations.Columns.Contains("end_datetime") Then
                dgvReservations.Columns("end_datetime").HeaderText = "End Time"
                dgvReservations.Columns("end_datetime").DefaultCellStyle.Format = "hh:mm tt"
            End If

            If dgvReservations.Columns.Contains("resident_id") Then dgvReservations.Columns("resident_id").Visible = False
            If dgvReservations.Columns.Contains("id") Then dgvReservations.Columns("id").Visible = False

        Catch ex As Exception
            MessageBox.Show("Error loading schedule: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSaveBooking_Click(sender As Object, e As EventArgs) Handles btnSaveBooking.Click
        Using addForm As New FormAddBooking()
            If addForm.ShowDialog() = DialogResult.OK Then
                Try
                    Dim repo As New ReservationRepository()

                    ' 1. Check for Conflicts
                    If repo.HasConflict(addForm.FacilityName, addForm.StartTime, addForm.EndTime) Then
                        MessageBox.Show("Booking conflict! This facility is already reserved during that time slot.", "Conflict Detected", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return ' Stop! Don't save.
                    End If

                    ' 2. Pack Data
                    Dim newRes As New Reservation()
                    newRes.ResidentId = addForm.SelectedResidentId

                    ' --- NEW FIELDS ---
                    newRes.IsResident = addForm.IsResident
                    newRes.ReserverName = addForm.ReserverName
                    newRes.InCharge = addForm.InCharge
                    ' ------------------

                    newRes.FacilityName = addForm.FacilityName
                    newRes.EventName = addForm.EventName
                    newRes.StartDateTime = addForm.StartTime
                    newRes.EndDateTime = addForm.EndTime

                    ' 3. Save
                    repo.AddReservation(newRes)

                    MessageBox.Show("Booking saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh grid for the currently selected calendar date
                    LoadScheduleData(calSchedule.SelectionStart.Date)

                Catch ex As Exception
                    MessageBox.Show("Error saving booking: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
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
            Dim repo As New OfficialRepository()
            Dim dt As DataTable = repo.GetAllOfficials()

            dgvOfficialsList.DataSource = dt

            If dgvOfficialsList.Columns.Contains("id") Then dgvOfficialsList.Columns("id").Visible = False
            If dgvOfficialsList.Columns.Contains("fullname") Then dgvOfficialsList.Columns("fullname").HeaderText = "Full Name"
            If dgvOfficialsList.Columns.Contains("position") Then dgvOfficialsList.Columns("position").HeaderText = "Position"
            If dgvOfficialsList.Columns.Contains("contactnumber") Then dgvOfficialsList.Columns("contactnumber").HeaderText = "Contact"

            ' Reset button states
            btnUpdateOfficial.Enabled = True
            btnDeleteOfficial.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Error loading officials: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddOfficial_Click(sender As Object, e As EventArgs) Handles btnAddOfficial.Click
        Using addForm As New FormAddOfficial()
            If addForm.ShowDialog() = DialogResult.OK Then
                Try
                    Dim newOff As New Official()
                    newOff.FullName = addForm.FullName
                    newOff.Position = addForm.Position
                    newOff.ContactNumber = addForm.ContactNumber

                    Dim repo As New OfficialRepository()
                    repo.AddOfficial(newOff)

                    MessageBox.Show("Official added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadOfficials()
                    LoadDashboardData() ' Updates the counts on your dashboard!
                Catch ex As Exception
                    MessageBox.Show("Error adding official: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnUpdateOfficial_Click(sender As Object, e As EventArgs) Handles btnUpdateOfficial.Click
        If dgvOfficialsList.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an official from the grid to update.", "No Official Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = CInt(dgvOfficialsList.CurrentRow.Cells("id").Value)

        Using editForm As New FormAddOfficial()
            editForm.OfficialID = id
            editForm.FullName = dgvOfficialsList.CurrentRow.Cells("fullname").Value.ToString()
            editForm.Position = dgvOfficialsList.CurrentRow.Cells("position").Value.ToString()
            editForm.ContactNumber = dgvOfficialsList.CurrentRow.Cells("contactnumber").Value.ToString()

            If editForm.ShowDialog() = DialogResult.OK Then
                Try
                    Dim upOff As New Official()
                    upOff.Id = id
                    upOff.FullName = editForm.FullName
                    upOff.Position = editForm.Position
                    upOff.ContactNumber = editForm.ContactNumber

                    Dim repo As New OfficialRepository()
                    repo.UpdateOfficial(upOff)

                    MessageBox.Show("Official updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadOfficials()
                    LoadDashboardData()
                Catch ex As Exception
                    MessageBox.Show("Error updating official: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnDeleteOfficial_Click(sender As Object, e As EventArgs) Handles btnDeleteOfficial.Click
        If dgvOfficialsList.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an official to delete.", "No Official Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = CInt(dgvOfficialsList.CurrentRow.Cells("id").Value)
        Dim name As String = dgvOfficialsList.CurrentRow.Cells("fullname").Value.ToString()

        If MessageBox.Show($"Are you sure you want to permanently delete {name}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim repo As New OfficialRepository()
                repo.DeleteOfficial(id)

                MessageBox.Show("Official deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadOfficials()
                LoadDashboardData()
            Catch ex As Exception
                MessageBox.Show("Error deleting official: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
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
        ' --- NEW: Fetch Dynamic System Settings! ---
        Dim certCaptain As String = ""
        Dim brgyName As String = ""
        Dim cityName As String = ""
        Dim provName As String = ""
        Try
            Dim settingsRepo As New SettingsRepository()
            Dim currentSettings = settingsRepo.GetSettings()
            certCaptain = currentSettings.CaptainName
            brgyName = currentSettings.BarangayName
            cityName = currentSettings.CityName
            provName = currentSettings.ProvinceName
        Catch ex As Exception
            certCaptain = "Error Loading Captain"
        End Try
        ' -------------------------------------------

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
            Dim repo As New CertificateRepository()
            Dim dt As DataTable = repo.GetDocumentHistory()

            dgvDocumentHistory.DataSource = dt

            ' Format the columns
            If dgvDocumentHistory.Columns.Contains("id") Then dgvDocumentHistory.Columns("id").Visible = False
            If dgvDocumentHistory.Columns.Contains("resident_id") Then dgvDocumentHistory.Columns("resident_id").Visible = False
            If dgvDocumentHistory.Columns.Contains("control_number") Then dgvDocumentHistory.Columns("control_number").HeaderText = "Control #"
            If dgvDocumentHistory.Columns.Contains("certificate_type") Then dgvDocumentHistory.Columns("certificate_type").HeaderText = "Type"
            If dgvDocumentHistory.Columns.Contains("purpose") Then dgvDocumentHistory.Columns("purpose").HeaderText = "Purpose"
            If dgvDocumentHistory.Columns.Contains("date_issued") Then dgvDocumentHistory.Columns("date_issued").HeaderText = "Date Issued"
            If dgvDocumentHistory.Columns.Contains("resident_name") Then dgvDocumentHistory.Columns("resident_name").HeaderText = "Resident"

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
            If dgvBlotter.Columns.Contains("complainant_cell") Then dgvBlotter.Columns("complainant_cell").Visible = False
            If dgvBlotter.Columns.Contains("respondent_cell") Then dgvBlotter.Columns("respondent_cell").Visible = False
            If dgvBlotter.Columns.Contains("street") Then dgvBlotter.Columns("street").Visible = False
            If dgvBlotter.Columns.Contains("full_information") Then dgvBlotter.Columns("full_information").Visible = False
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
                    newCase.ComplainantCell = addForm.ComplainantCell ' NEW
                    newCase.Respondent = addForm.Respondent
                    newCase.RespondentCell = addForm.RespondentCell   ' NEW
                    newCase.IncidentType = addForm.IncidentType
                    newCase.Location = addForm.IncidentLocation
                    newCase.Street = addForm.Street                   ' NEW
                    newCase.IncidentDate = addForm.IncidentDate
                    newCase.Status = addForm.Status
                    newCase.Narrative = addForm.Narrative
                    newCase.FullInformation = addForm.FullInformation ' NEW

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
            editForm.ComplainantCell = dgvBlotter.CurrentRow.Cells("complainant_cell").Value.ToString() ' NEW
            editForm.Respondent = dgvBlotter.CurrentRow.Cells("respondent").Value.ToString()
            editForm.RespondentCell = dgvBlotter.CurrentRow.Cells("respondent_cell").Value.ToString()   ' NEW
            editForm.IncidentType = dgvBlotter.CurrentRow.Cells("incident_type").Value.ToString()
            editForm.Status = dgvBlotter.CurrentRow.Cells("status").Value.ToString()

            If dgvBlotter.Columns.Contains("location") Then
                editForm.IncidentLocation = dgvBlotter.CurrentRow.Cells("location").Value.ToString()
            End If
            If dgvBlotter.Columns.Contains("street") Then
                editForm.Street = dgvBlotter.CurrentRow.Cells("street").Value.ToString() ' NEW
            End If
            If dgvBlotter.Columns.Contains("narrative") Then
                editForm.Narrative = dgvBlotter.CurrentRow.Cells("narrative").Value.ToString()
            End If
            If dgvBlotter.Columns.Contains("full_information") Then
                editForm.FullInformation = dgvBlotter.CurrentRow.Cells("full_information").Value.ToString() ' NEW
            End If

            Dim dDate = dgvBlotter.CurrentRow.Cells("incident_date").Value
            If IsDate(dDate) Then editForm.IncidentDate = CDate(dDate)

            ' 3. Show Form & Save
            If editForm.ShowDialog() = DialogResult.OK Then
                Try
                    Dim upCase As New BlotterCase()
                    upCase.Id = id
                    upCase.Complainant = editForm.Complainant
                    upCase.ComplainantCell = editForm.ComplainantCell ' NEW
                    upCase.Respondent = editForm.Respondent
                    upCase.RespondentCell = editForm.RespondentCell   ' NEW
                    upCase.IncidentType = editForm.IncidentType
                    upCase.Location = editForm.IncidentLocation
                    upCase.Street = editForm.Street                   ' NEW
                    upCase.IncidentDate = editForm.IncidentDate
                    upCase.Status = editForm.Status
                    upCase.Narrative = editForm.Narrative
                    upCase.FullInformation = editForm.FullInformation ' NEW

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
    Private Sub btnSystemMaintenance_Click(sender As Object, e As EventArgs) Handles btnSystemMaintenance.Click
        Using maintForm As New FormSystemMaintenance()
            maintForm.ShowDialog()
        End Using
    End Sub

    Private Sub LoadAuditTrail()
        Try
            Dim repo As New AuditRepository()
            dgvAuditTrail.DataSource = repo.GetAuditHistory()

            ' Formatting
            If dgvAuditTrail.Columns.Contains("action_date") Then
                dgvAuditTrail.Columns("action_date").HeaderText = "Date & Time"
                dgvAuditTrail.Columns("action_date").DefaultCellStyle.Format = "MM/dd/yyyy hh:mm tt"
            End If
            If dgvAuditTrail.Columns.Contains("username") Then dgvAuditTrail.Columns("username").HeaderText = "User"
            If dgvAuditTrail.Columns.Contains("role") Then dgvAuditTrail.Columns("role").HeaderText = "Role"
            If dgvAuditTrail.Columns.Contains("action_type") Then dgvAuditTrail.Columns("action_type").HeaderText = "Action"
            If dgvAuditTrail.Columns.Contains("module") Then dgvAuditTrail.Columns("module").HeaderText = "Module"
            If dgvAuditTrail.Columns.Contains("description") Then dgvAuditTrail.Columns("description").HeaderText = "Description"

            dgvAuditTrail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgvAuditTrail.ReadOnly = True
            dgvAuditTrail.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Catch ex As Exception
            MessageBox.Show("Error loading audit trail: " & ex.Message)
        End Try
    End Sub

    ' --- SUB-MENU 1: USER MAINTENANCE ---
    ' --- SUB-MENU 1: USER MAINTENANCE ---
    Private Sub btnUserMaintenance_Click(sender As Object, e As EventArgs) Handles btnUserMaintenance.Click
        HideAllPanels()

        ' 1. Turn ON the Parent Panel first!
        pnlUserMaintenance.Visible = True

        ' 2. Turn OFF the Audit child, Turn ON the Add Users child
        pnlAuditTrail.Visible = False
        pnlAddUsers.Visible = True

        ' 3. Force it to the front so it isn't hidden behind anything
        pnlAddUsers.BringToFront()

        ' 4. DYNAMICALLY LOAD SECURITY QUESTIONS!
        Try
            Dim repo As New LookupRepository()
            Dim dt As DataTable = repo.GetItemsByCategory("Security Question")

            cmbUserQuestion.Items.Clear()
            For Each row As DataRow In dt.Rows
                ' Grab the question text and add it to the dropdown
                cmbUserQuestion.Items.Add(row("item_value").ToString())
            Next
        Catch ex As Exception
            ' If it fails, we just leave it blank for now
        End Try

        LoadUsers()
    End Sub

    ' --- SUB-MENU 2: AUDIT TRAIL ---
    Private Sub btnAuditTrail_Click(sender As Object, e As EventArgs) Handles btnAuditTrail.Click
        HideAllPanels()

        ' 1. Turn ON the Parent Panel first!
        pnlUserMaintenance.Visible = True

        ' 2. Turn OFF the Add Users child, Turn ON the Audit child
        pnlAddUsers.Visible = False
        pnlAuditTrail.Visible = True

        ' 3. Force it to the front
        pnlAuditTrail.BringToFront()

        LoadAuditTrail()
    End Sub

    Private Sub TimerClock_Tick(sender As Object, e As EventArgs) Handles TimerClock.Tick
        ' Grabs the exact current time from your computer and formats it beautifully!
        lblClock.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy   |   hh:mm:ss tt")
    End Sub

    ' --- VALIDATION 1: Full Name Shield ---
    ' Allows ONLY Letters, Spaces, Backspace, and Dashes (e.g., "Mary-Jane")
    Private Sub txtUserFullname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUserFullname.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) AndAlso e.KeyChar <> "-" Then
            e.Handled = True ' Eats the keystroke so numbers/symbols never appear
        End If
    End Sub

    ' --- VALIDATION 2: Username Shield ---
    ' Allows ONLY Letters and Numbers (No spaces, no symbols, no dashes)
    Private Sub txtUserUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUserUsername.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Eats the keystroke
        End If
    End Sub

    Private Sub btnAddUsers_Click_Fix(sender As Object, e As EventArgs) Handles btnAddUsers.Click
        ' When they click the "Add Users" sub-menu button, 
        ' just mimic a click on the main Admin sidebar button!
        btnUserMaintenance.PerformClick()
    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles Label28.Click

    End Sub
End Class


