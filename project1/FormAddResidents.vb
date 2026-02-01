Imports MySql.Data.MySqlClient

Public Class FormAddResidents

    ' 1. CRITICAL: Add the ResidentID property for Edit Mode tracking
    Public Property ResidentID As Integer = 0 ' 0 means ADD mode, > 0 means EDIT mode

    Public MainForm As FormMain

    ' 2. Property Getters are the correct way to retrieve the final values
    '    when the main form accesses them (after DialogResult.OK)
    Public ReadOnly Property LastName As String
        Get
            Return txtLastName.Text.Trim()
        End Get
    End Property
    Public ReadOnly Property FirstName As String
        Get
            Return txtFirstName.Text.Trim()
        End Get
    End Property
    Public ReadOnly Property MiddleName As String
        Get
            Return txtMiddleName.Text.Trim()
        End Get
    End Property

    ' Calculates age on retrieval, useful if you need the age outside
    Public ReadOnly Property Age As Integer
        Get
            Dim bdate As Date = dtpBirthDate.Value.Date
            Dim calculatedAge As Integer = DateTime.Now.Year - bdate.Year
            ' Adjust for birthday not yet reached this year
            If DateTime.Now < bdate.AddYears(calculatedAge) Then
                calculatedAge -= 1
            End If
            Return If(calculatedAge < 0, 0, calculatedAge)
        End Get
    End Property

    Public ReadOnly Property Gender As String
        Get
            Return cmbGender.Text
        End Get
    End Property

    ' Note: Your existing code for Address, District, etc. needs to be converted
    '       to ReadOnly Properties too, like the examples above.
    '       I've provided the correct structure for all below.

    Public ReadOnly Property Address As String
        Get
            Return txtAddress.Text.Trim()
        End Get
    End Property
    Public ReadOnly Property District As String
        Get
            Return txtDistrict.Text.Trim()
        End Get
    End Property
    Public ReadOnly Property Barangay As String
        Get
            Return cmbBarangay.Text
        End Get
    End Property
    Public ReadOnly Property City As String
        Get
            Return txtCity.Text.Trim()
        End Get
    End Property

    Public ReadOnly Property BirthDate As Date
        Get
            Return dtpBirthDate.Value.Date
        End Get
    End Property

    Private Sub FormAddResidents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup ComboBox (Barangay is a bit static, but fine for now)
        cmbBarangay.Items.Clear()
        cmbBarangay.Items.AddRange(New String() {"Central", "South", "North"})
        If cmbBarangay.Items.Count > 0 Then cmbBarangay.SelectedIndex = 0
        cmbBarangay.DropDownStyle = ComboBoxStyle.DropDownList

        ' 3. EDIT MODE CHECK: Change the UI based on ResidentID
        If ResidentID > 0 Then
            Me.Text = "Edit Resident Information (ID: " & ResidentID.ToString() & ")"
            btnSaveResident.Text = "Update Resident"
            ' NOTE: The main form should have ALREADY populated the textboxes here.
        Else
            Me.Text = "Add New Resident"
            btnSaveResident.Text = "Save Resident"
        End If

        ' This forces the label to update based on whatever date is currently set
        dtpBirthDate_ValueChanged(Nothing, Nothing)
    End Sub

    Private Sub btnSaveResident_Click(sender As Object, e As EventArgs) Handles btnSaveResident.Click

        If String.IsNullOrWhiteSpace(txtLastName.Text) OrElse
           String.IsNullOrWhiteSpace(txtFirstName.Text) OrElse
           String.IsNullOrWhiteSpace(txtAddress.Text) OrElse
           String.IsNullOrWhiteSpace(txtDistrict.Text) OrElse
           String.IsNullOrWhiteSpace(cmbBarangay.Text) OrElse
           String.IsNullOrWhiteSpace(txtCity.Text) OrElse
           String.IsNullOrWhiteSpace(cmbGender.Text) Then

            MessageBox.Show("Please fill in all required fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Optional: Check if the calculated age is reasonable (e.g., birthdate isn't today)
        If Me.Age = 0 AndAlso dtpBirthDate.Value.Date < Date.Today Then
            MessageBox.Show("The calculated age is 0, please check the Birth Date.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' Return ' Uncomment to force correction
        End If

        If dtpBirthDate.Value.Date > Date.Today Then
            MessageBox.Show("Birth date cannot be in the future.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' 4. REMOVE REDUNDANT ASSIGNMENTS:
            '    Because the properties are ReadOnly and use Getters, they already pull the data
            '    from the textboxes/controls when the main form requests them.
            '    We only need to set DialogResult = OK.

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error preparing resident data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged

    End Sub

    Private Sub dtpBirthDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpBirthDate.ValueChanged
        ' 1. Get the selected date
        Dim bdate As Date = dtpBirthDate.Value.Date

        ' 2. Calculate the difference in years
        Dim currentAge As Integer = DateTime.Now.Year - bdate.Year

        ' 3. Check if the birthday has happened yet this year
        ' If today is BEFORE their birthday this year, subtract 1
        If DateTime.Now < bdate.AddYears(currentAge) Then
            currentAge -= 1
        End If

        ' 4. Safety Check: Prevent negative numbers (if they pick a future date)
        If currentAge < 0 Then
            currentAge = 0
            lblCalculatedAge.ForeColor = Color.Red ' Visual warning
            lblCalculatedAge.Text = "Invalid Date"
        Else
            lblCalculatedAge.ForeColor = Color.Black ' Normal color
            lblCalculatedAge.Text = "Age: " & currentAge.ToString()
        End If
    End Sub
End Class