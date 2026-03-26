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
        txtLastName = New TextBox()
        txtFirstName = New TextBox()
        txtMiddleName = New TextBox()
        txtDistrict = New TextBox()
        cmbSex = New ComboBox()
        btnSaveResident = New Button()
        btnCancel = New Button()
        Label5 = New Label()
        dtpBirthDate = New DateTimePicker()
        lblCalculatedAge = New Label()
        cmbStreet = New ComboBox()
        txtAddress = New TextBox()
        Label9 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
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
        Label6.Location = New Point(198, 401)
        Label6.Name = "Label6"
        Label6.Size = New Size(29, 17)
        Label6.TabIndex = 5
        Label6.Text = "Sex"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(43, 344)
        Label7.Name = "Label7"
        Label7.Size = New Size(44, 17)
        Label7.TabIndex = 6
        Label7.Text = "Street"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.Location = New Point(47, 401)
        Label8.Name = "Label8"
        Label8.Size = New Size(53, 17)
        Label8.TabIndex = 7
        Label8.Text = "District"
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
        ' txtDistrict
        ' 
        txtDistrict.Location = New Point(62, 424)
        txtDistrict.Name = "txtDistrict"
        txtDistrict.Size = New Size(105, 23)
        txtDistrict.TabIndex = 15
        ' 
        ' cmbSex
        ' 
        cmbSex.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSex.FormattingEnabled = True
        cmbSex.Items.AddRange(New Object() {"Male", "Female", "Other"})
        cmbSex.Location = New Point(204, 424)
        cmbSex.Name = "cmbSex"
        cmbSex.Size = New Size(121, 23)
        cmbSex.TabIndex = 18
        ' 
        ' btnSaveResident
        ' 
        btnSaveResident.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnSaveResident.FlatAppearance.BorderSize = 0
        btnSaveResident.FlatStyle = FlatStyle.Flat
        btnSaveResident.Font = New Font("Microsoft PhagsPa", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSaveResident.ForeColor = Color.White
        btnSaveResident.Location = New Point(78, 478)
        btnSaveResident.Name = "btnSaveResident"
        btnSaveResident.Size = New Size(227, 30)
        btnSaveResident.TabIndex = 19
        btnSaveResident.Text = "SAVE"
        btnSaveResident.UseVisualStyleBackColor = False
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.IndianRed
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(78, 515)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(227, 23)
        btnCancel.TabIndex = 20
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
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
        ' cmbStreet
        ' 
        cmbStreet.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStreet.FormattingEnabled = True
        cmbStreet.Items.AddRange(New Object() {"Male", "Female", "Other"})
        cmbStreet.Location = New Point(62, 364)
        cmbStreet.Name = "cmbStreet"
        cmbStreet.Size = New Size(263, 23)
        cmbStreet.TabIndex = 24
        ' 
        ' txtAddress
        ' 
        txtAddress.Location = New Point(62, 318)
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(263, 23)
        txtAddress.TabIndex = 26
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(43, 295)
        Label9.Name = "Label9"
        Label9.Size = New Size(57, 17)
        Label9.TabIndex = 25
        Label9.Text = "Address"
        ' 
        ' FormAddResidents
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(213), CByte(222), CByte(239))
        BackgroundImageLayout = ImageLayout.None
        ClientSize = New Size(404, 561)
        ControlBox = False
        Controls.Add(txtAddress)
        Controls.Add(Label9)
        Controls.Add(cmbStreet)
        Controls.Add(lblCalculatedAge)
        Controls.Add(dtpBirthDate)
        Controls.Add(btnCancel)
        Controls.Add(btnSaveResident)
        Controls.Add(cmbSex)
        Controls.Add(txtDistrict)
        Controls.Add(txtMiddleName)
        Controls.Add(txtFirstName)
        Controls.Add(txtLastName)
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
    Friend WithEvents txtDistrict As TextBox
    Friend WithEvents txtCity As TextBox
    Friend WithEvents cmbSex As ComboBox
    Friend WithEvents btnSaveResident As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents cmbBarangay As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpBirthDate As DateTimePicker
    Friend WithEvents lblCalculatedAge As Label
    Friend WithEvents cmbStreet As ComboBox
    Friend WithEvents txtAddress As TextBox
End Class
