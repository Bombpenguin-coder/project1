<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddOfficial
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
        txtFullName = New TextBox()
        txtPosition = New TextBox()
        txtContactNumber = New TextBox()
        btnCancel = New Button()
        btnSave = New Button()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        SuspendLayout()
        ' 
        ' txtFullName
        ' 
        txtFullName.Location = New Point(68, 71)
        txtFullName.Multiline = True
        txtFullName.Name = "txtFullName"
        txtFullName.Size = New Size(185, 29)
        txtFullName.TabIndex = 0
        ' 
        ' txtPosition
        ' 
        txtPosition.Location = New Point(68, 133)
        txtPosition.Multiline = True
        txtPosition.Name = "txtPosition"
        txtPosition.Size = New Size(185, 29)
        txtPosition.TabIndex = 1
        ' 
        ' txtContactNumber
        ' 
        txtContactNumber.Location = New Point(68, 197)
        txtContactNumber.Multiline = True
        txtContactNumber.Name = "txtContactNumber"
        txtContactNumber.Size = New Size(185, 29)
        txtContactNumber.TabIndex = 2
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.IndianRed
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(75, 305)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(173, 29)
        btnCancel.TabIndex = 3
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Microsoft PhagsPa", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(75, 261)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(173, 29)
        btnSave.TabIndex = 4
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(68, 53)
        Label1.Name = "Label1"
        Label1.Size = New Size(61, 15)
        Label1.TabIndex = 5
        Label1.Text = "Full Name"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(68, 115)
        Label2.Name = "Label2"
        Label2.Size = New Size(50, 15)
        Label2.TabIndex = 6
        Label2.Text = "Position"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(68, 179)
        Label3.Name = "Label3"
        Label3.Size = New Size(96, 15)
        Label3.TabIndex = 7
        Label3.Text = "Contact Number"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label4.Location = New Point(116, 22)
        Label4.Name = "Label4"
        Label4.Size = New Size(89, 18)
        Label4.TabIndex = 8
        Label4.Text = "Add Official"
        ' 
        ' FormAddOfficial
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(213), CByte(222), CByte(239))
        ClientSize = New Size(338, 381)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(btnSave)
        Controls.Add(btnCancel)
        Controls.Add(txtContactNumber)
        Controls.Add(txtPosition)
        Controls.Add(txtFullName)
        FormBorderStyle = FormBorderStyle.None
        Name = "FormAddOfficial"
        Text = "FormAddOfficial"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtFullName As TextBox
    Friend WithEvents txtPosition As TextBox
    Friend WithEvents txtContactNumber As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
