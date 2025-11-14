using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User user {get; set;}
        public int OfferId { get; set; }
        public Offer offer {get; set;}
        public int HotelId { get; set; }
        public Hotel hotel { get; set; }
        public int RoomId { get; set; }
        public Room room { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeInsurance { get; set; }
        public bool isFirstRatePaid { get; set; }
        public bool isFullPaid { get; set;}
        public decimal TotalPrice { get; set; }
        public decimal PriceLeftToPay { get; set; }
        public string UserNeeds { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}