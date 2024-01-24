Imports MySql.Data.MySqlClient
Imports System.Diagnostics
Imports System.IO

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
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
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
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
                            LoadData()
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
        Dim connectionString As String = "server=127.0.0.1;userid=root;password=;database=cashsales"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "UPDATE cashcustomer_details SET Link = @newLink WHERE Name = @customerName"
                Using command As New MySqlCommand(query, connection)
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
