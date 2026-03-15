Public Class Reservation
    Public Property Id As Integer
    Public Property ResidentId As Integer      ' Will be 0 if it's a non-resident
    Public Property IsResident As Boolean      ' NEW: Resident confirmation
    Public Property ReserverName As String     ' NEW: Indicate who reserved
    Public Property FacilityName As String
    Public Property EventName As String
    Public Property StartDateTime As DateTime
    Public Property EndDateTime As DateTime
    Public Property InCharge As String         ' NEW: Name of in-charge
End Class