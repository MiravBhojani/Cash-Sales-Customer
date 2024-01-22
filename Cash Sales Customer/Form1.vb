Imports MySql.Data.MySqlClient

Public Class Form1
    ' Form Load Event
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Database Connection String
        Dim connectionString As String = "server=127.0.0.1;userid=root;password=;database=cashsalescustomers"
        Using connection As New MySqlConnection(connectionString)
            Try
                ' Open Connection
                connection.Open()

                ' SQL Query to Fetch Data
                Dim query As String = "SELECT * FROM cashcustomer_details"

                ' Data Adapter with Query and Connection
                Dim da As New MySqlDataAdapter(query, connection)

                ' DataSet to Hold Data
                Dim ds As New DataSet()

                ' Fill DataSet with Data from Database
                da.Fill(ds, "cashcustomer_details")

                ' Set DataGridView DataSource
                DataGridView1.DataSource = ds.Tables("cashcustomer_details")
            Catch ex As Exception
                ' Show any Exception Messages
                MessageBox.Show(ex.Message)
            Finally
                ' Close Connection
                connection.Close()
            End Try
        End Using
    End Sub

    ' Optional: Other Event Handlers
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Code for handling cell content click events
    End Sub

    ' Optional: Other Event Handlers
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Code for handling label click events
    End Sub

    ' Optional: Other Event Handlers
    Private Sub View_Click(sender As Object, e As EventArgs) Handles View.Click
        ' Code for handling View button click events
    End Sub
End Class
