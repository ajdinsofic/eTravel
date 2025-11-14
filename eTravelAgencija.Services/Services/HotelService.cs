using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                // 🔹 1. Filtriraj slike (radi uvijek)
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
        
                // 🔹 2. Ako postoji RoomId, filtriraj sobe i izračunaj cijenu
                if (search?.RoomId != null)
                {
                    hotel.HotelRooms = hotel.HotelRooms
                        .Where(hr => hr.RoomId == search.RoomId)
                        .ToList();
        
                    await SetCalculatedPriceAsync(hotel, search.RoomId.Value);
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
                    return basePrice + 200;
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



    }
}
