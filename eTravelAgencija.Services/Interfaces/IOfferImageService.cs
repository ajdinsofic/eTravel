using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using Microsoft.AspNetCore.Http;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IOfferImageService : ICRUDService<Model.model.OfferImage, OfferImageSearchObject, OfferImageInsertRequest, OfferImageUpdateRequest>
    {
    }
}