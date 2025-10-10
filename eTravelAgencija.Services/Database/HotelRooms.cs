namespace eTravelAgencija.Services.Database
{
    public class HotelRooms
    {
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int RoomId { get; set; }
        public Rooms Rooms { get; set; }
        public string RoomsLeft { get; set; }
    }
}