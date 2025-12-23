
using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObject
{
    public class PayPalOrderResponse
{
    public string Id { get; set; } = string.Empty;      // orderId
    public string Status { get; set; } = string.Empty;  // CREATED
    public List<PayPalLink> Links { get; set; } = new List<PayPalLink>();
}
    
}
