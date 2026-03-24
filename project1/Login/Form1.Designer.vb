<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        PictureBox2 = New PictureBox()
        PictureBox3 = New PictureBox()
        Password = New Label()
        Username = New Label()
        TextBox1 = New TextBox()
        Loginbtn = New Button()
        TextBox2 = New TextBox()
        Label1 = New Label()
        pnlLogin = New Panel()
        chkShowPassword = New CheckBox()
        lblForgotPassword = New Label()
        pnlSetup = New Panel()
        btnCreateAdmin = New Button()
        cmbSetupQuestion = New ComboBox()
        txtSetupAnswer = New TextBox()
        txtSetupPassword = New TextBox()
        txtSetupUsername = New TextBox()
        txtSetupFullname = New TextBox()
        Label3 = New Label()
        Label2 = New Label()
        Panel2 = New Panel()
        PictureBox1 = New PictureBox()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        pnlLogin.SuspendLayout()
        pnlSetup.SuspendLayout()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.InitialImage = Nothing
        PictureBox2.Location = New Point(47, 169)
        PictureBox2.Margin = New Padding(0)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(22, 23)
        PictureBox2.TabIndex = 11
        PictureBox2.TabStop = False
        ' 
        ' PictureBox3
        ' 
        PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), Image)
        PictureBox3.Location = New Point(46, 232)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(22, 23)
        PictureBox3.TabIndex = 12
        PictureBox3.TabStop = False
        ' 
        ' Password
        ' 
        Password.AutoSize = True
        Password.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Password.ForeColor = Color.White
        Password.Location = New Point(79, 217)
        Password.Name = "Password"
        Password.Size = New Size(59, 15)
        Password.TabIndex = 2
        Password.Text = "Password"
        ' 
        ' Username
        ' 
        Username.AutoSize = True
        Username.BackColor = Color.Transparent
        Username.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Username.ForeColor = Color.White
        Username.Location = New Point(80, 152)
        Username.Name = "Username"
        Username.Size = New Size(64, 15)
        Username.TabIndex = 1
        Username.Text = "Username"
        ' 
        ' TextBox1
        ' 
        TextBox1.BackColor = Color.White
        TextBox1.BorderStyle = BorderStyle.None
        TextBox1.Cursor = Cursors.IBeam
        TextBox1.Location = New Point(77, 170)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(213, 23)
        TextBox1.TabIndex = 3
        TextBox1.UseWaitCursor = True
        ' 
        ' Loginbtn
        ' 
        Loginbtn.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        Loginbtn.FlatAppearance.BorderSize = 0
        Loginbtn.FlatStyle = FlatStyle.Flat
        Loginbtn.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Loginbtn.ForeColor = Color.White
        Loginbtn.Location = New Point(77, 319)
        Loginbtn.Name = "Loginbtn"
        Loginbtn.Size = New Size(195, 35)
        Loginbtn.TabIndex = 0
        Loginbtn.Text = "Login"
        Loginbtn.UseVisualStyleBackColor = False
        ' 
        ' TextBox2
        ' 
        TextBox2.BorderStyle = BorderStyle.None
        TextBox2.Cursor = Cursors.IBeam
        TextBox2.Location = New Point(77, 236)
        TextBox2.MaxLength = 99
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.PasswordChar = "*"c
        TextBox2.Size = New Size(213, 23)
        TextBox2.TabIndex = 4
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.Transparent
        Label1.Location = New Point(87, 98)
        Label1.MinimumSize = New Size(10, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(302, 32)
        Label1.TabIndex = 5
        Label1.Text = "Welcome to Barangay"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' pnlLogin
        ' 
        pnlLogin.BackColor = Color.FromArgb(CByte(98), CByte(155), CByte(181))
        pnlLogin.Controls.Add(chkShowPassword)
        pnlLogin.Controls.Add(lblForgotPassword)
        pnlLogin.Controls.Add(TextBox2)
        pnlLogin.Controls.Add(pnlSetup)
        pnlLogin.Controls.Add(Loginbtn)
        pnlLogin.Controls.Add(TextBox1)
        pnlLogin.Controls.Add(Username)
        pnlLogin.Controls.Add(Password)
        pnlLogin.Controls.Add(PictureBox3)
        pnlLogin.Controls.Add(PictureBox2)
        pnlLogin.Controls.Add(Label3)
        pnlLogin.Dock = DockStyle.Right
        pnlLogin.Location = New Point(444, 0)
        pnlLogin.Name = "pnlLogin"
        pnlLogin.Size = New Size(385, 526)
        pnlLogin.TabIndex = 5
        ' 
        ' chkShowPassword
        ' 
        chkShowPassword.AutoSize = True
        chkShowPassword.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        chkShowPassword.ForeColor = SystemColors.ButtonHighlight
        chkShowPassword.Location = New Point(77, 277)
        chkShowPassword.Name = "chkShowPassword"
        chkShowPassword.Size = New Size(108, 19)
        chkShowPassword.TabIndex = 15
        chkShowPassword.Text = "Show Password"
        chkShowPassword.UseVisualStyleBackColor = True
        ' 
        ' lblForgotPassword
        ' 
        lblForgotPassword.AutoSize = True
        lblForgotPassword.ForeColor = SystemColors.ButtonHighlight
        lblForgotPassword.Location = New Point(77, 374)
        lblForgotPassword.Name = "lblForgotPassword"
        lblForgotPassword.Size = New Size(100, 15)
        lblForgotPassword.TabIndex = 14
        lblForgotPassword.Text = "Forgot Password?"
        ' 
        ' pnlSetup
        ' 
        pnlSetup.Controls.Add(btnCreateAdmin)
        pnlSetup.Controls.Add(cmbSetupQuestion)
        pnlSetup.Controls.Add(txtSetupAnswer)
        pnlSetup.Controls.Add(txtSetupPassword)
        pnlSetup.Controls.Add(txtSetupUsername)
        pnlSetup.Controls.Add(txtSetupFullname)
        pnlSetup.Location = New Point(366, 0)
        pnlSetup.Name = "pnlSetup"
        pnlSetup.Size = New Size(372, 526)
        pnlSetup.TabIndex = 14
        ' 
        ' btnCreateAdmin
        ' 
        btnCreateAdmin.Location = New Point(111, 405)
        btnCreateAdmin.Name = "btnCreateAdmin"
        btnCreateAdmin.Size = New Size(168, 44)
        btnCreateAdmin.TabIndex = 5
        btnCreateAdmin.Text = "Create Super-Admin"
        btnCreateAdmin.UseVisualStyleBackColor = True
        ' 
        ' cmbSetupQuestion
        ' 
        cmbSetupQuestion.FormattingEnabled = True
        cmbSetupQuestion.Location = New Point(48, 260)
        cmbSetupQuestion.Name = "cmbSetupQuestion"
        cmbSetupQuestion.Size = New Size(158, 23)
        cmbSetupQuestion.TabIndex = 4
        ' 
        ' txtSetupAnswer
        ' 
        txtSetupAnswer.Location = New Point(48, 317)
        txtSetupAnswer.Name = "txtSetupAnswer"
        txtSetupAnswer.Size = New Size(137, 23)
        txtSetupAnswer.TabIndex = 3
        ' 
        ' txtSetupPassword
        ' 
        txtSetupPassword.Location = New Point(48, 209)
        txtSetupPassword.Name = "txtSetupPassword"
        txtSetupPassword.Size = New Size(137, 23)
        txtSetupPassword.TabIndex = 2
        ' 
        ' txtSetupUsername
        ' 
        txtSetupUsername.Location = New Point(48, 155)
        txtSetupUsername.Name = "txtSetupUsername"
        txtSetupUsername.Size = New Size(137, 23)
        txtSetupUsername.TabIndex = 1
        ' 
        ' txtSetupFullname
        ' 
        txtSetupFullname.Location = New Point(48, 98)
        txtSetupFullname.Name = "txtSetupFullname"
        txtSetupFullname.Size = New Size(137, 23)
        txtSetupFullname.TabIndex = 0
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Microsoft Tai Le", 20F, FontStyle.Bold)
        Label3.ForeColor = SystemColors.Window
        Label3.Location = New Point(123, 99)
        Label3.Name = "Label3"
        Label3.Size = New Size(98, 34)
        Label3.TabIndex = 13
        Label3.Text = "LOGIN"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Transparent
        Label2.Location = New Point(98, 144)
        Label2.MinimumSize = New Size(10, 10)
        Label2.Name = "Label2"
        Label2.Size = New Size(276, 32)
        Label2.TabIndex = 14
        Label2.Text = " New Lower Bicutan"
        Label2.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Transparent
        Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), Image)
        Panel2.BackgroundImageLayout = ImageLayout.Stretch
        Panel2.Controls.Add(PictureBox1)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(Label1)
        Panel2.Location = New Point(-4, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(454, 526)
        Panel2.TabIndex = 15
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(139, 209)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(178, 179)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 16
        PictureBox1.TabStop = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        ClientSize = New Size(829, 526)
        Controls.Add(Panel2)
        Controls.Add(pnlLogin)
        FormBorderStyle = FormBorderStyle.None
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login Form"
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        pnlLogin.ResumeLayout(False)
        pnlLogin.PerformLayout()
        pnlSetup.ResumeLayout(False)
        pnlSetup.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Password As Label
    Friend WithEvents Username As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Loginbtn As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents pnlLogin As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents pnlSetup As Panel
    Friend WithEvents txtSetupAnswer As TextBox
    Friend WithEvents txtSetupPassword As TextBox
    Friend WithEvents txtSetupUsername As TextBox
    Friend WithEvents txtSetupFullname As TextBox
    Friend WithEvents btnCreateAdmin As Button
    Friend WithEvents cmbSetupQuestion As ComboBox
    Friend WithEvents lblForgotPassword As Label
    Friend WithEvents chkShowPassword As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox

End Class
