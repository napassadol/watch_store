Imports System.IO

Public Class Formproduct

    Public comm As SqlConnection
    Public currentProduct As Product

    Public Sub New()
        InitializeComponent()
        comm = New SqlConnection()
    End Sub

    Private Function mapData() As Product
        Dim product As Product = New Product()
        Dim despath As String = txtProductID.Text + "." + OpenFileDialog1.SafeFileName.Split(".").Last()
        My.Computer.FileSystem.CopyFile(PictureBox1.ImageLocation, comm.getImageProductDir() + despath, True)
        product.image = despath
        product.serial = txtProductID.Text
        product.price = Convert.ToDouble(txtCostPrice.Text)
        product.name = txtProductName.Text
        product.amount = Convert.ToInt32(txtStock.Text)
        product.brand = cboCategory.Text
        product.detail = txtDetail.Text
        If radSale.Checked = True Then
            product.status = "sale"
        End If
        If radNotSale.Checked = True Then
            product.status = "notsale"
        End If
        Return product
    End Function

    Private Function remapData(product As Product)
        txtProductID.Text = product.serial
        txtCostPrice.Text = product.price.ToString()
        txtProductName.Text = product.name
        txtStock.Text = product.amount.ToString()
        cboCategory.Text = product.brand
        txtDetail.Text = product.detail
        If product.status = "sale" Then
            radSale.Checked = True
            radNotSale.Checked = False
        Else
            radSale.Checked = False
            radNotSale.Checked = True
        End If
        PictureBox1.ImageLocation = comm.getImageProductDir + product.image
    End Function

    Private Sub BtnBrowsePicture_Click(sender As Object, e As EventArgs) Handles btnBrowsePicture.Click
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            Dim path As String = OpenFileDialog1.FileName
            PictureBox1.ImageLocation = path
        End If
        Dim despath As String = My.Computer.FileSystem.CurrentDirectory
    End Sub

    Private Sub TsAdd_Click(sender As Object, e As EventArgs) Handles tsAdd.Click
        comm.addProduct(mapData())
        MessageBox.Show("Add product success")
    End Sub

    Private Sub Tssearch_Click(sender As Object, e As EventArgs) Handles tssearch.Click
        Dim product As Product
        If ToolStripComboBox1.Text = "รหัสสินค้า" Then
            product = comm.searchProduct(ToolStripTextBox1.Text, "")
        ElseIf ToolStripComboBox1.Text = "ชื่อสินค้า" Then
            product = comm.searchProduct("", ToolStripTextBox1.Text)
        End If
        remapData(product)
        currentProduct = product
    End Sub

    Private Sub TsSave_Click(sender As Object, e As EventArgs) Handles tsSave.Click
    End Sub

    Private Sub TsEdit_Click(sender As Object, e As EventArgs) Handles tsEdit.Click
        Dim product As Product = mapData()
        product.id = currentProduct.id
        If comm.updateProduct(product) = True Then
            MessageBox.Show("Edit product success")
        Else
            MessageBox.Show("Edit product fail")
        End If
    End Sub

    Private Sub TsCancel_Click(sender As Object, e As EventArgs) Handles tsCancel.Click
        remapData(currentProduct)
    End Sub
End Class