using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Model.RequestObjects
{
    public class HotelImageUpdateRequest
    {
        [Required]
        public int HotelId { get; set; }
        public bool IsMain { get; set; } = false;


    }
}
