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

    ' Event Handler for DataGridView Cell Content Click
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' Add your code here to handle DataGridView cell clicks
    End Sub

    ' Event Handler for Label Click
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' Add your code here to handle Label clicks
    End Sub

    ' Event Handler for Upload Button Click
    Private Sub Upload_Click(sender As Object, e As EventArgs) Handles Upload.Click
        ' Add your code here to handle Upload button clicks
    End Sub

    ' Event Handler for View Button Click
    Private Sub View_Click(sender As Object, e As EventArgs) Handles View.Click
        ' Add your code here to handle View button clicks
    End Sub
End Class
