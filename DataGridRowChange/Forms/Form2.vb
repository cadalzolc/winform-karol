Imports System.Data.SQLite

Public Class Form2

    ReadOnly DBPath As String = Application.StartupPath + "\database\try.db"
    Dim CurrentRowID As Integer = 0

    Private Sub updatetable()

        Dim sqlconn As New SQLiteConnection(String.Format("data source={0}", DBPath))
        sqlconn.Open()

        Dim sqlcmd As New SQLiteCommand With {
            .Connection = sqlconn,
            .CommandText = "select * from try"
        }
        Dim sqlRead As SQLiteDataReader = sqlcmd.ExecuteReader
        Dim sqldt As New DataTable

        sqldt.Load(sqlread)
        sqlread.Close()
        sqlconn.Close()
        DataGridView1.DataSource = sqldt

        DataGridView1.Columns("ID").Visible = False

    End Sub


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updatetable()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ADDbtn.Click
        Dim sqlconn As New SQLiteConnection(String.Format("data source={0}", DBPath))
        sqlconn.Open()
        Dim sqlcmd As New SQLiteCommand
        sqlcmd.Connection = sqlconn

        sqlcmd.CommandText = "insert into try (JOBNUMBER,PART,QUANTITY,DATE)
        VALUES (@Rec1,@Rec2,@Rec3,@Rec5)"

        sqlcmd.Parameters.AddWithValue("@Rec1", txtJOBNUMBER.Text)
        sqlcmd.Parameters.AddWithValue("@Rec2", txtPART.Text)
        sqlcmd.Parameters.AddWithValue("@Rec3", txtQUANTITY.Text)

        sqlcmd.Parameters.AddWithValue("@Rec5", DateTimePicker1.Text)


        sqlcmd.ExecuteNonQuery()
        sqlconn.Close()
        updatetable()


    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Recid As Integer = DataGridView1.SelectedRows(0).Cells("ID").Value

        Dim sqlconn As New SQLiteConnection(String.Format("data source={0}", DBPath))
        sqlconn.Open()
        Dim sqlcmd As New SQLiteCommand
        sqlcmd.Connection = sqlconn

        sqlcmd.CommandText = "DELETE FROM try WHERE ID=@ID"
        sqlcmd.Parameters.AddWithValue("@ID", Recid)

        sqlcmd.ExecuteNonQuery()
        sqlconn.Close()
        updatetable()


    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim FINISH = DataGridView1.Rows(e.RowIndex).Cells("FINISH").Value.ToString()
        If FINISH.Equals("YES") Then
            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        If CurrentRowID.Equals(0) Then
            MessageBox.Show("No row was selected!")
            Return
        End If

        Dim sqlconn As New SQLiteConnection(String.Format("data source={0}", DBPath))

        sqlconn.Open()

        Dim sqlcmd As New SQLiteCommand With {
            .Connection = sqlconn,
            .CommandText = "Update try SET FINISH='YES' WHERE ID=@ID"
        }
        sqlcmd.Parameters.AddWithValue("@ID", CurrentRowID)

        sqlcmd.ExecuteNonQuery()
        sqlconn.Close()
        updatetable()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex < 0 Then
            Return
        End If
        CurrentRowID = DataGridView1.Rows(e.RowIndex).Cells("ID").Value
    End Sub

End Class