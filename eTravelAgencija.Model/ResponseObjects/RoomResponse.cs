using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class RoomResponse
    {
        public string RoomType { get; set; }  // npr. "Dvokrevetna"
        public int RoomsLeft { get; set; }
    }

}