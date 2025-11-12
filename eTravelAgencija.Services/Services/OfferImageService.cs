// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using eTravelAgencija.Model.ResponseObjects;

// namespace eTravelAgencija.Services.Services
// {
//     public class OfferImageService : BaseImageService<OfferImage, OfferImageResponse>, IOfferImageService
//     {
//         public OfferImageService(eTravelAgencijaDbContext context)
//             : base(
//                 context,
//                 x => x.OfferId,
//                 x => x.ImageUrl)
//         {
//         }
//     }

// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;

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