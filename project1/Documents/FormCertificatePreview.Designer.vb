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
        wbPreview = New Microsoft.Web.WebView2.WinForms.WebView2()
        btnPrint = New Button()
        picLogo = New PictureBox()
        CType(wbPreview, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' wbPreview
        ' 
        wbPreview.AllowExternalDrop = True
        wbPreview.CreationProperties = Nothing
        wbPreview.DefaultBackgroundColor = Color.White
        wbPreview.Location = New Point(23, 12)
        wbPreview.Name = "wbPreview"
        wbPreview.Size = New Size(664, 436)
        wbPreview.TabIndex = 11
        wbPreview.ZoomFactor = 1R
        ' 
        ' btnPrint
        ' 
        btnPrint.Location = New Point(720, 46)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(75, 23)
        btnPrint.TabIndex = 12
        btnPrint.Text = "Print"
        btnPrint.UseVisualStyleBackColor = True
        ' 
        ' picLogo
        ' 
        picLogo.Image = CType(resources.GetObject("picLogo.Image"), Image)
        picLogo.Location = New Point(693, 130)
        picLogo.Name = "picLogo"
        picLogo.Size = New Size(138, 144)
        picLogo.SizeMode = PictureBoxSizeMode.StretchImage
        picLogo.TabIndex = 13
        picLogo.TabStop = False
        picLogo.Visible = False
        ' 
        ' FormCertificatePreview
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ControlLight
        ClientSize = New Size(843, 471)
        Controls.Add(picLogo)
        Controls.Add(btnPrint)
        Controls.Add(wbPreview)
        Name = "FormCertificatePreview"
        Text = "Barangay Clearance - Print Preview"
        CType(wbPreview, ComponentModel.ISupportInitialize).EndInit()
        CType(picLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents wbPreview As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents btnPrint As Button
    Friend WithEvents picLogo As PictureBox
End Class
