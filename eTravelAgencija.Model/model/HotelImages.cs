using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Model.model
{
    public partial class HotelImages
    {
        public int ImageId { get; set; }
        public int HotelId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }
}
