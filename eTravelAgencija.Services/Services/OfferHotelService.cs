using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class OfferHotelService : IOfferHotelService
    {
        private readonly eTravelAgencijaDbContext _context;

        public OfferHotelService(eTravelAgencijaDbContext context)
        {
            _context = context;
        }

        public async Task<OfferHotelResponse> LinkHotelToOffer(int hotelId, int offerId, DateTime departureDate, DateTime returnDate)
        {
            var entity = new OfferHotels
            {
                HotelId = hotelId,
                OfferDetailsId = offerId,
                DepartureDate = departureDate,
                ReturnDate = returnDate
            };

            _context.OfferHotels.Add(entity);
            await _context.SaveChangesAsync();

            return MapToOfferHotelResponse(entity);
        }

        public async Task<OfferHotelResponse?> GetOfferHotelLinkAsync(int offerId, int hotelId)
        {
            var entity = await _context.OfferHotels
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OfferDetailsId == offerId && x.HotelId == hotelId);

            if (entity == null)
                return null;

            return new OfferHotelResponse
            {
                OfferId = entity.OfferDetailsId,
                HotelId = entity.HotelId,
                Departuredate = entity.DepartureDate,
                ReturnDate = entity.ReturnDate
            };
        }


        public async Task<OfferHotelResponse> PutOfferHotelDates(int hotelId, int offerId, DateTime departureDate, DateTime returnDate)
        {

            var entity = await _context.OfferHotels
                .FirstOrDefaultAsync(x => x.HotelId == hotelId && x.OfferDetailsId == offerId);

            if (entity == null)
            {
                throw new Exception("Veza između hotela i ponude nije pronađena.");
            }


            entity.DepartureDate = departureDate;
            entity.ReturnDate = returnDate;

            await _context.SaveChangesAsync();


            return MapToOfferHotelResponse(entity);
        }

        private OfferHotelResponse MapToOfferHotelResponse(OfferHotels entity)
        {
            return new OfferHotelResponse
            {
                OfferId = entity.OfferDetailsId,
                HotelId = entity.HotelId,
                Departuredate = entity.DepartureDate,
                ReturnDate = entity.ReturnDate
            };
        }
    }
}