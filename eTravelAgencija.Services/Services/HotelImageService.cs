using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class HotelImageService : BaseImageService<HotelImages, HotelImageResponse>, IHotelImageService
    {
        public HotelImageService(eTravelAgencijaDbContext context)
            : base(
                context,
                x => x.HotelId,
                x => x.ImageUrl)
        {
        }
    }


}