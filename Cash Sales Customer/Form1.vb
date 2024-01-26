Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Form1
    Private customerNames As List(Of String)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        customerNames = New List(Of String)()
        LoadCustomerNames()
        ConfigureDataGridView()
        ' AddHandler customerdropdown.TextChanged, AddressOf CustomerDropdown_TextChanged
        InitializeTypeOfDocComboBox()
        documentbox.Enabled = True
    End Sub

    Private Sub LoadCustomerNames()
        Dim connectionString As String = "Server=MIRAVBHOJANI;Database=cashsales;Integrated Security=True;"
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT Name FROM cashcustomer_details ORDER BY Name"
                Dim command As New SqlCommand(query, connection)
                Dim reader As SqlDataReader = command.ExecuteReader()

                customerdropdown.Items.Clear()
                customerNames.Clear()
                While reader.Read()
                    Dim name As String = reader("Name").ToString()
                    customerdropdown.Items.Add(name)
                    customerNames.Add(name)
                End While
            Catch ex As Exception
                MessageBox.Show("Database error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub CustomerDropdown_TextChanged(sender As Object, e As EventArgs) Handles customerdropdown.TextChanged
        ' Avoid handling if the dropdown is open and an item is being selected with arrow keys
        If customerdropdown.DroppedDown AndAlso (customerdropdown.SelectedIndex >= 0) Then
            Return
        End If

        Dim enteredText As String = customerdropdown.Text
        customerdropdown.Items.Clear()

        If Not String.IsNullOrEmpty(enteredText) Then
            For Each name As String In customerNames
                If name.StartsWith(enteredText, StringComparison.CurrentCultureIgnoreCase) Then
                    customerdropdown.Items.Add(name)
                End If
            Next
        Else
            For Each name As String In customerNames
                customerdropdown.Items.Add(name)
            Next
        End If

        customerdropdown.SelectionStart = enteredText.Length
        customerdropdown.SelectionLength = 0
        customerdropdown.DroppedDown = True
        documentbox.Enabled = String.IsNullOrEmpty(customerdropdown.Text)
    End Sub
    Private Sub CustomerDropdown_KeyDown(sender As Object, e As KeyEventArgs) Handles customerdropdown.KeyDown
        If e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up Then
            ' Open the dropdown if it is not already open
            If Not customerdropdown.DroppedDown Then
                customerdropdown.DroppedDown = True
            End If
        End If
    End Sub
    Private Sub LoadDataByName(customerName As String)
        Dim connectionString As String = "Server=MIRAVBHOJANI;Database=cashsales;Integrated Security=True;"
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As New StringBuilder("SELECT * FROM cashcustomer_details")

                ' Check if a customer name is provided
                If Not String.IsNullOrEmpty(customerName) Then
                    query.Append(" WHERE Name = @customerName")
                Else
                    ' Apply radio button filter only if no customer name is provided
                    If availabledocrb.Checked OrElse unavailabledocrb.Checked Then
                        Dim docTypeColumn As String = String.Empty
                        Select Case typeofdoc.SelectedItem.ToString()
                            Case "Account opening form"
                                docTypeColumn = "aof_link"
                            Case "Contracts"
                                docTypeColumn = "contractlink"
                            Case "Reference"
                                docTypeColumn = "referencelink"
                        End Select

                        If availabledocrb.Checked Then
                            query.Append($" WHERE {docTypeColumn} IS NOT NULL")
                        ElseIf unavailabledocrb.Checked Then
                            query.Append($" WHERE {docTypeColumn} IS NULL")
                        End If
                    End If
                End If

                Dim adapter As New SqlDataAdapter(query.ToString(), connection)
                If Not String.IsNullOrEmpty(customerName) Then
                    adapter.SelectCommand.Parameters.AddWithValue("@customerName", customerName)
                End If

                Dim ds As New DataSet()
                adapter.Fill(ds, "cashcustomer_details")
                DataGridView1.DataSource = ds.Tables("cashcustomer_details")

                ' Hide specific columns
                HideUnwantedColumns()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                connection.Close()
            End Try
            Dim totalRowCount As Integer = GetTotalRowCount()
            Dim loadedRowCount As Integer = DataGridView1.RowCount
            UpdateRowCountDisplay(loadedRowCount, totalRowCount)
            If DataGridView1.Rows.Count > 0 Then
                ' AllowUserToAddRows is set to False to prevent an empty row from being added
                DataGridView1.AllowUserToAddRows = False
            Else
                ' If no data is loaded, you can optionally set AllowUserToAddRows to True or leave it False
                DataGridView1.AllowUserToAddRows = False
            End If

            ' Count the number of rows loaded in the DataGridView
            Dim loadedRowsCount As Integer = DataGridView1.RowCount

            ' Get the total row count from the database
            Dim totalRowsInDatabase As Integer = GetTotalRowCount()

            ' Display the row count information
            rowcountlabel.Text = $"{loadedRowsCount} out of {totalRowsInDatabase}"
        End Using
    End Sub
    Private Sub UpdateRowCountDisplay(loadedRowCount As Integer, totalRowCount As Integer)
        ' Assuming you have a label control to display the row count
        rowcountlabel.Text = $"{loadedRowCount} out of {totalRowCount}"
    End Sub
    Private Sub HideUnwantedColumns()
        Dim columnsToHide As String() = {"aof_link", "contractlink", "referencelink"}
        For Each colName As String In columnsToHide
            If DataGridView1.Columns.Contains(colName) Then
                DataGridView1.Columns(colName).Visible = False
            End If
        Next
    End Sub
    Private Sub ConfigureDataGridView()
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
    End Sub
    Private Sub LoadButton_Click(sender As Object, e As EventArgs) Handles AccountLoad.Click
        ' Check if a type of document is selected
        If typeofdoc.SelectedIndex < 0 Then
            MessageBox.Show("Please select a type of document.")
            Return
        End If
        If String.IsNullOrEmpty(customerdropdown.Text) Then
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to load all the data?", "Confirmation", MessageBoxButtons.YesNo)
            If result = DialogResult.No Then
                Return
            End If
        End If
        LoadDataByName(customerdropdown.Text)
    End Sub

    Private Sub viewBtn_Click(sender As Object, e As EventArgs) Handles viewbtn.Click
        If DataGridView1.SelectedRows.Count > 0 AndAlso typeofdoc.SelectedIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim documentType As String = typeofdoc.SelectedItem.ToString()
            Dim link As String = String.Empty

            Select Case documentType
                Case "Account opening form"
                    link = selectedRow.Cells("aof_link").Value.ToString()
                Case "Contracts"
                    link = selectedRow.Cells("contractlink").Value.ToString()
                Case "Reference"
                    link = selectedRow.Cells("referencelink").Value.ToString()
            End Select

            If Not String.IsNullOrWhiteSpace(link) Then
                If File.Exists(link) Then
                    Try
                        Process.Start(New ProcessStartInfo(link) With {.UseShellExecute = True})
                    Catch ex As Exception
                        MessageBox.Show("Error opening file: " & ex.Message)
                    End Try
                Else
                    MessageBox.Show("PDF file not found.")
                End If
            Else
                MessageBox.Show("No Link found for the selected document type.")
            End If
        Else
            MessageBox.Show("Please select a record and a document type from the list.")
        End If
    End Sub
    Private Sub uploadBtn_Click(sender As Object, e As EventArgs) Handles uploadbtn.Click
        If DataGridView1.SelectedRows.Count > 0 AndAlso typeofdoc.SelectedIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim documentType As String = typeofdoc.SelectedItem.ToString()
            Dim linkColumnName As String = String.Empty
            Dim destinationFolderPath As String = String.Empty

            Select Case documentType
                Case "Account opening form"
                    linkColumnName = "aof_link"
                    destinationFolderPath = "C:\Users\HP\Desktop\Cash Sale Customer Form\"
                Case "Contracts"
                    linkColumnName = "contractlink"
                    destinationFolderPath = "C:\Users\HP\Desktop\Contract\"
                Case "Reference"
                    linkColumnName = "referencelink"
                    destinationFolderPath = "C:\Users\HP\Desktop\Reference\"
            End Select

            Dim existingLink As String = selectedRow.Cells(linkColumnName).Value.ToString()

            If String.IsNullOrWhiteSpace(existingLink) Then
                Using ofd As New OpenFileDialog()
                    ofd.Filter = "PDF Files|*.pdf"

                    If ofd.ShowDialog() = DialogResult.OK Then
                        Dim sourceFilePath As String = ofd.FileName
                        Dim fileName As String = Path.GetFileName(sourceFilePath)
                        Dim destinationFilePath As String = destinationFolderPath & fileName

                        Try
                            File.Copy(sourceFilePath, destinationFilePath, True)
                            UpdateDocumentLink(selectedRow.Cells("Name").Value.ToString(), destinationFilePath, linkColumnName)
                            LoadDataByName(customerdropdown.Text)
                        Catch ex As Exception
                            MessageBox.Show("Error copying file: " & ex.Message)
                        End Try
                    End If
                End Using
            Else
                MessageBox.Show("A link already exists for this record.")
            End If
        Else
            MessageBox.Show("Please select a record and a document type from the list.")
        End If
    End Sub

    Private Sub Updateaof_link(customerName As String, newaof_link As String)
        Dim connectionString As String = "Server=MIRAVBHOJANI;Database=cashsales;Integrated Security=True;"
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "UPDATE cashcustomer_details SET aof_link = @newaof_link WHERE Name = @customerName"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@newaof_link", newaof_link)
                    command.Parameters.AddWithValue("@customerName", customerName)
                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("Database error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
    Private docTypes As List(Of String)
    Private Sub UpdateDocumentLink(customerName As String, newLink As String, linkColumnName As String)
        Dim connectionString As String = "Server=MIRAVBHOJANI;Database=cashsales;Integrated Security=True;"
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = $"UPDATE cashcustomer_details SET {linkColumnName} = @newLink WHERE Name = @customerName"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@newLink", newLink)
                    command.Parameters.AddWithValue("@customerName", customerName)
                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("Database error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
    Private Sub InitializeTypeOfDocComboBox()
        docTypes = New List(Of String) From {
        "Account opening form",
        "Contracts",
"Reference"
}

        For Each docType As String In docTypes
            typeofdoc.Items.Add(docType)
        Next
    End Sub
    Private Sub TypeOfDoc_TextChanged(sender As Object, e As EventArgs) Handles typeofdoc.TextChanged
        If typeofdoc.DroppedDown AndAlso (typeofdoc.SelectedIndex >= 0) Then
            Return
        End If

        Dim enteredText As String = typeofdoc.Text
        typeofdoc.Items.Clear()

        If Not String.IsNullOrEmpty(enteredText) Then
            For Each docType As String In docTypes
                If docType.StartsWith(enteredText, StringComparison.CurrentCultureIgnoreCase) Then
                    typeofdoc.Items.Add(docType)
                End If
            Next
        Else
            For Each docType As String In docTypes
                typeofdoc.Items.Add(docType)
            Next
        End If

        typeofdoc.SelectionStart = enteredText.Length
        typeofdoc.SelectionLength = 0
        typeofdoc.DroppedDown = True
    End Sub

    Private Sub TypeOfDoc_KeyDown(sender As Object, e As KeyEventArgs) Handles typeofdoc.KeyDown
        If e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up Then
            If Not typeofdoc.DroppedDown Then
                typeofdoc.DroppedDown = True
            End If
        End If
    End Sub
    Private Function GetTotalRowCount() As Integer
        Dim totalCount As Integer = 0
        Dim connectionString As String = "Server=MIRAVBHOJANI;Database=cashsales;Integrated Security=True;"
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT COUNT(*) FROM cashcustomer_details"
                Using command As New SqlCommand(query, connection)
                    totalCount = Convert.ToInt32(command.ExecuteScalar())
                End Using
            Catch ex As Exception
                MessageBox.Show("Database error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
        Return totalCount
    End Function

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles allrb.CheckedChanged

    End Sub

    Private Sub unavailabledocrb_CheckedChanged(sender As Object, e As EventArgs) Handles unavailabledocrb.CheckedChanged

    End Sub
    Private Sub CenterRowCountLabel()
        ' Disable AutoSize to allow manual resizing
        rowcountlabel.AutoSize = False

        ' Match the width of rowcountlabel to the width of rowsbox
        ' and set a fixed height (adjust the height as needed)
        rowcountlabel.Width = rowsbox.Width
        rowcountlabel.Height = 30 ' Adjust the height as needed

        ' Set the Text Alignment to Middle Center
        rowcountlabel.TextAlign = ContentAlignment.MiddleCenter

        ' Calculate the position to center the label in the rowsbox
        rowcountlabel.Location = New Point(
            (rowsbox.Width - rowcountlabel.Width) \ 2,
            (rowsbox.Height - rowcountlabel.Height) \ 2)
    End Sub


End Class
