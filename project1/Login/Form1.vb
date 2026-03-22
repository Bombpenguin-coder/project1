Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualBasic.ApplicationServices
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=login_db")
    Private Passwordhider As Boolean = True
    Private userRepo As New UserRepository()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PasswordVisible.Text = "👁"
        TextBox1.TabIndex = 0
        TextBox2.TabIndex = 1
        Loginbtn.TabIndex = 2
        Loginbtn.TabStop = False
        Loginbtn.FlatStyle = FlatStyle.Flat
        Loginbtn.FlatAppearance.BorderSize = 0

        ' --- NEW: FIRST-RUN DATABASE CHECK ---
        CheckFirstRunSetup()
    End Sub

    Private Sub CheckFirstRunSetup()
        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM users", conn)
            Dim userCount = Convert.ToInt32(cmd.ExecuteScalar())

            If userCount = 0 Then
                ' No users exist! Show the Setup Wizard and hide the login controls
                pnlSetup.Visible = True
                pnlSetup.BringToFront()
            Else
                ' Users exist! Hide the Setup Wizard
                pnlSetup.Visible = False
                TextBox1.Select()
                TextBox1.Focus()
            End If
        Catch ex As Exception
            MsgBox("Error checking database: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' --- NEW: CREATE THE FIRST SUPERADMIN ---
    Private Sub btnCreateAdmin_Click(sender As Object, e As EventArgs) Handles btnCreateAdmin.Click
        If String.IsNullOrWhiteSpace(txtSetupFullname.Text) OrElse String.IsNullOrWhiteSpace(txtSetupUsername.Text) OrElse String.IsNullOrWhiteSpace(txtSetupPassword.Text) OrElse String.IsNullOrWhiteSpace(cmbSetupQuestion.Text) OrElse String.IsNullOrWhiteSpace(txtSetupAnswer.Text) Then
            MsgBox("Please fill in all setup fields.", MsgBoxStyle.Exclamation, "Missing Data")
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
                ' Convert the answer to lowercase so it's not case-sensitive later!
                cmd.Parameters.AddWithValue("@Answer", txtSetupAnswer.Text.Trim().ToLower())

                cmd.ExecuteNonQuery()
            End Using

            MsgBox("System Initialization Complete! You may now log in.", MsgBoxStyle.Information, "Success")

            ' Hide the setup panel so they can log in normally
            pnlSetup.Visible = False
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()

        Catch ex As Exception
            MsgBox("Error creating admin: " & ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub DoLogin()

        Dim username As String = TextBox1.Text.Trim()
        Dim password As String = TextBox2.Text

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MsgBox("Username and Password are required.", MsgBoxStyle.Exclamation)
            Return
        End If

        Try
            Dim hashedPassword As String = HashPassword(password)

            Dim user = userRepo.Login(username, hashedPassword)

            If user IsNot Nothing Then

                ' ✅ NOW CORRECT
                Dim historyId As Integer = userRepo.InsertLoginHistory(user.Username, user.Role)

                MsgBox("Login successful! Welcome " & user.Fullname & " (" & user.Role & ")")

                Dim dashboard As New FormMain(user.Role, user.Fullname, historyId, user.Username)
                dashboard.Show()
                Me.Hide()

            Else
                MsgBox("Invalid username or password.", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try

    End Sub
    Private Function HashPassword(ByVal password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            ' Compute the hash from the password bytes
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))

            ' Convert the byte array to a hexadecimal string
            Dim builder As New StringBuilder()


            For i As Integer = 0 To bytes.Length - 1
                builder.Append(bytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Protected Overrides Sub OnActivated(e As EventArgs)
        MyBase.OnActivated(e)
        TextBox1.Focus()
    End Sub

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

    Private Sub PasswordVisible_Click(sender As Object, e As EventArgs) Handles PasswordVisible.Click
        ' 1. Toggle the password visibility
        TextBox2.UseSystemPasswordChar = Not TextBox2.UseSystemPasswordChar

        ' 2. Update the button icon to match
        If TextBox2.UseSystemPasswordChar = True Then
            ' It's NOW HIDDEN, so the button should offer to "Show"
            PasswordVisible.Text = "👁" ' "Show" icon
        Else
            ' It's NOW SHOWING, so the button should offer to "Hide"
            PasswordVisible.Text = "🙈" ' "Hide" icon
        End If
    End Sub

    Private Sub Cancelbtn_Click(sender As Object, e As EventArgs) Handles Cancelbtn.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
    End Sub
    Public Sub PrepareForLogin()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub lblForgotPassword_Click(sender As Object, e As EventArgs) Handles lblForgotPassword.Click
        Using recoveryForm As New FormForgotPassword()
            recoveryForm.ShowDialog()
        End Using
    End Sub

End Class