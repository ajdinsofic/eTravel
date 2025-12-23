using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObject
{
    public class PayPalPayments
{
    public List<PayPalCapture> Captures { get; set; } = new List<PayPalCapture>();
}
    
}
