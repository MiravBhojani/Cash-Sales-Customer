Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class Form1
    Private customerNames As List(Of String)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        customerNames = New List(Of String)()
        LoadCustomerNames()
        ConfigureDataGridView()
        ' AddHandler customerdropdown.TextChanged, AddressOf CustomerDropdown_TextChanged
        InitializeTypeOfDocComboBox()
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
                Dim query As String = "SELECT * FROM cashcustomer_details WHERE Name = @customerName"
                Dim adapter As New SqlDataAdapter(query, connection)
                adapter.SelectCommand.Parameters.AddWithValue("@customerName", customerName)
                Dim ds As New DataSet()
                adapter.Fill(ds, "cashcustomer_details")
                DataGridView1.DataSource = ds.Tables("cashcustomer_details")

                If DataGridView1.Columns.Contains("aof_link") Then
                    DataGridView1.Columns("aof_link").Visible = False
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub ConfigureDataGridView()
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.DataSource = Nothing
    End Sub

    Private Sub LoadButton_Click(sender As Object, e As EventArgs) Handles Load.Click
        If customerdropdown.Text <> String.Empty Then
            LoadDataByName(customerdropdown.Text)
        Else
            MessageBox.Show("Please select a customer name.")
        End If
    End Sub

    Private Sub viewBtn_Click(sender As Object, e As EventArgs) Handles viewbtn.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim aof_link As String = selectedRow.Cells("aof_link").Value.ToString()

            If Not String.IsNullOrWhiteSpace(aof_link) Then
                If File.Exists(aof_link) Then
                    Try
                        Process.Start(New ProcessStartInfo(aof_link) With {.UseShellExecute = True})
                    Catch ex As Exception
                        MessageBox.Show("Error opening file: " & ex.Message)
                    End Try
                Else
                    MessageBox.Show("PDF file not found.")
                End If
            Else
                MessageBox.Show("No Link found for the selected customer.")
            End If
        Else
            MessageBox.Show("Please select a record from the list.")
        End If
    End Sub

    Private Sub uploadBtn_Click(sender As Object, e As EventArgs) Handles uploadbtn.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim aof_link As String = selectedRow.Cells("aof_link").Value.ToString()

            If String.IsNullOrWhiteSpace(aof_link) Then
                Using ofd As New OpenFileDialog()
                    ofd.Filter = "PDF Files|*.pdf"

                    If ofd.ShowDialog() = DialogResult.OK Then
                        Dim sourceFilePath As String = ofd.FileName
                        Dim fileName As String = Path.GetFileName(sourceFilePath)
                        Dim destinationFilePath As String = "C:\Users\HP\Desktop\Cash Sale Customer Form\" & fileName

                        Try
                            File.Copy(sourceFilePath, destinationFilePath, True)
                            Updateaof_link(selectedRow.Cells("Name").Value.ToString(), destinationFilePath)
                            LoadDataByName(customerdropdown.Text)
                        Catch ex As Exception
                            MessageBox.Show("Error copying file: " & ex.Message)
                        End Try
                    End If
                End Using
            Else
                MessageBox.Show("A Link already exists for this record.")
            End If
        Else
            MessageBox.Show("Please select a record from the list.")
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

End Class
