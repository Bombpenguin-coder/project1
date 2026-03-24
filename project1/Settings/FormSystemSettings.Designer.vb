<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSystemSettings
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
        txtBarangayName = New TextBox()
        txtCityName = New TextBox()
        txtProvinceName = New TextBox()
        txtCaptainName = New TextBox()
        txtContactNumber = New TextBox()
        btnSave = New Button()
        btnCancel = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        SuspendLayout()
        ' 
        ' txtBarangayName
        ' 
        txtBarangayName.Location = New Point(50, 62)
        txtBarangayName.Multiline = True
        txtBarangayName.Name = "txtBarangayName"
        txtBarangayName.Size = New Size(201, 34)
        txtBarangayName.TabIndex = 0
        ' 
        ' txtCityName
        ' 
        txtCityName.Location = New Point(50, 128)
        txtCityName.Multiline = True
        txtCityName.Name = "txtCityName"
        txtCityName.Size = New Size(201, 34)
        txtCityName.TabIndex = 1
        ' 
        ' txtProvinceName
        ' 
        txtProvinceName.Location = New Point(50, 195)
        txtProvinceName.Multiline = True
        txtProvinceName.Name = "txtProvinceName"
        txtProvinceName.Size = New Size(201, 34)
        txtProvinceName.TabIndex = 2
        ' 
        ' txtCaptainName
        ' 
        txtCaptainName.Location = New Point(50, 255)
        txtCaptainName.Multiline = True
        txtCaptainName.Name = "txtCaptainName"
        txtCaptainName.Size = New Size(201, 34)
        txtCaptainName.TabIndex = 3
        ' 
        ' txtContactNumber
        ' 
        txtContactNumber.Location = New Point(50, 324)
        txtContactNumber.Multiline = True
        txtContactNumber.Name = "txtContactNumber"
        txtContactNumber.Size = New Size(201, 34)
        txtContactNumber.TabIndex = 4
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Microsoft PhagsPa", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(36, 364)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(233, 39)
        btnSave.TabIndex = 5
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.IndianRed
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(36, 409)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(233, 39)
        btnCancel.TabIndex = 6
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(50, 33)
        Label1.Name = "Label1"
        Label1.Size = New Size(91, 15)
        Label1.TabIndex = 7
        Label1.Text = "Barangay Name"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(50, 110)
        Label2.Name = "Label2"
        Label2.Size = New Size(63, 15)
        Label2.TabIndex = 8
        Label2.Text = "City Name"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(50, 177)
        Label3.Name = "Label3"
        Label3.Size = New Size(88, 15)
        Label3.TabIndex = 9
        Label3.Text = "Province Name"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(50, 237)
        Label4.Name = "Label4"
        Label4.Size = New Size(83, 15)
        Label4.TabIndex = 10
        Label4.Text = "Captain Name"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(50, 306)
        Label5.Name = "Label5"
        Label5.Size = New Size(96, 15)
        Label5.TabIndex = 11
        Label5.Text = "Contact Number"
        ' 
        ' FormSystemSettings
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(338, 450)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(btnCancel)
        Controls.Add(btnSave)
        Controls.Add(txtContactNumber)
        Controls.Add(txtCaptainName)
        Controls.Add(txtProvinceName)
        Controls.Add(txtCityName)
        Controls.Add(txtBarangayName)
        Name = "FormSystemSettings"
        Text = "FormSystemSettings"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtBarangayName As TextBox
    Friend WithEvents txtCityName As TextBox
    Friend WithEvents txtProvinceName As TextBox
    Friend WithEvents txtCaptainName As TextBox
    Friend WithEvents txtContactNumber As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
