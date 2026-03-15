Public Class FormSystemMaintenance

    ' --- 1. FORM LOAD ---
    Private Sub FormSystemMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup the Category dropdown
        cmbCategory.Items.Clear()
        cmbCategory.Items.AddRange(New String() {"Street", "Incident Type", "Facility"})
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList ' Forces them to pick from the list

        ' Select the first item automatically (this triggers the grid to load)
        If cmbCategory.Items.Count > 0 Then
            cmbCategory.SelectedIndex = 0
        End If
    End Sub

    ' --- 2. DYNAMIC GRID LOADING ---
    ' This fires automatically whenever the Admin changes the dropdown category!
    Private Sub cmbCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategory.SelectedIndexChanged
        LoadGridData()
    End Sub

    Private Sub LoadGridData()
        If cmbCategory.SelectedItem Is Nothing Then Return

        Dim selectedCategory As String = cmbCategory.SelectedItem.ToString()

        Try
            Dim repo As New LookupRepository()
            Dim dt As DataTable = repo.GetItemsByCategory(selectedCategory)

            dgvItems.DataSource = dt

            ' Format the grid
            If dgvItems.Columns.Contains("id") Then dgvItems.Columns("id").Visible = False
            If dgvItems.Columns.Contains("item_value") Then dgvItems.Columns("item_value").HeaderText = selectedCategory & " Name"

            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvItems.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- 3. ADD BUTTON ---
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim newItem As String = txtNewItem.Text.Trim()
        Dim category As String = cmbCategory.SelectedItem.ToString()

        If String.IsNullOrWhiteSpace(newItem) Then
            MessageBox.Show("Please enter a name to add.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim repo As New LookupRepository()
            repo.AddItem(category, newItem)

            MessageBox.Show($"{category} added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNewItem.Clear()

            ' Instantly refresh the grid to show the new item
            LoadGridData()

        Catch ex As Exception
            MessageBox.Show("Error adding item: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- 4. DELETE BUTTON ---
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvItems.CurrentRow Is Nothing Then
            MessageBox.Show("Please select an item from the list to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim id As Integer = CInt(dgvItems.CurrentRow.Cells("id").Value)
        Dim itemValue As String = dgvItems.CurrentRow.Cells("item_value").Value.ToString()
        Dim category As String = cmbCategory.SelectedItem.ToString()

        Dim result As DialogResult = MessageBox.Show($"Are you sure you want to permanently delete '{itemValue}' from the {category} list?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Try
                Dim repo As New LookupRepository()
                repo.DeleteItem(id)

                MessageBox.Show($"{category} deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Instantly refresh the grid
                LoadGridData()

            Catch ex As Exception
                MessageBox.Show("Error deleting item: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' --- 5. CANCEL BUTTON ---
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class