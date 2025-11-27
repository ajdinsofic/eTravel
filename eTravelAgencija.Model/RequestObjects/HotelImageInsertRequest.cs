using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eTravelAgencija.Model.RequestObjects
{
    public class HotelImageInsertRequest
    {
        [Required]
        public int HotelId { get; set; }
        public bool IsMain { get; set; } = false;
        public IFormFile image { get; set; }


    }
}
