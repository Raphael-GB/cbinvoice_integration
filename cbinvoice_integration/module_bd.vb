Imports Oracle.ManagedDataAccess.Client
Imports System.Configuration
Module module_bd

    Public nbano As Integer
    Public objconn_orcl As OracleConnection
    Public sqlTransaction As OracleTransaction

    Public Function CheckDBConnection_net(ByRef r_Err As String) As Boolean
        Dim i As Integer
        On Error GoTo Err_Handle
        Dim strErr As String
        Dim conststring As String

        Dim UserName As String
        Dim PWD As String
        Dim DataSource As String


        DataSource = ConfigurationManager.AppSettings("sid")
        UserName = ConfigurationManager.AppSettings("user_db")
        PWD = ConfigurationManager.AppSettings("pass_db")

        'conststring = "Data Source=" & DataSource & ";" & "User ID=" & UserName & ";" & "Password=" & PWD & ";"
        conststring = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.94.1.242)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=PRODCOMPLET)));User Id=ADM_WEB;Password=DEVEL;"
        If objconn_orcl Is Nothing Then
            objconn_orcl = New OracleConnection(conststring)
        End If

        If Not (objconn_orcl.State = ConnectionState.Open) Then
            objconn_orcl.Open()
        End If

        CheckDBConnection_net = True

        Exit Function
Err_Handle:
        objconn_orcl = Nothing
        'MsgBox(CStr(Err.Number) & "  " & Err.Description, vbOKOnly & vbInformation, Err.Source & ":CheckDBConnection")
        'r_Err = Err.Description
        CheckDBConnection_net = False
    End Function
    Public Function nbcaractere(ByVal valeur, ByVal caract) As Integer
        'Dim a As Integer = valeur.Count(Function(x) x = caract)

        Dim nb As Integer = 0
        For i = 0 To valeur.length - 1
            If valeur.substring(i, 1) = caract Then
                nb = nb + 1
            End If

        Next
        Return nb
    End Function
    Public Function retourne_resultat_tableau(requete As String) As System.Data.DataTable
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim objadap As Oracle.ManagedDataAccess.Client.OracleDataAdapter
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet

        objadap = New Oracle.ManagedDataAccess.Client.OracleDataAdapter(requete, objconn_orcl)

        objadap.Fill(ds, "Projet")
        Return ds.Tables("Projet")

    End Function

    Public Function retourne_resultat_code(requete As String) As String

        Dim commande As New OracleCommand
        Dim myReader As OracleDataReader



        commande = New OracleCommand(requete.Replace("%", ""), objconn_orcl)
        ' commande.Transaction = sqlTransaction
        myReader = commande.ExecuteReader()

        While myReader.Read()
            retourne_resultat_code = myReader.Item(0).ToString()
        End While
        myReader.Close()
        myReader.Dispose()
        commande.Dispose()
    End Function


    Public Function EXECUTE_FUNCTION_SQL_creation_lot(nom_fonction As String, client As String, typelot As String, entite As String, qte As String) As String
        Try
            Dim connOracle As Oracle.ManagedDataAccess.Client.OracleConnection
            Dim commOracle As New Oracle.ManagedDataAccess.Client.OracleCommand
            Dim paramOracle As Oracle.ManagedDataAccess.Client.OracleParameter


            commOracle.Connection = objconn_orcl
            commOracle.CommandType = CommandType.StoredProcedure
            commOracle.CommandText = nom_fonction

            paramOracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            paramOracle.ParameterName = "pReturnValue"
            paramOracle.DbType = DbType.Double
            paramOracle.Direction = ParameterDirection.ReturnValue
            commOracle.Parameters.Add(paramOracle)

            paramOracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            paramOracle.ParameterName = "client"
            paramOracle.DbType = DbType.String
            paramOracle.Value = client
            paramOracle.Direction = ParameterDirection.Input
            commOracle.Parameters.Add(paramOracle)

            paramOracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            paramOracle.ParameterName = "typ_lot"
            paramOracle.DbType = DbType.String
            paramOracle.Value = typelot
            paramOracle.Direction = ParameterDirection.Input
            commOracle.Parameters.Add(paramOracle)

            paramOracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            paramOracle.ParameterName = "ventite"
            paramOracle.DbType = DbType.String
            paramOracle.Value = entite
            paramOracle.Direction = ParameterDirection.Input
            commOracle.Parameters.Add(paramOracle)

            paramOracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            paramOracle.ParameterName = "QTE"
            paramOracle.DbType = DbType.String
            paramOracle.Value = qte
            paramOracle.Direction = ParameterDirection.Input
            commOracle.Parameters.Add(paramOracle)

            commOracle.ExecuteNonQuery()

            ecriture_log("trace", "Création du lot " & commOracle.Parameters.Item("pReturnValue").Value.ToString() & " pour le Client " & client)
            Return commOracle.Parameters.Item("pReturnValue").Value.ToString



        Catch ex As Exception
            ecriture_log("Erreur", ex.Message)
        End Try

    End Function
    Public Function execute_requette(requette As String) As Boolean
        Try
            Dim da As OracleDataAdapter = New OracleDataAdapter()
            Dim cmd As OracleCommand

            cmd = New OracleCommand(requette, objconn_orcl)
            'cmd.Transaction = sqlTransaction
            cmd.ExecuteNonQuery()

            cmd.Dispose()

            Return (True)
        Catch
            Return False

        End Try

    End Function

    Public Sub DeleteBatch(ByVal name As String)
        Try
            Dim da As OracleDataAdapter = New OracleDataAdapter()
            Dim cmd As OracleCommand
            Dim number = Convert.ToInt32(name.Split("-"c)(1))
            Dim code = Convert.ToInt32(name.Split("-"c)(3))

            Dim connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.94.1.242)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=SAISIEGB)));User Id=COMPOSTAGE;Password=DEVEL;"
            Dim connect = New OracleConnection(connectionString)

            Dim request = $"delete from detail_lot where lot_compostage='{number}' and code_client='{code}'"
            cmd = New OracleCommand(request, connect)
            cmd.ExecuteNonQuery()

            request = $"delete from lots where lot_compostage='{number}' and code_client='{code}'"
            cmd = New OracleCommand(request, connect)
            'cmd.Transaction = sqlTransaction
            cmd.ExecuteNonQuery()

            cmd.Dispose()

        Catch

        End Try

    End Sub

    'Public Sub DeleteBatch(ByVal number As Integer, ByVal code As String)
    '    Using db = DbFactories.GetDatabase("Compostage")
    '        db.Execute("delete from detail_lot where lot_compostage=@0 and code_client=@1", number, code)
    '        db.Execute("delete from lots where lot_compostage=@0 and code_client=@1", number, code)
    '    End Using
    'End Sub

End Module
