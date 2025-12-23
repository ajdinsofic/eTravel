
namespace eTravelAgencija.Model.ResponseObject
{
    public class PayPalLink
    {
        public string Href { get; set; } = string.Empty;
        public string Rel { get; set; } = string.Empty;   // approve
        public string Method { get; set; } = string.Empty;
    }


}
