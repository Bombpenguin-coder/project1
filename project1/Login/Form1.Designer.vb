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
        PasswordVisible = New Button()
        PictureBox2 = New PictureBox()
        PictureBox3 = New PictureBox()
        PictureBox1 = New PictureBox()
        Password = New Label()
        Username = New Label()
        TextBox1 = New TextBox()
        Loginbtn = New Button()
        TextBox2 = New TextBox()
        Cancelbtn = New Button()
        Label1 = New Label()
        Panel1 = New Panel()
        Label3 = New Label()
        PictureBox4 = New PictureBox()
        Label2 = New Label()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        CType(PictureBox4, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PasswordVisible
        ' 
        PasswordVisible.BackColor = Color.Transparent
        PasswordVisible.FlatAppearance.BorderSize = 0
        PasswordVisible.FlatStyle = FlatStyle.Flat
        PasswordVisible.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        PasswordVisible.ForeColor = Color.White
        PasswordVisible.Location = New Point(299, 275)
        PasswordVisible.Name = "PasswordVisible"
        PasswordVisible.Size = New Size(23, 23)
        PasswordVisible.TabIndex = 6
        PasswordVisible.Text = "👁"
        PasswordVisible.UseVisualStyleBackColor = False
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.InitialImage = Nothing
        PictureBox2.Location = New Point(52, 226)
        PictureBox2.Margin = New Padding(0)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(22, 23)
        PictureBox2.TabIndex = 11
        PictureBox2.TabStop = False
        ' 
        ' PictureBox3
        ' 
        PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), Image)
        PictureBox3.Location = New Point(51, 275)
        PictureBox3.Name = "PictureBox3"
        PictureBox3.Size = New Size(22, 23)
        PictureBox3.TabIndex = 12
        PictureBox3.TabStop = False
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(105, 3)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(165, 167)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 8
        PictureBox1.TabStop = False
        ' 
        ' Password
        ' 
        Password.AutoSize = True
        Password.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Password.ForeColor = Color.White
        Password.Location = New Point(84, 260)
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
        Username.Location = New Point(85, 209)
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
        TextBox1.Location = New Point(82, 227)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(213, 23)
        TextBox1.TabIndex = 3
        TextBox1.UseWaitCursor = True
        ' 
        ' Loginbtn
        ' 
        Loginbtn.BackColor = Color.FromArgb(CByte(28), CByte(53), CByte(45))
        Loginbtn.FlatAppearance.BorderSize = 0
        Loginbtn.FlatStyle = FlatStyle.Flat
        Loginbtn.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Loginbtn.ForeColor = Color.White
        Loginbtn.Location = New Point(90, 327)
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
        TextBox2.Location = New Point(82, 279)
        TextBox2.MaxLength = 99
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.PasswordChar = "*"c
        TextBox2.Size = New Size(213, 23)
        TextBox2.TabIndex = 4
        ' 
        ' Cancelbtn
        ' 
        Cancelbtn.BackColor = Color.White
        Cancelbtn.FlatAppearance.BorderSize = 0
        Cancelbtn.FlatStyle = FlatStyle.Flat
        Cancelbtn.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Cancelbtn.Location = New Point(90, 376)
        Cancelbtn.Name = "Cancelbtn"
        Cancelbtn.Size = New Size(195, 23)
        Cancelbtn.TabIndex = 7
        Cancelbtn.Text = "Cancel"
        Cancelbtn.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Gainsboro
        Label1.Font = New Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold)
        Label1.ForeColor = SystemColors.ControlLightLight
        Label1.Location = New Point(79, 23)
        Label1.MinimumSize = New Size(10, 10)
        Label1.Name = "Label1"
        Label1.Size = New Size(298, 31)
        Label1.TabIndex = 5
        Label1.Text = "Welcome to Barangay"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(62), CByte(95), CByte(68))
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Cancelbtn)
        Panel1.Controls.Add(TextBox2)
        Panel1.Controls.Add(Loginbtn)
        Panel1.Controls.Add(TextBox1)
        Panel1.Controls.Add(Username)
        Panel1.Controls.Add(Password)
        Panel1.Controls.Add(PictureBox3)
        Panel1.Controls.Add(PictureBox2)
        Panel1.Controls.Add(PasswordVisible)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Dock = DockStyle.Right
        Panel1.Location = New Point(442, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(387, 526)
        Panel1.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Microsoft Tai Le", 20F, FontStyle.Bold)
        Label3.ForeColor = SystemColors.Window
        Label3.Location = New Point(142, 155)
        Label3.Name = "Label3"
        Label3.Size = New Size(98, 34)
        Label3.TabIndex = 13
        Label3.Text = "LOGIN"
        ' 
        ' PictureBox4
        ' 
        PictureBox4.BackColor = Color.Transparent
        PictureBox4.BackgroundImageLayout = ImageLayout.None
        PictureBox4.Dock = DockStyle.Left
        PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), Image)
        PictureBox4.InitialImage = Nothing
        PictureBox4.Location = New Point(0, 0)
        PictureBox4.Name = "PictureBox4"
        PictureBox4.Size = New Size(445, 526)
        PictureBox4.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox4.TabIndex = 9
        PictureBox4.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Gainsboro
        Label2.Font = New Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold)
        Label2.ForeColor = SystemColors.ControlLightLight
        Label2.Location = New Point(90, 54)
        Label2.MinimumSize = New Size(10, 10)
        Label2.Name = "Label2"
        Label2.Size = New Size(273, 31)
        Label2.TabIndex = 14
        Label2.Text = " New Lower Bicutan"
        Label2.TextAlign = ContentAlignment.TopCenter
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(829, 526)
        Controls.Add(Label2)
        Controls.Add(Panel1)
        Controls.Add(Label1)
        Controls.Add(PictureBox4)
        FormBorderStyle = FormBorderStyle.None
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login Form"
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox3, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox4, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents PasswordVisible As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Password As Label
    Friend WithEvents Username As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Loginbtn As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Cancelbtn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Label2 As Label

End Class
