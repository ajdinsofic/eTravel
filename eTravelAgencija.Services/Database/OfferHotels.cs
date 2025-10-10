namespace eTravelAgencija.Services.Database
{ 
    public class OfferHotels
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }

}