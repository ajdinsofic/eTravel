using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Model.RequestObjects
{
    public class HotelImageUpsertRequest
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; } = false;
    }
}
