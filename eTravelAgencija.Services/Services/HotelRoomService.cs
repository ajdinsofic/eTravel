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
        : BaseCRUDService<Model.model.HotelRooms, BaseSearchObject, Database.HotelRooms, HotelRoomInsertRequest, HotelRoomUpdateRequest>,
          IHotelRoomsService
    {
        public HotelRoomsService(eTravelAgencijaDbContext context, IMapper mapper)
            : base(context, mapper)
        {}

       
    }
}
