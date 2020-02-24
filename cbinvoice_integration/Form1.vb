Imports System.IO
Imports System.Configuration
Imports System.Xml.Serialization
Imports System.Xml

Public Class Form1
    Public Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

    Public Structure Info_image
        Public chemin_origine As String
        Public chemin_destination As String
        Public id As String
        Public image_origine As String
        Public LOT As String
        Public num_facture As String
        Public enti As String
        Public CODE_FOURNISSEUR_SAISIE As String
        Public CODE_FOURNISSEUR_INSCRIPTION As String
        Public NUM_COMMANDE As String
        Public TITRE As String
        Public ECHANGE As String
        Public FACTURE_PAYE As String
    End Structure



    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim erreur As String

        ecriture_log("trace", "Initialisation")

        Timer1.Interval = ConfigurationManager.AppSettings("interval")


        CheckDBConnection_net(erreur)
        If erreur <> "" Then
            ecriture_log("erreur", erreur)
        Else
            ecriture_log("trace", "connection validée")
            For Each l As DataRow In retourne_resultat_tableau("select id_workflow from client where traitement='FACTURE' order by id_workflow").Rows
                listeclient.Items.Add(l.Item(0))
            Next


        End If



    End Sub
    Private Function faire_xml_alinea(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image))
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)



        Dim Xml As XmlDocument = New XmlDocument()




        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file:///C:/ProgramData/Kofax/KIC-ED/KCPlugIn/config/Schemas/Destination%20ALW/XmlAutoImport.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "ALW - Alinea Web")
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "ALW_FicheDeLot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "ClientCategory")
        batchIndexField.SetAttribute("Value", "MA")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "ClientCode")
        batchIndexField.SetAttribute("Value", "ALI")
        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)


        For Each elem As Info_image In tabimage_pdf


            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "ALW_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebBatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebPdfSource")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_INSCRIPTION <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_INSCRIPTION)
            Else
                If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                    batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
                Else
                    batchIndexField.SetAttribute("Value", "")
                End If
            End If

            batchIndexFields.AppendChild(batchIndexField)


            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\import pdf\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\import pdf\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function

    Private Function faire_xml_auchan(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image))
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)

        Dim Xml As XmlDocument = New XmlDocument()

        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file:///C:/ProgramData/Kofax/KIC-ED/KCPlugIn/config/Schemas/Destination%20ALW/XmlAutoImport.xsd")
        End If

        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "AUC - Auchan")
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "AUC_FicheLot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "ClientCategory")
        batchIndexField.SetAttribute("Value", "MA")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "ClientCode")
        batchIndexField.SetAttribute("Value", "AUC")
        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)


        For Each elem As Info_image In tabimage_pdf


            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "AUC_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebBatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebPdfSource")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_INSCRIPTION <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_INSCRIPTION)
            Else
                If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                    batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
                Else
                    batchIndexField.SetAttribute("Value", "")
                End If
            End If

            batchIndexFields.AppendChild(batchIndexField)


            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\import pdf\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\import pdf\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function

    Private Function faire_xml_suez(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image))
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)



        Dim Xml As XmlDocument = New XmlDocument()




        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file:///C:/ProgramData/Kofax/KIC-ED/KCPlugIn/config/Schemas/Destination%20SUE%20-%20WEB/XmlAutoImport.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "SUE - Suez")
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "SUE_FicheLot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)


        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchClientCode")
        batchIndexField.SetAttribute("Value", "SUE")
        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)


        For Each elem As Info_image In tabimage_pdf


            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "SUE_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebBatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebPdfSource")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_INSCRIPTION <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_INSCRIPTION)
            Else
                If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                    batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
                Else
                    batchIndexField.SetAttribute("Value", "")
                End If
            End If

            batchIndexFields.AppendChild(batchIndexField)


            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\import pdf\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\import pdf\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function
    Private Function faire_xml_total_be(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image), ENTITE As String)
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)



        Dim Xml As XmlDocument = New XmlDocument()




        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file:///C:/ProgramData/Kofax/KIC-ED/KCPlugin/config/Schemas/Destination TBE - WEB/output.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "TBE - Total Belgique")
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "TBE_FicheLot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)



        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchClientCode")
        batchIndexField.SetAttribute("Value", "TBE")
        batchIndexFields.AppendChild(batchIndexField)


        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchCompanyCode")
        batchIndexField.SetAttribute("Value", "1104")
        batchIndexFields.AppendChild(batchIndexField)


        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)


        For Each elem As Info_image In tabimage_pdf


            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "TBE_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebBatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebPdfSource")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_INSCRIPTION <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_INSCRIPTION)
            Else
                If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                    batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
                Else
                    batchIndexField.SetAttribute("Value", "")
                End If
            End If

            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebCompanyId")
            batchIndexField.SetAttribute("Value", elem.enti)
            batchIndexFields.AppendChild(batchIndexField)

            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\import pdf\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\import pdf\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function

    Private Function faire_xml_bv(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image), ENTITE As String)
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)

        Dim Xml As XmlDocument = New XmlDocument()
        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file:///C:/ProgramData/Kofax/KIC-ED/KCPlugIn/config/Schemas/Destination%20BVW%20-%20WEB/XmlAutoImport.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "BVW - Bureau Veritas Web")
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "BVW_FicheLot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchClientCode")
        batchIndexField.SetAttribute("Value", "BV")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "WebFolderCount")
        batchIndexField.SetAttribute("Value", tabimage_pdf.Count().ToString())
        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)

        i = 0
        For Each elem As Info_image In tabimage_pdf

            i = i + 1
            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "BVW_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebBatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebPdfSource")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
            Else
                batchIndexField.SetAttribute("Value", "")
            End If

            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebCompanyId")
            batchIndexField.SetAttribute("Value", elem.enti)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebPurchaseOrder")
            If elem.NUM_COMMANDE <> "" Then
                batchIndexField.SetAttribute("Value", elem.NUM_COMMANDE)
            Else
                batchIndexField.SetAttribute("Value", "")
            End If
            batchIndexFields.AppendChild(batchIndexField)


            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebFolderIndex")
            batchIndexField.SetAttribute("Value", i.ToString())
            batchIndexFields.AppendChild(batchIndexField)


            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebFolderCount")
            batchIndexField.SetAttribute("Value", tabimage_pdf.Count().ToString())
            batchIndexFields.AppendChild(batchIndexField)

            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\import pdf\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\import pdf\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function

    Private Function faire_xml_printemps(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image), ENTITE As String)
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)

        Dim Xml As XmlDocument = New XmlDocument()
        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file://10.94.0.98/Scan/XmlAutoImport.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "PRI - Printemps")
        batchnode.SetAttribute("Name", lot)
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "PRI_FicheLot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchClientCode")
        batchIndexField.SetAttribute("Value", ENTITE.Replace(" ", "").Substring(0, 4))
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDocumentCount")
        batchIndexField.SetAttribute("Value", tabimage_pdf.Count().ToString())
        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)

        i = 0
        For Each elem As Info_image In tabimage_pdf

            i = i + 1
            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "PRI_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentExportFullName")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            Dim directoryPath As String
            Dim splittedPath = elem.chemin_origine.Split("\").ToList()
            splittedPath.RemoveAt(splittedPath.Count() - 1)
            directoryPath = String.Join("\", splittedPath)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentExportDirectory")
            batchIndexField.SetAttribute("Value", directoryPath)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentFileName")
            batchIndexField.SetAttribute("Value", i.ToString())
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebCompanyId")
            batchIndexField.SetAttribute("Value", elem.enti)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentWorkflow")
            batchIndexField.SetAttribute("Value", "75")
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "BatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
            Else
                batchIndexField.SetAttribute("Value", "")
            End If

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentIndex")
            batchIndexField.SetAttribute("Value", i.ToString())
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentCount")
            batchIndexField.SetAttribute("Value", tabimage_pdf.Count().ToString())
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexFields.AppendChild(batchIndexField)

            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function

    Private Function faire_xml_mcdo(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image), ENTITE As String)
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)

        Dim Xml As XmlDocument = New XmlDocument()
        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file://10.94.0.98/Scan/XmlAutoImport.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "MCO - Mc Donald")
        batchnode.SetAttribute("Name", lot)
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "MCO_Lot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchClientCode")
        batchIndexField.SetAttribute("Value", "MCO")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDocumentCount")
        batchIndexField.SetAttribute("Value", tabimage_pdf.Count().ToString())
        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)

        i = 0
        For Each elem As Info_image In tabimage_pdf

            i = i + 1
            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "MCO_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentFilePath")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            Dim directoryPath As String
            Dim splittedPath = elem.chemin_origine.Split("\").ToList()
            splittedPath.RemoveAt(splittedPath.Count() - 1)
            directoryPath = String.Join("\", splittedPath)

            'batchIndexField = Xml.CreateElement("IndexField")
            'batchIndexField.SetAttribute("Name", "DocumentDirectoryPath")
            'batchIndexField.SetAttribute("Value", directoryPath)
            'batchIndexFields.AppendChild(batchIndexField)

            'batchIndexField = Xml.CreateElement("IndexField")
            'batchIndexField.SetAttribute("Name", "ExportFileName")
            'batchIndexField.SetAttribute("Value", i.ToString())
            'batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentWorkflow")
            batchIndexField.SetAttribute("Value", "15")
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "BatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
            Else
                batchIndexField.SetAttribute("Value", "")
            End If

            batchIndexFields.AppendChild(batchIndexField)

            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\import pdf\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\import pdf\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function

    Private Function faire_xml_mcdo_construction(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image), ENTITE As String)
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)

        Dim Xml As XmlDocument = New XmlDocument()
        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file://10.94.0.98/Scan/XmlAutoImport.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "MCO - Mc Donald Construction")
        batchnode.SetAttribute("Name", lot)
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "MCO_Lot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchClientCode")
        batchIndexField.SetAttribute("Value", "MCO")
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDocumentCount")
        batchIndexField.SetAttribute("Value", tabimage_pdf.Count().ToString())
        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)

        i = 0
        For Each elem As Info_image In tabimage_pdf

            i = i + 1
            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "MCO_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentFilePath")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            Dim directoryPath As String
            Dim splittedPath = elem.chemin_origine.Split("\").ToList()
            splittedPath.RemoveAt(splittedPath.Count() - 1)
            directoryPath = String.Join("\", splittedPath)

            'batchIndexField = Xml.CreateElement("IndexField")
            'batchIndexField.SetAttribute("Name", "DocumentDirectoryPath")
            'batchIndexField.SetAttribute("Value", directoryPath)
            'batchIndexFields.AppendChild(batchIndexField)

            'batchIndexField = Xml.CreateElement("IndexField")
            'batchIndexField.SetAttribute("Name", "ExportFileName")
            'batchIndexField.SetAttribute("Value", i.ToString())
            'batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "DocumentWorkflow")
            batchIndexField.SetAttribute("Value", "73")
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "BatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.TITRE <> "" Then 'Correspond a la valeur du code sap du factor table champ fichier id 5
                batchIndexField.SetAttribute("Value", elem.TITRE)
            Else
                batchIndexField.SetAttribute("Value", "")
            End If

            batchIndexFields.AppendChild(batchIndexField)

            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", "\\10.94.0.98\Scan\Mc Donald Construction\Output\" & lot & "\" & "FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = "\\10.94.0.98\Scan\Mc Donald Construction\Output\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function


    Private Function faire_xml_idgroup(enr As List(Of Info_image), repia As String, lot As String, tabimage_pdf As List(Of Info_image), ENTITE As String)
        Dim strsqla As String = ""
        Dim strerror As String = ""
        Dim i As Integer = 0
        Dim ds As DataSet = New DataSet
        Dim er As String = ""
        Dim nb_enr As Integer = 1
        CheckDBConnection_net(er)



        Dim Xml As XmlDocument = New XmlDocument()




        Dim XmlDeclaration As XmlDeclaration = Xml.CreateXmlDeclaration("1.0", "UTF-8", "")

        Dim rootNode As XmlElement = Xml.CreateElement("ImportSession")

        Xml.AppendChild(rootNode)
        Xml.InsertBefore(XmlDeclaration, rootNode)

        If nb_enr = 1 Then
            rootNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
            rootNode.SetAttribute("xsi:noNamespaceSchemaLocation", "file:///C:/ProgramData/Kofax/KIC-ED/KCPlugIn/config/Schemas/XmlAutoImport.xsd")
        End If



        Dim mainNode As XmlElement = Xml.CreateElement("Batches")
        Dim batchnode As XmlElement = Xml.CreateElement("Batch")
        Dim txtXML As XmlText = Xml.CreateTextNode("")
        mainNode.AppendChild(batchnode)
        batchnode.SetAttribute("BatchClassName", "IDW - IdGroup Web")
        batchnode.SetAttribute("Priority", "5")
        Dim batchdocuments As XmlElement = Xml.CreateElement("Documents")
        batchnode.AppendChild(batchdocuments)

        Dim batchdocument As XmlElement = Xml.CreateElement("Document")
        batchdocuments.AppendChild(batchdocument)
        batchdocument.SetAttribute("FormTypeName", "IDW_FicheDeLot")
        Dim batchIndexFields As XmlElement = Xml.CreateElement("IndexFields")
        batchdocument.AppendChild(batchIndexFields)
        Dim batchIndexField As XmlElement = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchName")
        batchIndexField.SetAttribute("Value", lot)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchDate")
        batchIndexField.SetAttribute("Value", Now.ToShortDateString)
        batchIndexFields.AppendChild(batchIndexField)

        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchType")
        batchIndexField.SetAttribute("Value", "WEB")
        batchIndexFields.AppendChild(batchIndexField)



        batchIndexField = Xml.CreateElement("IndexField")
        batchIndexField.SetAttribute("Name", "BatchClientCode")
        If ENTITE = "OKAIDI-ITALY" Then
            batchIndexField.SetAttribute("Value", "IDW")
        Else
            batchIndexField.SetAttribute("Value", "IDW")
        End If

        batchIndexFields.AppendChild(batchIndexField)

        Dim batchPAGES As XmlElement = Xml.CreateElement("Pages")
        batchdocument.AppendChild(batchPAGES)
        Dim batchPAGE As XmlElement = Xml.CreateElement("Page")
        batchPAGE.SetAttribute("ImportFileName", "FicheDeLotVirtuelle.pdf")
        batchPAGES.AppendChild(batchPAGE)


        For Each elem As Info_image In tabimage_pdf


            batchdocument = Xml.CreateElement("Document")
            batchdocuments.AppendChild(batchdocument)
            batchdocument.SetAttribute("FormTypeName", "IDW_Facture")

            batchIndexFields = Xml.CreateElement("IndexFields")
            batchdocument.AppendChild(batchIndexFields)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebBatchName")
            batchIndexField.SetAttribute("Value", lot)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebEnvoiId")
            batchIndexField.SetAttribute("Value", elem.id)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebPdfSource")
            batchIndexField.SetAttribute("Value", elem.chemin_origine)
            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebVendorId")
            If elem.CODE_FOURNISSEUR_INSCRIPTION <> "" Then
                batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_INSCRIPTION)
            Else
                If elem.CODE_FOURNISSEUR_SAISIE <> "" Then
                    batchIndexField.SetAttribute("Value", elem.CODE_FOURNISSEUR_SAISIE)
                Else
                    batchIndexField.SetAttribute("Value", "")
                End If
            End If

            batchIndexFields.AppendChild(batchIndexField)

            batchIndexField = Xml.CreateElement("IndexField")
            batchIndexField.SetAttribute("Name", "WebCompanyId")
            batchIndexField.SetAttribute("Value", elem.enti)
            batchIndexFields.AppendChild(batchIndexField)

            batchPAGES = Xml.CreateElement("Pages")
            batchdocument.AppendChild(batchPAGES)
            batchPAGE = Xml.CreateElement("Page")
            batchPAGE.SetAttribute("ImportFileName", elem.chemin_destination.Replace(".att", ".pdf").Split("\")(elem.chemin_destination.Replace(".att", ".pdf").Split("\").Count - 1))
            batchPAGES.AppendChild(batchPAGE)

            rootNode.AppendChild(mainNode)

        Next

        File.Copy(Application.StartupPath & "\FicheDeLotVirtuelle.pdf", repia & "\import pdf\" & lot & "\" & "\FicheDeLotVirtuelle.pdf")

        Dim fichier_xml As String = repia & "\import pdf\" & lot & "\" & lot & ".xml"


        Dim nomfic As String = fichier_xml

        Xml.Save(nomfic)
    End Function


    Public Shared Sub ExportToXml(ByVal filename As String, ByVal Objet As Object)
        Try
            Dim serialiseur As XmlSerializer = New XmlSerializer(Objet.GetType())
            Dim writer As TextWriter = New StreamWriter(filename)

            serialiseur.Serialize(writer, Objet)

            writer.Close()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub envoi_mail(ByVal mail As String, ByVal sujet As String, ByVal corps As String)

        Dim objSmtpMail As New System.Net.Mail.SmtpClient


        Dim Mailmsg As New System.Net.Mail.MailMessage

        objSmtpMail.Host = "10.94.0.9"

        objSmtpMail.Port = 25


        Mailmsg.From = New System.Net.Mail.MailAddress("NOREPLY@CBA.FR")

        Mailmsg.Sender = New System.Net.Mail.MailAddress("NOREPLY@CBA.FR")


        objSmtpMail.EnableSsl = False
        Dim SMTPUserInfo As New System.Net.NetworkCredential("noreply@cba.fr", "cba123")

        objSmtpMail.UseDefaultCredentials = False

        objSmtpMail.Credentials = SMTPUserInfo

        Mailmsg.To.Add(mail)

        'Mailmsg.Bcc = txtBCC.Text

        Mailmsg.Subject = sujet.ToString

        Mailmsg.Body = corps

        'Mailmsg.Headers.Add(« X-Organization », « demo.openhost.fr »)

        'Ajout possible du degr de priorit du mail
        Mailmsg.Priority = Net.Mail.MailPriority.Normal

        objSmtpMail.Send(Mailmsg)

        Mailmsg = Nothing

        objSmtpMail = Nothing

    End Sub
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick

        Dim requete As String = ""

        Dim var_PROD As String = "O" ' T pout test O pour prod


        Me.Text = "Cb Invoice Integration : " & DateDiff("n", Convert.ToDateTime(ConfigurationManager.AppSettings("heure_final")), Date.Now).ToString

        requete = " select"
        requete = requete & " idwork, repertoire_production, repertoire_ia, CODE_CLIENT, entite, nb_lot,sum(nb) nb ,ss_ctrl,traitement"
        requete = requete & " from ("
        requete = requete & " select"
        requete = requete & " c.id_workflow idwork  ,"
        requete = requete & " c.repertoire_production,"
        requete = requete & " c.repertoire_ia,"
        requete = requete & " c.nb_par_lot nb_lot,"
        requete = requete & " c.CODE_CLIENT CODE_CLIENT,"
        requete = requete & " count(*) nb,"
        requete = requete & " (select distinct valeur_champ from champ_fichier where id_envoi=(h.ID_ENVOI) and id_champ in (select id_champ from champ where (libelle_champ_fr='Entité facturée' or (libelle_champ_fr='TSA' and id_workflow=c.id_workflow)) and id_workflow= c.id_workflow )) entite,"
        requete = requete & " U.SS_CTRL ss_ctrl,f.type_fichier traitement"
        requete = requete & " from"
        requete = requete & " historique h, fichier f,utilisateur u,client c"
        requete = requete & " where"
        requete = requete & " h.ID_ENVOI = f.id_envoi"
        requete = requete & " and C.MEP='" & var_PROD & "'"
        requete = requete & " and (C.traitement='FACTURE' )"
        requete = requete & " and (upper(f.type_fichier)='FACTURE') " 'OR upper(f.type_fichier)='MAIL')
        requete = requete & " and h.id_etat=2"
        requete = requete & " and  h.id_envoi in (select"
        requete = requete & " id_envoi"
        requete = requete & " from"
        requete = requete & " historique"
        requete = requete & " group by"
        requete = requete & " id_envoi  having max(id_etat)=2"
        requete = requete & " )"
        requete = requete & " and u.identifiant=f.identifiant"
        requete = requete & " and u.id_workflow=c.id_workflow"
        If listeclient.Text <> "" Then
            requete = requete & " and c.id_workflow=" & listeclient.Text
        End If
        requete = requete & " group by"
        requete = requete & " f.type_fichier,h.ID_ENVOI,c.id_workflow,c.repertoire_production,c.repertoire_ia,c.nb_par_lot,c.CODE_CLIENT,ss_ctrl order by c.id_workflow"
        requete = requete & " ) temp"



        requete = requete & " where (entite is not null)"
        requete = requete & " group by traitement,idwork,repertoire_production,repertoire_ia,CODE_CLIENT,entite,nb_lot,SS_CTRL"



        DataGridView1.DataSource = retourne_resultat_tableau(requete)





        Dim id As String = ""


        If DataGridView1.Rows.Count <= 1 Then
            Exit Sub
        End If


        Timer1.Enabled = False

        For Each ligne As DataGridViewRow In DataGridView1.Rows
            Dim NBTOTAL As Integer = Convert.ToDouble(DirectCast(ligne.Cells.Item("NB_LOT"), System.Windows.Forms.DataGridViewTextBoxCell).Value)
            Dim nbactuel As Integer = Convert.ToDouble(DirectCast(ligne.Cells.Item("NB"), System.Windows.Forms.DataGridViewTextBoxCell).Value)
            Dim id_work As String = DirectCast(ligne.Cells.Item("idwork"), System.Windows.Forms.DataGridViewTextBoxCell).Value
            Dim code_client As String = DirectCast(ligne.Cells.Item("CODE_CLIENT"), System.Windows.Forms.DataGridViewTextBoxCell).Value
            Dim entite As String = DirectCast(ligne.Cells.Item("entite"), System.Windows.Forms.DataGridViewTextBoxCell).Value
            Dim type_traitement As String = DirectCast(ligne.Cells.Item("traitement"), System.Windows.Forms.DataGridViewTextBoxCell).Value

            If (Checkvider.Checked = True) Or (NBTOTAL <= nbactuel And nbactuel <> 0) Or (DateDiff("n", Convert.ToDateTime(ConfigurationManager.AppSettings("heure_final")), Date.Now) > 0 And DateDiff("n", Convert.ToDateTime(ConfigurationManager.AppSettings("heure_final")), Date.Now) < 60) Then
                requete = "select"
                requete = requete & " distinct h.id_envoi idenv,c.id_workflow idwork  ,"
                requete = requete & " c.repertoire_production,"
                requete = requete & " c.repertoire_ia,"
                requete = requete & " c.nb_par_lot nb_lot,"
                requete = requete & $" '\\10.168.94.12\e\SITES\{IIf(id_work = "73", "CBWEBINVOICE_MCDO_CONSTRUCTION", "CBWEBINVOICE")}\portail\'||c.repertoire_production||'\'||f.nom_fichier_new fichier_source,"
                requete = requete & " c.repertoire_ia rep_destination,"
                'requete = requete & " (select valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=(select id_champ from champ where libelle_champ_fr='Numéro de facture' and champ.id_workflow=c.ID_WORKFLOW)) num_facture,"
                'requete = requete & " (select valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=(select id_champ from champ where libelle_champ_fr='Entité facturée' and champ.id_workflow=c.ID_WORKFLOW)) entite,"
                'requete = requete & " (select valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=(select id_champ from champ where libelle_champ_fr='Code fournisseur' and champ.id_workflow=c.ID_WORKFLOW)) CODE_FOURNISSEUR_SAISIE,"


                requete = requete & " (select distinct valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=1) num_facture,"
                requete = requete & " (select distinct valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=2) entite,"
                requete = requete & " (select distinct valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=3) CODE_FOURNISSEUR_SAISIE,"
                requete = requete & " (select distinct valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=4) num_commande,"
                requete = requete & " (select distinct valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=5) TITRE,"
                requete = requete & " (select distinct valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=6) ECHANGE,"
                requete = requete & " (select distinct valeur_champ  from champ_fichier where champ_fichier.ID_ENVOI=h.ID_ENVOI and champ_fichier.id_workflow=c.id_workflow and  id_champ=7) FACTURE_PAYE,"


                requete = requete & " U.CODE_SAP CODE_FOURNISSEUR_INSCRIPTION,u.ss_ctrl"


                requete = requete & " from"
                requete = requete & " historique h, fichier f,utilisateur u,client c"
                requete = requete & " where"
                requete = requete & " c.id_workflow=" & id_work & "  and h.ID_ENVOI = f.id_envoi"
                requete = requete & " and (upper(f.type_fichier)='" & type_traitement & "')"
                requete = requete & " and h.id_etat=2"
                requete = requete & " and C.MEP='" & var_PROD & "'"
                requete = requete & " and (C.traitement='FACTURE' )"
                requete = requete & " and  h.id_envoi in (select id_envoi from historique where identifiant in (select identifiant from utilisateur where id_workflow=" & id_work & ")"
                requete = requete & " and id_envoi in (select id_envoi  from CHAMP_FICHIER WHERE VALEUR_champ='" & entite & "' AND ID_WORKFLOW=" & id_work & " AND id_champ in (select id_champ from champ where (libelle_champ_fr='Entité facturée' or (libelle_champ_fr='TSA' and id_workflow='62')) and id_workflow= " & id_work & " ) )"
                requete = requete & " group by id_envoi  having max(id_etat)=2)"
                requete = requete & " and u.identifiant=f.identifiant"
                requete = requete & " and u.id_workflow=" & id_work
                requete = requete & " and rownum<" & NBTOTAL + 1
                requete = requete & " group by "
                requete = requete & $" U.CODE_SAP,c.repertoire_ia,h.id_envoi,c.id_workflow,c.repertoire_production,c.repertoire_ia,c.nb_par_lot,'\\10.168.94.12\e\SITES\{IIf(id_work = "73", "CBWEBINVOICE_MCDO_CONSTRUCTION", "CBWEBINVOICE")}\portail\'||c.repertoire_production||'\'||f.nom_fichier_new,u.ss_ctrl order by c.id_workflow"
                DataGridView2.DataSource = retourne_resultat_tableau(requete)

                Dim qte_arrivee As Integer = 0

                If nbactuel >= NBTOTAL Then
                    qte_arrivee = NBTOTAL
                Else
                    qte_arrivee = nbactuel
                End If

                Dim LOT As String = ""
                'client sans entite

                Dim TYPE_LOT As String = ""
                Select Case type_traitement
                    Case "MAIL"
                        TYPE_LOT = "MAIL"
                    Case Else
                        TYPE_LOT = "WEB"
                End Select





                Select Case code_client
                    Case "IDG"
                        If entite.Contains("ITALY") Then
                            LOT = EXECUTE_FUNCTION_SQL_creation_lot("CREATION_LOT_COMPOSTAGE", "IDI", TYPE_LOT, "", qte_arrivee)
                            LOT = "000000-" & LOT.PadLeft(6, "0") & "-000000-" & "IDI" & "-" & entite.Replace("-", "").Substring(0, 4) & "-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                        ElseIf entite.Length = 3 Then
                            LOT = EXECUTE_FUNCTION_SQL_creation_lot("CREATION_LOT_COMPOSTAGE", code_client, TYPE_LOT, "", qte_arrivee)
                            LOT = "000000-" & LOT.PadLeft(6, "0") & "-000000-" & code_client & "-" & entite.Replace("-", "").Substring(0, 3) & "-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                        Else
                            LOT = EXECUTE_FUNCTION_SQL_creation_lot("CREATION_LOT_COMPOSTAGE", code_client, TYPE_LOT, "", qte_arrivee)
                            LOT = "000000-" & LOT.PadLeft(6, "0") & "-000000-" & code_client & "-" & entite.Replace("-", "").Substring(0, 4) & "-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                        End If
                    Case "AXE"
                        LOT = EXECUTE_FUNCTION_SQL_creation_lot("CREATION_LOT_COMPOSTAGE", code_client, TYPE_LOT, "AXEO", qte_arrivee)
                        LOT = "000000000-" & LOT.PadLeft(6, "0") & "-000000-" & code_client & "-AXEO-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                        entite = "AXEO"
                    Case "VIN"
                        LOT = EXECUTE_FUNCTION_SQL_creation_lot("CREATION_LOT_COMPOSTAGE", code_client, TYPE_LOT, entite, qte_arrivee)
                        LOT = "000000-" & LOT.PadLeft(6, "0") & "-000000-" & code_client & "-0000-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                        entite = "0000"
                    Case "MCO", "MDC"
                        LOT = EXECUTE_FUNCTION_SQL_creation_lot("CREATION_LOT_COMPOSTAGE", code_client, TYPE_LOT, entite, qte_arrivee)
                        If (id_work = "15") Then
                            LOT = "000000-" & LOT.PadLeft(6, "0") & "-000000-" & code_client & "-MCO-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                            entite = "MCO"
                        Else
                            LOT = "000000-" & LOT.PadLeft(6, "0") & "-000000-" & code_client & "-MCC-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                            entite = "MCC"
                        End If
                    Case Else
                        LOT = EXECUTE_FUNCTION_SQL_creation_lot("CREATION_LOT_COMPOSTAGE", code_client, TYPE_LOT, entite, qte_arrivee)
                        LOT = "000000-" & LOT.PadLeft(6, "0") & "-000000-" & code_client & "-" & entite & "-" & TYPE_LOT & "-" & Date.Now.ToShortDateString.Replace("/", "")
                End Select



                'If id_work = "18" Then
                '    entite = retourne_resultat_code("select code_convergence from societe_safran@lien_bprocess where code_tsa='" & entite & "'")
                'End If



                ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot en cours d import sous IA :" & LOT)
                'Dim lot As String = "test"
                Dim tabimage As New List(Of Info_image)
                Dim tabvaleur As New List(Of String)
                Dim chaine_maj As String = ""
                Dim num_facture As String = ""
                Dim CODE_FOURNISSEUR_SAISIE As String = ""
                Dim CODE_FOURNISSEUR_INSCRIPTION As String = ""
                Dim enti As String = ""
                Dim repia As String = ""
                Dim FACTURE_PAYE As String = ""
                Dim ECHANGE As String = ""
                Dim TITRE As String = ""
                Dim num_commande As String = ""


                For Each ligne_fact As DataGridViewRow In DataGridView2.Rows

                    id = DirectCast(ligne_fact.Cells.Item("idenv"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    If Not IsDBNull(ligne_fact.Cells.Item("num_facture").Value) Then
                        num_facture = DirectCast(ligne_fact.Cells.Item("num_facture"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    Else
                        num_facture = ""

                    End If
                    If Not IsDBNull(ligne_fact.Cells.Item("CODE_FOURNISSEUR_SAISIE").Value) Then
                        CODE_FOURNISSEUR_SAISIE = DirectCast(ligne_fact.Cells.Item("CODE_FOURNISSEUR_SAISIE"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    Else
                        CODE_FOURNISSEUR_SAISIE = ""
                    End If
                    If Not IsDBNull(ligne_fact.Cells.Item("CODE_FOURNISSEUR_INSCRIPTION").Value) Then
                        CODE_FOURNISSEUR_INSCRIPTION = DirectCast(ligne_fact.Cells.Item("CODE_FOURNISSEUR_INSCRIPTION"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    Else
                        CODE_FOURNISSEUR_INSCRIPTION = ""
                    End If
                    ' cas particulier safran qui a des code fournisseurs <> selon les entite
                    If id_work = "18" Then
                        If CODE_FOURNISSEUR_INSCRIPTION <> "" Then
                            Dim req As String = "select distinct code_sap from fournisseur_safran@LIEN_BPROCESS where numero_tva_intraco in (select distinct numero_tva_intraco from fournisseur_safran@LIEN_BPROCESS where "
                            req = req + " code_sap = '" & CODE_FOURNISSEUR_INSCRIPTION & "') and "
                            req = req + " entite in  (select code_convergence from societe_safran@LIEN_BPROCESS where code_tsa='" & entite & "')"
                            'CODE_FOURNISSEUR_INSCRIPTION = retourne_resultat_code(req)
                            If CODE_FOURNISSEUR_INSCRIPTION Is Nothing Then
                                CODE_FOURNISSEUR_INSCRIPTION = ""
                            End If
                        End If
                    End If
                    '----------------------------------------------------------------------
                    If id_work = "508" Then
                        enti = "0000"
                    Else
                        enti = DirectCast(ligne_fact.Cells.Item("entite"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    End If
                    If Not IsDBNull(ligne_fact.Cells.Item("NUM_COMMANDE").Value) Then
                        num_commande = DirectCast(ligne_fact.Cells.Item("NUM_COMMANDE"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    Else
                        num_commande = ""
                    End If

                    If Not IsDBNull(ligne_fact.Cells.Item("TITRE").Value) Then
                        TITRE = DirectCast(ligne_fact.Cells.Item("TITRE"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    Else
                        TITRE = ""
                    End If

                    If Not IsDBNull(ligne_fact.Cells.Item("ECHANGE").Value) Then
                        ECHANGE = DirectCast(ligne_fact.Cells.Item("ECHANGE"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    Else
                        ECHANGE = ""
                    End If

                    If Not IsDBNull(ligne_fact.Cells.Item("FACTURE_PAYE").Value) Then
                        FACTURE_PAYE = DirectCast(ligne_fact.Cells.Item("FACTURE_PAYE"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    Else
                        FACTURE_PAYE = ""
                    End If

                    If repia = "" Then repia = DirectCast(ligne_fact.Cells.Item("repertoire_ia"), System.Windows.Forms.DataGridViewTextBoxCell).Value


                    Dim img As Info_image
                    Dim image_origine As String = DirectCast(ligne_fact.Cells.Item("fichier_source"), System.Windows.Forms.DataGridViewTextBoxCell).Value
                    img.chemin_origine = image_origine

                    If Not image_origine Is Nothing And File.Exists(image_origine) = True Then
                        chaine_maj = chaine_maj & "id_envoi='" & DirectCast(ligne_fact.Cells.Item("idenv"), System.Windows.Forms.DataGridViewTextBoxCell).Value & "' or "
                        img.chemin_destination = repia & image_origine.Split("\")(image_origine.Split("\").Count - 1)
                        img.id = id
                        img.enti = enti
                        img.LOT = LOT
                        img.num_facture = num_facture
                        img.CODE_FOURNISSEUR_SAISIE = CODE_FOURNISSEUR_SAISIE
                        img.CODE_FOURNISSEUR_INSCRIPTION = CODE_FOURNISSEUR_INSCRIPTION

                        '=========================================
                        'Gestion chargement code fournisseur Axeo
                        '=========================================
                        If id_work = "32" AndAlso CODE_FOURNISSEUR_INSCRIPTION = "" Then
                            img.CODE_FOURNISSEUR_INSCRIPTION = CODE_FOURNISSEUR_SAISIE
                        End If

                        img.NUM_COMMANDE = num_commande
                        img.TITRE = TITRE
                        img.ECHANGE = ECHANGE
                        img.FACTURE_PAYE = FACTURE_PAYE

                        tabimage.Add(img)
                    Else
                        If image_origine <> "" Then
                            envoi_mail("lfrackowiak@cba.fr", "document portail non présent", "l'image :" & image_origine & " est inexistante")
                            envoi_mail("crenaud@cba.fr", "document portail non présent", "l'image :" & image_origine & " est inexistante")
                            envoi_mail("rhansch@cba.fr", "document portail non présent", "l'image :" & image_origine & " est inexistante")
                            envoi_mail("lchappotteau@cba.fr", "document portail non présent", "l'image :" & image_origine & " est inexistante")
                        End If

                    End If


                Next



                Select Case id_work
                    Case "18", "118", "509", "932", "432", "500", "3", "506", "508", "32", "62", "61", "67"


                        Dim tabimage_pdf As New List(Of Info_image)
                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id
                            Dim F As StreamWriter = New StreamWriter(racine_pdf & ".txt")

                            Dim chemin_tif As String = ""



                            F.WriteLine(elem.id & ";" & elem.chemin_origine & ";" & elem.LOT & ";" & elem.num_facture & ";" & elem.enti & ";" & IIf(String.IsNullOrEmpty(elem.CODE_FOURNISSEUR_SAISIE), elem.CODE_FOURNISSEUR_INSCRIPTION, elem.CODE_FOURNISSEUR_SAISIE) & ";" & elem.CODE_FOURNISSEUR_INSCRIPTION & ";" & elem.NUM_COMMANDE & ";" & elem.TITRE & ";" & elem.ECHANGE & ";" & elem.FACTURE_PAYE)

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)

                            F.Close()

                        Next



                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine, tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next




                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "57"


                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE
                            tempimg.enti = elem.enti
                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)



                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct (select identifiant from historique where id_envoi=h.id_envoi and id_etat=2), h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine.Replace(" .pdf", ".pdf"), tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            faire_xml_suez(tabimage, repia, LOT, tabimage_pdf)

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "1"


                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE
                            tempimg.enti = elem.enti
                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)



                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct (select identifiant from historique where id_envoi=h.id_envoi and id_etat=2), h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine.Replace(" .pdf", ".pdf"), tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            faire_xml_auchan(tabimage, repia, LOT, tabimage_pdf)

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "55", "3"


                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE
                            tempimg.enti = elem.enti
                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)

                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct (select identifiant from historique where id_envoi=h.id_envoi and id_etat=2), h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine.Replace(" .pdf", ".pdf"), tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            faire_xml_alinea(tabimage, repia, LOT, tabimage_pdf)

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "21"


                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE

                            tempimg.enti = elem.enti

                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)
                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine, tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            faire_xml_idgroup(tabimage, repia, LOT, tabimage_pdf, entite)

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "731"


                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE

                            tempimg.enti = elem.enti

                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)
                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine, tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            faire_xml_total_be(tabimage, repia, LOT, tabimage_pdf, entite)

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "26"


                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE

                            tempimg.enti = elem.enti
                            tempimg.NUM_COMMANDE = elem.NUM_COMMANDE
                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)
                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine, tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            faire_xml_bv(tabimage, repia, LOT, tabimage_pdf, entite)

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "15", "73"
                        Dim tabimage_pdf As New List(Of Info_image)
                        Dim direction As String = ""
                        If (id_work = "73") Then
                            direction = "\\10.94.0.98\Scan\Mc Donald Construction\Output\" & LOT
                        Else
                            direction = repia & "\import pdf\" & LOT
                        End If

                        For Each elem As Info_image In tabimage

                            If Directory.Exists(direction) = False Then
                                Directory.CreateDirectory(direction)
                            End If



                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = ""
                            If (id_work = 73) Then
                                racine_pdf = direction & "\" & elem.id
                            Else
                                racine_pdf = repia & "\import pdf\" & LOT & "\" & elem.id
                            End If


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE
                            tempimg.TITRE = elem.TITRE
                            tempimg.enti = elem.enti
                            tempimg.NUM_COMMANDE = elem.NUM_COMMANDE
                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)
                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                'If (id_work = "73") Then
                                '    tt.chemin_origine = tt.chemin_origine.Replace("CBWEBINVOICE", "CBWEBINVOICE_MCDO_CONSTRUCTION")
                                'End If
                                FileCopy(tt.chemin_origine, tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            If (id_work = "15") Then
                                faire_xml_mcdo(tabimage, repia, LOT, tabimage_pdf, entite)
                            Else
                                faire_xml_mcdo_construction(tabimage, repia, LOT, tabimage_pdf, entite)
                            End If


                            'Dim racine_pdf As String = repia & "\import pdf\" & LOT & ".TRG"
                            Dim racine_pdf As String = direction & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If
                    Case "75"
                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & LOT) = False Then
                                Directory.CreateDirectory(repia & "\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.CODE_FOURNISSEUR_INSCRIPTION = elem.CODE_FOURNISSEUR_INSCRIPTION
                            tempimg.CODE_FOURNISSEUR_SAISIE = elem.CODE_FOURNISSEUR_SAISIE

                            tempimg.enti = elem.enti
                            tempimg.NUM_COMMANDE = elem.NUM_COMMANDE
                            tempimg.num_facture = elem.num_facture
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)
                        Next

                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine, tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            faire_xml_printemps(tabimage, repia, LOT, tabimage_pdf, entite)

                            Dim racine_pdf As String = repia & "\" & LOT & ".TRG"
                            Dim F As StreamWriter = New StreamWriter(racine_pdf)
                            F.Close()

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If

                    Case "99"
                        Dim tabimage_pdf As New List(Of Info_image)

                        For Each elem As Info_image In tabimage
                            If Directory.Exists(repia & "\import pdf\" & LOT) = False Then
                                Directory.CreateDirectory(repia & "\import pdf\" & LOT)
                            End If

                            Dim tempimg As New Info_image

                            Dim racine_pdf As String = repia & "\import pdf\" & LOT & "\" & elem.id


                            Dim chemin_tif As String = ""

                            tempimg.chemin_destination = racine_pdf & ".att"
                            tempimg.chemin_origine = elem.chemin_origine
                            tempimg.id = elem.id
                            tabimage_pdf.Add(tempimg)

                        Next

                        faire_xml_alinea(tabimage, repia, LOT, tabimage_pdf)


                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then

                            For Each tt As Info_image In tabimage_pdf
                                FileCopy(tt.chemin_origine, tt.chemin_destination)
                            Next

                            For Each tt As Info_image In tabimage_pdf
                                Rename(tt.chemin_destination, tt.chemin_destination.Replace(".att", ".pdf"))
                            Next

                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If

                    Case Else
                        Dim desired_x_dpi As Integer = 96
                        Dim desired_y_dpi As Integer = 96
                        Dim lastInstalledVersion As GhostscriptVersionInfo
                        Dim Rasterizer As GhostscriptRasterizer
                        lastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL, GhostscriptLicense.GPL)
                        Rasterizer = New GhostscriptRasterizer()
                        Dim gsProcessor = New GhostscriptProcessor(lastInstalledVersion, True)

                        Dim racine As String = repia & "\lot\" & LOT & enti
                        Dim F As StreamWriter = New StreamWriter(racine & ".txt")
                        Dim tabimage_att As New List(Of Image)
                        Dim tabimage_pdf As New List(Of String)
                        For Each elem As Info_image In tabimage

                            'information_pdf(elem.chemin_origine.ToString)
                            Dim chemin_tif As String = ""
                            chemin_tif = Application.StartupPath & "\temp_image\" & elem.chemin_origine.ToString.Split("\")(elem.chemin_origine.ToString.Split("\").Count - 1) & ".tif"



                            If Not File.Exists(chemin_tif) Then

                                Try
                                    Dim switches = GetGsSwitches(elem.chemin_origine.ToString, chemin_tif)
                                    gsProcessor.StartProcessing(switches.ToArray, Nothing)

                                Catch
                                    transformation_tiff(elem.chemin_origine.ToString)
                                End Try



                            End If


                            'RunGS("-q", "-dNOPAUSE", "-dBATCH", "-dSAFER", "-sCompression=g4", "-sDEVICE=tiffg4", "-r200", "-sOutputFile=" & elem.chemin_destination.ToString & ".tif", elem.chemin_origine.ToString)
                            Dim ATT As Image = Image.FromFile(chemin_tif)
                            Dim fd1 As System.Drawing.Imaging.FrameDimension = New System.Drawing.Imaging.FrameDimension(ATT.FrameDimensionsList(0))

                            For t = 0 To ATT.GetFrameCount(fd1) - 1
                                F.WriteLine(elem.id & ";" & elem.chemin_origine & ";" & elem.LOT & ";" & elem.num_facture & ";" & elem.enti & ";" & elem.CODE_FOURNISSEUR_SAISIE & ";" & elem.CODE_FOURNISSEUR_INSCRIPTION)
                            Next t



                            tabimage_att.Add(ATT)


                        Next

                        F.Close()

                        SaveMultiTiffnew(tabimage_att.ToArray, racine & ".att")
                        If execute_requette("insert into historique (identifiant,id_envoi,id_etat,lot_compostage) select distinct h.identifiant, h.ID_ENVOI,3,'" & LOT & "' from historique h where " & chaine_maj & " identifiant='" & id & "'") Then


                            Rename(racine & ".att", racine & ".tif")
                            ecriture_log("trace", "[" & DateTime.Now.ToString() & "] lot exporté sous IA :" & LOT)
                        End If

                End Select


                Timer1.Enabled = True
                Exit Sub
            End If

        Next





        Timer1.Enabled = True
        'ecriture_log("erreur", Err.Description)


        'ecriture_log("trace", "affichage des factures en attente d'integration " & retourne_resultat_tableau(requete).Rows.Count)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Timer1.Enabled = False
        ecriture_log("trace", "[" & DateTime.Now.ToString() & "] Mise en pause de l'application")
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Timer1.Enabled = True
        ecriture_log("trace", "[" & DateTime.Now.ToString() & "] Activation de l'application")
    End Sub

    Private Sub txtlog_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtlog.TextChanged

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
