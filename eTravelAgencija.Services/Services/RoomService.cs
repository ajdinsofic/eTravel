using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Services
{
    public class RoomService : BaseService<Model.model.Room, BaseSearchObject, Database.Rooms>, IRoomService
    {
        public RoomService(eTravelAgencijaDbContext context, IMapper mapper) : base (context, mapper)
        {
            
        }
    }
}