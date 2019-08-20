Imports MySql.Data.MySqlClient

Public Class login

    Public comm As SqlConnection

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        comm = New SqlConnection()
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        Dim userLogin As User = comm.login(txtUserName.Text, txtPassword.Text)
        If userLogin.id = 0 Then
            MessageBox.Show("Username or password wrong")
            Return
        End If
        Dim menu As Formmenu = New Formmenu(userLogin)
        menu.Show()
    End Sub
End Class
