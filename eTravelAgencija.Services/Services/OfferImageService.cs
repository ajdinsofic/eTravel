using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;

namespace eTravelAgencija.Services.Services
{
    public class OfferImageService : BaseImageService<OfferImage, OfferImageResponse>, IOfferImageService
    {
        public OfferImageService(eTravelAgencijaDbContext context)
            : base(
                context,
                x => x.OfferId,
                x => x.ImageUrl)
        {
        }
    }

}