namespace eTravelAgencija.Services.Database
{
    public class HotelImages
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string ImageUrl { get; set; }
        
    }
}