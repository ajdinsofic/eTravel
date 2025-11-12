

namespace eTravelAgencija.Model.SearchObjects
{
    public class HotelRoomSearchObject : BaseSearchObject
    {
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }
        public int? OfferId { get; set; }
        public string? RoomType { get; set; }
        public int? MinRoomsLeft { get; set; }
        public int? MaxRoomsLeft { get; set; }
    }
}
