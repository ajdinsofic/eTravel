using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;

namespace eTravelAgencija.Model.RequestObjects
{
    public class HotelUpsertRequest
    {
        [Required]
        public int OfferId { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int Stars { get; set; }

        public DateTime DepartureDate { get; set; } // Ovdje ce morati biti konverzija u DATEONLY

        public DateTime ReturnTime { get; set; } // Ovdje ce morati biti konverzija u DATEONLY

        //public List<string> ImageUrls { get; set; }

    }
}