using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.RequestObjects
{
    public class ReservationPreviewRequest
    {
        public int UserId { get; set; }
        public int OfferId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public decimal? BasePrice { get; set; }
        public bool? IncludeInsurance { get; set; }
        public string? VoucherCode { get; set; }
         
    }
}