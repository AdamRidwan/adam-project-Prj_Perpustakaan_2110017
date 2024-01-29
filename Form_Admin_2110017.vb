Imports MySql.Data.MySqlClient
Public Class Form_Admin_2110017

    Dim level As String
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
        Dim creset As New DataGridViewButtonColumn
        creset.Name = "creset"
        creset.HeaderText = ""
        creset.FlatStyle = FlatStyle.Popup
        creset.DefaultCellStyle.ForeColor = Color.Red
        creset.Text = "Reset Password"
        creset.Width = 100
        creset.UseColumnTextForButtonValue = True
        dgv.Columns.Add(creset)
        'tambahkan button edit
        Dim cblokir As New DataGridViewButtonColumn
        cblokir.Name = "cblokir"
        cblokir.HeaderText = ""
        cblokir.FlatStyle = FlatStyle.Popup
        cblokir.DefaultCellStyle.ForeColor = Color.Red
        cblokir.Text = "Blokir"
        cblokir.Width = 50
        cblokir.UseColumnTextForButtonValue = True
        dgv.Columns.Add(cblokir)
        Dim caktif As New DataGridViewButtonColumn
        caktif.Name = "caktif"
        caktif.HeaderText = ""
        caktif.FlatStyle = FlatStyle.Popup
        caktif.DefaultCellStyle.ForeColor = Color.Red
        caktif.Text = "Aktif"
        caktif.Width = 50
        caktif.UseColumnTextForButtonValue = True
        dgv.Columns.Add(caktif)
    End Sub
    Sub setdg()
        dgv.Columns(0).HeaderText = "Id Admin"
        dgv.Columns(1).HeaderText = "Nama Lengkap"
        dgv.Columns(2).HeaderText = "Level Akses"
        dgv.Columns(0).Width = 130
        dgv.Columns(1).Width = 280
        dgv.Columns(2).Width = 150
    End Sub


    Sub bersih()

        tkdbuku.Text = ""
        tnama.Text = ""
        cmbjurusan.Text = ""

    End Sub

    Sub cekkode()
        kon.Close()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "select * from admin2110017 where adminid2110017='" & tkdbuku.Text & "'"
        cek = perintah.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            tnama.Text = cek.Item("adminnamalengkap2110017")
            level = cek.Item("adminlevel2110017")
            If level = 1 Then

                cmbjurusan.Text = "Admin Utama"
            Else
                cmbjurusan.Text = "Pustakawan"
            End If
        End If
        kon.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tkdbuku.TextChanged
        Call cekkode()
    End Sub

    Private Sub csimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles csimpan.Click
        If cmbjurusan.Text = "Admin Utama" Then
            level = 1
        Else
            level = 2
        End If
        If csimpan.Text = "SIMPAN" Then

            Call proses("INSERT INTO admin2110017 VALUES('" & tkdbuku.Text & "','" & tnama.Text & "',md5('12345'),'" & level & "',1)")
            MsgBox("Password Anggota Baru Adalah : 12345")
        Else
            Call proses("update admin2110017 set adminnamalengkap2110017='" & tnama.Text & "',adminlevel2110017='" & level & "' where adminid2110017='" & tkdbuku.Text & "'")
            csimpan.Text = "SIMPAN"

            tkdbuku.Enabled = True
        End If
        Call bersih()
        dgv.Columns.Clear()
        Call tampil("SELECT adminid2110017,adminnamalengkap2110017,if(adminlevel2110017=1,'Admin Utama','Pustakawan')as level from admin2110017 where adminlevel2110017!=3")
        dgv.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()
        csimpan.Text = "SIMPAN"

    End Sub

    Private Sub tcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tcari.TextChanged
        dgv.Columns.Clear()
        Call tampil("SELECT adminid2110017,adminnamalengkap2110017,if(adminlevel2110017=1,'Admin Utama','Pustakawan')as level from admin2110017 where adminid2110017 like '%" & tcari.Text & "%' or adminnamalengkap2110017 like '%" & tcari.Text & "%' and adminlevel2110017!=3")
        dgv.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()

    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick
        i = dgv.CurrentRow.Index
        id = dgv.Rows.Item(i).Cells(0).Value

        If e.ColumnIndex = 3 Then
            tkdbuku.Text = id
            tkdbuku.Enabled = False
            tnama.Focus()
            csimpan.Text = "UPDATE"
        End If
        If e.ColumnIndex = 4 Then
            Dim x As Byte
            x = MsgBox("Reset Password Admin dengan kode " & id & " ?", MsgBoxStyle.Critical +vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "update admin2110017 set adminpass2110017=md5('1234567') where adminid2110017='" & dgv.Rows.Item(i).Cells(0).Value & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                MsgBox("Password Baru : 1234567")
                dgv.Columns.Clear()
                Call tampil("SELECT adminid2110017,adminnamalengkap2110017,if(adminlevel2110017=1,'Admin Utama','Pustakawan')as level from admin2110017 where adminlevel2110017!=3")
                dgv.DataSource = (ds.Tables("data"))
                Call setdg()
                Call buattombol()
            End If
        End If
        If e.ColumnIndex = 5 Then
            Dim x As Byte
            x = MsgBox("Blokir Admin dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo,"Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "update admin2110017 set adminaktif2110017=2 where adminid2110017='" & dgv.Rows.Item(i).Cells(0).Value & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                dgv.Columns.Clear()
                Call tampil("SELECT adminid2110017,adminnamalengkap2110017,if(adminlevel2110017=1,'Admin Utama','Pustakawan')as level from admin2110017 where adminlevel2110017!=3")
                dgv.DataSource = (ds.Tables("data"))
                Call setdg()
                Call buattombol()
            End If
        End If
        If e.ColumnIndex = 6 Then
            Dim x As Byte
            x = MsgBox("Aktif Admin dengan kode " & id & " ?", MsgBoxStyle.Critical + vbYesNo, "Konfirmasi")
            If x = vbYes Then
                kon.Close()
                kon.Open()
                perintah.Connection = kon
                perintah.CommandType = CommandType.Text
                perintah.CommandText = "update admin2110017 set adminaktif2110017=1 where adminid2110017='" & dgv.Rows.Item(i).Cells(0).Value & "'"
                perintah.ExecuteNonQuery()
                kon.Close()
                dgv.Columns.Clear()
                Call tampil("SELECT adminid2110017,adminnamalengkap2110017,if(adminlevel2110017=1,'Admin Utama','Pustakawan')as level from admin2110017 where adminlevel2110017!=3")
                dgv.DataSource = (ds.Tables("data"))
                Call setdg()
                Call buattombol()
            End If
        End If
    End Sub

    Private Sub Form_Admin_2110017_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        dgv.Columns.Clear()
        Call tampil("SELECT adminid2110017,adminnamalengkap2110017,if(adminlevel2110017=1,'Admin Utama','Pustakawan')as level from admin2110017 where adminlevel2110017!=3")
        dgv.DataSource = (ds.Tables("data"))
        Call setdg()
        Call buattombol()
    End Sub


    Private Sub Form_Admin_2110017_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbjurusan.Items.Add("Admin Utama")
        cmbjurusan.Items.Add("Pustakawan")
    End Sub
End Class