using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferHotelService : ICRUDService<Model.model.OfferHotels, BaseSearchObject, OfferHotelUpsertRequest, OfferHotelUpsertRequest>
    {

    }
}