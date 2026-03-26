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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
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
        Label1 = New Label()
        CType(dgvResidentLookup, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(23, 394)
        Label14.Name = "Label14"
        Label14.Size = New Size(59, 17)
        Label14.TabIndex = 19
        Label14.Text = "Purpose:"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(23, 321)
        Label12.Name = "Label12"
        Label12.Size = New Size(100, 17)
        Label12.TabIndex = 18
        Label12.Text = "Certificate Type:"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(41, 68)
        Label11.Name = "Label11"
        Label11.Size = New Size(50, 17)
        Label11.TabIndex = 17
        Label11.Text = "Search:"
        ' 
        ' txtPurpose
        ' 
        txtPurpose.Location = New Point(23, 427)
        txtPurpose.Multiline = True
        txtPurpose.Name = "txtPurpose"
        txtPurpose.Size = New Size(313, 95)
        txtPurpose.TabIndex = 16
        ' 
        ' cmbCertificateType
        ' 
        cmbCertificateType.FormattingEnabled = True
        cmbCertificateType.Location = New Point(24, 350)
        cmbCertificateType.Name = "cmbCertificateType"
        cmbCertificateType.Size = New Size(145, 25)
        cmbCertificateType.TabIndex = 15
        ' 
        ' lblSelectedResident
        ' 
        lblSelectedResident.AutoSize = True
        lblSelectedResident.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblSelectedResident.Location = New Point(269, 67)
        lblSelectedResident.Name = "lblSelectedResident"
        lblSelectedResident.Size = New Size(76, 17)
        lblSelectedResident.TabIndex = 14
        lblSelectedResident.Text = "Placeholder"
        ' 
        ' dgvResidentLookup
        ' 
        dgvResidentLookup.AllowUserToAddRows = False
        dgvResidentLookup.AllowUserToDeleteRows = False
        dgvResidentLookup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResidentLookup.BackgroundColor = Color.FromArgb(CByte(240), CByte(243), CByte(250))
        dgvResidentLookup.BorderStyle = BorderStyle.None
        dgvResidentLookup.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgvResidentLookup.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(98), CByte(155), CByte(181))
        DataGridViewCellStyle1.Font = New Font("Segoe UI Light", 9.75F)
        DataGridViewCellStyle1.ForeColor = SystemColors.Window
        DataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(CByte(123), CByte(189), CByte(232))
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvResidentLookup.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvResidentLookup.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(235), CByte(243), CByte(249))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(CByte(123), CByte(189), CByte(232))
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.ButtonFace
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        dgvResidentLookup.DefaultCellStyle = DataGridViewCellStyle2
        dgvResidentLookup.EnableHeadersVisualStyles = False
        dgvResidentLookup.Location = New Point(16, 114)
        dgvResidentLookup.Name = "dgvResidentLookup"
        dgvResidentLookup.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken
        dgvResidentLookup.RowHeadersVisible = False
        dgvResidentLookup.Size = New Size(738, 170)
        dgvResidentLookup.TabIndex = 13
        ' 
        ' txtResidentSearch
        ' 
        txtResidentSearch.Location = New Point(106, 65)
        txtResidentSearch.Name = "txtResidentSearch"
        txtResidentSearch.Size = New Size(111, 25)
        txtResidentSearch.TabIndex = 12
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(610, 459)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(107, 42)
        btnCancel.TabIndex = 25
        btnCancel.Text = "Print Preview"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(406, 392)
        Label16.Name = "Label16"
        Label16.Size = New Size(106, 17)
        Label16.TabIndex = 24
        Label16.Text = "Control Number:"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(406, 317)
        Label15.Name = "Label15"
        Label15.Size = New Size(85, 17)
        Label15.TabIndex = 23
        Label15.Text = "Amount Paid:"
        ' 
        ' btnIssue
        ' 
        btnIssue.BackColor = Color.FromArgb(CByte(23), CByte(80), CByte(126))
        btnIssue.FlatStyle = FlatStyle.Flat
        btnIssue.Font = New Font("Microsoft PhagsPa", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnIssue.ForeColor = Color.White
        btnIssue.Location = New Point(610, 394)
        btnIssue.Name = "btnIssue"
        btnIssue.Size = New Size(107, 42)
        btnIssue.TabIndex = 22
        btnIssue.Text = "Issue Save"
        btnIssue.UseVisualStyleBackColor = False
        ' 
        ' txtControlNumber
        ' 
        txtControlNumber.Location = New Point(406, 427)
        txtControlNumber.Name = "txtControlNumber"
        txtControlNumber.ReadOnly = True
        txtControlNumber.Size = New Size(147, 25)
        txtControlNumber.TabIndex = 21
        ' 
        ' txtAmountPaid
        ' 
        txtAmountPaid.Location = New Point(406, 350)
        txtAmountPaid.Name = "txtAmountPaid"
        txtAmountPaid.Size = New Size(147, 25)
        txtAmountPaid.TabIndex = 20
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(23, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(130, 18)
        Label1.TabIndex = 26
        Label1.Text = "Issue Documents"
        ' 
        ' FormIssueCertificate
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(213), CByte(222), CByte(239))
        ClientSize = New Size(769, 605)
        Controls.Add(Label1)
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
        Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
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
    Friend WithEvents Label1 As Label
End Class
