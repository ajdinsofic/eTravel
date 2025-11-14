using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class HotelUpsertRequest
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public int Stars { get; set; }

    }
}