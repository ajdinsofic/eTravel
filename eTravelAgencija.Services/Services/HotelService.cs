using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class HotelService : IHotelService
    {
        private readonly eTravelAgencijaDbContext _context;
        private readonly IHotelImageService _hotelImageService;
        private readonly IOfferHotelService _offerHotelService;

        public HotelService(eTravelAgencijaDbContext context, IHotelImageService hotelImageService, IOfferHotelService offerHotelService)
        {
            _context = context;
            _hotelImageService = hotelImageService;
            _offerHotelService = offerHotelService;
        }



        // DISKUSIJA (BITNO JE PRVO NARAVITI DA RADI ALI PITANJE ZA RAZMISLJANJE) - DA LI SMO MOGLI OVA DVA GETA ZA USER I ADMIN SPOJITI, STAVITI IM ZNACAJKU DA LI SE RADI OD USERU ILI ADMINU

        public async Task<PagedResult<HotelResponse>> GetHotelsForUserBySearch(HotelUserSearchObject search)
        {
            var query = _context.Hotels
                .Include(h => h.HotelRooms)
                    .ThenInclude(hr => hr.Rooms)
                .AsQueryable();

            if (search.OfferId > 0)
            {
                query = query.Where(h => h.OfferHotels.Any(oh => oh.OfferDetailsId == search.OfferId));
            }

            if (search.DepartureDate != default)
            {
                query = query.Where(h => h.OfferHotels.Any(oh => oh.DepartureDate.Date == search.DepartureDate.Date));
            }

            if (search.RoomId > 0)
            {
                query = query.Where(h => h.HotelRooms.Any(r => r.Id == search.RoomId));
            }

            if (!string.IsNullOrEmpty(search.FTS))
            {
                var fts = search.FTS.ToLower();
                query = query.Where(h =>
                    h.Name.ToLower().Contains(fts) ||
                    h.City.ToLower().Contains(fts) ||
                    h.Country.ToLower().Contains(fts));
            }

            int totalCount = 0;
            if (search.IncludeTotalCount)
            {
                totalCount = await query.CountAsync();
            }

            if (!search.RetrieveAll)
            {
                int skip = (search.Page ?? 0) * (search.PageSize ?? 10);
                int take = search.PageSize ?? 10;
                query = query.Skip(skip).Take(take);
            }

            var hotels = await query.ToListAsync();

            // 🔄 Mapiranje slika koristeći HotelImageService
            var resultItems = new List<HotelResponse>();

            foreach (var h in hotels)
            {
                var mainImage = await _hotelImageService.GetMainImageAsync(h.Id);

                resultItems.Add(new HotelResponse
                {
                    HotelId = h.Id,
                    HotelName = h.Name,
                    Stars = h.Stars,
                    MainImage = mainImage.ImageUrl,
                    Room = h.HotelRooms.Select(r => new RoomResponse   // Ovdje cemo napraviti roomService i popravit ovo
                    {
                        RoomType = r.Rooms.RoomType,
                        RoomsLeft = r.RoomsLeft
                    }).FirstOrDefault()
                });
            }

            return new PagedResult<HotelResponse>
            {
                Items = resultItems,
                TotalCount = search.IncludeTotalCount ? totalCount : 0
            };
        }


        // Ovdje kada je vec kreiran hotel, moci cemo direktno da imamo opciju da vidimo pregled,
        // brisemo i dodajemo slike itd. // ISTO TAKO PAGINACIJA
        public async Task<PagedResult<HotelResponse>> GetHotelsForAdminByOfferId(HotelAdminSearchObject search)
        {
            var query = _context.Hotels
                .Include(h => h.OfferHotels)
                .Where(h => h.OfferHotels.Any(oh => oh.OfferDetailsId == search.OfferId))
                .AsQueryable();

            int totalCount = 0;
            if (search.IncludeTotalCount)
            {
                totalCount = await query.CountAsync();
            }

            if (!search.RetrieveAll)
            {
                int skip = (search.Page ?? 0) * (search.PageSize ?? 10);
                int take = search.PageSize ?? 10;

                query = query.Skip(skip).Take(take);
            }

            var hotels = await query.ToListAsync();

            var resultItems = new List<HotelResponse>();

            foreach (var h in hotels)
            {
                var mainImage = await _hotelImageService.GetMainImageAsync(h.Id); // 👈 koristi servis

                resultItems.Add(new HotelResponse
                {
                    HotelId = h.Id,
                    HotelName = h.Name,
                    Stars = h.Stars,
                    MainImage = mainImage.ImageUrl
                });
            }

            return new PagedResult<HotelResponse>
            {
                Items = resultItems,
                TotalCount = search.IncludeTotalCount ? totalCount : 0
            };
        }


        // public async Task<HotelResponse> GetHotelforAdminById(int id)
        // {
        //     var hotel = await _context.Hotels
        //         .Include(h => h.HotelRooms)
        //             .ThenInclude(hr => hr.Rooms)
        //         .FirstOrDefaultAsync(h => h.Id == id);

        //     if (hotel == null)
        //         throw new Exception("Hotel nije pronađen.");

        //     var mainImage = await _hotelImageService.GetMainImageAsync(hotel.Id);

        //     return new HotelResponse
        //     {
        //         HotelId = hotel.Id,
        //         HotelName = hotel.Name,
        //         Stars = hotel.Stars,
        //         MainImage = mainImage.ImageUrl,
        //     };
        // }


        // Kada se vrati da je hotel napravljen, onda omogucujemo opciju da se slike mogu dodavati
        public async Task<HotelResponse> PostHotel(HotelUpsertRequest request)
        {

            var hotel = new Hotel
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                Stars = request.Stars
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            _offerHotelService.LinkHotelToOffer(hotel.Id, request.OfferId, request.DepartureDate, request.ReturnTime);

            return new HotelResponse
            {
                HotelId = hotel.Id,
                HotelName = hotel.Name,
                Stars = hotel.Stars,
            };
        }

        public async Task<HotelResponse> PutHotel(int id, HotelUpsertRequest request)
        {
            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
                throw new Exception("Hotel nije pronađen.");

            hotel.Name = request.Name;
            hotel.Address = request.Address;
            hotel.City = request.City;
            hotel.Country = request.Country;
            hotel.Stars = request.Stars;

            await _context.SaveChangesAsync();

            _offerHotelService.PutOfferHotelDates(id, request.OfferId, request.DepartureDate, request.ReturnTime);

            return new HotelResponse
            {
                HotelId = hotel.Id,
                HotelName = hotel.Name,
                Stars = hotel.Stars,
            };
        }

        public async Task<bool> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}