using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IOfferService : ICRUDService<Model.model.Offer, OfferSearchObject, OfferInsertRequest, OfferUpdateRequest>
    {
        List<Model.model.Offer> RecommendOffers(int offerId);
    }
}