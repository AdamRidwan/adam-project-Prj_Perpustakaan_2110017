Public Class Form_Booking_2110017
    Dim kode, kodebaru, tgl As String
    Dim va As Integer
    Sub buattombol()
        'tambahkan button edit
        Dim cedit As New DataGridViewButtonColumn
        cedit.Name = "cedit"
        cedit.HeaderText = ""
        cedit.FlatStyle = FlatStyle.Popup
        cedit.DefaultCellStyle.ForeColor = Color.DarkGreen
        cedit.Text = "Booking"
        cedit.Width = 60
        cedit.UseColumnTextForButtonValue = True
        dg.Columns.Add(cedit)
    End Sub
    Sub setdg()
        dg.Columns(0).HeaderText = "Kode Buku"
        dg.Columns(1).HeaderText = "Judul Buku"
        dg.Columns(2).HeaderText = "Penerbit"
        dg.Columns(3).HeaderText = "Tahun Terbit"
        dg.Columns(4).HeaderText = "Pengarang"
        dg.Columns(5).HeaderText = "Stok"
        dg.Columns(0).Width = 200
        dg.Columns(1).Width = 300
        dg.Columns(2).Width = 200
        dg.Columns(3).Width = 100
        dg.Columns(4).Width = 200
        dg.Columns(5).Width = 100
    End Sub

    Sub ceknama()
        lblnama.Text = adminnama
        kon.Close()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from anggota2110017 where nama_anggota2110017='" & lblnama.Text & "'"
        cek = perintah.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            lblkode.Text = cek.Item("idanggota2110017")
        End If
        kon.Close()
    End Sub
    Sub buatnomor()
        Dim no As Integer
        tgl = Format(Now, "MM/yyyy")
        kon.Close()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from booking2110017 order by kdbooking2110017 desc limit 1"
        cek = perintah.ExecuteReader()
        cek.Read()
        If cek.HasRows Then
            kode = cek.Item("kdbooking2110017")
            no = Val(Microsoft.VisualBasic.Right(kode, 3))
            no = no + 1
            kodebaru = "BOK-" + tgl + "-" + Format(no, "000")
        Else
            kodebaru = "BOK-" + tgl + "-" + "001"
        End If
        kon.Close()
    End Sub
    Private Sub tcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcari.TextChanged
        If tcari.Text = "" Then
            dg.Columns.Clear()
            Call tampil("select idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017 where idbuku2110017 not in (select idbuku2110017 from booking2110017 where kdanggota2110017='" & lblkode.Text & "')")
            dg.DataSource = (ds.Tables("data"))
        Else
            dg.Columns.Clear()
            Call tampil("SELECT idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017 where idbuku2110017 like '%" & tcari.Text & "%' or judul_buku2110017 like '%" & tcari.Text & "%' or penerbit2110017 like'%" & tcari.Text & "%' or pengarang2110017 like '%" & tcari.Text & "%' and idbuku2110017 not in (select idbuku2110017 from booking2110017 where kdanggota2110017='" & lblkode.Text & "')")
            dg.DataSource = (ds.Tables("data"))
        End If
        Call setdg()
        Call buattombol()
    End Sub

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick
        tgl = Date.Now.ToString("yy-MM-dd")
        i = dg.CurrentRow.Index
        id = dg.Rows.Item(i).Cells(0).Value
        va = dg.Rows.Item(i).Cells(5).Value
        If e.ColumnIndex = 6 Then
            Dim x As Byte
            If va <= 0 Then
                x = MsgBox("Buku ini tidak tersedia")
            Else
                x = MsgBox("Booking Buku dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
                If x = vbYes Then
                    kon.Close()
                    kon.Open()
                    perintah.Connection = kon
                    perintah.CommandType = CommandType.Text
                    perintah.CommandText = "insert into booking2110017(kdbooking2110017, tglbooking2110017,kdanggota2110017, kdbuku2110017, jml2110017, status2110017) values ('" & kodebaru & "','" & tgl & "','" & lblkode.Text & "','" & id & "','" & 1 & "','" & 1 & "')"
                    perintah.ExecuteNonQuery()
                    kon.Close()
                    MsgBox("Sukses")
                    dg.Columns.Clear()
                    Call tampil("select idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017 where idbuku2110017 not in (select kdbuku2110017 from booking2110017 where kdanggota2110017='" & lblkode.Text & "')")
                    dg.DataSource = (ds.Tables("data"))
                    Call setdg()
                    Call buattombol()
                End If
            End If
        End If
    End Sub

    Private Sub Form_Booking_2110017_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Call buatnomor()
        Call ceknama()
        dg.Columns.Clear()
        Call tampil("select idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,pengarang2110017,stok2110017 from buku2110017 where idbuku2110017 not in (select kdbuku2110017 from booking2110017 where kdanggota2110017='" & lblkode.Text & "')")
        dg.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()
    End Sub
End Class