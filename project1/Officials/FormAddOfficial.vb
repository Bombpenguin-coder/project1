Public Class FormAddOfficial
    ' ID Tracker for Editing
    Public Property OfficialID As Integer = 0

    ' --- PROPERTIES (Bridges for Main Form) ---
    Public Property FullName As String
        Get
            Return txtFullName.Text.Trim()
        End Get
        Set(value As String)
            txtFullName.Text = value
        End Set
    End Property

    Public Property Position As String
        Get
            Return txtPosition.Text.Trim()
        End Get
        Set(value As String)
            txtPosition.Text = value
        End Set
    End Property

    Public Property ContactNumber As String
        Get
            Return txtContactNumber.Text.Trim()
        End Get
        Set(value As String)
            txtContactNumber.Text = value
        End Set
    End Property

    ' --- FORM EVENTS ---
    Private Sub FormAddOfficial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OfficialID > 0 Then
            Me.Text = "Edit Official (ID: " & OfficialID & ")"
            btnSave.Text = "Update"
        Else
            Me.Text = "Add New Official"
            btnSave.Text = "Save"
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validation
        If String.IsNullOrWhiteSpace(FullName) OrElse String.IsNullOrWhiteSpace(Position) Then
            MessageBox.Show("Please fill in the Name and Position fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Contact Number Validation (Must be 11 digits and start with 09)
        If ContactNumber.Length > 0 Then
            If ContactNumber.Length <> 11 Then
                MessageBox.Show("Contact number must be exactly 11 digits (e.g., 09xxxxxxxxx).", "Invalid Contact", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If Not ContactNumber.StartsWith("09") Then
                MessageBox.Show("Contact number must start with '09'.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' Optional: Restrict contact number box to only allow numbers
    Private Sub txtContactNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContactNumber.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then
            e.Handled = True
        End If
    End Sub
End Class