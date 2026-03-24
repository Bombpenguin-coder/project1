Public Class FormCertificatePreview

    Private _pendingHtml As String = ""
    ' This is the main function that receives the data
    Public Sub PopulateCertificate(ByVal certType As String, ByVal fullName As String, ByVal address As String, ByVal purpose As String, ByVal controlNumber As String, ByVal dateIssued As String, ByVal captainName As String)

        ' 1. Steal the logo from our hidden PictureBox!
        Dim logoHtml As String = GetBase64Logo(picLogo.Image)

        ' 2. Set up the CSS optimized for A4 Paper Printing
        Dim css As String = "
            <style>
                /* THIS MAGICAL LINE HIDES THE URL AND DATE HEADERS! */
                @page { size: A4; margin: 0mm; } 
                
                /* Set up the margins exactly like a Microsoft Word document */
                body { 
                    font-family: 'Arial', sans-serif; 
                    background-color: white; 
                    color: black; 
                    margin: 25mm 25mm 25mm 25mm; /* Top, Right, Bottom, Left */
                }
                
                /* Make the text larger and spaced out to fill the paper */
                .header { text-align: center; margin-bottom: 50px; }
                .header h3 { font-size: 22px; margin: 5px; }
                .header h4 { font-size: 18px; margin: 5px; font-weight: normal; }
                
                .title { text-align: center; font-size: 32px; font-weight: bold; text-decoration: underline; margin-top: 60px; margin-bottom: 60px; }
                
                .content { text-align: justify; font-size: 20px; line-height: 2.2; }
                
                /* Push the signature block down and make it bigger */
                .signature-block { margin-top: 150px; text-align: center; float: right; width: 350px; }
                .signature-line { border-bottom: 1px solid black; font-weight: bold; font-size: 22px; padding-bottom: 5px; }
                
                /* Push the control number to the absolute bottom */
                .footer { margin-top: 250px; font-size: 14px; color: dimgray; clear: both; }
            </style>"

        ' 3. Generate the dynamic paragraphs based on the Certificate Type
        Dim bodyParagraphs As String = ""

        Select Case certType
            Case "Barangay Clearance"
                bodyParagraphs = $"
                    <p><strong>TO WHOM IT MAY CONCERN:</strong></p>
                    <p>This is to certify that <strong>{fullName.ToUpper()}</strong>, of legal age, is a bona fide resident of this barangay and is known to me to be a peaceful and law-abiding citizen with good moral character.</p>
                    <p>This certification is being issued upon the request of the above-named person for the purpose of: <strong>{purpose}</strong>.</p>"

            Case "Certificate of Indigency"
                bodyParagraphs = $"
                    <p><strong>TO WHOM IT MAY CONCERN:</strong></p>
                    <p>This is to certify that <strong>{fullName.ToUpper()}</strong>, of legal age, is a recognized and bona fide resident of this barangay.</p>
                    <p>It is further certified that the above-named resident belongs to an indigent family in this community whose combined income is not sufficient to support their basic needs.</p>
                    <p>This certification is issued upon the request of the interested party for the purpose of: <strong>{purpose}</strong>.</p>"

            Case "Certificate of Residency"
                bodyParagraphs = $"
                    <p><strong>TO WHOM IT MAY CONCERN:</strong></p>
                    <p>This is to certify that <strong>{fullName.ToUpper()}</strong>, of legal age, whose signature appears below, is a permanent and bona fide resident of this barangay.</p>
                    <p>Based on the records of this office, he/she has been residing at {address}.</p>
                    <p>This certification is issued upon his/her request for the purpose of: <strong>{purpose}</strong>.</p>"

            Case Else
                ' THE UNIVERSAL FALLBACK TEMPLATE
                bodyParagraphs = $"
                    <p><strong>TO WHOM IT MAY CONCERN:</strong></p>
                    <p>This is to certify that <strong>{fullName.ToUpper()}</strong>, of legal age, is a recognized and bona fide resident of this barangay.</p>
                    <p>This official document is generated and issued upon the request of the interested party for the purpose of: <strong>{purpose}</strong>.</p>
                    <p>Any further details or specific verifications regarding this newly requested document type can be confirmed directly with the Barangay Office records.</p>"
        End Select

        ' 4. Put it all together into one massive HTML String!
        Dim fullHtml As String = $"
            <html>
            <head>{css}</head>
            <body>
                <div class='header' style='position: relative;'>
                    <img src='{logoHtml}' style='width: 120px; height: 120px; position: absolute; left: 0px; top: 0px;' />
                    
                    <h3>Republic of the Philippines</h3>
                    <h4>Office of the Punong Barangay</h4>
                </div>

                <div class='title'>{certType.ToUpper()}</div>

                <div class='content'>
                    {bodyParagraphs}
                    <p>Issued this <strong>{dateIssued}</strong>.</p>
                </div>

                <div class='signature-block'>
                    <div class='signature-line'>{captainName.ToUpper()}</div>
                    <div style='text-align: center;'>Punong Barangay</div>
                </div>

                <div class='footer'>
                    Control Number: {controlNumber} <br>
                    <i>* Valid for 6 months from the date of issuance. *</i>
                </div>
            </body>
            </html>"

        ' 5. Tell WebView2 to display our HTML!
        _pendingHtml = fullHtml
    End Sub
    ' 1. INITIALIZE WEBVIEW2
    ' It takes a split second to load the Chromium engine, so we use "Async" and "Await"
    Private Async Sub FormCertificatePreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Wait for the Chromium engine to turn on
        Await wbPreview.EnsureCoreWebView2Async(Nothing)

        ' 2. Now that it is fully awake, inject the HTML!
        If Not String.IsNullOrEmpty(_pendingHtml) Then
            wbPreview.NavigateToString(_pendingHtml)
        End If
    End Sub

    ' 2. THE NEW PRINT BUTTON
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        ' Open the modern Edge Print Dialog!
        If wbPreview.CoreWebView2 IsNot Nothing Then
            wbPreview.CoreWebView2.ShowPrintUI()
        End If
    End Sub

    ' Converts your saved image into a string that HTML can read!
    Private Function GetBase64Logo(img As Image) As String
        Using ms As New System.IO.MemoryStream()
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            Return "data:image/png;base64," & Convert.ToBase64String(ms.ToArray())
        End Using
    End Function
End Class