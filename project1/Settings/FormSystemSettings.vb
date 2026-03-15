Public Class FormSystemSettings

    Private Sub FormSystemSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' 1. Create the worker
            Dim repo As New SettingsRepository()

            ' 2. Get the current settings from the database
            Dim currentSettings As SystemSetting = repo.GetSettings()

            ' 3. Fill the textboxes with the data
            txtBarangayName.Text = currentSettings.BarangayName
            txtCityName.Text = currentSettings.CityName
            txtProvinceName.Text = currentSettings.ProvinceName
            txtCaptainName.Text = currentSettings.CaptainName
            txtContactNumber.Text = currentSettings.ContactNumber

        Catch ex As Exception
            MessageBox.Show("Error loading settings: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validation: Don't let them save blank fields!
        If String.IsNullOrWhiteSpace(txtBarangayName.Text) OrElse
           String.IsNullOrWhiteSpace(txtCaptainName.Text) Then
            MessageBox.Show("Barangay Name and Captain Name cannot be empty.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' 1. Pack the updated data into our Model
            Dim updatedSettings As New SystemSetting()
            updatedSettings.BarangayName = txtBarangayName.Text.Trim()
            updatedSettings.CityName = txtCityName.Text.Trim()
            updatedSettings.ProvinceName = txtProvinceName.Text.Trim()
            updatedSettings.CaptainName = txtCaptainName.Text.Trim()
            updatedSettings.ContactNumber = txtContactNumber.Text.Trim()

            ' 2. Send it to the Repository to save
            Dim repo As New SettingsRepository()
            repo.UpdateSettings(updatedSettings)

            ' 3. Close the form with a success flag
            MessageBox.Show("System settings updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error saving settings: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class