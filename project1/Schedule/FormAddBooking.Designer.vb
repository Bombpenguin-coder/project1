<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddBooking
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        grpBooking = New GroupBox()
        Label2 = New Label()
        Label1 = New Label()
        txtInCharge = New TextBox()
        txtReserverName = New TextBox()
        rdoNonResident = New RadioButton()
        rdoResident = New RadioButton()
        btnCancel = New Button()
        btnSave = New Button()
        Label18 = New Label()
        dgvResidentLookup = New DataGridView()
        Label19 = New Label()
        cmbFacility = New ComboBox()
        Label20 = New Label()
        Label21 = New Label()
        txtResidentSearch = New TextBox()
        Label22 = New Label()
        lblSelectedResident = New Label()
        txtEventName = New TextBox()
        btnSaveBooking = New Button()
        dtpEndTime = New DateTimePicker()
        dtpStartTime = New DateTimePicker()
        grpBooking.SuspendLayout()
        CType(dgvResidentLookup, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' grpBooking
        ' 
        grpBooking.BackColor = Color.FromArgb(CByte(98), CByte(155), CByte(181))
        grpBooking.Controls.Add(Label2)
        grpBooking.Controls.Add(Label1)
        grpBooking.Controls.Add(txtInCharge)
        grpBooking.Controls.Add(txtReserverName)
        grpBooking.Controls.Add(rdoNonResident)
        grpBooking.Controls.Add(rdoResident)
        grpBooking.Controls.Add(btnCancel)
        grpBooking.Controls.Add(btnSave)
        grpBooking.Controls.Add(Label18)
        grpBooking.Controls.Add(dgvResidentLookup)
        grpBooking.Controls.Add(Label19)
        grpBooking.Controls.Add(cmbFacility)
        grpBooking.Controls.Add(Label20)
        grpBooking.Controls.Add(Label21)
        grpBooking.Controls.Add(txtResidentSearch)
        grpBooking.Controls.Add(Label22)
        grpBooking.Controls.Add(lblSelectedResident)
        grpBooking.Controls.Add(txtEventName)
        grpBooking.Controls.Add(btnSaveBooking)
        grpBooking.Controls.Add(dtpEndTime)
        grpBooking.Controls.Add(dtpStartTime)
        grpBooking.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        grpBooking.ForeColor = Color.White
        grpBooking.Location = New Point(19, 12)
        grpBooking.Name = "grpBooking"
        grpBooking.Size = New Size(782, 448)
        grpBooking.TabIndex = 3
        grpBooking.TabStop = False
        grpBooking.Text = "New Booking"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(23, 187)
        Label2.Name = "Label2"
        Label2.Size = New Size(124, 20)
        Label2.TabIndex = 25
        Label2.Text = "In-Charge Name"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(23, 134)
        Label1.Name = "Label1"
        Label1.Size = New Size(117, 20)
        Label1.TabIndex = 24
        Label1.Text = "Reserver Name"
        ' 
        ' txtInCharge
        ' 
        txtInCharge.Location = New Point(23, 205)
        txtInCharge.Name = "txtInCharge"
        txtInCharge.Size = New Size(178, 27)
        txtInCharge.TabIndex = 23
        ' 
        ' txtReserverName
        ' 
        txtReserverName.Location = New Point(23, 157)
        txtReserverName.Name = "txtReserverName"
        txtReserverName.Size = New Size(178, 27)
        txtReserverName.TabIndex = 22
        ' 
        ' rdoNonResident
        ' 
        rdoNonResident.AutoSize = True
        rdoNonResident.Location = New Point(372, 158)
        rdoNonResident.Name = "rdoNonResident"
        rdoNonResident.Size = New Size(241, 24)
        rdoNonResident.TabIndex = 21
        rdoNonResident.TabStop = True
        rdoNonResident.Text = "Non-Resident/Other Barangay"
        rdoNonResident.UseVisualStyleBackColor = True
        ' 
        ' rdoResident
        ' 
        rdoResident.AutoSize = True
        rdoResident.Location = New Point(237, 158)
        rdoResident.Name = "rdoResident"
        rdoResident.Size = New Size(88, 24)
        rdoResident.TabIndex = 20
        rdoResident.TabStop = True
        rdoResident.Text = "Resident"
        rdoResident.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.IndianRed
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(553, 374)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(86, 34)
        btnCancel.TabIndex = 19
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(656, 374)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(86, 34)
        btnSave.TabIndex = 18
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(19, 84)
        Label18.Name = "Label18"
        Label18.Size = New Size(73, 20)
        Label18.TabIndex = 13
        Label18.Text = "Facilities:"
        ' 
        ' dgvResidentLookup
        ' 
        dgvResidentLookup.AllowUserToAddRows = False
        dgvResidentLookup.AllowUserToDeleteRows = False
        dgvResidentLookup.AllowUserToResizeColumns = False
        dgvResidentLookup.AllowUserToResizeRows = False
        dgvResidentLookup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResidentLookup.BackgroundColor = SystemColors.Window
        dgvResidentLookup.BorderStyle = BorderStyle.None
        dgvResidentLookup.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgvResidentLookup.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(235), CByte(243), CByte(249))
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 11.25F)
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(CByte(123), CByte(189), CByte(232))
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvResidentLookup.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvResidentLookup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Control
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 11.25F)
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvResidentLookup.DefaultCellStyle = DataGridViewCellStyle2
        dgvResidentLookup.EnableHeadersVisualStyles = False
        dgvResidentLookup.GridColor = SystemColors.Info
        dgvResidentLookup.Location = New Point(23, 325)
        dgvResidentLookup.Name = "dgvResidentLookup"
        dgvResidentLookup.RowHeadersVisible = False
        dgvResidentLookup.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvResidentLookup.Size = New Size(393, 83)
        dgvResidentLookup.TabIndex = 10
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(19, 251)
        Label19.Name = "Label19"
        Label19.Size = New Size(124, 20)
        Label19.TabIndex = 14
        Label19.Text = "Search Resident:"
        ' 
        ' cmbFacility
        ' 
        cmbFacility.Font = New Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        cmbFacility.FormattingEnabled = True
        cmbFacility.Location = New Point(21, 106)
        cmbFacility.Name = "cmbFacility"
        cmbFacility.Size = New Size(254, 25)
        cmbFacility.TabIndex = 3
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(21, 34)
        Label20.Name = "Label20"
        Label20.Size = New Size(98, 20)
        Label20.TabIndex = 15
        Label20.Text = "Event Name:"
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(539, 32)
        Label21.Name = "Label21"
        Label21.Size = New Size(84, 20)
        Label21.TabIndex = 16
        Label21.Text = "Date Start:"
        ' 
        ' txtResidentSearch
        ' 
        txtResidentSearch.Font = New Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtResidentSearch.ForeColor = SystemColors.WindowText
        txtResidentSearch.Location = New Point(21, 274)
        txtResidentSearch.Name = "txtResidentSearch"
        txtResidentSearch.Size = New Size(172, 25)
        txtResidentSearch.TabIndex = 4
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(539, 85)
        Label22.Name = "Label22"
        Label22.Size = New Size(76, 20)
        Label22.TabIndex = 17
        Label22.Text = "Date End:"
        ' 
        ' lblSelectedResident
        ' 
        lblSelectedResident.AutoSize = True
        lblSelectedResident.Location = New Point(21, 302)
        lblSelectedResident.Name = "lblSelectedResident"
        lblSelectedResident.Size = New Size(136, 20)
        lblSelectedResident.TabIndex = 5
        lblSelectedResident.Text = "Selected Resident:"
        ' 
        ' txtEventName
        ' 
        txtEventName.Font = New Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtEventName.Location = New Point(21, 56)
        txtEventName.Name = "txtEventName"
        txtEventName.Size = New Size(254, 25)
        txtEventName.TabIndex = 6
        ' 
        ' btnSaveBooking
        ' 
        btnSaveBooking.Font = New Font("Segoe UI Light", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnSaveBooking.ForeColor = Color.Black
        btnSaveBooking.Location = New Point(910, 190)
        btnSaveBooking.Name = "btnSaveBooking"
        btnSaveBooking.Size = New Size(113, 53)
        btnSaveBooking.TabIndex = 9
        btnSaveBooking.Text = "Save Booking"
        btnSaveBooking.UseVisualStyleBackColor = True
        ' 
        ' dtpEndTime
        ' 
        dtpEndTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt"
        dtpEndTime.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpEndTime.Format = DateTimePickerFormat.Custom
        dtpEndTime.Location = New Point(542, 108)
        dtpEndTime.Name = "dtpEndTime"
        dtpEndTime.Size = New Size(200, 25)
        dtpEndTime.TabIndex = 8
        ' 
        ' dtpStartTime
        ' 
        dtpStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt"
        dtpStartTime.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpStartTime.Format = DateTimePickerFormat.Custom
        dtpStartTime.Location = New Point(542, 57)
        dtpStartTime.Name = "dtpStartTime"
        dtpStartTime.Size = New Size(200, 25)
        dtpStartTime.TabIndex = 7
        ' 
        ' FormAddBooking
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(825, 484)
        Controls.Add(grpBooking)
        FormBorderStyle = FormBorderStyle.None
        Name = "FormAddBooking"
        Text = "FormAddBooking"
        grpBooking.ResumeLayout(False)
        grpBooking.PerformLayout()
        CType(dgvResidentLookup, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents grpBooking As GroupBox
    Friend WithEvents Label18 As Label
    Friend WithEvents dgvResidentLookup As DataGridView
    Friend WithEvents Label19 As Label
    Friend WithEvents cmbFacility As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtResidentSearch As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents lblSelectedResident As Label
    Friend WithEvents txtEventName As TextBox
    Friend WithEvents btnSaveBooking As Button
    Friend WithEvents dtpEndTime As DateTimePicker
    Friend WithEvents dtpStartTime As DateTimePicker
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents rdoNonResident As RadioButton
    Friend WithEvents rdoResident As RadioButton
    Friend WithEvents txtReserverName As TextBox
    Friend WithEvents txtInCharge As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
