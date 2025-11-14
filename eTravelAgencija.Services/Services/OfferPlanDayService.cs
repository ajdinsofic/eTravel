using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;
using eTravelAgencija.Model.SearchObjects;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Services
{
    
    public class OfferPlanDayService : BaseCRUDService<Model.model.OfferPlanDay,OfferPlanDaySearchObject,Database.OfferPlanDay,OfferPlanDayInsertRequest,OfferPlanDayUpdateRequest>, IOfferPlanDayService
    {
        public OfferPlanDayService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<OfferPlanDay> ApplyFilter(IQueryable<OfferPlanDay> query, OfferPlanDaySearchObject search)
        {
            if (search.OfferId.HasValue)
            {
                query = query.Where(x => x.OfferDetailsId == search.OfferId.Value);
            }

            if (search.dayNumber.HasValue)
            {
                query = query.Where(x => x.DayNumber == search.dayNumber);
            }
            return base.ApplyFilter(query, search);
        }
    }
}
