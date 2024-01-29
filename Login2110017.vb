Public Class Flogin_2110017

    Private Sub Flogin_2110017_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tusername.Focus()
    End Sub
   

    Private Sub tusername_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tusername.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                tpass.Focus()
        End Select
    End Sub

    Private Sub tpass_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tpass.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                btlogin_Click(e, AcceptButton)
        End Select
    End Sub


    Private Sub btlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btlogin.Click
        kon.Close()
        kon.Open()
        perintah.Connection = kon
        perintah.CommandType = CommandType.Text
        perintah.CommandText = "SELECT * FROM admin2110017 WHERE adminid2110017='" & tusername.Text & "' AND adminpass2110017 = MD5('" & tpass.Text & "') AND adminaktif2110017=1"
        cek = perintah.ExecuteReader
        cek.Read()
        If cek.HasRows Then
            Dim leveladmin As String
            leveladmin = cek.Item("adminlevel2110017")
            If leveladmin = 2 Then ' jika yg login adalah Pustakawan
                FMenu_2110017.ADMINToolStripMenuItem.Visible = False
            ElseIf leveladmin = 3 Then ' jika yg login adalah Anggota
                FMenu_2110017.MasterToolStripMenuItem.Visible = False
                FMenu_2110017.TransaksiToolStripMenuItem.Visible = False
                FMenu_2110017.LAPORANToolStripMenuItem.Visible = False
                FMenu_2110017.BookingToolStripMenuItem.Visible = True
            End If
            FMenu_2110017.Show()
            adminlogin = cek.Item("adminid2110017")
            adminnama = cek.Item("adminnamalengkap2110017")
            FMenu_2110017.admin2110017.Text = adminnama
            Me.Hide()
        Else
            MsgBox("Id atau password salah", MsgBoxStyle.Information,"Informasi")
        End If
        kon.Close()
    End Sub

    Private Sub bklik_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bklik.Click
        Call Form_Admin_2110017.Show()
    End Sub
End Class
