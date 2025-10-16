using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;

namespace eTravelAgencija.Services.Services
{
    public interface IHotelService
    {
        Task<List<HotelResponse>> GetHotel(HotelSearchObject search);
        Task<HotelDetailsResponse> GetHotelById(int hotelId, int offerId);

        //Task<HotelResponse> PostHotel(HotelUpsertRequest request);
        //Task<bool> DeleteHotel(int id);
        //Task<HotelResponse> PutHotel(int id, HotelUpsertRequest request);
    }
}