

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
using System.IO;


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
                query = query.Where(h => h.HotelId == search.hotelId);
            }
            return base.ApplyFilter(query, search);
        }

        public override Task BeforeDeleteAsync(HotelImages entity)
        {
            try
            {
                
                var folderPath = Path.Combine("wwwroot", "images", "hotels");

               
                var filePath = Path.Combine(folderPath, entity.ImageUrl);

                
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

            entity.ImageUrl = fileName;
        }

    }


}