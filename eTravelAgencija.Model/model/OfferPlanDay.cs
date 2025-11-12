using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Model.model
{
    public partial class OfferPlanDay
    {
        public int OfferDetailsId { get; set; }
        public virtual Offer Offer { get; set; }
        public int DayNumber { get; set; }
        public string DayTitle { get; set; }
        public string DayDescription { get; set; }
    }
}
