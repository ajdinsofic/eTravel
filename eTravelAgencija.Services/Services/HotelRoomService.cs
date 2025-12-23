using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Services
{
    public class HotelRoomsService
        : BaseCRUDService<Model.model.HotelRooms, HotelRoomSearchObject, Database.HotelRooms, HotelRoomInsertRequest, HotelRoomUpdateRequest>,
          IHotelRoomsService
    {
        public HotelRoomsService(eTravelAgencijaDbContext context, IMapper mapper)
            : base(context, mapper)
        { }

        public override IQueryable<HotelRooms> ApplyFilter(IQueryable<HotelRooms> query, HotelRoomSearchObject search)
        {
            if (search.hotelId.HasValue)
            {
                query = query.Where(h => h.HotelId == search.hotelId);
            }
            return base.ApplyFilter(query, search);
        }

        public async Task<bool> IncreaseRoomsLeft(int hotelId, int roomId)
        {
            var hotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(x =>
                    x.HotelId == hotelId &&
                    x.RoomId == roomId);

            if (hotelRoom == null)
                throw new Exception("HotelRoom nije pronađen.");

            hotelRoom.RoomsLeft += 1;

            await _context.SaveChangesAsync();
            return true;
        }



    }
}
