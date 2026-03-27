Imports Microsoft.Web.WebView2.Core

Public Class FormBlotterPreview
    Private _pendingHtml As String = ""

    Public Sub PopulateBlotterReport(ByVal caseId As String, ByVal dateRecorded As String, ByVal timeRecorded As String, ByVal complainant As String, ByVal respondent As String, ByVal incidentType As String, ByVal location As String, ByVal narrative As String, ByVal status As String, ByVal captainName As String, ByVal brgyName As String, ByVal cityName As String, ByVal provName As String)

        ' 1. Steal the logo from the hidden PictureBox!
        Dim logoHtml As String = GetBase64Logo(picLogo.Image)

        ' 2. CSS optimized for A4 Paper
        Dim css As String = "
            <style>
                @page { size: A4; margin: 0mm; } 
                body { font-family: 'Arial', sans-serif; background-color: white; color: black; margin: 25mm; }
                .header { text-align: center; margin-bottom: 40px; }
                .header h3, .header h4 { margin: 5px; font-weight: normal; }
                .title { text-align: center; font-size: 24px; font-weight: bold; text-decoration: underline; margin-bottom: 30px; }
                .content { font-size: 16px; line-height: 1.6; text-align: justify; }
                
                /* The grid for the case details */
                .details-table { width: 100%; border-collapse: collapse; margin-bottom: 20px; }
                .details-table td { padding: 8px; border-bottom: 1px solid #ddd; }
                .details-label { font-weight: bold; width: 30%; }
                
                .narrative-box { border: 1px solid black; padding: 15px; min-height: 150px; margin-top: 10px; background-color: #fafafa; }
                
                .signature-block { margin-top: 80px; text-align: center; float: right; width: 300px; }
                .signature-line { border-bottom: 1px solid black; font-weight: bold; font-size: 18px; padding-bottom: 5px; }
            </style>"

        ' 3. The HTML Layout for an Official Blotter Extract
        Dim bodyHtml As String = $"
            <p><strong>TO WHOM IT MAY CONCERN:</strong></p>
            <p>This is to certify that the following is a true and correct extract from the official Barangay Blotter Book of this office.</p>
            
            <table class='details-table'>
                <tr><td class='details-label'>Blotter Entry / Case No:</td><td>{caseId.PadLeft(4, "0"c)}</td></tr>
                <tr><td class='details-label'>Date Recorded:</td><td>{dateRecorded}</td></tr>
                <tr><td class='details-label'>Time Recorded:</td><td>{timeRecorded}</td></tr>
                <tr><td class='details-label'>Incident Type:</td><td><strong>{incidentType.ToUpper()}</strong></td></tr>
                <tr><td class='details-label'>Complainant:</td><td>{complainant.ToUpper()}</td></tr>
                <tr><td class='details-label'>Respondent:</td><td>{respondent.ToUpper()}</td></tr>
                <tr><td class='details-label'>Place of Incident:</td><td>{location}</td></tr>
                <tr><td class='details-label'>Current Status:</td><td><strong>{status.ToUpper()}</strong></td></tr>
            </table>

            <h4 style='margin-bottom: 0px;'>INCIDENT NARRATIVE:</h4>
            <div class='narrative-box'>
                {narrative}
            </div>"

        ' 4. Put it all together into the Master String
        Dim fullHtml As String = $"
            <html>
            <head>{css}</head>
            <body>
                <div class='header' style='position: relative;'>
                    <img src='{logoHtml}' style='width: 100px; height: 100px; position: absolute; left: 0px; top: 0px;' />
                    <h4>Republic of the Philippines</h4>
                    <h4>Province of {provName}</h4>
                    <h4>City of {cityName}</h4>
                    <h3 style='margin-top: 10px; font-weight: bold;'>BARANGAY {brgyName.ToUpper()}</h3>
                    <h4>OFFICE OF THE PUNONG BARANGAY</h4>
                </div>

                <div class='title'>CERTIFICATION OF BLOTTER EXTRACT</div>

                <div class='content'>
                    {bodyHtml}
                </div>

                <div class='signature-block'>
                    <div class='signature-line'>{captainName.ToUpper()}</div>
                    <div style='text-align: center;'>Punong Barangay</div>
                </div>
            </body>
            </html>"

        ' 5. Pass it to WebView2
        _pendingHtml = fullHtml
    End Sub

    Private Async Sub FormBlotterPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await wbPreview.EnsureCoreWebView2Async(Nothing)
        If Not String.IsNullOrEmpty(_pendingHtml) Then
            wbPreview.NavigateToString(_pendingHtml)
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If wbPreview.CoreWebView2 IsNot Nothing Then
            wbPreview.CoreWebView2.ShowPrintUI()
        End If
    End Sub

    Private Function GetBase64Logo(img As Image) As String
        Using ms As New System.IO.MemoryStream()
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            Return "data:image/png;base64," & Convert.ToBase64String(ms.ToArray())
        End Using
    End Function
End Class