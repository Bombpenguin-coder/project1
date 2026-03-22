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
        txtComplainantCell = New TextBox()
        txtRespondentCell = New TextBox()
        txtFullInformation = New RichTextBox()
        cmbStreet = New ComboBox()
        Label1 = New Label()
        dtpIncidentTime = New DateTimePicker()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        SuspendLayout()
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(12, 324)
        Label36.Name = "Label36"
        Label36.Size = New Size(55, 15)
        Label36.TabIndex = 31
        Label36.Text = "Narrative"
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(36, 266)
        Label35.Name = "Label35"
        Label35.Size = New Size(39, 15)
        Label35.TabIndex = 30
        Label35.Text = "Status"
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Location = New Point(275, 209)
        Label34.Name = "Label34"
        Label34.Size = New Size(77, 15)
        Label34.TabIndex = 29
        Label34.Text = "Incident Date"
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Location = New Point(36, 209)
        Label33.Name = "Label33"
        Label33.Size = New Size(49, 15)
        Label33.TabIndex = 28
        Label33.Text = "Address"
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Location = New Point(275, 148)
        Label32.Name = "Label32"
        Label32.Size = New Size(77, 15)
        Label32.TabIndex = 27
        Label32.Text = "Incident Type"
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(275, 44)
        Label31.Name = "Label31"
        Label31.Size = New Size(70, 15)
        Label31.TabIndex = 26
        Label31.Text = "Respondent"
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(36, 44)
        Label30.Name = "Label30"
        Label30.Size = New Size(76, 15)
        Label30.TabIndex = 25
        Label30.Text = "Complainant"
        ' 
        ' txtNarrative
        ' 
        txtNarrative.Location = New Point(12, 342)
        txtNarrative.Multiline = True
        txtNarrative.Name = "txtNarrative"
        txtNarrative.Size = New Size(222, 89)
        txtNarrative.TabIndex = 24
        ' 
        ' cmbStatus
        ' 
        cmbStatus.FormattingEnabled = True
        cmbStatus.Location = New Point(36, 284)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(136, 23)
        cmbStatus.TabIndex = 23
        ' 
        ' txtLocation
        ' 
        txtLocation.Location = New Point(36, 227)
        txtLocation.Name = "txtLocation"
        txtLocation.Size = New Size(136, 23)
        txtLocation.TabIndex = 22
        ' 
        ' txtRespondent
        ' 
        txtRespondent.Location = New Point(275, 62)
        txtRespondent.Name = "txtRespondent"
        txtRespondent.Size = New Size(147, 23)
        txtRespondent.TabIndex = 21
        ' 
        ' dtpIncidentDate
        ' 
        dtpIncidentDate.CustomFormat = "MMMM dd, yyyy"
        dtpIncidentDate.Location = New Point(275, 227)
        dtpIncidentDate.Name = "dtpIncidentDate"
        dtpIncidentDate.Size = New Size(147, 23)
        dtpIncidentDate.TabIndex = 20
        ' 
        ' cmbIncidentType
        ' 
        cmbIncidentType.FormattingEnabled = True
        cmbIncidentType.Location = New Point(275, 170)
        cmbIncidentType.Name = "cmbIncidentType"
        cmbIncidentType.Size = New Size(147, 23)
        cmbIncidentType.TabIndex = 19
        ' 
        ' txtComplainant
        ' 
        txtComplainant.Location = New Point(36, 62)
        txtComplainant.Name = "txtComplainant"
        txtComplainant.Size = New Size(136, 23)
        txtComplainant.TabIndex = 18
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(258, 464)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(75, 23)
        btnSave.TabIndex = 32
        btnSave.Text = "Save Case"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(362, 464)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(75, 23)
        btnCancel.TabIndex = 33
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' txtComplainantCell
        ' 
        txtComplainantCell.Location = New Point(36, 112)
        txtComplainantCell.Name = "txtComplainantCell"
        txtComplainantCell.Size = New Size(136, 23)
        txtComplainantCell.TabIndex = 34
        ' 
        ' txtRespondentCell
        ' 
        txtRespondentCell.Location = New Point(275, 112)
        txtRespondentCell.Name = "txtRespondentCell"
        txtRespondentCell.Size = New Size(147, 23)
        txtRespondentCell.TabIndex = 35
        ' 
        ' txtFullInformation
        ' 
        txtFullInformation.Location = New Point(258, 342)
        txtFullInformation.Name = "txtFullInformation"
        txtFullInformation.Size = New Size(254, 96)
        txtFullInformation.TabIndex = 36
        txtFullInformation.Text = ""
        ' 
        ' cmbStreet
        ' 
        cmbStreet.FormattingEnabled = True
        cmbStreet.Location = New Point(36, 170)
        cmbStreet.Name = "cmbStreet"
        cmbStreet.Size = New Size(136, 23)
        cmbStreet.TabIndex = 37
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(275, 266)
        Label1.Name = "Label1"
        Label1.Size = New Size(79, 15)
        Label1.TabIndex = 39
        Label1.Text = "Incident Time"
        ' 
        ' dtpIncidentTime
        ' 
        dtpIncidentTime.CustomFormat = "hh:mm tt"
        dtpIncidentTime.Location = New Point(275, 284)
        dtpIncidentTime.Name = "dtpIncidentTime"
        dtpIncidentTime.ShowUpDown = True
        dtpIncidentTime.Size = New Size(147, 23)
        dtpIncidentTime.TabIndex = 38
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(36, 94)
        Label2.Name = "Label2"
        Label2.Size = New Size(143, 15)
        Label2.TabIndex = 40
        Label2.Text = "Complainant Contact No."
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(275, 94)
        Label3.Name = "Label3"
        Label3.Size = New Size(137, 15)
        Label3.TabIndex = 41
        Label3.Text = "Respondent Contact No."
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(36, 152)
        Label4.Name = "Label4"
        Label4.Size = New Size(83, 15)
        Label4.TabIndex = 42
        Label4.Text = "Incident Street"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(258, 324)
        Label5.Name = "Label5"
        Label5.Size = New Size(92, 15)
        Label5.TabIndex = 43
        Label5.Text = "Full Information"
        ' 
        ' FormAddBlotter
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(524, 492)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(dtpIncidentTime)
        Controls.Add(cmbStreet)
        Controls.Add(txtFullInformation)
        Controls.Add(txtRespondentCell)
        Controls.Add(txtComplainantCell)
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
    Friend WithEvents txtComplainantCell As TextBox
    Friend WithEvents txtRespondentCell As TextBox
    Friend WithEvents txtFullInformation As RichTextBox
    Friend WithEvents cmbStreet As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpIncidentTime As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
End Class
