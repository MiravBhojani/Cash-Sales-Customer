Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Diagnostics
Imports System.Windows.Forms

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub LoadData()
        Dim connectionString As String = "server=127.0.0.1;userid=root;password=;database=cashsales"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT * FROM cashcustomer_details"
                Dim da As New MySqlDataAdapter(query, connection)
                Dim ds As New DataSet()
                da.Fill(ds, "cashcustomer_details")

                DataGridView1.DataSource = ds.Tables("cashcustomer_details")
                DataGridView1.Columns("Link").Visible = False
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub viewBtn_Click(sender As Object, e As EventArgs) Handles viewbtn.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim link As String = DataGridView1.SelectedRows(0).Cells("Link").Value.ToString()
            If Not String.IsNullOrEmpty(link) AndAlso File.Exists(link) Then
                Process.Start(New ProcessStartInfo(link) With {.UseShellExecute = True})
            Else
                MessageBox.Show("Pdf file of the selected customer not found.")
            End If
        Else
            MessageBox.Show("Please select a row first.")
        End If
    End Sub

    Private Sub uploadBtn_Click(sender As Object, e As EventArgs) Handles uploadbtn.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Dim filePath As String = openFileDialog.FileName
                Dim customerName As String = DataGridView1.SelectedRows(0).Cells("Name").Value.ToString()
                UpdateDatabaseWithFilePath(customerName, filePath)
                LoadData()
            End If
        Else
            MessageBox.Show("Please select a customer from the list.")
        End If
    End Sub

    Private Sub UpdateDatabaseWithFilePath(customerName As String, filePath As String)
        Dim connectionString As String = "server=127.0.0.1;userid=root;password=;database=cashsales"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "UPDATE cashcustomer_details SET Link = @filePath WHERE Name = @customerName"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@filePath", filePath)
                    command.Parameters.AddWithValue("@customerName", customerName)
                    command.ExecuteNonQuery()
                End Using
                MessageBox.Show("File path updated successfully.")
            Catch ex As Exception
                MessageBox.Show("Database error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
End Class
