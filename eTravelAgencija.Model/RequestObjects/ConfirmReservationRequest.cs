
using System;

namespace eTravelAgencija.Model.RequestObjects

{

    public class ConfirmReservationRequest
    {
        public int ReservationId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}


