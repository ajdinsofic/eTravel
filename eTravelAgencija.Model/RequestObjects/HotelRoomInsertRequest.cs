using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class HotelRoomInsertRequest
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int RoomsLeft { get; set; }
    }
}