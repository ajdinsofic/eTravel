using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class HotelImageController : BaseImageController<HotelImages, HotelImageResponse>
    {
        public HotelImageController(IHotelImageService service)
            : base(service)
        {
        }
    }

}