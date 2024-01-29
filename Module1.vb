Imports MySql.Data.MySqlClient
Module Module1
    Dim strkon As String = "server=localhost;uid=root;database=db_perpustakaan_2110017"
    Public kon As New MySqlConnection(strkon)
    Public perintah As New MySqlCommand
    Public cek As MySqlDataReader
    Public mda As New MySqlDataAdapter
    Public ds As New DataSet
    Public adminlogin, adminnama, id, idd As String
    Public i As Integer
    Public Sub koneksi()
        kon = New MySqlConnection(strkon)
        If kon.State = ConnectionState.Closed Then
            kon.Open()
        Else
            kon.Close()
        End If
    End Sub
    Public Function tampil(ByVal sql As String) 'menampilkan data ke datagrid
        koneksi()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = sql
        perintah.CommandTimeout = 0
        mda.SelectCommand = perintah
        ds.Tables.Clear()
        mda.Fill(ds, "data")
        Return sql
    End Function
    Public Function proses(ByVal sql As String) 'proses simpan, update dan hapus
        Try
            Call koneksi()
            perintah.Connection = kon
            perintah.CommandType = CommandType.Text
            perintah.CommandText = sql
            perintah.CommandTimeout = 0
            perintah.ExecuteNonQuery()
            Return sql
        Catch ex As Exception
        End Try
        Return (sql)
    End Function
End Module
