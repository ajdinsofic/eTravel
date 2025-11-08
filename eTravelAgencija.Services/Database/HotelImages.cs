using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTravelAgencija.Services.Services;

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

        public bool IsMain { get; set; }


    }
}
