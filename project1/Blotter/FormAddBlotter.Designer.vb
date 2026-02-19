<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddBlotter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label36 = New Label()
        Label35 = New Label()
        Label34 = New Label()
        Label33 = New Label()
        Label32 = New Label()
        Label31 = New Label()
        Label30 = New Label()
        txtNarrative = New TextBox()
        cmbStatus = New ComboBox()
        txtLocation = New TextBox()
        txtRespondent = New TextBox()
        dtpIncidentDate = New DateTimePicker()
        cmbIncidentType = New ComboBox()
        txtComplainant = New TextBox()
        btnSave = New Button()
        btnCancel = New Button()
        SuspendLayout()
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(12, 259)
        Label36.Name = "Label36"
        Label36.Size = New Size(55, 15)
        Label36.TabIndex = 31
        Label36.Text = "Narrative"
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(36, 199)
        Label35.Name = "Label35"
        Label35.Size = New Size(39, 15)
        Label35.TabIndex = 30
        Label35.Text = "Status"
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Location = New Point(234, 199)
        Label34.Name = "Label34"
        Label34.Size = New Size(77, 15)
        Label34.TabIndex = 29
        Label34.Text = "Incident Date"
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Location = New Point(36, 116)
        Label33.Name = "Label33"
        Label33.Size = New Size(53, 15)
        Label33.TabIndex = 28
        Label33.Text = "Location"
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Location = New Point(234, 116)
        Label32.Name = "Label32"
        Label32.Size = New Size(77, 15)
        Label32.TabIndex = 27
        Label32.Text = "Incident Type"
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(234, 48)
        Label31.Name = "Label31"
        Label31.Size = New Size(70, 15)
        Label31.TabIndex = 26
        Label31.Text = "Respondent"
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(36, 48)
        Label30.Name = "Label30"
        Label30.Size = New Size(76, 15)
        Label30.TabIndex = 25
        Label30.Text = "Complainant"
        ' 
        ' txtNarrative
        ' 
        txtNarrative.Location = New Point(12, 277)
        txtNarrative.Multiline = True
        txtNarrative.Name = "txtNarrative"
        txtNarrative.Size = New Size(488, 89)
        txtNarrative.TabIndex = 24
        ' 
        ' cmbStatus
        ' 
        cmbStatus.FormattingEnabled = True
        cmbStatus.Location = New Point(36, 217)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(136, 23)
        cmbStatus.TabIndex = 23
        ' 
        ' txtLocation
        ' 
        txtLocation.Location = New Point(36, 146)
        txtLocation.Name = "txtLocation"
        txtLocation.Size = New Size(136, 23)
        txtLocation.TabIndex = 22
        ' 
        ' txtRespondent
        ' 
        txtRespondent.Location = New Point(234, 78)
        txtRespondent.Name = "txtRespondent"
        txtRespondent.Size = New Size(147, 23)
        txtRespondent.TabIndex = 21
        ' 
        ' dtpIncidentDate
        ' 
        dtpIncidentDate.CustomFormat = "MMMM dd, yyyy - hh:mm tt"
        dtpIncidentDate.Location = New Point(234, 217)
        dtpIncidentDate.Name = "dtpIncidentDate"
        dtpIncidentDate.Size = New Size(147, 23)
        dtpIncidentDate.TabIndex = 20
        ' 
        ' cmbIncidentType
        ' 
        cmbIncidentType.FormattingEnabled = True
        cmbIncidentType.Location = New Point(234, 146)
        cmbIncidentType.Name = "cmbIncidentType"
        cmbIncidentType.Size = New Size(147, 23)
        cmbIncidentType.TabIndex = 19
        ' 
        ' txtComplainant
        ' 
        txtComplainant.Location = New Point(36, 78)
        txtComplainant.Name = "txtComplainant"
        txtComplainant.Size = New Size(136, 23)
        txtComplainant.TabIndex = 18
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(258, 399)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(75, 23)
        btnSave.TabIndex = 32
        btnSave.Text = "Save Case"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(362, 399)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(75, 23)
        btnCancel.TabIndex = 33
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' FormAddBlotter
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(524, 450)
        Controls.Add(btnCancel)
        Controls.Add(btnSave)
        Controls.Add(Label36)
        Controls.Add(Label35)
        Controls.Add(Label34)
        Controls.Add(Label33)
        Controls.Add(Label32)
        Controls.Add(Label31)
        Controls.Add(Label30)
        Controls.Add(txtNarrative)
        Controls.Add(cmbStatus)
        Controls.Add(txtLocation)
        Controls.Add(txtRespondent)
        Controls.Add(dtpIncidentDate)
        Controls.Add(cmbIncidentType)
        Controls.Add(txtComplainant)
        Name = "FormAddBlotter"
        Text = "FormAddBlotter"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label36 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents txtNarrative As TextBox
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents txtLocation As TextBox
    Friend WithEvents txtRespondent As TextBox
    Friend WithEvents dtpIncidentDate As DateTimePicker
    Friend WithEvents cmbIncidentType As ComboBox
    Friend WithEvents txtComplainant As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
End Class
