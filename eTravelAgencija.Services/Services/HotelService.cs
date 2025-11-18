using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Services.Recommendation;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace eTravelAgencija.Services.Services
{
    public class HotelService : BaseCRUDService<Model.model.Hotel, HotelSearchObject, Database.Hotel, HotelUpsertRequest, HotelUpsertRequest>, IHotelService
    {
        public HotelService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        // DA ZNAS VRACAMO OPET LISTU ALI IZ LISTE CEMO VADITI KOJI SE TO HOTEL KLIKNUO I PREKO FRONTENDA CEMO
        // SAMO NJEGA ZAMJENITI
        public override async Task<IEnumerable<Database.Hotel>> AfterGetAsync(IEnumerable<Database.Hotel> entities, HotelSearchObject? search = null)
        {
            foreach (var hotel in entities)
            {
                if (search?.isMainImage == true)
                {
                    hotel.HotelImages = hotel.HotelImages
                        .Where(img => img.IsMain)
                        .ToList();
                }
                else
                {
                    hotel.HotelImages = hotel.HotelImages.ToList();
                }

                if (search?.RoomId != null)
                {
                    hotel.HotelRooms = hotel.HotelRooms
                        .Where(hr => hr.RoomId == search.RoomId)
                        .ToList();

                    await SetCalculatedPriceAsync(hotel, search.RoomId.Value);
                }
                else
                {
                    var firstRoomId = hotel.HotelRooms.FirstOrDefault()?.RoomId ?? 0;

                    if (firstRoomId != 0)
                        await SetCalculatedPriceAsync(hotel, firstRoomId);
                }

            }

            return entities;
        }



        public override IQueryable<Hotel> AddInclude(IQueryable<Hotel> query, HotelSearchObject? search = null)
        {
            query = query
                .Include(h => h.HotelImages)
                .Include(h => h.HotelRooms)
                    .ThenInclude(hr => hr.Rooms)
                .Include(h => h.OfferHotels)
                    .ThenInclude(h => h.OfferDetails);

            return base.AddInclude(query, search);
        }

        public override IQueryable<Hotel> ApplyFilter(IQueryable<Hotel> query, HotelSearchObject search)
        {
            if (search.OfferId.HasValue)
                query = query.Where(h => h.OfferHotels.Any(oh => oh.OfferDetailsId == search.OfferId));

            if (search.DepartureDate.HasValue)
                query = query.Where(h => h.OfferHotels.Any(oh => oh.DepartureDate == search.DepartureDate));

            if (search.RoomId.HasValue)
                query = query.Where(h => h.HotelRooms.Any(hr => hr.RoomId == search.RoomId));

            if (search?.isMainImage == true)
            {
                query = query.Where(h => h.HotelImages.Any(hr => hr.IsMain == search.isMainImage));
            }

            return base.ApplyFilter(query, search);
        }

        public decimal CalculateCalculatedPrice(decimal basePrice, string roomType)
        {
            switch (roomType.ToLower())
            {
                case "dvokrevetna":
                    return basePrice; // ostaje ista cijena
                case "trokrevetna":
                    return basePrice + 100; // +100 KM
                case "cetverokrevetna":
                    return basePrice + 200; // +200 KM
                case "petokrevetna":
                    return basePrice + 300;
                default:
                    return basePrice; // fallback ako nije prepoznata soba
            }
        }

        private async Task SetCalculatedPriceAsync(Hotel hotel, int roomId)
        {
            var offerDetailsId = hotel.OfferHotels.FirstOrDefault()?.OfferDetailsId;

            if (offerDetailsId == null)
                return;

            var offer = await _context.OfferDetails
                .FirstOrDefaultAsync(o => o.OfferId == offerDetailsId);

            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            if (offer != null && room != null)
            {
                hotel.CalculatedPrice = CalculateCalculatedPrice(offer.MinimalPrice, room.RoomType);
            }
        }

        public Model.model.Hotel GetMostPopularHotelForOffer(int offerId)
        {
            
            var hotelIds = _context.OfferHotels
                .Where(oh => oh.OfferDetailsId == offerId)
                .Select(oh => oh.HotelId)
                .Distinct()
                .ToList();

            if (!hotelIds.Any())
                return null;

            
            var popularity = _context.Reservations
                .Where(r => hotelIds.Contains(r.HotelId))
                .GroupBy(r => r.HotelId)
                .Select(g => new
                {
                    HotelId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            
            int chosenHotelId = popularity?.HotelId ?? hotelIds.First();

            
            var hotel = _context.Hotels
                .Where(h => h.Id == chosenHotelId)
                .Include(h => h.HotelImages.Where(img => img.IsMain == true))
                .Include(h => h.HotelRooms).ThenInclude(hr => hr.Rooms)
                .Include(h => h.OfferHotels)
                .FirstOrDefault();

            return _mapper.Map<Model.model.Hotel>(hotel);
        }

    }
}
