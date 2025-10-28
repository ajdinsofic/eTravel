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
        Task<PagedResult<OfferAdminResponse>> GetSearchOffersForAdmin(OfferSearchObject request);
        Task<PagedResult<OfferUserResponce>> GetSearchOffersForUser(OfferSearchObject request);
        Task<OfferAdminDetailResponse> GetOfferDetailsByIdForAdmin(int id);
        Task<OfferUserDetailResponse> GetOfferDetailsByIdForUser(int id);
        Task<OfferUpsertResponse> PostOffer(OfferRequest request);
        Task<OfferUpsertResponse> PutOffer(int id, OfferRequest request);
        Task<bool> DeleteOffer(int id);

    }
}