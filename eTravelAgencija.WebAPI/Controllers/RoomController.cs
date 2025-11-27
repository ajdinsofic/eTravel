using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    
    public class RoomController : BaseController<Model.model.Room, BaseSearchObject>
    {
        public RoomController(ILogger<BaseController<Model.model.Room, BaseSearchObject>> logger, IRoomService roomService) : base (logger, roomService)
        {
            
        }
    }
}