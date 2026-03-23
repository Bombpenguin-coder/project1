Imports MySql.Data.MySqlClient

Public Class FormIssueCertificate
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' Trackers for the selected resident
    Public Property SelectedResidentId As Integer = 0
    Public Property SelectedResidentName As String = ""

    ' --- PROPERTIES (Bridges for Main Form) ---
    Public Property CertificateType As String
        Get
            Return cmbCertificateType.Text
        End Get
        Set(value As String)
            cmbCertificateType.Text = value
        End Set
    End Property

    Public Property Purpose As String
        Get
            Return txtPurpose.Text.Trim()
        End Get
        Set(value As String)
            txtPurpose.Text = value
        End Set
    End Property

    Public Property AmountPaid As Decimal
        Get
            Dim amt As Decimal
            If Decimal.TryParse(txtAmountPaid.Text, amt) Then Return amt
            Return 0D
        End Get
        Set(value As Decimal)
            txtAmountPaid.Text = value.ToString("F2")
        End Set
    End Property

    ' --- 1. DYNAMIC LOAD FIX ---
    Private Sub FormIssueCertificate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Fetch the live document types from your System Settings!
        Try
            Dim repo As New LookupRepository()
            Dim dt As DataTable = repo.GetItemsByCategory("Document Type")

            cmbCertificateType.Items.Clear()
            For Each row As DataRow In dt.Rows
                cmbCertificateType.Items.Add(row("item_value").ToString())
            Next
        Catch ex As Exception
            ' Fallback just in case the database is empty
            cmbCertificateType.Items.AddRange(New String() {"Barangay Clearance", "Certificate of Indigency", "Certificate of Residency"})
        End Try

        cmbCertificateType.DropDownStyle = ComboBoxStyle.DropDownList
        If cmbCertificateType.Items.Count > 0 Then cmbCertificateType.SelectedIndex = 0

        txtAmountPaid.Text = "0.00"
        lblSelectedResident.Text = "Selected Resident: (None)"

        ' Load the grid!
        LoadResidentsForLookup()
    End Sub

    ' --- 2. THE DATABASE CRASH FIX ---
    Private Sub LoadResidentsForLookup(Optional searchTerm As String = "")
        Try
            Using conn As New MySqlConnection(connectionString)
                conn.Open()

                ' FIX: Changed 'barangay' to 'district' so MySQL doesn't crash!
                Dim query As String = "SELECT id, lastname, firstname, middlename, address, district FROM residents"

                If Not String.IsNullOrWhiteSpace(searchTerm) Then
                    query &= " WHERE CONCAT(lastname, ' ', firstname, ' ', middlename) LIKE @SearchTerm OR address LIKE @SearchTerm "
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
        If e.RowIndex >= 0 AndAlso dgvResidentLookup.CurrentRow IsNot Nothing Then
            Dim row = dgvResidentLookup.CurrentRow
            SelectedResidentId = CInt(row.Cells("id").Value)
            SelectedResidentName = $"{row.Cells("lastname").Value}, {row.Cells("firstname").Value}"
            lblSelectedResident.Text = $"Selected Resident: {SelectedResidentName} (ID: {SelectedResidentId})"
        End If
    End Sub

    ' --- BUTTONS & VALIDATION ---
    Private Sub btnIssue_Click(sender As Object, e As EventArgs) Handles btnIssue.Click
        If SelectedResidentId = 0 Then
            MessageBox.Show("Please search and select a resident first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(CertificateType) Then
            MessageBox.Show("Please select a certificate type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If String.IsNullOrWhiteSpace(Purpose) Then
            MessageBox.Show("Please enter a purpose.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim amt As Decimal
        If Not Decimal.TryParse(txtAmountPaid.Text, amt) Then
            MessageBox.Show("Please enter a valid amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' Restrict Amount text box to numbers and decimals only
    Private Sub txtAmountPaid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmountPaid.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        ' Prevent multiple decimals
        If e.KeyChar = "."c AndAlso txtAmountPaid.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbCertificateType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCertificateType.SelectedIndexChanged
        If cmbCertificateType.SelectedItem Is Nothing Then Return

        Dim selectedDoc As String = cmbCertificateType.Text
        Dim repo As New LookupRepository()

        ' Fetch all document types from the database
        Dim dt As DataTable = repo.GetItemsByCategory("Document Type")

        ' Search the results for the exact document they selected
        For Each row As DataRow In dt.Rows
            If row("item_value").ToString() = selectedDoc Then
                ' Found it! Paste the price into the Amount Paid box!
                ' (Change 'txtAmountPaid' to whatever you named your textbox)
                txtAmountPaid.Text = Convert.ToDecimal(row("item_price")).ToString("0.00")
                Exit For
            End If
        Next
    End Sub
End Class