using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace eTravelAgencija.Services.Database
{
    public class HotelRooms
    {
        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        [ForeignKey(nameof(Rooms))]
        public int RoomId { get; set; }
        public Rooms Rooms { get; set; }
        public int RoomsLeft { get; set; }
    }
}
