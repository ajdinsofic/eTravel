
using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObject
{
    public class PayPalAmount
    {
        public string CurrencyCode { get; set; } = string.Empty; // EUR
        public string Value { get; set; } = string.Empty;        // "100.00"
    }


}
