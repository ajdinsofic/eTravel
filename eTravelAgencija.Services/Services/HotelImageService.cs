using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.Services.Services
{
    public class HotelImageService : BaseCRUDService<
    Model.model.HotelImages, HotelImageSearchObject, Database.HotelImages, HotelImageInsertRequest, HotelImageUpdateRequest>, IHotelImageService
    {
        public HotelImageService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<HotelImages> ApplyFilter(IQueryable<HotelImages> query, HotelImageSearchObject search)
        {
            if (search.hotelId.HasValue)
            {
                query = query.Where(o => o.HotelId == search.hotelId);
            }
            return base.ApplyFilter(query, search);
        }

        public override Task BeforeDeleteAsync(HotelImages entity)
        {
            try
            {
                // 1. Lokacija foldera
                var folderPath = Path.Combine("wwwroot", "images", "hotels");

                // 2. Kompletna putanja slike
                var filePath = Path.Combine(folderPath, entity.ImageUrl);

                // 3. Ako fajl postoji — obriši ga
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Greška pri brisanju slike sa diska: " + ex.Message);
            }

            return base.BeforeDeleteAsync(entity);
        }


        public override async Task BeforeInsertAsync(HotelImages entity, HotelImageInsertRequest request)
        {
            if (request.image == null)
                throw new Exception("Image file not provided");

            var extension = Path.GetExtension(request.image.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            var folder = Path.Combine("wwwroot", "images", "hotels");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await request.image.CopyToAsync(stream);

            // BACKEND SETS THE URL
            entity.ImageUrl = fileName;
        }
    }

}