Imports System.Data.OleDb

Partial Class Auth
    Inherits System.Web.UI.Page

    ' نص الاتصال يشير إلى ملف db.accdb في مجلد المشروع الرئيسي
    Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Server.MapPath("~/db.accdb")

    ' دالة التسجيل (جديد)
    Protected Sub btnRegister_Click(sender As Object, e As EventArgs)
        Using conn As New OleDbConnection(connStr)
            ' تأكدي أن أسماء الحقول (CustomerName, Phone, Email, [Password]) تطابق تماماً ما في الأكسس
            Dim sql As String = "INSERT INTO Customers (CustomerName, Phone, Email, [Password]) VALUES (?, ?, ?, ?)"
            Dim cmd As New OleDbCommand(sql, conn)

            ' إضافة القيم من التكست بوكس
            cmd.Parameters.AddWithValue("?", CustomerName.Text)
            cmd.Parameters.AddWithValue("?", Phone.Text)
            cmd.Parameters.AddWithValue("?", Email.Text)
            cmd.Parameters.AddWithValue("?", Password.Text)

            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                Response.Write("<script>alert('تم تسجيلك بنجاح في The Art Yard!'); window.location.href='my_account.html';</script>")
            Catch ex As Exception
                Response.Write("<script>alert('خطأ في الربط: " & ex.Message.Replace("'", "") & "');</script>")
            End Try
        End Using
    End Sub

    ' دالة تسجيل الدخول (اختياري - للتحقق من البيانات)
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Using conn As New OleDbConnection(connStr)
            Dim sql As String = "SELECT COUNT(*) FROM Customers WHERE Email=? AND [Password]=?"
            Dim cmd As New OleDbCommand(sql, conn)
            cmd.Parameters.AddWithValue("?", loginEmail.Text)
            cmd.Parameters.AddWithValue("?", loginPassword.Text)

            Try
                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    Response.Write("<script>alert('أهلاً بك مجدداً!'); window.location.href='index.html';</script>")
                Else
                    Response.Write("<script>alert('خطأ في الإيميل أو كلمة المرور');</script>")
                End If
            Catch ex As Exception
                Response.Write("<script>alert('حدث خطأ: " & ex.Message & "');</script>")
            End Try
        End Using
    End Sub
End Class