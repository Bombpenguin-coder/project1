Imports System.Security.Cryptography
Imports System.Text
Imports MySql.Data.MySqlClient

Public Class Form1
    Private _service As UserService
    Private _repo As UserRepository
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=login_db")
    Public Sub New(service As UserService)
        InitializeComponent()
        _service = service
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.TabIndex = 0
        TextBox2.TabIndex = 1
        Loginbtn.TabIndex = 2

        chkShowPassword.Checked = False
        TextBox2.UseSystemPasswordChar = False
        Loginbtn.TabStop = False
        Loginbtn.FlatStyle = FlatStyle.Flat
        Loginbtn.FlatAppearance.BorderSize = 0

        pnlSetup.Location = pnlLogin.Location

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
        Try
            _service.CreateAdmin(
            txtSetupFullname.Text.Trim(),
            txtSetupUsername.Text.Trim(),
            txtSetupPassword.Text,
            cmbSetupQuestion.Text,
            txtSetupAnswer.Text.Trim()
        )

            MsgBox("System Initialization Complete!")

            pnlSetup.Visible = False
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DoLogin()
        Try
            Dim username As String = TextBox1.Text.Trim()
            Dim password As String = TextBox2.Text

            Dim user = _service.Login(username, password)

            If user IsNot Nothing Then
                Dim historyId As Integer = _service.InsertLoginHistory(user.Username, user.Role)

                MsgBox("Welcome " & user.Fullname)

                Dim dashboard As New FormMain(user.Role, user.Fullname, historyId, user.Username)
                dashboard.Show()
                Me.Hide()
            Else
                MsgBox("Invalid username or password.")
                TextBox2.Clear()
                TextBox2.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles pnlLogin.Paint

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub
End Class