<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSystemMaintenance
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
        cmbCategory = New ComboBox()
        dgvItems = New DataGridView()
        txtNewItem = New TextBox()
        btnAdd = New Button()
        btnDelete = New Button()
        btnCancel = New Button()
        CType(dgvItems, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' cmbCategory
        ' 
        cmbCategory.FormattingEnabled = True
        cmbCategory.Location = New Point(49, 60)
        cmbCategory.Name = "cmbCategory"
        cmbCategory.Size = New Size(121, 23)
        cmbCategory.TabIndex = 0
        ' 
        ' dgvItems
        ' 
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvItems.Location = New Point(49, 113)
        dgvItems.Name = "dgvItems"
        dgvItems.Size = New Size(306, 150)
        dgvItems.TabIndex = 1
        ' 
        ' txtNewItem
        ' 
        txtNewItem.Location = New Point(49, 299)
        txtNewItem.Name = "txtNewItem"
        txtNewItem.Size = New Size(135, 23)
        txtNewItem.TabIndex = 2
        ' 
        ' btnAdd
        ' 
        btnAdd.Location = New Point(129, 353)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(88, 34)
        btnAdd.TabIndex = 3
        btnAdd.Text = "Add"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(252, 353)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(88, 34)
        btnDelete.TabIndex = 4
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(369, 353)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(88, 34)
        btnCancel.TabIndex = 5
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' FormSystemMaintenance
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(469, 450)
        Controls.Add(btnCancel)
        Controls.Add(btnDelete)
        Controls.Add(btnAdd)
        Controls.Add(txtNewItem)
        Controls.Add(dgvItems)
        Controls.Add(cmbCategory)
        Name = "FormSystemMaintenance"
        Text = "FormSystemMaintenance"
        CType(dgvItems, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cmbCategory As ComboBox
    Friend WithEvents dgvItems As DataGridView
    Friend WithEvents txtNewItem As TextBox
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnCancel As Button
End Class
