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
        SuspendLayout()
        ' 
        ' txtRecoverUsername
        ' 
        txtRecoverUsername.Location = New Point(43, 83)
        txtRecoverUsername.Name = "txtRecoverUsername"
        txtRecoverUsername.Size = New Size(121, 23)
        txtRecoverUsername.TabIndex = 0
        txtRecoverUsername.Text = "Username"
        ' 
        ' txtSecurityAnswer
        ' 
        txtSecurityAnswer.Location = New Point(43, 240)
        txtSecurityAnswer.Name = "txtSecurityAnswer"
        txtSecurityAnswer.Size = New Size(121, 23)
        txtSecurityAnswer.TabIndex = 1
        txtSecurityAnswer.Text = "Security Answer"
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Location = New Point(43, 285)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.Size = New Size(121, 23)
        txtNewPassword.TabIndex = 2
        txtNewPassword.Text = "New Password"
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(43, 140)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(99, 29)
        btnSearch.TabIndex = 3
        btnSearch.Text = "Search"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' btnResetPassword
        ' 
        btnResetPassword.Location = New Point(95, 347)
        btnResetPassword.Name = "btnResetPassword"
        btnResetPassword.Size = New Size(155, 63)
        btnResetPassword.TabIndex = 4
        btnResetPassword.Text = "Reset Password"
        btnResetPassword.UseVisualStyleBackColor = True
        ' 
        ' lblSecurityQuestion
        ' 
        lblSecurityQuestion.AutoSize = True
        lblSecurityQuestion.Location = New Point(43, 198)
        lblSecurityQuestion.Name = "lblSecurityQuestion"
        lblSecurityQuestion.Size = New Size(16, 15)
        lblSecurityQuestion.TabIndex = 5
        lblSecurityQuestion.Text = "..."
        ' 
        ' FormForgotPassword
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(354, 450)
        Controls.Add(lblSecurityQuestion)
        Controls.Add(btnResetPassword)
        Controls.Add(btnSearch)
        Controls.Add(txtNewPassword)
        Controls.Add(txtSecurityAnswer)
        Controls.Add(txtRecoverUsername)
        Name = "FormForgotPassword"
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
End Class
