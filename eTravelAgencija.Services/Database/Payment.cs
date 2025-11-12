using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Database
{
    public class Payment
    {
        [ForeignKey(nameof(Reservation))]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        [ForeignKey(nameof(Rate))]
        public int RateId { get; set; }
        public Rate Rate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; } 

        public bool IsConfirmed { get; set; } = false;
    }

}