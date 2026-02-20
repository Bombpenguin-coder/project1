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
        Label18 = New Label()
        dgvResidentSearch = New DataGridView()
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
        btnSave = New Button()
        btnCancel = New Button()
        grpBooking.SuspendLayout()
        CType(dgvResidentSearch, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' grpBooking
        ' 
        grpBooking.BackColor = Color.FromArgb(CByte(92), CByte(118), CByte(80))
        grpBooking.Controls.Add(btnCancel)
        grpBooking.Controls.Add(btnSave)
        grpBooking.Controls.Add(Label18)
        grpBooking.Controls.Add(dgvResidentSearch)
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
        grpBooking.Size = New Size(704, 389)
        grpBooking.TabIndex = 3
        grpBooking.TabStop = False
        grpBooking.Text = "New Booking"
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(23, 97)
        Label18.Name = "Label18"
        Label18.Size = New Size(73, 20)
        Label18.TabIndex = 13
        Label18.Text = "Facilities:"
        ' 
        ' dgvResidentSearch
        ' 
        dgvResidentSearch.AllowUserToAddRows = False
        dgvResidentSearch.AllowUserToDeleteRows = False
        dgvResidentSearch.AllowUserToResizeColumns = False
        dgvResidentSearch.AllowUserToResizeRows = False
        dgvResidentSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResidentSearch.BackgroundColor = SystemColors.Window
        dgvResidentSearch.BorderStyle = BorderStyle.None
        dgvResidentSearch.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgvResidentSearch.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(62), CByte(95), CByte(68))
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.Window
        DataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(CByte(128), CByte(175), CByte(129))
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvResidentSearch.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvResidentSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.ControlDarkDark
        DataGridViewCellStyle2.Font = New Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvResidentSearch.DefaultCellStyle = DataGridViewCellStyle2
        dgvResidentSearch.EnableHeadersVisualStyles = False
        dgvResidentSearch.Location = New Point(25, 228)
        dgvResidentSearch.Name = "dgvResidentSearch"
        dgvResidentSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvResidentSearch.Size = New Size(393, 83)
        dgvResidentSearch.TabIndex = 10
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(23, 154)
        Label19.Name = "Label19"
        Label19.Size = New Size(124, 20)
        Label19.TabIndex = 14
        Label19.Text = "Search Resident:"
        ' 
        ' cmbFacility
        ' 
        cmbFacility.Font = New Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        cmbFacility.FormattingEnabled = True
        cmbFacility.Location = New Point(25, 119)
        cmbFacility.Name = "cmbFacility"
        cmbFacility.Size = New Size(254, 25)
        cmbFacility.TabIndex = 3
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(25, 47)
        Label20.Name = "Label20"
        Label20.Size = New Size(98, 20)
        Label20.TabIndex = 15
        Label20.Text = "Event Name:"
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(469, 44)
        Label21.Name = "Label21"
        Label21.Size = New Size(84, 20)
        Label21.TabIndex = 16
        Label21.Text = "Date Start:"
        ' 
        ' txtResidentSearch
        ' 
        txtResidentSearch.Font = New Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtResidentSearch.Location = New Point(25, 177)
        txtResidentSearch.Name = "txtResidentSearch"
        txtResidentSearch.Size = New Size(172, 25)
        txtResidentSearch.TabIndex = 4
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(469, 97)
        Label22.Name = "Label22"
        Label22.Size = New Size(76, 20)
        Label22.TabIndex = 17
        Label22.Text = "Date End:"
        ' 
        ' lblSelectedResident
        ' 
        lblSelectedResident.AutoSize = True
        lblSelectedResident.Location = New Point(23, 205)
        lblSelectedResident.Name = "lblSelectedResident"
        lblSelectedResident.Size = New Size(136, 20)
        lblSelectedResident.TabIndex = 5
        lblSelectedResident.Text = "Selected Resident:"
        ' 
        ' txtEventName
        ' 
        txtEventName.Font = New Font("Segoe UI Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtEventName.Location = New Point(25, 69)
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
        dtpEndTime.Location = New Point(472, 120)
        dtpEndTime.Name = "dtpEndTime"
        dtpEndTime.Size = New Size(200, 25)
        dtpEndTime.TabIndex = 8
        ' 
        ' dtpStartTime
        ' 
        dtpStartTime.CustomFormat = "MMMM dd, yyyy - hh:mm tt"
        dtpStartTime.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpStartTime.Format = DateTimePickerFormat.Custom
        dtpStartTime.Location = New Point(472, 69)
        dtpStartTime.Name = "dtpStartTime"
        dtpStartTime.Size = New Size(200, 25)
        dtpStartTime.TabIndex = 7
        ' 
        ' btnSave
        ' 
        btnSave.Font = New Font("Segoe UI Light", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnSave.ForeColor = Color.Black
        btnSave.Location = New Point(586, 296)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(86, 34)
        btnSave.TabIndex = 18
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Font = New Font("Segoe UI Light", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnCancel.ForeColor = Color.Black
        btnCancel.Location = New Point(483, 296)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(86, 34)
        btnCancel.TabIndex = 19
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' FormAddBooking
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(743, 426)
        Controls.Add(grpBooking)
        Name = "FormAddBooking"
        Text = "FormAddBooking"
        grpBooking.ResumeLayout(False)
        grpBooking.PerformLayout()
        CType(dgvResidentSearch, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents grpBooking As GroupBox
    Friend WithEvents Label18 As Label
    Friend WithEvents dgvResidentSearch As DataGridView
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
End Class
