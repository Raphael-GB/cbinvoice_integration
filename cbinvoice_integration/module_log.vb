Module module_log
    Public Sub ecriture_log(niveau As String, chaine As String)
        Form1.txtlog.Text = Form1.txtlog.Text & niveau & " - - " & chaine & vbNewLine
    End Sub
End Module
