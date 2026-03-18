Public Class FormCertificatePreview

    Private _pendingHtml As String = ""
    ' This is the main function that receives the data
    Public Sub PopulateCertificate(ByVal certType As String, ByVal fullName As String, ByVal address As String, ByVal purpose As String, ByVal controlNumber As String, ByVal dateIssued As String, ByVal captainName As String)

        ' 1. Set up the CSS to make it look like a formal document
        Dim css As String = "
            <style>
                body { font-family: 'Arial', sans-serif; margin: 40px; background-color: white; color: black; }
                .header { text-align: center; margin-bottom: 40px; }
                .title { text-align: center; font-size: 24px; font-weight: bold; text-decoration: underline; margin-bottom: 30px; }
                .content { text-align: justify; font-size: 16px; line-height: 1.8; }
                .signature-block { margin-top: 80px; text-align: right; float: right; width: 300px; }
                .signature-line { border-bottom: 1px solid black; text-align: center; font-weight: bold; font-size: 18px; }
                .footer { margin-top: 100px; font-size: 12px; color: gray; clear: both; }
            </style>"

        ' 2. Generate the dynamic paragraphs based on the Certificate Type
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
        End Select

        ' 3. Put it all together into one massive HTML String!
        Dim fullHtml As String = $"
            <html>
            <head>{css}</head>
            <body>
                <div class='header'>
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

        ' 4. Tell WebView2 to display our HTML!
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
End Class