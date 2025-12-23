
using System;
using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObject
{
    public class PayPalCapture
{
    public string Id { get; set; } = string.Empty;       // Capture ID
    public string Status { get; set; } = string.Empty;   // COMPLETED
    public PayPalAmount Amount { get; set; } = new PayPalAmount();
    public DateTime CreateTime { get; set; }
}


}
