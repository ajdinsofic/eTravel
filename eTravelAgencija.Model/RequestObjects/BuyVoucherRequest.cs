using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class BuyVoucherRequest 
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int VoucherId { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Broj tokena ne može biti negativan.")]
        public int NumberOfTokens { get; set; }

    }
}