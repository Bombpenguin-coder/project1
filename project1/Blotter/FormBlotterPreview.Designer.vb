<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBlotterPreview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormBlotterPreview))
        btnPrint = New Button()
        wbPreview = New Microsoft.Web.WebView2.WinForms.WebView2()
        picLogo = New PictureBox()
        CType(wbPreview, ComponentModel.ISupportInitialize).BeginInit()
        CType(picLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnPrint
        ' 
        btnPrint.Location = New Point(617, 82)
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(99, 37)
        btnPrint.TabIndex = 0
        btnPrint.Text = "Print Report"
        btnPrint.UseVisualStyleBackColor = True
        ' 
        ' wbPreview
        ' 
        wbPreview.AllowExternalDrop = True
        wbPreview.CreationProperties = Nothing
        wbPreview.DefaultBackgroundColor = Color.White
        wbPreview.Location = New Point(12, 12)
        wbPreview.Name = "wbPreview"
        wbPreview.Size = New Size(566, 426)
        wbPreview.TabIndex = 1
        wbPreview.ZoomFactor = 1R
        ' 
        ' picLogo
        ' 
        picLogo.Image = CType(resources.GetObject("picLogo.Image"), Image)
        picLogo.Location = New Point(606, 165)
        picLogo.Name = "picLogo"
        picLogo.Size = New Size(129, 107)
        picLogo.SizeMode = PictureBoxSizeMode.StretchImage
        picLogo.TabIndex = 2
        picLogo.TabStop = False
        picLogo.Visible = False
        ' 
        ' FormBlotterPreview
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(758, 450)
        Controls.Add(picLogo)
        Controls.Add(wbPreview)
        Controls.Add(btnPrint)
        Name = "FormBlotterPreview"
        Text = "FormBlotterPreview"
        CType(wbPreview, ComponentModel.ISupportInitialize).EndInit()
        CType(picLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents btnPrint As Button
    Friend WithEvents wbPreview As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents picLogo As PictureBox
End Class
