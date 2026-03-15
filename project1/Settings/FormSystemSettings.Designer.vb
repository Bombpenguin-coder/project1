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
        txtCityName.Location = New Point(50, 119)
        txtCityName.Multiline = True
        txtCityName.Name = "txtCityName"
        txtCityName.Size = New Size(201, 34)
        txtCityName.TabIndex = 1
        ' 
        ' txtProvinceName
        ' 
        txtProvinceName.Location = New Point(50, 167)
        txtProvinceName.Multiline = True
        txtProvinceName.Name = "txtProvinceName"
        txtProvinceName.Size = New Size(201, 34)
        txtProvinceName.TabIndex = 2
        ' 
        ' txtCaptainName
        ' 
        txtCaptainName.Location = New Point(50, 217)
        txtCaptainName.Multiline = True
        txtCaptainName.Name = "txtCaptainName"
        txtCaptainName.Size = New Size(201, 34)
        txtCaptainName.TabIndex = 3
        ' 
        ' txtContactNumber
        ' 
        txtContactNumber.Location = New Point(50, 272)
        txtContactNumber.Multiline = True
        txtContactNumber.Name = "txtContactNumber"
        txtContactNumber.Size = New Size(201, 34)
        txtContactNumber.TabIndex = 4
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(418, 324)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(76, 39)
        btnSave.TabIndex = 5
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(532, 324)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(76, 39)
        btnCancel.TabIndex = 6
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' FormSystemSettings
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(676, 450)
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
End Class
