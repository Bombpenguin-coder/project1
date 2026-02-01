Public Class FormCertificatePreview

    ' This is the main function that receives the data
    Public Sub PopulateCertificate(ByVal certType As String,
                                   ByVal fullName As String,
                                   ByVal address As String,
                                   ByVal purpose As String,
                                   ByVal controlNumber As String,
                                   ByVal dateIssued As String,
                                   ByVal captainName As String)

        ' 1. Fill in the data that's always the same
        lblFullName.Text = fullName
        lblAddress.Text = address
        lblPurpose.Text = purpose
        lblControlNumber.Text = controlNumber
        lblDateIssued.Text = dateIssued
        lblCaptainName.Text = captainName

        ' 2. Call our new function to set the template text
        UpdateTemplate(certType, fullName, address)
    End Sub

    ' This new function changes the template based on the certType
    Private Sub UpdateTemplate(ByVal certType As String, ByVal fullName As String, ByVal address As String)

        ' --- This is where we set the text for the labels we just created ---

        Select Case certType
            Case "Barangay Clearance"
                lblTitle.Text = "BARANGAY CLEARANCE"
                lblBodyText1.Text = "This is to certify that"
                ' We'll put the name and address labels between BodyText1 and BodyText2
                lblBodyText2.Text = "is a resident of this Barangay and is a person of good moral character." &
                                    vbCrLf & vbCrLf & "This clearance is issued for the purpose of:"

            Case "Certificate of Indigency"
                lblTitle.Text = "CERTIFICATE OF INDIGENCY"
                lblBodyText1.Text = "This is to certify that"
                lblBodyText2.Text = "is a bona fide resident of this Barangay and belongs to a low-income family." &
                                    vbCrLf & vbCrLf & "This certification is issued upon the request of the interested party for:"

            Case "Certificate of Residency"
                lblTitle.Text = "CERTIFICATE OF RESIDENCY"
                lblBodyText1.Text = "This is to certify that"
                lblBodyText2.Text = "is a bona fide resident of this Barangay, residing at the address listed above." &
                                    vbCrLf & vbCrLf & "This certification is issued upon his/her request for:"

                ' You can add more cases here for "Business Clearance Endorsement", etc.
            Case Else
                ' A default message if the type is unknown
                lblTitle.Text = "DOCUMENT PREVIEW"
                lblBodyText1.Text = "This document type does not have a template."
                lblBodyText2.Text = "Purpose:"
        End Select

        ' --- This part arranges the labels ---
        ' We dynamically move the labels to flow like a real document.
        ' (You may need to adjust the 'Top + 30' numbers to look good on your form)

        ' Position Name right below BodyText1
        lblFullName.Top = lblBodyText1.Top + 30

        ' Position Address right below Name
        lblAddress.Top = lblFullName.Top + 30

        ' Position BodyText2 right below Address
        lblBodyText2.Top = lblAddress.Top + 30

        ' Position Purpose right below BodyText2
        lblPurpose.Top = lblBodyText2.Top + 30

    End Sub

    Private Sub lblFullName_Click(sender As Object, e As EventArgs) Handles lblFullName.Click

    End Sub

    Private Sub lblPurpose_Click(sender As Object, e As EventArgs) Handles lblPurpose.Click

    End Sub
End Class