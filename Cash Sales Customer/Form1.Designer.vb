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
        viewbtn = New Button()
        uploadbtn = New Button()
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
        DataGridView1.Dock = DockStyle.Top
        DataGridView1.ImeMode = ImeMode.On
        DataGridView1.Location = New Point(0, 0)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 62
        DataGridView1.Size = New Size(1924, 893)
        DataGridView1.TabIndex = 1
        ' 
        ' viewbtn
        ' 
        viewbtn.BackColor = Color.MediumSeaGreen
        viewbtn.Font = New Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        viewbtn.ForeColor = SystemColors.ButtonHighlight
        viewbtn.Location = New Point(676, 910)
        viewbtn.Name = "viewbtn"
        viewbtn.Size = New Size(233, 54)
        viewbtn.TabIndex = 2
        viewbtn.Text = "View"
        viewbtn.UseVisualStyleBackColor = False
        ' 
        ' uploadbtn
        ' 
        uploadbtn.BackColor = Color.DodgerBlue
        uploadbtn.Font = New Font("Segoe UI", 14F)
        uploadbtn.ForeColor = SystemColors.ButtonHighlight
        uploadbtn.Location = New Point(956, 910)
        uploadbtn.Name = "uploadbtn"
        uploadbtn.Size = New Size(233, 54)
        uploadbtn.TabIndex = 3
        uploadbtn.Text = "Upload"
        uploadbtn.UseVisualStyleBackColor = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Cornsilk
        ClientSize = New Size(1924, 1050)
        Controls.Add(uploadbtn)
        Controls.Add(viewbtn)
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
    Friend WithEvents viewbtn As Button
    Friend WithEvents uploadbtn As Button
    Friend WithEvents Upload As System.Windows.Forms.Button
    Friend WithEvents View As System.Windows.Forms.Button
    Friend WithEvents btnViewPdf As System.Windows.Forms.Button



End Class
