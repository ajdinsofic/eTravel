using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;

namespace eTravelAgencija.Services.Services
{
    public interface IHotelService
    {
        // Prepraviti slobodno HotelUserSearchObject jer HotelAdminSearchObject ima samo offerId pa tu mozemo stavit samo offerId
        Task<PagedResult<HotelResponse>> GetHotelsForUserBySearch(HotelUserSearchObject search); // Lista hotela kada korisnik izabere ponudu i datum i tip sobe pa mu tamo izbaci koje hotele 
        Task<PagedResult<HotelResponse>> GetHotelsForAdminByOfferId(HotelAdminSearchObject search); // Lista hotela u admin panelu sa paginacijom i filterima
        // Task<HotelResponse> GetHotelforAdminById(int id); // Na listi hotela u adminu kada se klikne na jedan hotel; 
        Task<HotelResponse> PostHotel(HotelUpsertRequest request); // Dodavanje na adminu, kada se klikne na zeljeni hotel
        Task<bool> DeleteHotel(int id); // Uklanjanje hotela
        Task<HotelResponse> PutHotel(int id, HotelUpsertRequest request); // Izmjena hotela
    }
}