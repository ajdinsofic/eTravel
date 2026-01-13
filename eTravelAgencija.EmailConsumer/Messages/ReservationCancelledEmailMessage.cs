namespace eTravelAgencija.EmailConsumer.Messages; // ili eTravelAgencija.Services.Messages (kako ti je već)

public class ReservationCancelledEmailMessage
{
    public string To { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int ReservationId { get; set; }
    public string DestinationName { get; set; } = default!;
    public string CancelReason { get; set; } = default!;
    public bool VoucherUsed { get; set; }
    public decimal PaidAmount { get; set; }
    public string AgencyName { get; set; } = "eTravel";
    public string Phone { get; set; } = "☎ +387 61 123 456";
}
