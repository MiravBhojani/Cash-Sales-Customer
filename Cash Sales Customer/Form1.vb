Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerNames()
        ConfigureDataGridView()
        AddHandler customerdropdown.TextChanged, AddressOf CustomerDropdown_TextChanged
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
                While reader.Read()
                    customerdropdown.Items.Add(reader("Name").ToString())
                End While
            Catch ex As Exception
                MessageBox.Show("Database error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub CustomerDropdown_TextChanged(sender As Object, e As EventArgs)
        Dim enteredText As String = customerdropdown.Text
        Dim closestMatchIndex As Integer = -1
        Dim closestMatchLength As Integer = Integer.MaxValue

        For i As Integer = 0 To customerdropdown.Items.Count - 1
            Dim itemText As String = customerdropdown.Items(i).ToString()
            If itemText.StartsWith(enteredText, StringComparison.CurrentCultureIgnoreCase) AndAlso itemText.Length < closestMatchLength Then
                closestMatchIndex = i
                closestMatchLength = itemText.Length
            End If
        Next

        If closestMatchIndex >= 0 Then
            customerdropdown.SelectedIndex = closestMatchIndex
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

                ' Hide the Link column
                If DataGridView1.Columns.Contains("Link") Then
                    DataGridView1.Columns("Link").Visible = False
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
            Dim link As String = selectedRow.Cells("Link").Value.ToString()

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
                MessageBox.Show("No link found for the selected customer.")
            End If
        Else
            MessageBox.Show("Please select a record from the list.")
        End If
    End Sub

    Private Sub uploadBtn_Click(sender As Object, e As EventArgs) Handles uploadbtn.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim link As String = selectedRow.Cells("Link").Value.ToString()

            If String.IsNullOrWhiteSpace(link) Then
                Using ofd As New OpenFileDialog()
                    ofd.Filter = "PDF Files|*.pdf"

                    If ofd.ShowDialog() = DialogResult.OK Then
                        Dim sourceFilePath As String = ofd.FileName
                        Dim fileName As String = Path.GetFileName(sourceFilePath)
                        Dim destinationFilePath As String = "C:\Users\HP\Desktop\Cash Sale Customer Form\" & fileName

                        Try
                            File.Copy(sourceFilePath, destinationFilePath, True)
                            UpdateLink(selectedRow.Cells("Name").Value.ToString(), destinationFilePath)
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
            MessageBox.Show("Please select a record from the list.")
        End If
    End Sub

    Private Sub UpdateLink(customerName As String, newLink As String)
        Dim connectionString As String = "Server=MIRAVBHOJANI;Database=cashsales;Integrated Security=True;"
        Using connection As New SqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "UPDATE cashcustomer_details SET Link = @newLink WHERE Name = @customerName"
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
End Class
