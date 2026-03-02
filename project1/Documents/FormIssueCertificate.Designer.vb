<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormIssueCertificate
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
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label14 = New Label()
        Label12 = New Label()
        Label11 = New Label()
        txtPurpose = New TextBox()
        cmbCertificateType = New ComboBox()
        lblSelectedResident = New Label()
        dgvResidentLookup = New DataGridView()
        txtResidentSearch = New TextBox()
        btnCancel = New Button()
        Label16 = New Label()
        Label15 = New Label()
        btnIssue = New Button()
        txtControlNumber = New TextBox()
        txtAmountPaid = New TextBox()
        CType(dgvResidentLookup, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(23, 348)
        Label14.Name = "Label14"
        Label14.Size = New Size(53, 15)
        Label14.TabIndex = 19
        Label14.Text = "Purpose:"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(23, 283)
        Label12.Name = "Label12"
        Label12.Size = New Size(91, 15)
        Label12.TabIndex = 18
        Label12.Text = "Certificate Type:"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(41, 60)
        Label11.Name = "Label11"
        Label11.Size = New Size(45, 15)
        Label11.TabIndex = 17
        Label11.Text = "Search:"
        ' 
        ' txtPurpose
        ' 
        txtPurpose.Location = New Point(23, 377)
        txtPurpose.Multiline = True
        txtPurpose.Name = "txtPurpose"
        txtPurpose.Size = New Size(313, 84)
        txtPurpose.TabIndex = 16
        ' 
        ' cmbCertificateType
        ' 
        cmbCertificateType.FormattingEnabled = True
        cmbCertificateType.Location = New Point(24, 309)
        cmbCertificateType.Name = "cmbCertificateType"
        cmbCertificateType.Size = New Size(145, 23)
        cmbCertificateType.TabIndex = 15
        ' 
        ' lblSelectedResident
        ' 
        lblSelectedResident.AutoSize = True
        lblSelectedResident.Location = New Point(269, 59)
        lblSelectedResident.Name = "lblSelectedResident"
        lblSelectedResident.Size = New Size(69, 15)
        lblSelectedResident.TabIndex = 14
        lblSelectedResident.Text = "Placeholder"
        ' 
        ' dgvResidentLookup
        ' 
        dgvResidentLookup.AllowUserToAddRows = False
        dgvResidentLookup.AllowUserToDeleteRows = False
        dgvResidentLookup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResidentLookup.BackgroundColor = SystemColors.ControlLightLight
        dgvResidentLookup.BorderStyle = BorderStyle.None
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(62), CByte(95), CByte(68))
        DataGridViewCellStyle3.Font = New Font("Segoe UI Light", 9F)
        DataGridViewCellStyle3.ForeColor = SystemColors.Window
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(128), CByte(175), CByte(129))
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.True
        dgvResidentLookup.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        dgvResidentLookup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = SystemColors.ButtonFace
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(CByte(128), CByte(175), CByte(129))
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.ButtonFace
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvResidentLookup.DefaultCellStyle = DataGridViewCellStyle4
        dgvResidentLookup.Location = New Point(12, 91)
        dgvResidentLookup.Name = "dgvResidentLookup"
        dgvResidentLookup.RowHeadersVisible = False
        dgvResidentLookup.Size = New Size(738, 150)
        dgvResidentLookup.TabIndex = 13
        ' 
        ' txtResidentSearch
        ' 
        txtResidentSearch.Location = New Point(106, 57)
        txtResidentSearch.Name = "txtResidentSearch"
        txtResidentSearch.Size = New Size(111, 23)
        txtResidentSearch.TabIndex = 12
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(610, 436)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(107, 37)
        btnCancel.TabIndex = 25
        btnCancel.Text = "Print Preview"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(406, 346)
        Label16.Name = "Label16"
        Label16.Size = New Size(97, 15)
        Label16.TabIndex = 24
        Label16.Text = "Control Number:"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(406, 280)
        Label15.Name = "Label15"
        Label15.Size = New Size(80, 15)
        Label15.TabIndex = 23
        Label15.Text = "Amount Paid:"
        ' 
        ' btnIssue
        ' 
        btnIssue.Location = New Point(610, 377)
        btnIssue.Name = "btnIssue"
        btnIssue.Size = New Size(107, 37)
        btnIssue.TabIndex = 22
        btnIssue.Text = "Issue Save"
        btnIssue.UseVisualStyleBackColor = True
        ' 
        ' txtControlNumber
        ' 
        txtControlNumber.Location = New Point(406, 377)
        txtControlNumber.Name = "txtControlNumber"
        txtControlNumber.ReadOnly = True
        txtControlNumber.Size = New Size(147, 23)
        txtControlNumber.TabIndex = 21
        ' 
        ' txtAmountPaid
        ' 
        txtAmountPaid.Location = New Point(406, 309)
        txtAmountPaid.Name = "txtAmountPaid"
        txtAmountPaid.Size = New Size(147, 23)
        txtAmountPaid.TabIndex = 20
        ' 
        ' FormIssueCertificate
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(769, 534)
        Controls.Add(btnCancel)
        Controls.Add(Label16)
        Controls.Add(Label15)
        Controls.Add(btnIssue)
        Controls.Add(txtControlNumber)
        Controls.Add(txtAmountPaid)
        Controls.Add(Label14)
        Controls.Add(Label12)
        Controls.Add(Label11)
        Controls.Add(txtPurpose)
        Controls.Add(cmbCertificateType)
        Controls.Add(lblSelectedResident)
        Controls.Add(dgvResidentLookup)
        Controls.Add(txtResidentSearch)
        Name = "FormIssueCertificate"
        Text = "FormIssueCertificate"
        CType(dgvResidentLookup, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtPurpose As TextBox
    Friend WithEvents cmbCertificateType As ComboBox
    Friend WithEvents lblSelectedResident As Label
    Friend WithEvents dgvResidentLookup As DataGridView
    Friend WithEvents txtResidentSearch As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents btnIssue As Button
    Friend WithEvents txtControlNumber As TextBox
    Friend WithEvents txtAmountPaid As TextBox
End Class
