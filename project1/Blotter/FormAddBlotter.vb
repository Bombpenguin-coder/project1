Public Class FormAddBlotter

    ' ID Tracker
    Public Property CaseID As Integer = 0

    ' --- PROPERTIES (Bridges for the Main Form) ---
    Public Property Complainant As String
        Get
            Return txtComplainant.Text.Trim()
        End Get
        Set(value As String)
            txtComplainant.Text = value
        End Set
    End Property

    Public Property Respondent As String
        Get
            Return txtRespondent.Text.Trim()
        End Get
        Set(value As String)
            txtRespondent.Text = value
        End Set
    End Property

    Public Property IncidentLocation As String
        Get
            Return txtLocation.Text.Trim()
        End Get
        Set(value As String)
            txtLocation.Text = value
        End Set
    End Property

    Public Property Narrative As String
        Get
            Return txtNarrative.Text.Trim()
        End Get
        Set(value As String)
            txtNarrative.Text = value
        End Set
    End Property

    Public Property IncidentType As String
        Get
            Return cmbIncidentType.Text
        End Get
        Set(value As String)
            cmbIncidentType.Text = value
        End Set
    End Property

    Public Property Status As String
        Get
            Return cmbStatus.Text
        End Get
        Set(value As String)
            cmbStatus.Text = value
        End Set
    End Property

    Public Property IncidentDate As Date
        Get
            Return dtpIncidentDate.Value
        End Get
        Set(value As Date)
            dtpIncidentDate.Value = value
        End Set
    End Property

    Public Property ComplainantCell As String
        Get
            Return txtComplainantCell.Text.Trim()
        End Get
        Set(value As String)
            txtComplainantCell.Text = value
        End Set
    End Property

    Public Property RespondentCell As String
        Get
            Return txtRespondentCell.Text.Trim()
        End Get
        Set(value As String)
            txtRespondentCell.Text = value
        End Set
    End Property

    Public Property Street As String
        Get
            Return cmbStreet.Text
        End Get
        Set(value As String)
            cmbStreet.Text = value
        End Set
    End Property

    Public Property FullInformation As String
        Get
            Return txtFullInformation.Text.Trim()
        End Get
        Set(value As String)
            txtFullInformation.Text = value
        End Set
    End Property

    ' --- FORM EVENTS ---
    Private Sub FormAddBlotter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- 1. LOAD DYNAMIC LISTS FROM DATABASE ---
        Try
            Dim repo As New LookupRepository()

            ' Load Incident Types
            cmbIncidentType.DataSource = repo.GetItemsByCategory("Incident Type")
            cmbIncidentType.DisplayMember = "item_value"

            ' Load Streets
            cmbStreet.DataSource = repo.GetItemsByCategory("Street")
            cmbStreet.DisplayMember = "item_value"

        Catch ex As Exception
            MessageBox.Show("Error loading dropdowns: " & ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' --- 2. LOAD STATIC LIST (Status usually doesn't change) ---
        If cmbStatus.Items.Count = 0 Then
            cmbStatus.Items.AddRange(New String() {"Active", "Settled", "Referred to Police", "Dismissed"})
        End If

        dtpIncidentDate.MaxDate = DateTime.Now

        ' --- 3. SET TITLE BASED ON MODE ---
        If CaseID > 0 Then
            Me.Text = "Edit Blotter Case (ID: " & CaseID & ")"
            btnSave.Text = "Update Case"
        Else
            Me.Text = "Add New Blotter Case"
            btnSave.Text = "Save Case"
            cmbStatus.SelectedIndex = 0 ' Default to Active
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validation
        If String.IsNullOrWhiteSpace(Complainant) OrElse String.IsNullOrWhiteSpace(Respondent) Then
            MessageBox.Show("Please enter both Complainant and Respondent.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If dtpIncidentDate.Value.Date > Date.Today Then
            MessageBox.Show("Incident date cannot be in the future.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmbStreet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStreet.SelectedIndexChanged

    End Sub
End Class