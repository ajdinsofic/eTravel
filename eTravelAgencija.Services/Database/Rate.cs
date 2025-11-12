using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Database
{
    public class Rate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // npr. "I rata", "II rata", "III rata", "Puni iznos"

        // Možeš dodati i defaultne rasporede ako želiš
        public int? OrderNumber { get; set; } // npr. 1, 2, 3, 4 (za sortiranje)
    }
}