<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        DataGridView1 = New DataGridView()
        View = New Button()
        Upload = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.FlatStyle = FlatStyle.Flat
        Label1.Font = New Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(266, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(257, 45)
        Label1.TabIndex = 0
        Label1.Text = "Cash Customers"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(0, 62)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 62
        DataGridView1.Size = New Size(801, 225)
        DataGridView1.TabIndex = 1
        ' 
        ' View
        ' 
        View.BackColor = Color.MediumSeaGreen
        View.ForeColor = SystemColors.ButtonHighlight
        View.Location = New Point(266, 358)
        View.Name = "View"
        View.Size = New Size(112, 34)
        View.TabIndex = 2
        View.Text = "View"
        View.UseVisualStyleBackColor = False
        ' 
        ' Upload
        ' 
        Upload.BackColor = Color.DodgerBlue
        Upload.ForeColor = SystemColors.ButtonHighlight
        Upload.Location = New Point(411, 358)
        Upload.Name = "Upload"
        Upload.Size = New Size(112, 34)
        Upload.TabIndex = 3
        Upload.Text = "Upload"
        Upload.UseVisualStyleBackColor = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Cornsilk
        ClientSize = New Size(800, 450)
        Controls.Add(Upload)
        Controls.Add(View)
        Controls.Add(DataGridView1)
        Controls.Add(Label1)
        Name = "Form1"
        Text = "Cash Sales Customer"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents View As Button
    Friend WithEvents Upload As Button

End Class
