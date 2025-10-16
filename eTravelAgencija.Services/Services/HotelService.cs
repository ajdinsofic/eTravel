using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class HotelService : IHotelService
    {
        private readonly eTravelAgencijaDbContext _context;

        public HotelService(eTravelAgencijaDbContext context)
        {
            _context = context;
        }

        public async Task<List<HotelResponse>> GetHotel(HotelSearchObject search)
        {
            if (search == null)
                throw new ArgumentNullException(nameof(search));

            var query = _context.OfferHotels
                .Include(oh => oh.Hotel)
                    .ThenInclude(h => h.HotelImages)
                .Include(oh => oh.Hotel)
                    .ThenInclude(h => h.HotelRooms)
                        .ThenInclude(hr => hr.Rooms)
                .Where(oh => oh.OfferDetailsId == search.OfferId
                          && oh.DepartureDate == search.DepartureDate
                          && oh.Hotel.HotelRooms.Any(r => r.RoomId == search.RoomId))
                .Select(oh => oh.Hotel)
                .Distinct();

            var hotels = await query.ToListAsync();

            var result = hotels.Select(h => new HotelResponse
            {
                HotelId = h.Id,
                HotelName = h.Name,
                Stars = h.Stars,
                MainImage = h.HotelImages.FirstOrDefault()?.ImageUrl ?? "",

                Rooms = h.HotelRooms.Select(r => new RoomResponse
                {
                    RoomType = r.Rooms.RoomType,
                    RoomsLeft = r.RoomsLeft
                }).ToList()

            }).ToList();

            return result;
        }

        public async Task<HotelDetailsResponse> GetHotelById(int hotelId, int offerId)
        {
            var offerHotel = await _context.OfferHotels
                .Include(oh => oh.Hotel)
                    .ThenInclude(h => h.HotelImages)
                .FirstOrDefaultAsync(oh => oh.HotelId == hotelId && oh.OfferDetailsId == offerId);

            if (offerHotel == null || offerHotel.Hotel == null)
                return null;

            var hotel = offerHotel.Hotel;

            var response = new HotelDetailsResponse
            {
                Address = hotel.Address,
                HotelImages = hotel.HotelImages.Select(img => img.ImageUrl).ToList()
            };

            return response;
        }

        // Dodaj mapping objekata


    }
}