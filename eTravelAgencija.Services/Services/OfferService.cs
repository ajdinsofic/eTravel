using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class OfferService : IOfferService
    {
        private readonly eTravelAgencijaDbContext _context;

        public OfferService(eTravelAgencijaDbContext context)
        {
            _context = context;
        }

        public async Task<List<OfferResponse>> GetSpecialOffers(OfferCategoryAndSubcategoryRequest request)
        {
            return await GetOffersByCategoryName(request);
        }

        public async Task<List<OfferResponse>> GetHolidayOffers(OfferCategoryAndSubcategoryRequest request)
        {
            return await GetOffersByCategoryName(request);
        }

        public async Task<List<OfferResponse>> GetFeelTheMonth(OfferCategoryAndSubcategoryRequest request)
        {
            return await GetOffersByCategoryName(request);
        }

        public async Task<OfferResponse> GetOfferById(int id)
        {
            var offer = await _context.Offers
                .Include(o => o.SubCategory)
                    .ThenInclude(sc => sc.Category)
                .Include(o => o.Details)
                    .ThenInclude(d => d.OfferImages)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
            {
                return null;
            }

            return MapToOffer(offer);
        }

        Task<bool> IOfferService.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OfferResponse>> GetOffersByCategoryName(OfferCategoryAndSubcategoryRequest request)
        {
            if (string.IsNullOrEmpty(request.CategoryName))
            {
                throw new ArgumentException("Category name must be provided.");
            }

            var query = _context.Offers
                .Include(o => o.SubCategory)
                    .ThenInclude(sc => sc.Category)
                .Include(o => o.Details)
                    .ThenInclude(d => d.OfferImages)
                .Where(o => o.SubCategory.Category.Name == request.CategoryName);

            if (!string.IsNullOrEmpty(request.Subcategory))
            {
                query = query.Where(o => o.SubCategory.Name == request.Subcategory);
            }

            var offers = await query.ToListAsync();

            return offers.Select(MapToOffer).ToList();
        }

        private OfferResponse MapToOffer(Offer o)
        {
            return new OfferResponse
            {
                Id = o.Id,
                OfferName = o.Title,
                DaysInTotal = o.DaysInTotal,
                MinimalPrice = (int)o.Price,
                WayOfTravel = o.WayOfTravel,
                OfferImage = o.Details.OfferImages.Select(img => img.ImageUrl).ToList()
            };
        }
    }
}
