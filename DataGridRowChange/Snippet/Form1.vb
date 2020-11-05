Public Class Form1

    Dim CurrentRow As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetGrid()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        DataGridView1.Rows(CurrentRow).DefaultCellStyle.BackColor = Color.Black

    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim Gender = DataGridView1.Rows(e.RowIndex).Cells("Gender").Value.ToString()
        If Gender.Equals("Female") Then
            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Red
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        CurrentRow = e.RowIndex
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each Row As DataGridViewRow In DataGridView1.Rows
            Row.DefaultCellStyle.BackColor = Color.White
        Next
        SetGrid()
    End Sub

    Sub SetGrid()
        Dim Persons As New List(Of Person) From
        {
              New Person With {.Name = "John Doe", .Gender = "Male"},
              New Person With {.Name = "Alice Blue", .Gender = "Female"},
              New Person With {.Name = "Mark Twain", .Gender = "Male"},
              New Person With {.Name = "Mike Lewd", .Gender = "Male"}
        }
        DataGridView1.DataSource = Nothing
        DataGridView1.DataSource = Persons
    End Sub

End Class
