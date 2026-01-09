using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTravelAgencija.Model.model;

namespace eTravelAgencija.Services.Database
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(OfferDetails))]
        public int OfferId { get; set; }
        public OfferDetails OfferDetails { get; set; }

        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public Rooms Room { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeInsurance { get; set; }
        public bool isFirstRatePaid { get; set; }
        public bool isFullPaid { get; set; }
        public bool isDiscountUsed {get; set;}
        public double DiscountValue {get; set;}
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public decimal PriceLeftToPay { get; set; }
        public string addedNeeds { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

}
