using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public partial class Payment
    {
        public int ReservationId { get; set; }
        public int RateId { get; set; }
        public string RateName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsConfirmed { get; set; }
    }
}