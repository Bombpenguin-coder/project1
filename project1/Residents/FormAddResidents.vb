Imports MySql.Data.MySqlClient

Public Class FormAddResidents

    ' 1. ID Property 
    Public Property ResidentID As Integer = 0

    ' 2. TEXT FIELDS
    Public Property LastName As String
        Get
            Return txtLastName.Text.Trim()
        End Get
        Set(value As String)
            txtLastName.Text = value
        End Set
    End Property

    Public Property FirstName As String
        Get
            Return txtFirstName.Text.Trim()
        End Get
        Set(value As String)
            txtFirstName.Text = value
        End Set
    End Property

    Public Property MiddleName As String
        Get
            Return txtMiddleName.Text.Trim()
        End Get
        Set(value As String)
            txtMiddleName.Text = value
        End Set
    End Property

    Public Property Address As String
        Get
            Return txtAddress.Text.Trim()
        End Get
        Set(value As String)
            txtAddress.Text = value
        End Set
    End Property

    ' --- NEW STREET COMBOBOX ---
    Public Property Street As String
        Get
            Return cmbStreet.Text
        End Get
        Set(value As String)
            cmbStreet.Text = value
        End Set
    End Property

    ' 3. DROPDOWNS 
    Public Property Sex As String
        Get
            Return cmbSex.Text
        End Get
        Set(value As String)
            cmbSex.Text = value
        End Set
    End Property

    ' 4. DATE & AGE
    Public Property BirthDate As Date
        Get
            Return dtpBirthDate.Value.Date
        End Get
        Set(value As Date)
            If value < dtpBirthDate.MinDate Then
                dtpBirthDate.Value = dtpBirthDate.MinDate
            ElseIf value > dtpBirthDate.MaxDate Then
                dtpBirthDate.Value = dtpBirthDate.MaxDate
            Else
                dtpBirthDate.Value = value
            End If
        End Set
    End Property

    Public ReadOnly Property Age As Integer
        Get
            Dim bdate As Date = dtpBirthDate.Value.Date
            Dim calculatedAge As Integer = DateTime.Now.Year - bdate.Year
            If DateTime.Now < bdate.AddYears(calculatedAge) Then
                calculatedAge -= 1
            End If
            Return If(calculatedAge < 0, 0, calculatedAge)
        End Get
    End Property

    ' ==========================================
    ' FORM EVENTS & VALIDATIONS
    ' ==========================================

    Private Sub FormAddResidents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup ComboBox default for Sex
        If cmbSex.Items.Count = 0 Then
            cmbSex.Items.AddRange(New String() {"Male", "Female"})
        End If

        dtpBirthDate.MaxDate = Date.Today

        ' Dynamically load streets from database
        LoadStreetsFromDatabase()

        ' UI Tweak: Change title based on mode
        If ResidentID > 0 Then
            Me.Text = "Edit Resident (ID: " & ResidentID.ToString() & ")"
            btnSaveResident.Text = "Update"
        Else
            Me.Text = "Add New Resident"
            btnSaveResident.Text = "Save"
        End If

        dtpBirthDate_ValueChanged(Nothing, Nothing)
    End Sub

    Private Sub LoadStreetsFromDatabase()
        cmbStreet.Items.Clear()
        Dim connString As String = "server=localhost;user id=root;password=;database=barangay_db"

        Try
            Using conn As New MySqlConnection(connString)
                conn.Open()
                ' Now looking at your actual System Lookups table!
                Dim query As String = "SELECT item_value FROM system_lookups WHERE category = 'Street' ORDER BY item_value ASC"
                Dim cmd As New MySqlCommand(query, conn)

                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        ' Grabbing the item_value column
                        cmbStreet.Items.Add(reader("item_value").ToString())
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Could not load streets: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub dtpBirthDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpBirthDate.ValueChanged
        lblCalculatedAge.Text = "Age: " & Me.Age.ToString()
    End Sub

    ' The "No Letters Allowed" Shield for Names
    Private Sub NameFields_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFirstName.KeyPress, txtLastName.KeyPress, txtMiddleName.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) AndAlso e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnSaveResident_Click(sender As Object, e As EventArgs) Handles btnSaveResident.Click
        ' Validation Check: Now includes Street and District!
        If String.IsNullOrWhiteSpace(LastName) OrElse
           String.IsNullOrWhiteSpace(FirstName) OrElse
           String.IsNullOrWhiteSpace(Address) OrElse
           String.IsNullOrWhiteSpace(Street) OrElse
           String.IsNullOrWhiteSpace(Sex) Then

            MessageBox.Show("Please fill in all required fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Me.Age < 0 Then
            MessageBox.Show("Invalid Birth Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Success! Pass data back to main form
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class