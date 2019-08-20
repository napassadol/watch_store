Imports MySql.Data.MySqlClient
Public Class SqlConnection

    Dim SQLConn As MySqlConnection
    Public Shared user, password, database, host As String

    Public Sub SetData()
        host = "localhost"
        user = "admin"
        password = "123456"
        database = "data"
    End Sub

    Public Function connect() As Boolean
        Dim connStr As String
        SetData()
        connStr = "server=" + host + ";user=" + user + ";database=" + database + ";port=3306;password=" + password
        SQLConn = New MySqlConnection(connStr)
        Try
            SQLConn.Open()
            Console.WriteLine("Connect Success")
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
            SQLConn.Close()
            Return False
        End Try
    End Function

    Public Function getImageProductDir() As String
        Return My.Computer.FileSystem.CurrentDirectory + "\productImage\"
    End Function

    Public Function login(username As String, password As String) As User
        Dim userLogin As User
        userLogin = New User()
        If connect() = True Then
            Dim queryStr As String
            Dim cmd As MySqlCommand
            Dim reader As MySqlDataReader

            queryStr = "SELECT * FROM user WHERE username = @username AND password = @password"
            cmd = New MySqlCommand(queryStr, SQLConn)
            cmd.Parameters.AddWithValue("@username", username)
            cmd.Parameters.AddWithValue("@password", password)
            reader = cmd.ExecuteReader()
            If reader.HasRows = True Then
                reader.Read()
                userLogin.id = Convert.ToInt32(reader.Item(0))
                userLogin.username = reader.Item(1).ToString()
                userLogin.password = reader.Item(2).ToString()
                userLogin.full_name = reader.Item(3).ToString()
                userLogin.id_card = reader.Item(4).ToString()
                userLogin.tel = reader.Item(5).ToString()
                userLogin.address = reader.Item(6).ToString()
            End If
            SQLConn.Close()
        End If
        Return userLogin
    End Function

    Public Function addProduct(product As Product) As Boolean
        Dim result As Boolean = False
        If connect() = True Then
            Dim queryStr As String
            Dim cmd As MySqlCommand

            queryStr = "INSERT INTO `product` (`serial`, `name`, `brand`, `detail`, `amont`, `price`, `status`, `image`) VALUES (@serial,@name,@brand,@detail,@amont,@price,@status,@image);"
            cmd = New MySqlCommand(queryStr, SQLConn)
            cmd.Parameters.AddWithValue("@serial", product.serial)
            cmd.Parameters.AddWithValue("@name", product.name)
            cmd.Parameters.AddWithValue("@brand", product.brand)
            cmd.Parameters.AddWithValue("@detail", product.detail)
            cmd.Parameters.AddWithValue("@amont", Convert.ToInt32(product.amount))
            cmd.Parameters.AddWithValue("@price", Convert.ToDouble(product.price))
            cmd.Parameters.AddWithValue("@status", product.status)
            cmd.Parameters.AddWithValue("@image", product.image)
            cmd.ExecuteReader()
            SQLConn.Close()
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    Public Function updateProduct(product As Product) As Boolean
        Dim result As Boolean = False
        If connect() = True Then
            Dim queryStr As String
            Dim cmd As MySqlCommand

            queryStr = "UPDATE product SET `serial` = @serial, `name` = @name, `brand` = @brand, `detail` = @detail, `amont` = @amont, `price` = @price, `status` = @status, `image` = @image WHERE (`id` = @id);"
            cmd = New MySqlCommand(queryStr, SQLConn)
            cmd.Parameters.AddWithValue("@id", product.id)
            cmd.Parameters.AddWithValue("@serial", product.serial)
            cmd.Parameters.AddWithValue("@name", product.name)
            cmd.Parameters.AddWithValue("@brand", product.brand)
            cmd.Parameters.AddWithValue("@detail", product.detail)
            cmd.Parameters.AddWithValue("@amont", Convert.ToInt32(product.amount))
            cmd.Parameters.AddWithValue("@price", Convert.ToDouble(product.price))
            cmd.Parameters.AddWithValue("@status", product.status)
            cmd.Parameters.AddWithValue("@image", product.image)
            cmd.ExecuteReader()
            SQLConn.Close()
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    Public Function searchProduct(serial As String, name As String) As Product
        Dim product As Product = New Product()
        If connect() = True Then
            Dim queryStr As String
            Dim cmd As MySqlCommand
            Dim reader As MySqlDataReader

            If serial <> "" Then
                queryStr = "SELECT * FROM product WHERE serial = @serial"
                cmd = New MySqlCommand(queryStr, SQLConn)
                cmd.Parameters.AddWithValue("@serial", serial)
            ElseIf name <> "" Then
                queryStr = "SELECT * FROM product WHERE name = @name"
                cmd = New MySqlCommand(queryStr, SQLConn)
                cmd.Parameters.AddWithValue("@name", name)
            End If
            reader = cmd.ExecuteReader()
            If reader.HasRows = True Then
                reader.Read()
                product.id = Convert.ToInt32(reader.Item(0))
                product.serial = reader.Item(1).ToString()
                product.name = reader.Item(2).ToString()
                product.brand = reader.Item(3).ToString()
                product.detail = reader.Item(4).ToString()
                product.amount = Convert.ToInt32(reader.Item(5).ToString())
                product.price = Convert.ToDouble(reader.Item(6).ToString())
                product.status = reader.Item(7).ToString()
                product.image = reader.Item(8).ToString()
            End If
            SQLConn.Close()
        End If
        Return product
    End Function

End Class
