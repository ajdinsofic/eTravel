using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTravelAgencija.Services.Database
{ 
    public class OfferHotels
{

    [ForeignKey(nameof(OfferDetails))]
    public int OfferDetailsId { get; set; }  
    public OfferDetails OfferDetails { get; set; }

    [ForeignKey(nameof(Hotel))]
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; }
    
    [ForeignKey(nameof(DepartureDate))]
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
}


}