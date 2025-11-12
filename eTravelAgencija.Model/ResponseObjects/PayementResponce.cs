using System;

namespace eTravelAgencija.Model.ResponseObjects
{
    public class PaymentResponse
    {
        public int RateId { get; set; }
        public string RateName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsConfirmed { get; set; }
    }

}
