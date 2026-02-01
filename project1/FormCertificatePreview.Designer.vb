<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCertificatePreview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCertificatePreview))
        lblTitle = New Label()
        lblBodyText1 = New Label()
        lblBodyText2 = New Label()
        lblFullName = New Label()
        lblAddress = New Label()
        lblPurpose = New Label()
        lblControlNumber = New Label()
        lblDateIssued = New Label()
        lblCaptainName = New Label()
        PictureBox1 = New PictureBox()
        Panel1 = New Panel()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.BackColor = Color.White
        lblTitle.Location = New Point(149, 81)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(29, 15)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Title"
        ' 
        ' lblBodyText1
        ' 
        lblBodyText1.AutoSize = True
        lblBodyText1.BackColor = Color.White
        lblBodyText1.Location = New Point(138, 132)
        lblBodyText1.Name = "lblBodyText1"
        lblBodyText1.Size = New Size(40, 15)
        lblBodyText1.TabIndex = 1
        lblBodyText1.Text = "Body1"
        ' 
        ' lblBodyText2
        ' 
        lblBodyText2.AutoSize = True
        lblBodyText2.BackColor = Color.White
        lblBodyText2.Location = New Point(138, 262)
        lblBodyText2.Name = "lblBodyText2"
        lblBodyText2.Size = New Size(40, 15)
        lblBodyText2.TabIndex = 2
        lblBodyText2.Text = "Body2"
        ' 
        ' lblFullName
        ' 
        lblFullName.AutoSize = True
        lblFullName.BackColor = Color.White
        lblFullName.Location = New Point(176, 192)
        lblFullName.Name = "lblFullName"
        lblFullName.Size = New Size(61, 15)
        lblFullName.TabIndex = 3
        lblFullName.Text = "Full Name"
        ' 
        ' lblAddress
        ' 
        lblAddress.AutoSize = True
        lblAddress.BackColor = Color.White
        lblAddress.Location = New Point(323, 192)
        lblAddress.Name = "lblAddress"
        lblAddress.Size = New Size(42, 15)
        lblAddress.TabIndex = 4
        lblAddress.Text = "Adress"
        ' 
        ' lblPurpose
        ' 
        lblPurpose.AutoSize = True
        lblPurpose.BackColor = Color.White
        lblPurpose.ForeColor = Color.Black
        lblPurpose.Location = New Point(289, 305)
        lblPurpose.Name = "lblPurpose"
        lblPurpose.Size = New Size(50, 15)
        lblPurpose.TabIndex = 5
        lblPurpose.Text = "Purpose"
        ' 
        ' lblControlNumber
        ' 
        lblControlNumber.AutoSize = True
        lblControlNumber.BackColor = Color.White
        lblControlNumber.Location = New Point(84, 37)
        lblControlNumber.Name = "lblControlNumber"
        lblControlNumber.Size = New Size(94, 15)
        lblControlNumber.TabIndex = 6
        lblControlNumber.Text = "Control Number"
        ' 
        ' lblDateIssued
        ' 
        lblDateIssued.AutoSize = True
        lblDateIssued.BackColor = Color.White
        lblDateIssued.Location = New Point(355, 37)
        lblDateIssued.Name = "lblDateIssued"
        lblDateIssued.Size = New Size(67, 15)
        lblDateIssued.TabIndex = 7
        lblDateIssued.Text = "Date Issued"
        ' 
        ' lblCaptainName
        ' 
        lblCaptainName.AutoSize = True
        lblCaptainName.BackColor = Color.White
        lblCaptainName.Location = New Point(383, 374)
        lblCaptainName.Name = "lblCaptainName"
        lblCaptainName.Size = New Size(83, 15)
        lblCaptainName.TabIndex = 8
        lblCaptainName.Text = "Captain Name"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.White
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(498, 12)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(103, 94)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 9
        PictureBox1.TabStop = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.White
        Panel1.Location = New Point(68, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(533, 469)
        Panel1.TabIndex = 10
        ' 
        ' FormCertificatePreview
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ControlLight
        ClientSize = New Size(670, 471)
        Controls.Add(PictureBox1)
        Controls.Add(lblCaptainName)
        Controls.Add(lblDateIssued)
        Controls.Add(lblControlNumber)
        Controls.Add(lblPurpose)
        Controls.Add(lblAddress)
        Controls.Add(lblFullName)
        Controls.Add(lblBodyText2)
        Controls.Add(lblBodyText1)
        Controls.Add(lblTitle)
        Controls.Add(Panel1)
        Name = "FormCertificatePreview"
        Text = "Barangay Clearance - Print Preview"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lblBodyText1 As Label
    Friend WithEvents lblBodyText2 As Label
    Friend WithEvents lblFullName As Label
    Friend WithEvents lblAddress As Label
    Friend WithEvents lblPurpose As Label
    Friend WithEvents lblControlNumber As Label
    Friend WithEvents lblDateIssued As Label
    Friend WithEvents lblCaptainName As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
End Class
