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
        public int OfferId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeInsurance { get; set; }
        public bool isFirstRatePaid { get; set; }
        public bool isFullPaid { get; set;}
        public bool isDiscountUsed {get; set;}
        public double? DiscountValue {get; set;}
        public decimal TotalPrice { get; set; }
        public decimal PriceLeftToPay { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserNeeds { get; set; }
    }
}