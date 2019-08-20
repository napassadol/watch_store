Public Class Product
    Public id As Integer
    Public serial As String
    Public name As String
    Public brand As String
    Public detail As String
    Public amount As Integer
    Public price As Double
    Public status As String
    Public image As String

    Public Sub New()
        id = 0
        serial = ""
        name = ""
        brand = ""
        detail = ""
        amount = 0
        price = 0
        status = ""
        image = ""
    End Sub
End Class
