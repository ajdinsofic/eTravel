using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Model.model
{ 
    public partial class OfferHotels
{
    public int OfferId { get; set; }  
    public int HotelId { get; set; }
    public virtual DateTime DepartureDate { get; set; }
    public virtual DateTime ReturnDate { get; set; }
}


}

