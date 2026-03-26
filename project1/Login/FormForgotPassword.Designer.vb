<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormForgotPassword
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
        txtRecoverUsername = New TextBox()
        txtSecurityAnswer = New TextBox()
        txtNewPassword = New TextBox()
        btnSearch = New Button()
        btnResetPassword = New Button()
        lblSecurityQuestion = New Label()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' txtRecoverUsername
        ' 
        txtRecoverUsername.Location = New Point(43, 61)
        txtRecoverUsername.Name = "txtRecoverUsername"
        txtRecoverUsername.Size = New Size(121, 23)
        txtRecoverUsername.TabIndex = 0
        ' 
        ' txtSecurityAnswer
        ' 
        txtSecurityAnswer.Location = New Point(43, 178)
        txtSecurityAnswer.Name = "txtSecurityAnswer"
        txtSecurityAnswer.Size = New Size(121, 23)
        txtSecurityAnswer.TabIndex = 1
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Location = New Point(43, 255)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.Size = New Size(121, 23)
        txtNewPassword.TabIndex = 2
        ' 
        ' btnSearch
        ' 
        btnSearch.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.ForeColor = Color.White
        btnSearch.Location = New Point(191, 61)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(83, 23)
        btnSearch.TabIndex = 3
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = False
        ' 
        ' btnResetPassword
        ' 
        btnResetPassword.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnResetPassword.FlatStyle = FlatStyle.Flat
        btnResetPassword.ForeColor = Color.White
        btnResetPassword.Location = New Point(123, 322)
        btnResetPassword.Name = "btnResetPassword"
        btnResetPassword.Size = New Size(122, 40)
        btnResetPassword.TabIndex = 4
        btnResetPassword.Text = "Reset Password"
        btnResetPassword.UseVisualStyleBackColor = False
        ' 
        ' lblSecurityQuestion
        ' 
        lblSecurityQuestion.AutoSize = True
        lblSecurityQuestion.Location = New Point(43, 107)
        lblSecurityQuestion.Name = "lblSecurityQuestion"
        lblSecurityQuestion.Size = New Size(16, 15)
        lblSecurityQuestion.TabIndex = 5
        lblSecurityQuestion.Text = "..."
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(43, 33)
        Label1.Name = "Label1"
        Label1.Size = New Size(66, 15)
        Label1.TabIndex = 6
        Label1.Text = "Username"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Black
        Label2.Location = New Point(43, 149)
        Label2.Name = "Label2"
        Label2.Size = New Size(101, 15)
        Label2.TabIndex = 7
        Label2.Text = "Security Answer"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label3.ForeColor = Color.Black
        Label3.Location = New Point(43, 228)
        Label3.Name = "Label3"
        Label3.Size = New Size(93, 15)
        Label3.TabIndex = 8
        Label3.Text = "New Password"
        ' 
        ' FormForgotPassword
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(213), CByte(222), CByte(239))
        ClientSize = New Size(354, 450)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(lblSecurityQuestion)
        Controls.Add(btnResetPassword)
        Controls.Add(btnSearch)
        Controls.Add(txtNewPassword)
        Controls.Add(txtSecurityAnswer)
        Controls.Add(txtRecoverUsername)
        Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "FormForgotPassword"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "FormForgotPassword"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtRecoverUsername As TextBox
    Friend WithEvents txtSecurityAnswer As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnResetPassword As Button
    Friend WithEvents lblSecurityQuestion As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
