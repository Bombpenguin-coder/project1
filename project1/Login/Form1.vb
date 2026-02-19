Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Form1
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=login_db")
    Private Passwordhider As Boolean = True

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Start with the password HIDDEN

        ' 2. Set the button's starting icon to "Show"
        PasswordVisible.Text = "👁" ' Make sure this is your "Show" icon

        ' 3. Set focus to the first textbox
        TextBox1.Select()
        TextBox1.Focus()

        ' 4. Set TabIndex for proper navigation
        TextBox1.TabIndex = 0
        TextBox2.TabIndex = 1
        Loginbtn.TabIndex = 2

        ' 5. Style your button (optional)
        Loginbtn.TabStop = False
        Loginbtn.FlatStyle = FlatStyle.Flat
        Loginbtn.FlatAppearance.BorderSize = 0

        ' Note: This line isn't needed if you set focus to TextBox1
        ' Me.ActiveControl = Nothing 

        ' Note: This is good for the KeyDown handlers
        ' Me.AcceptButton = Nothing 
    End Sub

    Private Sub DoLogin()
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM users WHERE username=@username AND password=@password", conn)
            cmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim())

            Dim inputPasswordHashed As String = HashPassword(TextBox2.Text)
            cmd.Parameters.AddWithValue("@password", inputPasswordHashed)


            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                Dim username As String = reader("username").ToString()
                Dim role As String = reader("role").ToString()
                Dim fullname As String = reader("fullname").ToString()
                reader.Close()

                ' 1. Insert the history record
                Dim historyCmd As New MySqlCommand("INSERT INTO login_history (username, role, login_time) VALUES (@username, @role, NOW())", conn)
                historyCmd.Parameters.AddWithValue("@username", username)
                historyCmd.Parameters.AddWithValue("@role", role)
                historyCmd.ExecuteNonQuery()

                ' 2. Get the ID of the record we just inserted
                Dim historyId As Integer = 0
                historyCmd.CommandText = "SELECT LAST_INSERT_ID()" ' MySQL function to get last ID
                Dim result = historyCmd.ExecuteScalar()
                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                    historyId = Convert.ToInt32(result)
                End If

                Microsoft.VisualBasic.Interaction.MsgBox("Login successful! Welcome " & fullname & " (" & role & ")", Microsoft.VisualBasic.MsgBoxStyle.Information, "Welcome")

                ' 3. Pass the new historyId to FormMain
                Dim Dashboard As New FormMain(role, fullname, historyId, username)
                Dashboard.Show()
                Me.Hide()
            Else
                reader.Close()
                MsgBox("Invalid username or password.", MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            If conn IsNot Nothing AndAlso conn.State <> ConnectionState.Closed Then conn.Close()
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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class