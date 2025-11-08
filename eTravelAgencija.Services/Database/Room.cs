using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Services.Database
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public int RoomCount { get; set; }
    }
}
