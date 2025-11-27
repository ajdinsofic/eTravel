using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;

using eTravelAgencija.Services.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Database
{
    public class OfferHotelService : BaseCRUDService<Model.model.OfferHotels, OfferHotelSearchObject, Database.OfferHotels, OfferHotelInsertRequest, OfferHotelUpdateRequest>, IOfferHotelService
    {
        public OfferHotelService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IQueryable<OfferHotels> ApplyFilter(
    IQueryable<OfferHotels> query,
    OfferHotelSearchObject search)
        {
            if (search.offerDetailsId.HasValue)
            {
                query = query.Where(x => x.OfferDetailsId == search.offerDetailsId);
            }

            return base.ApplyFilter(query, search);
        }

    }
}
