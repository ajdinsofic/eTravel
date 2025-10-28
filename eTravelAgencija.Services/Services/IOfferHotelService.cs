using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferHotelService
    {
        Task<OfferHotelResponse> LinkHotelToOffer(int hotelId, int offerId, DateTime departureDate, DateTime returnDate);
        Task<OfferHotelResponse> PutOfferHotelDates(int hotelId, int offerId, DateTime departureDate, DateTime returnDate);
    }
}