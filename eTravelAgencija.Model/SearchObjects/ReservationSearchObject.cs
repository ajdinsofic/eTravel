using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class ReservationSearchObject : BaseSearchObject
    {
        public int? UserId { get; set; }
        public bool isActive { get; set; }

    }
}