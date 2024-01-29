Public Class Form_Peminjaman_2110017
    Dim kode, kodebaru, tgl, tgc, kd As String
        Dim va As Integer
        Sub buattombol()
            'tambahkan button edit
            Dim cedit As New DataGridViewButtonColumn
            cedit.Name = "cedit"
            cedit.HeaderText = ""
            cedit.FlatStyle = FlatStyle.Popup
            cedit.DefaultCellStyle.ForeColor = Color.DarkGreen
        cedit.Text = "Confirm"
            cedit.Width = 60
            cedit.UseColumnTextForButtonValue = True
            dg.Columns.Add(cedit)
    End Sub
    Sub setdg()
        dg.Columns(0).HeaderText = "Kode Booking"
        dg.Columns(1).HeaderText = "Tanggal Booking"
        dg.Columns(2).HeaderText = "Nama Anggota"
        dg.Columns(3).HeaderText = "Jurusan"
        dg.Columns(4).HeaderText = "Kode Buku"
        dg.Columns(5).HeaderText = "Judul Buku"
        dg.Columns(6).HeaderText = "Penerbit"
        dg.Columns(7).HeaderText = "Tahun Terbit"
        dg.Columns(8).Visible = False

        dg.Columns(0).Width = 200
        dg.Columns(1).Width = 300
        dg.Columns(2).Width = 200
        dg.Columns(3).Width = 100
        dg.Columns(4).Width = 200
        dg.Columns(5).Width = 100
        dg.Columns(6).Width = 200
        dg.Columns(7).Width = 100
        dg.Columns(8).Width = 0
    End Sub
        Sub buatnomor()
            Dim no As Integer
            tgl = Format(Now, "MM/yyyy")
            kon.Close()
            kon.Open()
            perintah.Connection = kon
            perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from peminjaman2110017 order by kdpinjam2110017 desc limit 1"
            cek = perintah.ExecuteReader()
            cek.Read()
            If cek.HasRows Then
            kode = cek.Item("kdpinjam2110017")
                no = Val(Microsoft.VisualBasic.Right(kode, 3))
                no = no + 1
            kodebaru = "PJM-" + tgl + "-" + Format(no, "000")
            Else
            kodebaru = "PJM-" + tgl + "-" + "001"
            End If
            kon.Close()
        End Sub
        Private Sub tcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcari.TextChanged
           dg.Columns.Clear()
        Call tampil("select kdbooking2110017,tglbooking2110017,nama_anggota2110017,jurusan2110017,idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,id2110017 from booking2110017 join buku2110017 on idbuku2110017=kdbuku2110017 join anggota2110017 on kdanggota2110017=idanggota2110017 where kdbooking2110017 like '%" & tcari.Text & "%' or judul_buku2110017 like '%" & tcari.Text & "%' or penerbit2110017 like'%" & tcari.Text & "%'")
                dg.DataSource = (ds.Tables("data"))
            Call setdg()
            Call buattombol()
        End Sub

       

   

    Private Sub dg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dg.CellContentClick
        Dim tanggal As Date = Date.Now.ToString("yyyy-MM-dd")
        Dim jumlah As Integer = 3
        Dim tanggalbaru As Date = tanggal.AddDays(jumlah)
        tgl = Date.Now.ToString("yyyy-MM-dd")
        i = dg.CurrentRow.Index
        id = dg.Rows.Item(i).Cells(0).Value
        va = dg.Rows.Item(i).Cells(8).Value
        
        If e.ColumnIndex = 9 Then
            Dim x As Byte
            x = MsgBox("Pinjam Buku dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "insert into peminjaman2110017 (kdpinjam2110017, tglpinjam2110017,idbooking2110017, jmlpinj2110017,lamapinjam2110017, tglharuskembali2110017, stat2110017, admininput2110017) values ('" & kodebaru & "','" & tanggal.ToString("yyyy-MM-dd") & "','" & id & "','" & 1 & "','" & jumlah & "','" & tanggalbaru.ToString("yyyy-MM-dd") & "','" & 0 & "','" & adminnama & "')"
                perintah.ExecuteNonQuery()
                kon.Close()
                MsgBox("Sukses")
                dg.Columns.Clear()
                Call proses("update booking2110017 set status2110017= 0 where id2110017 = '" & va & "'")
                Call tampil("select kdbooking2110017,tglbooking2110017,nama_anggota2110017,jurusan2110017,idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,id2110017 from booking2110017 join buku2110017 on idbuku2110017=kdbuku2110017 join anggota2110017 on kdanggota2110017=idanggota2110017 where status2110017= 1")
                dg.DataSource = (ds.Tables("data"))
                Call setdg()
                Call buattombol()
            End If
        End If
    End Sub

    Private Sub Form_Peminjaman_2110017_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Call buatnomor()
        dg.Columns.Clear()
        Call tampil("select kdbooking2110017,tglbooking2110017,nama_anggota2110017,jurusan2110017,idbuku2110017,judul_buku2110017,penerbit2110017,thn_terbit2110017,id2110017 from booking2110017 join buku2110017 on idbuku2110017=kdbuku2110017 join anggota2110017 on kdanggota2110017=idanggota2110017 where status2110017= 1")
        dg.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()
    End Sub

   
End Class