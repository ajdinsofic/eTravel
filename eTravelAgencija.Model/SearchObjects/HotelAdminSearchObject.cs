using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class HotelAdminSearchObject : BaseSearchObject
    {
        [Required]
        public int OfferId { get; set; }
    }
}