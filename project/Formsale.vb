Public Class Formsale
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Formcustomer.Show()
    End Sub

    Private Sub btnBrowseCustomer_Click(sender As Object, e As EventArgs) Handles btnBrowseCustomer.Click
        Formcustomerlist.Show()
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub
End Class