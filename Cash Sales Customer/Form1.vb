Imports MySql.Data.MySqlClient

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
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                connection.Close()
            End Try
        End Using

        isFormLoaded = True
        CenterButtonHorizontally(Upload)
        CenterButtonHorizontally(View)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If isFormLoaded Then
            CenterButtonHorizontally(Upload)
            CenterButtonHorizontally(View)
        End If
    End Sub

    Private Sub CenterButtonHorizontally(btn As Button)
        If btn IsNot Nothing Then
            btn.Left = (Me.ClientSize.Width - btn.Width) / 2
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
