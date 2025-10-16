using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Services.Database
{
    public class OfferPlanDay
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(OfferDetails))]
        public int OfferDetailsId { get; set; }

        public OfferDetails OfferDetails { get; set; }

        public int DayNumber { get; set; }

        public string DayTitle { get; set; }

        public string DayDescription { get; set; }
    }
}
