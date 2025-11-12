using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public partial class Room
    {
        public int Id { get; set; }

        public string RoomType { get; set; }

        public int RoomCount { get; set; }
    }
}