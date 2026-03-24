Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class FormForgotPassword
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=login_db")
    Dim savedAnswer As String = ""

    ' 1. SEARCH FOR THE USER
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT security_question, security_answer FROM users WHERE username = @Username", conn)
            cmd.Parameters.AddWithValue("@Username", txtRecoverUsername.Text.Trim())

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                ' User found! Display their question
                lblSecurityQuestion.Text = reader("security_question").ToString()
                savedAnswer = reader("security_answer").ToString()
            Else
                MsgBox("Username not found.", MsgBoxStyle.Critical, "Error")
                lblSecurityQuestion.Text = "..."
                savedAnswer = ""
            End If
            reader.Close()
        Catch ex As Exception
            MsgBox("Error searching database: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' 2. VERIFY ANSWER AND RESET PASSWORD
    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        If String.IsNullOrWhiteSpace(savedAnswer) Then
            MsgBox("Please search for a valid username first.", MsgBoxStyle.Exclamation, "Wait")
            Return
        End If

        ' Check if they answered correctly (converted to lowercase to make it easier for the user)
        If txtSecurityAnswer.Text.Trim().ToLower() <> savedAnswer.ToLower() Then
            MsgBox("Incorrect Security Answer.", MsgBoxStyle.Critical, "Access Denied")
            Return
        End If

        If String.IsNullOrWhiteSpace(txtNewPassword.Text) Then
            MsgBox("Please enter a new password.", MsgBoxStyle.Exclamation, "Missing Data")
            Return
        End If

        ' Everything is correct, update the password!
        Try
            conn.Open()
            Dim newHashedPassword As String = HashPassword(txtNewPassword.Text)

            Dim cmd As New MySqlCommand("UPDATE users SET password = @Password WHERE username = @Username", conn)
            cmd.Parameters.AddWithValue("@Password", newHashedPassword)
            cmd.Parameters.AddWithValue("@Username", txtRecoverUsername.Text.Trim())

            cmd.ExecuteNonQuery()

            MsgBox("Password reset successfully! You may now log in.", MsgBoxStyle.Information, "Success")
            Me.Close() ' Close the recovery form

        Catch ex As Exception
            MsgBox("Error resetting password: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' SHA-256 Hashing Function (Same as Form1)
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

    Private Sub lblSecurityQuestion_Click(sender As Object, e As EventArgs) Handles lblSecurityQuestion.Click

    End Sub
End Class