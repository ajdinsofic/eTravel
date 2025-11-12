using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace eTravelAgencija.Model.model
{
    public partial class HotelRooms
    {
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int RoomsLeft { get; set; }
    }
}
