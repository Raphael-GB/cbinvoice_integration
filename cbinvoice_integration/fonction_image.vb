Imports System.IO
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging

'Imports Pdftools.Pdf

Module fonction_image

    Public Function GetGsSwitches(inputFile As String, outputFile As String) As List(Of String)

        Dim switches As New List(Of String)

        'switches.Add("-sDEVICE=pdfwrite")
        switches.Add("-sCompression=g4")
        switches.Add("-sDEVICE=tiffg4")
        switches.Add("-dNOPAUSE")
        switches.Add("-dQUIET")
        switches.Add("-r200")
        switches.Add("-dBATCH")
        switches.Add("-sOutputFile=" + outputFile)
        switches.Add(inputFile)
        'switches.Add("-sFONTPATH=%windir%/fonts")
        'switches.Add("-dEmbedAllFonts=true")

        Return switches
    End Function
    Public Function information_pdf(ficname As String) As Boolean
        Dim pdf As New pdflib.ClsPDF(ficname)
        information_pdf = True
        Console.WriteLine("Information pour {0}", pdf.FileInfo.FullName)
        Console.WriteLine("  {0}", pdf.Version)
        Console.WriteLine("  {0} pages", pdf.PageCount)

        If pdf.IsLinearized Then
            Console.WriteLine("  est linearisable")
            information_pdf = False
        End If

        If pdf.IsEncrypt Then
            Console.WriteLine("  encryté")
            Console.WriteLine("    Filter:             {0}", pdf.EncryptionInformation.Filter)
            information_pdf = False
        End If

        Console.WriteLine("  pdf information:")
        Console.WriteLine("    titre:             {0}", pdf.DocumentInformation.Title)
        Console.WriteLine("    auteur:            {0}", pdf.DocumentInformation.Author)
        Console.WriteLine("    subjet:            {0}", pdf.DocumentInformation.Subject)
        Console.WriteLine("    createur:          {0}", pdf.DocumentInformation.Creator)
        Console.WriteLine("    date creation:     {0}", pdf.DocumentInformation.CreationDate)
        Console.WriteLine("    Date modification: {0}", pdf.DocumentInformation.GetModificationDate)
        Return information_pdf
    End Function

    Public Function extract_image_to_pdf(fichierpdf As String)

        Dim count As Integer
        Dim imagelist As New List(Of clsPDFImage)
        Dim image As clsPDFImage
        Dim page As clsPDFPage

        fichierpdf = "c:\2.pdf"
        If information_pdf(fichierpdf) = False Then
            MessageBox.Show("fichier protégé et impossible de le concatener")
            Exit Function
        End If
        Dim myPDF As New ClsPDF(fichierpdf)

        imagelist = myPDF.GetImages

        'We save them
        count = 0
        For Each image In imagelist
            count += 1
            image.Save("C:\Test\pdf_images\" + "_image_" + count.ToString)
        Next

        'Get Images from page 1 only
        page = myPDF.GetPage(1)
        imagelist = page.GetImages

        myPDF.close()

    End Function

    Public Function transformation_tiff(nomfic As String) As String


        Dim CONV As New Pdf2Img.Converter
        CONV.Compression = PDFCompression.eComprGroup3



        Dim depart As Date = Date.Now

        Dim pathNew As String = Application.StartupPath & "\temp_image\" & nomfic.Split("\")(nomfic.Split("\").Count - 1).Replace(".PDF", "") & ".tif"

        CONV.ConvertFile(nomfic, pathNew, "")

        Return pathNew

    End Function
    Public Function decoupe_img(nomfic As String, offset_depart As Double, offset_longueur As Double, index As String) As System.Drawing.Image

        Dim imgtemp As System.Drawing.Image

        Dim pathSource As String = nomfic
        Dim pathNew As String = Application.StartupPath & "\temp_image\" & nomfic.Split("\")(nomfic.Split("\").Count - 1).Replace(".img", "") & "_" & index & ".tif"
        Try
            Using fsSource As FileStream = New FileStream(pathSource, FileMode.Open, FileAccess.Read)
                ' Read the source file into a byte array.
                Dim bytes() As Byte = New Byte(offset_longueur) {}
                Dim numBytesToRead As Integer = CType(offset_longueur, Integer)
                Dim numBytesRead As Integer = 0
                If numBytesRead < 0 Then numBytesRead = 0
                fsSource.Seek(offset_depart, SeekOrigin.Begin)
                While (numBytesToRead > 0)

                    Dim n As Integer = fsSource.Read(bytes, numBytesRead, numBytesToRead)
                    If (n = 0) Then
                        Exit While
                    End If
                    numBytesRead = (numBytesRead + n)
                    numBytesToRead = (numBytesToRead - n)
                End While
                numBytesToRead = bytes.Length

                Using fsNew As FileStream = New FileStream(pathNew, FileMode.Create, FileAccess.Write)
                    fsNew.Write(bytes, 0, numBytesToRead)
                End Using
            End Using


            Return image.FromFile(pathNew)



        Catch ioEx As FileNotFoundException
            Console.WriteLine(ioEx.Message)
        End Try
    End Function

    Sub SaveAddTiff(ByVal img As Image, ByVal filename As String)
        If Not IO.File.Exists(filename) Then
            Dim codec As ImageCodecInfo = GetEncoderInfo("image/tiff")
            Dim enc As Encoder = Encoder.SaveFlag
            Dim ep As New EncoderParameters(2)
            ep.Param(0) = New EncoderParameter(enc, CLng(EncoderValue.MultiFrame))
            ep.Param(1) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionLZW)) '
            Dim tiff As System.Drawing.Image = img





            tiff.Save(filename, codec, ep)
            'img.Save(filename, Imaging.ImageFormat.Tiff)
        Else


            Dim frames As List(Of Image) = getFrames(filename)
            frames.Add(img)
            SaveMultiTiff(frames.ToArray, filename)
        End If
        img.Dispose()
    End Sub
    'Private Function GetEncoderInfo(ByVal mimeType As String) As ImageCodecInfo
    '    Dim j As Integer
    '    Dim encoders() As ImageCodecInfo
    '    encoders = ImageCodecInfo.GetImageEncoders()

    '    j = 0
    '    While j < encoders.Length
    '        If encoders(j).MimeType = mimeType Then
    '            Return encoders(j)
    '        End If
    '        j += 1
    '    End While
    '    Return Nothing

    'End Function 'GetEncoderInfo
    Sub SaveMultiTiff(ByVal frames() As Image, ByVal filename As String)




        Dim codec As ImageCodecInfo = GetEncoderInfo("image/tiff")
        Dim enc As Encoder = Encoder.SaveFlag
        Dim ep As New EncoderParameters(2)
        ep.Param(0) = New EncoderParameter(enc, CLng(EncoderValue.MultiFrame))
        ep.Param(1) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionLZW)) '
        Dim tiff As System.Drawing.Image = frames(0)





        For i As Integer = 0 To frames.Length - 1
            If i = 0 Then
                tiff.Save(filename, codec, ep)

            Else
                Dim ep1 As New EncoderParameters(2)
                ep1.Param(1) = New EncoderParameter(enc, CLng(EncoderValue.FrameDimensionPage))
                ep1.Param(0) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionLZW))
                tiff.SaveAdd(frames(i), ep1)
            End If

            frames(i).Dispose()
        Next
        ep.Param(0) = New EncoderParameter(enc, CLng(EncoderValue.Flush))
        tiff.SaveAdd(ep)
        tiff.Dispose()
    End Sub

    Function getTiffCodec() As ImageCodecInfo
        For Each ice As ImageCodecInfo In ImageCodecInfo.GetImageEncoders()
            If ice.MimeType = "image/tiff" Then
                Return ice
            End If
        Next
        Return Nothing
    End Function

    Function getFrames(ByVal filename) As List(Of Image)
        Dim frames As New List(Of Image)
        Dim img As Image = image.FromFile(filename)
        For i As Integer = 0 To img.GetFrameCount(Imaging.FrameDimension.Page) - 1
            img.SelectActiveFrame(Imaging.FrameDimension.Page, i)
            Dim tmp As New Bitmap(img.Width, img.Height)
            Dim g As Graphics = Graphics.FromImage(tmp)
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit
            g.DrawImageUnscaled(img, 0, 0)
            frames.Add(tmp)
            g.Dispose()
        Next
        img.Dispose()
        Return frames
    End Function



    Private Function GetEncoderInfo(ByVal mimeType As [String]) As ImageCodecInfo
        Dim i As Integer
        Dim encoders() As ImageCodecInfo
        encoders = ImageCodecInfo.GetImageEncoders()

        For i = 0 To (encoders.Length - 1)
            If (encoders(i).MimeType = mimeType) Then
                Return encoders(i)
            End If
        Next i
    End Function

    Sub SaveMultiTiffnew(ByVal frames() As Image, ByVal filename As String)

        Dim img As System.Drawing.Image
        Dim img1, img2, imgBoth As Bitmap

        'Dim img2Path As String = "C:\two.tiff"
        Dim imgBothPath As String = filename

        Dim imgPages As Int16 ' page count for image1
        img = frames(0) ' grab first tiff

        Dim fd As System.Drawing.Imaging.FrameDimension = New System.Drawing.Imaging.FrameDimension(img.FrameDimensionsList(0))
        imgPages = (img.GetFrameCount(fd)) ' set page count

        'img2 = Image.FromFile(img2Path) 'grab second image, assumming it has only 1 page
        imgBoth = New Bitmap(img)

        ' Create an EncoderParameters object.
        Dim encParams As New EncoderParameters(1)
        Dim encpar As New EncoderParameters(2)

        ' Get an ImageCodecInfo object that represents the TIFF codec.
        Dim codecInfo As ImageCodecInfo = GetEncoderInfo("image/tiff")
        'set the type of tiff


        encParams.Param(0) = New EncoderParameter(Encoder.SaveFlag, CLng(EncoderValue.MultiFrame))
        'encpar.Param(1) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionCCITT4)) '

        imgBoth.Save(imgBothPath, codecInfo, encParams) ' save first page which is 0


        ' append new page to tiff

        encpar.Param(0) = New EncoderParameter(Encoder.SaveFlag, CLng(EncoderValue.FrameDimensionPage))
        encpar.Param(1) = New EncoderParameter(Encoder.Compression, CLng(EncoderValue.CompressionCCITT4)) '

        For j = 0 To frames.Count - 1
            Dim imgactuelle As Bitmap = frames(j)
            Dim fd1 As System.Drawing.Imaging.FrameDimension = New System.Drawing.Imaging.FrameDimension(imgactuelle.FrameDimensionsList(0))
            Dim imgPages1 As Integer = (imgactuelle.GetFrameCount(fd1)) ' set page count

            For jJ = 0 To imgPages1 - 1
                If j = 0 And jJ = 0 Then

                Else
                    imgactuelle.SelectActiveFrame(fd1, jJ)
                    imgBoth.SaveAdd(imgactuelle, encpar)
                End If

            Next jJ
        Next j

            'imgBoth.SaveAdd(frames(2), encParams)

            ' Close the multiple-frame file.
            encParams.Param(0) = New EncoderParameter(Encoder.SaveFlag, (CLng(EncoderValue.Flush)))
            imgBoth.SaveAdd(encParams)

            img.Dispose()
            ' img1.Dispose()
            'img2.Dispose()

    End Sub

    Public MyGhostPath As String
    Public Function SILENT_GHOST_PNG_THESE_PAGES( _
                            ByVal mInFile As String, _
                            ByVal mOutFile As String, _
                            ByVal BeginRange As Integer, _
                            ByVal EndRange As Integer, _
                            ByRef m_ErrMsg As String, _
                            Optional ByVal PNum_PNGS As Boolean = True, _
                            Optional ByVal mDpi As Integer = 72) As Boolean




        Try
            Dim MyStandardOutput As String
            Dim MyStandardError As String

            Dim LowRange As String
            Dim HighRange As String

            'assume user knows what he's doing.
            'ie...        BeginRange <=  EndRange & Nonzero

            If PNum_PNGS = True Then
                mOutFile = mOutFile & ".%00000d.png"
            End If

            LowRange = " -dFirstPage=" & BeginRange
            HighRange = " -dLastPage=" & EndRange

            m_ErrMsg = ""
            'MessageBox.Show("a")
            Dim MyP As New System.Diagnostics.Process()

            MyP.StartInfo.FileName = MyGhostPath '"C:\Program Files\GhostScript\gs8.14\bin\gswin32c.exe"
            MyStandardError = ""
            MyStandardOutput = ""
            MyP.StartInfo.Arguments = " -dSAFER -dBATCH -dNOPAUSE -r" & mDpi & _
                        LowRange & HighRange & " -sDEVICE=png256 -dTextAlphaBits=4 -sOutputFile=" _
                        & mOutFile & " " & mInFile

            MyP.StartInfo.UseShellExecute = False
            MyP.StartInfo.RedirectStandardOutput = True
            MyP.StartInfo.RedirectStandardError = True
            MyP.StartInfo.CreateNoWindow = True
            MyP.Start()
            MyStandardOutput = MyP.StandardOutput.ReadToEnd
            MyStandardError = MyP.StandardError.ReadToEnd

            MyP.WaitForExit()
            MyP.Dispose()

            'MessageBox.Show(MyStandardOutput & vbCrLf & MyStandardError)
            'MessageBox.Show(MyStandardOutput & vbCrLf & MyStandardError)
            If MyStandardError <> "" Then
                m_ErrMsg = MyStandardError & vbCrLf & MyStandardOutput
                Return False
            End If
            'MsgBox(MyStandardOutput & vbCrLf & mOutFile)
            Return True
        Catch ex As Exception
            m_ErrMsg = ex.Message
            'MessageBox.Show("AAAA" & m_ErrMsg)
            Return False
        End Try


    End Function

End Module
