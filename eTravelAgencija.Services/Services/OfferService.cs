using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Services
{
    public class OfferService : IOfferService
    {
        private readonly eTravelAgencijaDbContext _context;
        private readonly IOfferImageService _offerImageService;
        private readonly IMapper _mapper;

        public OfferService(
            eTravelAgencijaDbContext context,
            IOfferImageService offerImageService,
            IMapper mapper)
        {
            _context = context;
            _offerImageService = offerImageService;
            _mapper = mapper;
        }

        public async Task<PagedResult<OfferAdminResponse>> GetSearchOffersForAdmin(OfferSearchObject search)
        {
            var query = _context.Offers.AsQueryable();

            if (search.SubCategoryId > 0)
                query = query.Where(o => o.SubCategoryId == search.SubCategoryId);

            if (search.CategoryId > 0)
                query = query.Where(o => o.SubCategory.CategoryId == search.CategoryId);

            if (!string.IsNullOrWhiteSpace(search.FTS))
                query = query.Where(o => o.Title.ToLower().Contains(search.FTS.ToLower()));

            int totalCount = 0;
            if (!search.RetrieveAll && search.IncludeTotalCount)
                totalCount = await query.CountAsync();

            if (!search.RetrieveAll)
                query = query
                    .Skip(search.Page.GetValueOrDefault() * search.PageSize.GetValueOrDefault())
                    .Take(search.PageSize.GetValueOrDefault());

            var offers = await query.ToListAsync();

            var responses = new List<OfferAdminResponse>();
            foreach (var o in offers)
            {
                var mainImage = await _offerImageService.GetMainImageAsync(o.Id);
                var dto = _mapper.Map<OfferAdminResponse>(o);
                dto.MainImage = mainImage.ImageUrl;
                responses.Add(dto);
            }

            return new PagedResult<OfferAdminResponse>
            {
                Items = responses,
                TotalCount = search.IncludeTotalCount ? totalCount : 0
            };
        }

        public async Task<PagedResult<OfferUserResponce>> GetSearchOffersForUser(OfferSearchObject search)
        {
            var query = _context.Offers
                .Include(o => o.SubCategory)
                    .ThenInclude(sc => sc.Category)
                .AsQueryable();

            if (search.SubCategoryId > 0)
                query = query.Where(o => o.SubCategoryId == search.SubCategoryId);

            if (search.CategoryId > 0 && search.SubCategoryId == 0)
                query = query.Where(o => o.SubCategory.CategoryId == search.CategoryId);

            if (!string.IsNullOrWhiteSpace(search.FTS))
                query = query.Where(o => o.Title.ToLower().Contains(search.FTS.ToLower()));

            int totalCount = 0;
            if (!search.RetrieveAll && search.IncludeTotalCount)
                totalCount = await query.CountAsync();

            if (!search.RetrieveAll)
                query = query
                    .Skip(search.Page.GetValueOrDefault() * search.PageSize.GetValueOrDefault())
                    .Take(search.PageSize.GetValueOrDefault());

            var offers = await query.ToListAsync();

            var mainImages = await Task.WhenAll(offers.Select(async o =>
                new { o.Id, Image = await _offerImageService.GetMainImageAsync(o.Id) }));

            var imageMap = mainImages.ToDictionary(x => x.Id, x => x.Image);

            var responses = new List<OfferUserResponce>();

            foreach (var offer in offers)
            {
                var mainImage = await _offerImageService.GetMainImageAsync(offer.Id);
                var dto = _mapper.Map<OfferUserResponce>(offer);
                dto.MainImage = mainImage?.ImageUrl;
                responses.Add(dto);
            }

            return new PagedResult<OfferUserResponce>
            {
                Items = responses,
                TotalCount = search.IncludeTotalCount ? totalCount : 0
            };

        }

        public async Task<OfferAdminDetailResponse> GetOfferDetailsByIdForAdmin(int id)
        {
            var offer = await _context.Offers
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null || offer.Details == null)
                throw new Exception("Ponuda nije pronađena ili nema detalje.");

            return _mapper.Map<OfferAdminDetailResponse>(offer.Details);
        }

        public async Task<OfferUserDetailResponse> GetOfferDetailsByIdForUser(int id)
        {
            var offer = await _context.Offers
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null || offer.Details == null)
                throw new Exception("Ponuda nije pronađena ili nema detalje.");

            return _mapper.Map<OfferUserDetailResponse>(offer.Details);
        }

        public async Task<OfferUpsertResponse> PostOffer(OfferRequest request)
        {
            var offer = _mapper.Map<Offer>(request);

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return _mapper.Map<OfferUpsertResponse>(offer);
        }

        public async Task<OfferUpsertResponse> PutOffer(int id, OfferRequest request)
        {
            var offer = await _context.Offers
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
                throw new Exception("Ponuda nije pronađena.");

            _mapper.Map(request, offer);

            // Mapiranje detalja posebno
            offer.Details.Description = request.Description;
            offer.Details.Country = request.Country;
            offer.Details.City = request.City;

            await _context.SaveChangesAsync();

            return _mapper.Map<OfferUpsertResponse>(offer);
        }

        public async Task<bool> DeleteOffer(int id)
        {
            var offer = await _context.Offers.FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
                return false;

            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
