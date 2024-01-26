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
        DataGridView1 = New DataGridView()
        uploadbtn = New Button()
        customerdropdown = New ComboBox()
        Label1 = New Label()
        viewbtn = New Button()
        AccountLoad = New Button()
        Typedoclabel = New Label()
        typeofdoc = New ComboBox()
        documentbox = New GroupBox()
        allrb = New RadioButton()
        unavailabledocrb = New RadioButton()
        availabledocrb = New RadioButton()
        rowsbox = New GroupBox()
        rowcountlabel = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        documentbox.SuspendLayout()
        rowsbox.SuspendLayout()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.BackgroundColor = SystemColors.AppWorkspace
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Dock = DockStyle.Bottom
        DataGridView1.ImeMode = ImeMode.On
        DataGridView1.Location = New Point(0, 262)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 62
        DataGridView1.Size = New Size(1924, 788)
        DataGridView1.TabIndex = 1
        ' 
        ' uploadbtn
        ' 
        uploadbtn.BackColor = Color.DodgerBlue
        uploadbtn.Font = New Font("Segoe UI", 14F)
        uploadbtn.ForeColor = SystemColors.ButtonHighlight
        uploadbtn.Location = New Point(1695, 5)
        uploadbtn.Name = "uploadbtn"
        uploadbtn.Size = New Size(113, 54)
        uploadbtn.TabIndex = 3
        uploadbtn.Text = "Upload"
        uploadbtn.UseVisualStyleBackColor = False
        ' 
        ' customerdropdown
        ' 
        customerdropdown.FormattingEnabled = True
        customerdropdown.Location = New Point(219, 21)
        customerdropdown.Name = "customerdropdown"
        customerdropdown.Size = New Size(554, 33)
        customerdropdown.TabIndex = 4
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(21, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(192, 30)
        Label1.TabIndex = 5
        Label1.Text = "Customer Name: "
        ' 
        ' viewbtn
        ' 
        viewbtn.BackColor = Color.MediumSeaGreen
        viewbtn.Font = New Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        viewbtn.ForeColor = SystemColors.ButtonHighlight
        viewbtn.Location = New Point(1576, 5)
        viewbtn.Name = "viewbtn"
        viewbtn.Size = New Size(113, 54)
        viewbtn.TabIndex = 2
        viewbtn.Text = "View"
        viewbtn.UseVisualStyleBackColor = False
        ' 
        ' AccountLoad
        ' 
        AccountLoad.BackColor = Color.DarkCyan
        AccountLoad.Font = New Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        AccountLoad.ForeColor = SystemColors.ButtonHighlight
        AccountLoad.Location = New Point(1457, 5)
        AccountLoad.Name = "AccountLoad"
        AccountLoad.Size = New Size(113, 54)
        AccountLoad.TabIndex = 6
        AccountLoad.Text = "Load"
        AccountLoad.UseVisualStyleBackColor = False
        ' 
        ' Typedoclabel
        ' 
        Typedoclabel.AutoSize = True
        Typedoclabel.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        Typedoclabel.Location = New Point(795, 20)
        Typedoclabel.Name = "Typedoclabel"
        Typedoclabel.Size = New Size(215, 30)
        Typedoclabel.TabIndex = 7
        Typedoclabel.Text = "Type of Document: "
        ' 
        ' typeofdoc
        ' 
        typeofdoc.FormattingEnabled = True
        typeofdoc.Location = New Point(1016, 21)
        typeofdoc.Name = "typeofdoc"
        typeofdoc.Size = New Size(415, 33)
        typeofdoc.TabIndex = 8
        ' 
        ' documentbox
        ' 
        documentbox.Controls.Add(allrb)
        documentbox.Controls.Add(unavailabledocrb)
        documentbox.Controls.Add(availabledocrb)
        documentbox.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        documentbox.Location = New Point(21, 60)
        documentbox.Name = "documentbox"
        documentbox.Size = New Size(636, 76)
        documentbox.TabIndex = 9
        documentbox.TabStop = False
        documentbox.Text = "Document "
        ' 
        ' allrb
        ' 
        allrb.AutoSize = True
        allrb.Location = New Point(546, 30)
        allrb.Name = "allrb"
        allrb.Size = New Size(87, 34)
        allrb.TabIndex = 2
        allrb.TabStop = True
        allrb.Text = "Both"
        allrb.UseVisualStyleBackColor = True
        ' 
        ' unavailabledocrb
        ' 
        unavailabledocrb.AutoSize = True
        unavailabledocrb.Location = New Point(268, 30)
        unavailabledocrb.Name = "unavailabledocrb"
        unavailabledocrb.Size = New Size(272, 34)
        unavailabledocrb.TabIndex = 1
        unavailabledocrb.TabStop = True
        unavailabledocrb.Text = "Unavailable Document"
        unavailabledocrb.UseVisualStyleBackColor = True
        ' 
        ' availabledocrb
        ' 
        availabledocrb.AutoSize = True
        availabledocrb.Location = New Point(10, 30)
        availabledocrb.Name = "availabledocrb"
        availabledocrb.Size = New Size(252, 34)
        availabledocrb.TabIndex = 0
        availabledocrb.TabStop = True
        availabledocrb.Text = "Available Document "
        availabledocrb.UseVisualStyleBackColor = True
        ' 
        ' rowsbox
        ' 
        rowsbox.Controls.Add(rowcountlabel)
        rowsbox.Font = New Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        rowsbox.Location = New Point(674, 60)
        rowsbox.Name = "rowsbox"
        rowsbox.Size = New Size(300, 76)
        rowsbox.TabIndex = 11
        rowsbox.TabStop = False
        rowsbox.Text = "Row Count"
        ' 
        ' rowcountlabel
        ' 
        rowcountlabel.AutoSize = True
        rowcountlabel.Location = New Point(20, 33)
        rowcountlabel.Name = "rowcountlabel"
        rowcountlabel.Size = New Size(0, 30)
        rowcountlabel.TabIndex = 0
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.WhiteSmoke
        ClientSize = New Size(1924, 1050)
        Controls.Add(rowsbox)
        Controls.Add(documentbox)
        Controls.Add(typeofdoc)
        Controls.Add(Typedoclabel)
        Controls.Add(AccountLoad)
        Controls.Add(Label1)
        Controls.Add(customerdropdown)
        Controls.Add(uploadbtn)
        Controls.Add(viewbtn)
        Controls.Add(DataGridView1)
        Name = "Form1"
        Text = "Cash Sales Customer"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        documentbox.ResumeLayout(False)
        documentbox.PerformLayout()
        rowsbox.ResumeLayout(False)
        rowsbox.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents uploadbtn As Button
    Friend WithEvents Upload As System.Windows.Forms.Button
    Friend WithEvents View As System.Windows.Forms.Button
    Friend WithEvents btnViewPdf As System.Windows.Forms.Button
    Friend WithEvents customerdropdown As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents viewbtn As Button
    Friend WithEvents AccountLoad As Button
    Friend WithEvents Typedoclabel As Label
    Friend WithEvents typeofdoc As ComboBox
    Friend WithEvents documentbox As GroupBox
    Friend WithEvents unavailabledocrb As RadioButton
    Friend WithEvents availabledocrb As RadioButton
    Friend WithEvents allrb As RadioButton
    Friend WithEvents rowsbox As GroupBox
    Friend WithEvents rowcountlabel As Label



End Class
