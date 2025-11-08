using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class HotelRoomUpsertResponse
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string RoomType { get; set; }
        public int RoomsLeft { get; set; }
    }

}