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

    ' --- FORM EVENTS ---
    Private Sub FormAddBlotter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Dropdowns
        If cmbIncidentType.Items.Count = 0 Then
            cmbIncidentType.Items.AddRange(New String() {"Amicable Settlement", "Theft", "Physical Injury", "Noise Complaint", "Property Damage", "Threats", "Others"})
        End If
        If cmbStatus.Items.Count = 0 Then
            cmbStatus.Items.AddRange(New String() {"Active", "Settled", "Referred to Police", "Dismissed"})
        End If

        ' Set Title based on Mode
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
End Class