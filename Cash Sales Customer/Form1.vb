Imports MySql.Data.MySqlClient
Imports System.Diagnostics

Public Class Form1
    Private isFormLoaded As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connectionString As String = "server=127.0.0.1;userid=root;password=;database=cashsales"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT * FROM cashcustomer_details"
                Dim da As New MySqlDataAdapter(query, connection)
                Dim ds As New DataSet()
                da.Fill(ds, "cashcustomer_details")
                DataGridView1.DataSource = ds.Tables("cashcustomer_details")

                ' Hide the Link column if it exists
                If DataGridView1.Columns.Contains("Link") Then
                    DataGridView1.Columns("Link").Visible = False
                End If

                ' Set AutoSizeColumnsMode to Fill
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        isFormLoaded = True
    End Sub

    Private Sub viewBtn_Click(sender As Object, e As EventArgs) Handles viewbtn.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim link As String = selectedRow.Cells("Link").Value.ToString()

            If Not String.IsNullOrWhiteSpace(link) Then
                Try
                    Process.Start(New ProcessStartInfo(link) With {.UseShellExecute = True})
                Catch ex As Exception
                    MessageBox.Show("Error opening file: " & ex.Message)
                End Try
            Else
                MessageBox.Show("PDF link not found for the selected customer.")
            End If
        Else
            MessageBox.Show("Please select a customer from the list.")
        End If
    End Sub

    ' ... Other methods or event handlers can be added here ...

End Class
