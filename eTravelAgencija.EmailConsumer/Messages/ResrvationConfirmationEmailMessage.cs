namespace eTravelAgencija.EmailConsumer.Messages

{
    public class ReservationConfirmationEmailMessage
    {
        public string To { get; set; } = "";
        public string UserName { get; set; } = "";

        public string OfferName { get; set; } = "";
        public string HotelName { get; set; } = "";
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }

        public string ReservationNumber { get; set; } = "";
    }

}
