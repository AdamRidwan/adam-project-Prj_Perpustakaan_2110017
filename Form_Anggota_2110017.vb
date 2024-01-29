Public Class Form_Anggota_2110017
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

        Dim creset As New DataGridViewButtonColumn
        creset.Name = "creset"
        creset.HeaderText = ""
        creset.FlatStyle = FlatStyle.Popup
        creset.DefaultCellStyle.ForeColor = Color.Red
        creset.Text = "Reset Password"
        creset.Width = 120
        creset.UseColumnTextForButtonValue = True
        dgv.Columns.Add(creset)

        Dim cblokir As New DataGridViewButtonColumn
        cblokir.Name = "cblokir"
        cblokir.HeaderText = ""
        cblokir.FlatStyle = FlatStyle.Popup
        cblokir.DefaultCellStyle.ForeColor = Color.Red
        cblokir.Text = "Blokir"
        cblokir.Width = 50
        cblokir.UseColumnTextForButtonValue = True
        dgv.Columns.Add(cblokir)

        Dim caktifkan As New DataGridViewButtonColumn
        caktifkan.Name = "caktifkan"
        caktifkan.HeaderText = ""
        caktifkan.FlatStyle = FlatStyle.Popup
        caktifkan.DefaultCellStyle.ForeColor = Color.Red
        caktifkan.Text = "Aktifkan"
        caktifkan.Width = 80
        caktifkan.UseColumnTextForButtonValue = True
        dgv.Columns.Add(caktifkan)
    End Sub
    Sub setdgv()
        dgv.Columns(0).HeaderText = "Kode Anggota"
        dgv.Columns(1).HeaderText = "Nama Anggota"
        dgv.Columns(2).HeaderText = "Jurusan"
        dgv.Columns(3).HeaderText = "Alamat"
        dgv.Columns(4).HeaderText = "No Telpon"

        dgv.Columns(0).Width = 130
        dgv.Columns(1).Width = 180
        dgv.Columns(2).Width = 180
        dgv.Columns(3).Width = 170
        dgv.Columns(4).Width = 100
    End Sub

    Sub bersih()
        tkdanggota.Text = ""
        tnama.Text = ""
        cmbjurusan.Text = ""
        tnotelp.Text = ""
        talamat.Text = ""
    End Sub
    Sub cekkode()
        kon.Close()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from anggota2110017 where idanggota2110017='" & tkdanggota.Text & "'"
        cek = perintah.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            tnama.Text = cek.Item("nama_anggota2110017")
            cmbjurusan.Text = cek.Item("jurusan2110017")
            talamat.Text = cek.Item("alamat2110017")
            tnotelp.Text = cek.Item("notlp2110017")

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
        perintah.CommandText = "select * from anggota2110017 " & _
            " order by idanggota2110017 desc limit 1"
        cek = perintah.ExecuteReader()
        cek.Read()
        If cek.HasRows Then
            kode = cek.Item("idanggota2110017")
            no = Val(Microsoft.VisualBasic.Right(kode, 3))
            no = no + 1
            kodebaru = "ANG-" + tgl + "-" + Format(no, "000")
            tkdanggota.Text = kodebaru
        Else
            tkdanggota.Text = "ANG-" + tgl + "-" + "001"
        End If
        kon.Close()
    End Sub


        Private Sub ckeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckeluar.Click
            Me.Close()
        End Sub

        Private Sub btbuatnomor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btbuatnomor.Click
            Call buatnomor()
    End Sub

    Private Sub Form_Anggota_2110017_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dgv.Columns.Clear()
        Call tampil("SELECT idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017 from anggota2110017")
        dgv.DataSource = (ds.Tables("data"))
        Call setdgv()
        Call buattombol()
    End Sub

    Private Sub Form_Anggota_2110017_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbjurusan.Items.Clear()
        cmbjurusan.Items.Add("Sistem Informasi")
        cmbjurusan.Items.Add("Sistem Komputer")
        cmbjurusan.Items.Add("Manajemen Informatika")
        
    End Sub


    Private Sub csimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles csimpan.Click
        If csimpan.Text = "SIMPAN" Then
            Call proses("insert into anggota2110017(idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017,admininput2110017) values('" & tkdanggota.Text & "', '" & tnama.Text & "','" & cmbjurusan.Text & "','" & talamat.Text & "', '" & tnotelp.Text & "', '" & adminnama & "')")

            Call proses("insert into admin2110017 values('" & tkdanggota.Text & "', '" & tnama.Text & "', md5('12345') ,3 ,1)")
            MsgBox("Data Sukses Tersimpan", MsgBoxStyle.Information, "Informasi")
        Else
            Call proses("update anggota2110017 set nama_anggota2110017='" & tnama.Text & "', jurusan2110017='" & cmbjurusan.Text & "', alamat2110017='" & talamat.Text & "', notlp2110017='" & tnotelp.Text & "' where idanggota2110017='" & tkdanggota.Text & "'")
            csimpan.Text = "SIMPAN"
        End If
        Call bersih()
        dgv.Columns.Clear()
        Call tampil("SELECT idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017 from anggota2110017")
        dgv.DataSource = (ds.Tables("data"))
        Call setdgv()
        Call buattombol()
        csimpan.Text = "SIMPAN"
    End Sub

    Private Sub tkdanggota_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tkdanggota.TextChanged
        Call cekkode()
    End Sub

    Private Sub tcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcari.TextChanged
        dgv.Columns.Clear()
        Call tampil("SELECT idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017 from anggota2110017 where idanggota2110017 like '%" & tcari.Text & "%' or nama_anggota2110017 like '%" & tcari.Text & "%'")
        dgv.DataSource = (ds.Tables("data"))
        Call setdgv()
        Call buattombol()
    End Sub

   
    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        i = dgv.CurrentRow.Index
        id = dgv.Rows.Item(i).Cells(0).Value
        If e.ColumnIndex = 5 Then
            tkdanggota.Text = id
            tkdanggota.Enabled = False
            tnama.Focus()
            csimpan.Text = "UPDATE"
        End If

        If e.ColumnIndex = 6 Then
            Dim x As Byte
            x = MsgBox("Hapus Data Anggota dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "DELETE FROM anggota2110017, admin2110017 USING anggota2110017, admin2110017 WHERE(anggota2110017.idanggota2110017 = admin2110017.adminid2110017) AND anggota2110017.idanggota2110017 = '" & id & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                dgv.Columns.Clear()
                Call tampil("SELECT idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017 from anggota2110017")
                dgv.DataSource = (ds.Tables("data"))
                Call setdgv()
                Call buattombol()
            End If
        End If
        If e.ColumnIndex = 7 Then
            Dim x As Byte
            x = MsgBox("Reset Password Anggota dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "UPDATE admin2110017 set adminpass2110017=md5('12345') WHERE adminid2110017='" & dgv.Rows.Item(i).Cells(0).Value & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                MsgBox("Password Baru : 12345")
                dgv.Columns.Clear()
                Call tampil("SELECT idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017 from anggota2110017")
                dgv.DataSource = (ds.Tables("data"))
                Call setdgv()
                Call buattombol()
            End If
        End If
        If e.ColumnIndex = 8 Then
            Dim x As Byte
            x = MsgBox("Blokir Anggota dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "update admin2110017 set adminaktif2110017=0 where adminid2110017='" &
                    dgv.Rows.Item(i).Cells(0).Value & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                dgv.Columns.Clear()
                Call tampil("SELECT idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017 from anggota2110017")
                dgv.DataSource = (ds.Tables("data"))
                Call setdgv()
                Call buattombol()
            End If
        End If
        If e.ColumnIndex = 9 Then
            Dim x As Byte
            x = MsgBox("Aktifkan Anggota dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "update admin2110017 set adminaktif2110017=1 where adminid2110017='" &
                    dgv.Rows.Item(i).Cells(0).Value & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                dgv.Columns.Clear()
                Call tampil("SELECT idanggota2110017,nama_anggota2110017,jurusan2110017,alamat2110017,notlp2110017 from anggota2110017")
                dgv.DataSource = (ds.Tables("data"))
                Call setdgv()
                Call buattombol()
            End If
        End If
    End Sub
End Class