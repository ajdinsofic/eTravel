using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferImageService : ICRUDService<Model.model.OfferImage, BaseSearchObject, OfferImageUpsertRequest, OfferImageUpsertRequest>
    {
        
    }
}