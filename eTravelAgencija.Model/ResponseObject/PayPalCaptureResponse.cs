
using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObject
{
    public class PayPalCaptureResponse
    {
        public string Id { get; set; } = string.Empty;      // orderId
        public string Status { get; set; } = string.Empty;  // COMPLETED

        public List<PayPalPurchaseUnit> PurchaseUnits { get; set; } = new List<PayPalPurchaseUnit>();
    }

}
