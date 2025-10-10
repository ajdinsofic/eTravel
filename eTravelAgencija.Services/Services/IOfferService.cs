using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferService
    {
        Task<List<OfferResponse>> GetAsync(OfferSearchObject? search = null);

        Task<OfferResponse?> GetByIdAsync(string id);

        Task<OfferResponse> PostAsync(OfferRequest offer);

        Task<OfferResponse?> PutAsync(int id, UserUpsertRequest offer);

        Task<bool> DeleteAsync(int id);


    }
}