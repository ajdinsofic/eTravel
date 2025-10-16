using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Services.Database
{
    public class HotelImages
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
