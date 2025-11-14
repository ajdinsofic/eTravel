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
using System.Security.Cryptography.X509Certificates;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Database
{
    public class OfferService : BaseCRUDService<Model.model.Offer, OfferSearchObject, Database.Offer, OfferInsertRequest, OfferUpdateRequest>, IOfferService
    {
        public OfferService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Offer> ApplyFilter(IQueryable<Offer> query, OfferSearchObject search)
        {
            if (search.SubCategoryId == -1 || search.SubCategoryId > 0)
            {
                query = query.Where(o => o.SubCategoryId == search.SubCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(search.FTS))
            {
                query = query.Where(o => o.Title.ToLower().Contains(search.FTS.ToLower()));
            }

            return base.ApplyFilter(query, search);
        }

        public override IQueryable<Offer> AddInclude(IQueryable<Offer> query, OfferSearchObject search)
        {
            query = query.Include(o => o.Details);

            if (search.isMainImage)
            {
                query = query
                    .Include(o => o.Details)
                        .ThenInclude(d => d.OfferImages.Where(img => img.isMain));
            }
            else
            {
                query = query
                    .Include(o => o.Details)
                        .ThenInclude(d => d.OfferImages);
            }

            if (search.isOfferHotels)
                query = query.Include(o => o.Details.OfferHotels);


            if (search.isOfferPlanDays)
                query = query.Include(o => o.Details.OfferPlanDays);

            return base.AddInclude(query, search);
            
        }

        public override async Task<Offer> SetDetails(Offer entity)
        {
            if (entity == null)
                return null;
        
            return await _context.Offers
                .Include(u => u.Details)
                .FirstOrDefaultAsync(u => u.Id == entity.Id);
        }
  
        public override async Task BeforeInsertAsync(Database.Offer entity, OfferInsertRequest request)
        {

            var details = new OfferDetails
            {
                MinimalPrice = request.MinimalPrice,
                ResidenceTotal = request.ResidenceTotal,
                TravelInsuranceTotal = request.TravelInsuranceTotal,
                ResidenceTaxPerDay = request.ResidenceTotal / request.DaysInTotal,
                City = request.City,
                Country = request.Country,
                Description = request.Description,
            };

            entity.Details = details;

            await base.BeforeInsertAsync(entity, request);
        }

        public override async Task BeforeUpdateAsync(Database.Offer entity, OfferUpdateRequest request)
        {
            await _context.Entry(entity)
            .Reference(o => o.Details)
            .LoadAsync();

            if (entity.Details != null)
            {
                entity.Details.MinimalPrice = request.MinimalPrice;
                entity.Details.ResidenceTotal = request.ResidenceTotal;
                entity.Details.TravelInsuranceTotal = request.TravelInsuranceTotal;
                entity.Details.City = request.City;
                entity.Details.Country = request.Country;
                entity.Details.Description = request.Description;
            }

            await base.BeforeUpdateAsync(entity, request);
        }

    }
}
