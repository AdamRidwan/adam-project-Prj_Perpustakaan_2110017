Public Class FMenu_2110017

    Private Sub KELUARToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KELUARToolStripMenuItem.Click
        Flogin_2110017.Show()
        Me.Close()
    End Sub

    Private Sub ADMINToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ADMINToolStripMenuItem.Click
        Form_Admin_2110017.MdiParent = Me
        Form_Admin_2110017.Show()
        Form_Admin_2110017.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BUKUToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BUKUToolStripMenuItem.Click
        Form_Buku_2110017.MdiParent = Me
        Form_Buku_2110017.Show()
        Form_Buku_2110017.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ANGGOTAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ANGGOTAToolStripMenuItem.Click
        Form_Anggota_2110017.MdiParent = Me
        Form_Anggota_2110017.Show()
        Form_Anggota_2110017.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub PEMINJAMANToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PEMINJAMANToolStripMenuItem.Click
        Form_Peminjaman_2110017.MdiParent = Me
        Form_Peminjaman_2110017.Show()
        Form_Peminjaman_2110017.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub BookingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookingToolStripMenuItem.Click
        Form_Booking_2110017.MdiParent = Me
        Form_Booking_2110017.Show()
        Form_Booking_2110017.WindowState = FormWindowState.Maximized
    End Sub
End Class