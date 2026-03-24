Imports System.Security.Cryptography
Imports System.Text
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=login_db")
    Private userRepo As New UserRepository()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.TabIndex = 0
        TextBox2.TabIndex = 1
        Loginbtn.TabIndex = 2

        ' ✅ CORRECT INITIAL STATE
        chkShowPassword.Checked = False
        TextBox2.UseSystemPasswordChar = False
        Loginbtn.TabStop = False
        Loginbtn.FlatStyle = FlatStyle.Flat
        Loginbtn.FlatAppearance.BorderSize = 0

        CheckFirstRunSetup()
    End Sub

    Private Sub CheckFirstRunSetup()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM users", conn)
            Dim userCount = Convert.ToInt32(cmd.ExecuteScalar())

            If userCount = 0 Then
                pnlSetup.Visible = True
                pnlSetup.BringToFront()
            Else
                pnlSetup.Visible = False
                TextBox1.Focus()
            End If
        Catch ex As Exception
            MsgBox("Error checking database: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btnCreateAdmin_Click(sender As Object, e As EventArgs) Handles btnCreateAdmin.Click
        If String.IsNullOrWhiteSpace(txtSetupFullname.Text) OrElse
           String.IsNullOrWhiteSpace(txtSetupUsername.Text) OrElse
           String.IsNullOrWhiteSpace(txtSetupPassword.Text) OrElse
           String.IsNullOrWhiteSpace(cmbSetupQuestion.Text) OrElse
           String.IsNullOrWhiteSpace(txtSetupAnswer.Text) Then

            MsgBox("Please fill in all setup fields.", MsgBoxStyle.Exclamation)
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()

            Dim hashedPassword As String = HashPassword(txtSetupPassword.Text)

            Dim query As String = "INSERT INTO users (fullname, username, password, role, security_question, security_answer) VALUES (@Fullname, @Username, @Password, 'Superadmin', @Question, @Answer)"

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Fullname", txtSetupFullname.Text.Trim())
                cmd.Parameters.AddWithValue("@Username", txtSetupUsername.Text.Trim())
                cmd.Parameters.AddWithValue("@Password", hashedPassword)
                cmd.Parameters.AddWithValue("@Question", cmbSetupQuestion.Text)
                cmd.Parameters.AddWithValue("@Answer", txtSetupAnswer.Text.Trim().ToLower())
                cmd.ExecuteNonQuery()
            End Using

            MsgBox("System Initialization Complete!")

            pnlSetup.Visible = False
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()

        Catch ex As Exception
            MsgBox("Error creating admin: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub DoLogin()
        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MsgBox("Username and Password are required.")
            Return
        End If

        Try
            Dim hashedPassword As String = HashPassword(password)
            Dim user = userRepo.Login(username, hashedPassword)

            If user IsNot Nothing Then
                Dim historyId As Integer = userRepo.InsertLoginHistory(user.Username, user.Role)

                MsgBox("Welcome " & user.Fullname)

                Dim dashboard As New FormMain(user.Role, user.Fullname, historyId, user.Username)
                dashboard.Show()
                Me.Hide()
            Else
                MsgBox("Invalid username or password.")

                ' ✅ CLEAR PASSWORD
                TextBox2.Clear()
                TextBox2.Focus()
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)

            TextBox2.Clear()
            TextBox2.Focus()
        End Try
    End Sub

    Private Function HashPassword(ByVal password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()

            For i As Integer = 0 To bytes.Length - 1
                builder.Append(bytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            DoLogin()
        End If
    End Sub

    Private Sub Loginbtn_Click(sender As Object, e As EventArgs) Handles Loginbtn.Click
        DoLogin()
    End Sub

    Public Sub PrepareForLogin()
        TextBox1.Clear()
        TextBox2.Clear()

        ' ✅ RESET PASSWORD STATE
        chkShowPassword.Checked = False
        TextBox2.UseSystemPasswordChar = False

        TextBox1.Focus()
    End Sub
    Private Sub lblForgotPassword_Click(sender As Object, e As EventArgs) Handles lblForgotPassword.Click
        Using recoveryForm As New FormForgotPassword()
            recoveryForm.ShowDialog()
        End Using
    End Sub

    ' ✅ FINAL CORRECT LOGIC
    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        If chkShowPassword.Checked Then
            TextBox2.UseSystemPasswordChar = True  ' SHOW
        Else
            TextBox2.UseSystemPasswordChar = False ' HIDE
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub
End Class