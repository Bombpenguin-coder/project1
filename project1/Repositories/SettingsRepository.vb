Imports MySql.Data.MySqlClient

Public Class SettingsRepository
    Private connectionString As String = "server=localhost;port=3306;user id=root;password=;database=barangay_db;"

    ' 1. LOAD SETTINGS
    Public Function GetSettings() As SystemSetting
        Dim setting As New SystemSetting()
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM system_settings WHERE id = 1"
            Using cmd As New MySqlCommand(query, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        setting.BarangayName = reader("barangay_name").ToString()
                        setting.CityName = reader("city_name").ToString()
                        setting.ProvinceName = reader("province_name").ToString()
                        setting.CaptainName = reader("captain_name").ToString()
                        setting.ContactNumber = reader("contact_number").ToString()
                    End If
                End Using
            End Using
        End Using
        Return setting
    End Function

    ' 2. UPDATE SETTINGS
    Public Sub UpdateSettings(setting As SystemSetting)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE system_settings SET barangay_name=@Brgy, city_name=@City, province_name=@Prov, captain_name=@Capt, contact_number=@Contact WHERE id = 1"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@Brgy", setting.BarangayName)
                cmd.Parameters.AddWithValue("@City", setting.CityName)
                cmd.Parameters.AddWithValue("@Prov", setting.ProvinceName)
                cmd.Parameters.AddWithValue("@Capt", setting.CaptainName)
                cmd.Parameters.AddWithValue("@Contact", setting.ContactNumber)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class