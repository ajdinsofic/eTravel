namespace eTravelAgencija.Model.ResponseObjects
{
    public class OfferResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int DaysInTotal { get; set; }

        public string WayOfTravel { get; set; }

        public decimal MinimalPrice { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public decimal ResidenceTaxPerDay { get; set; }
        public decimal TravelInsuranceTotal { get; set; }

        public decimal ResidenceTotal { get; set; }
    }
}
