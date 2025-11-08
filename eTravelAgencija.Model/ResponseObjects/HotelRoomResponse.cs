using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class HotelRoomResponse
    {
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public int RoomsLeft { get; set; }
        public decimal RoomPrice { get; set; }
    }
}