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
    public class HotelRoomsService : IHotelRoomsService
    {
        private readonly eTravelAgencijaDbContext _context;
        private readonly IOfferService _offerService;

        public HotelRoomsService(eTravelAgencijaDbContext context, IOfferService offerService)
        {
            _context = context;
            _offerService = offerService;
        }

        public async Task<List<HotelRoomResponse>> GetAllAsync()
        {
            var rooms = await _context.HotelRooms
                .Include(hr => hr.Rooms)
                .ToListAsync();

            return rooms.Select(hr => new HotelRoomResponse
            {
                RoomId = hr.RoomId,
                RoomType = hr.Rooms.RoomType,
                RoomsLeft = hr.RoomsLeft
            }).ToList();
        }

        public async Task<HotelRoomResponse> GetByKeyForUserAsync(int offerId, int hotelId, int RoomId)
        {
            var hr = await _context.HotelRooms
                .Include(hr => hr.Rooms)
                .FirstOrDefaultAsync(hr => hr.HotelId == hotelId && hr.RoomId == RoomId);

            if (hr == null) return null;

            return new HotelRoomResponse
            {
                RoomId = hr.RoomId,
                RoomType = hr.Rooms.RoomType,
                RoomsLeft = hr.RoomsLeft,
                RoomPrice = await CalculateRoomPrice(offerId, hr.Rooms.RoomCount)
            };
        }

        // Admin version – shows stored data without recalculating price
        public async Task<HotelRoomResponse> GetByKeyForAdminAsync(int hotelId, int roomId)
        {
            var hr = await _context.HotelRooms
                .Include(hr => hr.Rooms)
                .FirstOrDefaultAsync(hr => hr.HotelId == hotelId && hr.RoomId == roomId);

            if (hr == null)
                return null;

            return new HotelRoomResponse
            {
                RoomId = hr.RoomId,
                RoomType = hr.Rooms.RoomType,
                RoomsLeft = hr.RoomsLeft,
            };
        }


        public async Task<HotelRoomUpsertResponse> CreateAsync(HotelRoomRequest request)
        {
            var hr = new HotelRooms
            {
                HotelId = request.HotelId,
                RoomId = request.RoomId,
                RoomsLeft = request.RoomsLeft
            };

            _context.HotelRooms.Add(hr);
            await _context.SaveChangesAsync();
            await _context.Entry(hr).Reference(r => r.Rooms).LoadAsync();

            return new HotelRoomUpsertResponse
            {
                HotelId = hr.HotelId,
                RoomId = hr.RoomId,
                RoomType = hr.Rooms.RoomType,
                RoomsLeft = hr.RoomsLeft
            };
        }

        public async Task<HotelRoomUpsertResponse> UpdateAsync(int hotelId, int RoomId, HotelRoomRequest request)
        {
            var hr = await _context.HotelRooms
                .FirstOrDefaultAsync(h => h.HotelId == hotelId && h.RoomId == RoomId);

            if (hr == null) throw new Exception("HotelRoom nije pronađen.");

            hr.RoomsLeft = request.RoomsLeft;

            await _context.SaveChangesAsync();
            await _context.Entry(hr).Reference(r => r.Rooms).LoadAsync();

            return new HotelRoomUpsertResponse
            {
                HotelId = hr.HotelId,
                RoomId = hr.RoomId,
                RoomType = hr.Rooms.RoomType,
                RoomsLeft = hr.RoomsLeft
            };
        }

        public async Task<bool> DeleteAsync(int hotelId, int RoomId)
        {
            var hr = await _context.HotelRooms
                .FirstOrDefaultAsync(h => h.HotelId == hotelId && h.RoomId == RoomId);

            if (hr == null) return false;

            _context.HotelRooms.Remove(hr);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<decimal> CalculateRoomPrice(int offerId, int roomCount)
        {
            const decimal ROOM_INCREMENT = 100m;

            // 1️⃣ Find the offer from database
            var offer = await _offerService.GetOfferDetailsByIdForUser(offerId);
            if (offer == null)
                throw new Exception($"Offer with ID {offerId} not found.");

            // 2️⃣ Calculate based on minimalPrice and roomCount
            decimal finalPrice = offer.minimalPrice + (roomCount - 2) * ROOM_INCREMENT;

            // Prevent negative prices (optional safety)
            if (finalPrice < 0)
                finalPrice = 0;

            return finalPrice;
        }
    }
}