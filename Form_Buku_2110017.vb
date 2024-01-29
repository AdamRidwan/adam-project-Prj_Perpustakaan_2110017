Public Class Form_Buku_2110017
    Sub buattombol()
        'tambahkan button edit
        Dim cedit As New DataGridViewButtonColumn
        cedit.Name = "cedit"
        cedit.HeaderText = ""
        cedit.FlatStyle = FlatStyle.Popup
        cedit.DefaultCellStyle.ForeColor = Color.DarkGreen
        cedit.Text = "Edit"
        cedit.Width = 50
        cedit.UseColumnTextForButtonValue = True
        dgv.Columns.Add(cedit)
        'tambahkan button edit
        Dim chapus As New DataGridViewButtonColumn
        chapus.Name = "chapus"
        chapus.HeaderText = ""
        chapus.FlatStyle = FlatStyle.Popup
        chapus.DefaultCellStyle.ForeColor = Color.Red
        chapus.Text = "Hapus"
        chapus.Width = 50
        chapus.UseColumnTextForButtonValue = True
        dgv.Columns.Add(chapus)
    End Sub
    Sub setdg()
        dgv.Columns(0).HeaderText = "Kode Buku"
        dgv.Columns(1).HeaderText = "Judul Buku"
        dgv.Columns(2).HeaderText = "Penerbit"
        dgv.Columns(3).HeaderText = "Tahun Terbit"
        dgv.Columns(4).HeaderText = "Pengarang"
        dgv.Columns(5).HeaderText = "Stok"

        dgv.Columns(0).Width = 130
        dgv.Columns(1).Width = 180
        dgv.Columns(2).Width = 250
        dgv.Columns(3).Width = 100
        dgv.Columns(4).Width = 150
        dgv.Columns(5).Width = 100
    End Sub
    Sub bersih()
        tkdbuku.Text = ""
        tjudul.Text = ""
        tpengarang.Text = ""
        tpenerbit.Text = ""
        tstok.Text = ""
        thterbit.Text = ""
    End Sub
    Sub cekkode()
        kon.Close()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from buku2110017 where idbuku2110017='" & tkdbuku.Text & "'"
        cek = perintah.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            tjudul.Text = cek.Item("judul_buku2110017")
            tpengarang.Text = cek.Item("penerbit2110017")
            thterbit.Text = cek.Item("thn_terbit2110017")
            tpenerbit.Text = cek.Item("pengarang2110017")
            tstok.Text = cek.Item("stok2110017")
        End If
        kon.Close()
    End Sub
    Sub buatnomor()
        Dim kode, kodebaru, tgl As String
        Dim no As Integer
        tgl = Format(Now, "MM/yyyy")
        kon.Close()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from buku2110017 " & " order by idbuku2110017 desc limit 1"
        cek = perintah.ExecuteReader()
        cek.Read()
        If cek.HasRows Then
            kode = cek.Item("idbuku2110017")
            no = Val(Microsoft.VisualBasic.Right(kode, 3))
            no = no + 1
            kodebaru = "REG-" + tgl + "-" + Format(no, "000")
            tkdbuku.Text = kodebaru
        Else
            tkdbuku.Text = "REG-" + tgl + "-" + "001"
        End If
        kon.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Form_Buku_2110017_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dgv.Columns.Clear()
        Call tampil("SELECT idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017")
        dgv.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()
    End Sub

    Private Sub tcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcari.TextChanged
        dgv.Columns.Clear()
        Call tampil("SELECT idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017 where idbuku2110017 like '%" & tcari.Text & "%' or judul_buku2110017 like '%" & tcari.Text & "%' or penerbit2110017 like '%" & tcari.Text & "%' or pengarang2110017 like '%" & tcari.Text & "%'")
        dgv.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()

    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        i = dgv.CurrentRow.Index
        id = dgv.Rows.Item(i).Cells(0).Value
        If e.ColumnIndex = 6 Then
            tkdbuku.Text = id
            tkdbuku.Enabled = False
            tjudul.Focus()
            csimpan.Text = "UPDATE"
        End If
        If e.ColumnIndex = 7 Then
            Dim x As Byte
            x = MsgBox("Hapus Data Buku dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "delete from buku2110017 where idbuku2110017='" & id & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                dgv.Columns.Clear()
                Call tampil("SELECT idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017")
                dgv.DataSource = (ds.Tables("data"))
                Call setdg()
                Call buattombol()
            End If
        End If
    End Sub

    Private Sub csimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles csimpan.Click
        If csimpan.Text = "SIMPAN" Then
            Call proses("INSERT INTO buku2110017(idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017,admininput2110017) values('" & tkdbuku.Text & "', '" & tjudul.Text & "','" & tpenerbit.Text & "','" & thterbit.Text & "','" & tpengarang.Text & "', '" & tstok.Text & "', '" & adminnama & "')")
            MsgBox("Data Sukses Tersimpan", MsgBoxStyle.Information, "Informasi")
            Call bersih()
        Else
            Call proses("update buku2110017 set judul_buku2110017='" & tjudul.Text & "', penerbit2110017='" &
                        tpenerbit.Text & "', thn_terbit2110017='" & thterbit.Text & "'," & _
                        " pengarang2110017='" & tpengarang.Text & "', stok2110017='" & tstok.Text & "' where idbuku2110017='" & tkdbuku.Text & "'")
            csimpan.Text = "SIMPAN"

        End If
        Call bersih()
        dgv.Columns.Clear()
        Call tampil("SELECT idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017")
        dgv.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()
        csimpan.Text = "SIMPAN"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call buatnomor()
    End Sub

    Private Sub tkdbuku_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tkdbuku.TextChanged
        Call cekkode()
    End Sub
End Class