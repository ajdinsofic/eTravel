using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferService
    {
        // Ako nije ulogovan, ako jeste onda moramo da vidimo sistem "preporuceno za vas"
        Task<List<OfferResponse>> GetSpecialOffers(OfferCategoryAndSubcategoryRequest request);
        Task<List<OfferResponse>> GetHolidayOffers(OfferCategoryAndSubcategoryRequest request);
        Task<List<OfferResponse>> GetFeelTheMonth(OfferCategoryAndSubcategoryRequest request);
        Task<OfferResponse> GetOfferById(int id);
        Task<bool> DeleteAsync(int id);

        //Task<List<OfferResponse>> GetSearchPutovanje(OfferSearchObject? search = null);

    }
}