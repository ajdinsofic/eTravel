using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Services.Database
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomType { get; set; }  // Ispravio sam typo iz RoomTyoe u RoomType
    }
}
