using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;

using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Services
{
    public class OfferImageService : BaseCRUDService<
    Model.model.OfferImage, BaseSearchObject, Database.OfferImage, OfferImageUpsertRequest, OfferImageUpsertRequest>, IOfferImageService
    {
        public OfferImageService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }

}