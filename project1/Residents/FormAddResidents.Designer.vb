<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddResidents
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
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        Label10 = New Label()
        txtLastName = New TextBox()
        txtFirstName = New TextBox()
        txtMiddleName = New TextBox()
        txtAddress = New TextBox()
        txtDistrict = New TextBox()
        txtCity = New TextBox()
        cmbGender = New ComboBox()
        btnSaveResident = New Button()
        btnCancel = New Button()
        cmbBarangay = New ComboBox()
        Label5 = New Label()
        dtpBirthDate = New DateTimePicker()
        lblCalculatedAge = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(76), CByte(118), CByte(59))
        Label1.Location = New Point(100, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(186, 27)
        Label1.TabIndex = 0
        Label1.Text = "ADD RESIDENTS"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(43, 62)
        Label2.Name = "Label2"
        Label2.Size = New Size(73, 17)
        Label2.TabIndex = 1
        Label2.Text = "Last Name"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.Location = New Point(43, 118)
        Label3.Name = "Label3"
        Label3.Size = New Size(75, 17)
        Label3.TabIndex = 2
        Label3.Text = "First Name"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(43, 170)
        Label4.Name = "Label4"
        Label4.Size = New Size(91, 17)
        Label4.TabIndex = 3
        Label4.Text = "Middle Name"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label6.Location = New Point(198, 400)
        Label6.Name = "Label6"
        Label6.Size = New Size(52, 17)
        Label6.TabIndex = 5
        Label6.Text = "Gender"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(43, 283)
        Label7.Name = "Label7"
        Label7.Size = New Size(57, 17)
        Label7.TabIndex = 6
        Label7.Text = "Address"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(47, 340)
        Label8.Name = "Label8"
        Label8.Size = New Size(53, 17)
        Label8.TabIndex = 7
        Label8.Text = "District"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(198, 340)
        Label9.Name = "Label9"
        Label9.Size = New Size(65, 17)
        Label9.TabIndex = 8
        Label9.Text = "Barangay"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label10.Location = New Point(47, 400)
        Label10.Name = "Label10"
        Label10.Size = New Size(115, 17)
        Label10.TabIndex = 9
        Label10.Text = "City/Municipality"
        ' 
        ' txtLastName
        ' 
        txtLastName.Location = New Point(62, 89)
        txtLastName.Name = "txtLastName"
        txtLastName.Size = New Size(263, 23)
        txtLastName.TabIndex = 10
        ' 
        ' txtFirstName
        ' 
        txtFirstName.Location = New Point(62, 141)
        txtFirstName.Name = "txtFirstName"
        txtFirstName.Size = New Size(263, 23)
        txtFirstName.TabIndex = 11
        ' 
        ' txtMiddleName
        ' 
        txtMiddleName.Location = New Point(62, 193)
        txtMiddleName.Name = "txtMiddleName"
        txtMiddleName.Size = New Size(263, 23)
        txtMiddleName.TabIndex = 12
        ' 
        ' txtAddress
        ' 
        txtAddress.Location = New Point(62, 303)
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(263, 23)
        txtAddress.TabIndex = 14
        ' 
        ' txtDistrict
        ' 
        txtDistrict.Location = New Point(62, 363)
        txtDistrict.Name = "txtDistrict"
        txtDistrict.Size = New Size(105, 23)
        txtDistrict.TabIndex = 15
        ' 
        ' txtCity
        ' 
        txtCity.Location = New Point(62, 423)
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(105, 23)
        txtCity.TabIndex = 17
        ' 
        ' cmbGender
        ' 
        cmbGender.FormattingEnabled = True
        cmbGender.Items.AddRange(New Object() {"Male", "Female", "Other"})
        cmbGender.Location = New Point(204, 423)
        cmbGender.Name = "cmbGender"
        cmbGender.Size = New Size(121, 23)
        cmbGender.TabIndex = 18
        ' 
        ' btnSaveResident
        ' 
        btnSaveResident.BackColor = Color.FromArgb(CByte(115), CByte(148), CByte(107))
        btnSaveResident.FlatAppearance.BorderSize = 0
        btnSaveResident.FlatStyle = FlatStyle.Flat
        btnSaveResident.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSaveResident.ForeColor = Color.White
        btnSaveResident.Location = New Point(75, 462)
        btnSaveResident.Name = "btnSaveResident"
        btnSaveResident.Size = New Size(227, 30)
        btnSaveResident.TabIndex = 19
        btnSaveResident.Text = "SAVE"
        btnSaveResident.UseVisualStyleBackColor = False
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.Firebrick
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(75, 499)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(227, 23)
        btnCancel.TabIndex = 20
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' cmbBarangay
        ' 
        cmbBarangay.BackColor = Color.White
        cmbBarangay.FormattingEnabled = True
        cmbBarangay.Location = New Point(204, 363)
        cmbBarangay.Name = "cmbBarangay"
        cmbBarangay.Size = New Size(121, 23)
        cmbBarangay.TabIndex = 21
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label5.Location = New Point(43, 231)
        Label5.Name = "Label5"
        Label5.Size = New Size(71, 17)
        Label5.TabIndex = 4
        Label5.Text = "Birth Date"
        ' 
        ' dtpBirthDate
        ' 
        dtpBirthDate.CustomFormat = ""
        dtpBirthDate.Format = DateTimePickerFormat.Short
        dtpBirthDate.Location = New Point(62, 254)
        dtpBirthDate.MaxDate = New Date(2025, 10, 24, 0, 0, 0, 0)
        dtpBirthDate.Name = "dtpBirthDate"
        dtpBirthDate.Size = New Size(121, 23)
        dtpBirthDate.TabIndex = 22
        dtpBirthDate.Value = New Date(2025, 10, 24, 0, 0, 0, 0)
        ' 
        ' lblCalculatedAge
        ' 
        lblCalculatedAge.AutoSize = True
        lblCalculatedAge.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblCalculatedAge.Location = New Point(204, 254)
        lblCalculatedAge.Name = "lblCalculatedAge"
        lblCalculatedAge.Size = New Size(47, 17)
        lblCalculatedAge.TabIndex = 23
        lblCalculatedAge.Text = "Age: 0"
        ' 
        ' FormAddResidents
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Menu
        BackgroundImageLayout = ImageLayout.None
        ClientSize = New Size(404, 561)
        ControlBox = False
        Controls.Add(lblCalculatedAge)
        Controls.Add(dtpBirthDate)
        Controls.Add(cmbBarangay)
        Controls.Add(btnCancel)
        Controls.Add(btnSaveResident)
        Controls.Add(cmbGender)
        Controls.Add(txtCity)
        Controls.Add(txtDistrict)
        Controls.Add(txtAddress)
        Controls.Add(txtMiddleName)
        Controls.Add(txtFirstName)
        Controls.Add(txtLastName)
        Controls.Add(Label10)
        Controls.Add(Label9)
        Controls.Add(Label8)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.None
        Name = "FormAddResidents"
        StartPosition = FormStartPosition.CenterScreen
        Text = "FormAddResidents"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtMiddleName As TextBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtDistrict As TextBox
    Friend WithEvents txtCity As TextBox
    Friend WithEvents cmbGender As ComboBox
    Friend WithEvents btnSaveResident As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents cmbBarangay As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpBirthDate As DateTimePicker
    Friend WithEvents lblCalculatedAge As Label
End Class
