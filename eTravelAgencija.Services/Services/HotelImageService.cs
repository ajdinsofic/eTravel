

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;
using eTravelAgencija.Services.Interfaces;


namespace eTravelAgencija.Services.Services
{
    public class HotelImageService : BaseCRUDService<
    Model.model.HotelImages, BaseSearchObject, Database.HotelImages, HotelImageUpsertRequest, HotelImageUpsertRequest>, IHotelImageService
    {
        public HotelImageService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }


}