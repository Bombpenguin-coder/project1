Public Class FormAddResidents

    ' 1. ID Property (Stores the ID if we are editing)
    Public Property ResidentID As Integer = 0

    ' 2. TEXT FIELDS (Read/Write Bridges)
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

    Public Property District As String
        Get
            Return txtDistrict.Text.Trim()
        End Get
        Set(value As String)
            txtDistrict.Text = value
        End Set
    End Property

    Public Property City As String
        Get
            Return txtCity.Text.Trim()
        End Get
        Set(value As String)
            txtCity.Text = value
        End Set
    End Property

    ' 3. DROPDOWNS
    Public Property Gender As String
        Get
            Return cmbGender.Text
        End Get
        Set(value As String)
            cmbGender.Text = value
        End Set
    End Property

    Public Property Barangay As String
        Get
            Return cmbBarangay.Text
        End Get
        Set(value As String)
            cmbBarangay.Text = value
        End Set
    End Property

    ' 4. DATE & AGE (With Safety Checks)
    Public Property BirthDate As Date
        Get
            Return dtpBirthDate.Value.Date
        End Get
        Set(value As Date)
            ' Protect the DatePicker from crashing with invalid dates
            If value < dtpBirthDate.MinDate Then
                dtpBirthDate.Value = dtpBirthDate.MinDate
            ElseIf value > dtpBirthDate.MaxDate Then
                dtpBirthDate.Value = dtpBirthDate.MaxDate
            Else
                dtpBirthDate.Value = value
            End If
        End Set
    End Property

    ' Age is calculated automatically, so we only need a Getter (ReadOnly)
    Public ReadOnly Property Age As Integer
        Get
            Dim bdate As Date = dtpBirthDate.Value.Date
            Dim calculatedAge As Integer = DateTime.Now.Year - bdate.Year

            ' Subtract 1 if birthday hasn't happened yet this year
            If DateTime.Now < bdate.AddYears(calculatedAge) Then
                calculatedAge -= 1
            End If

            Return If(calculatedAge < 0, 0, calculatedAge)
        End Get
    End Property

    ' ==========================================
    ' FORM EVENTS
    ' ==========================================

    Private Sub FormAddResidents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup ComboBox defaults
        If cmbBarangay.Items.Count = 0 Then
            cmbBarangay.Items.AddRange(New String() {"Central", "South", "North"})
        End If

        ' UI Tweak: Change title based on mode
        If ResidentID > 0 Then
            Me.Text = "Edit Resident (ID: " & ResidentID.ToString() & ")"
            btnSaveResident.Text = "Update"
        Else
            Me.Text = "Add New Resident"
            btnSaveResident.Text = "Save"
        End If

        ' Trigger the age calculation display
        dtpBirthDate_ValueChanged(Nothing, Nothing)
    End Sub

    Private Sub dtpBirthDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpBirthDate.ValueChanged
        ' Update the Age label visually when date changes
        lblCalculatedAge.Text = "Age: " & Me.Age.ToString()
    End Sub

    Private Sub btnSaveResident_Click(sender As Object, e As EventArgs) Handles btnSaveResident.Click
        ' Validation
        If String.IsNullOrWhiteSpace(LastName) OrElse
           String.IsNullOrWhiteSpace(FirstName) OrElse
           String.IsNullOrWhiteSpace(Address) OrElse
           String.IsNullOrWhiteSpace(Gender) Then

            MessageBox.Show("Please fill in all required fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Me.Age < 0 Then
            MessageBox.Show("Invalid Birth Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Success! The Main Form will retrieve the values from the Properties above.
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class