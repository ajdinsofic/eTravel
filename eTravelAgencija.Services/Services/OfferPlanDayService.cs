// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using eTravelAgencija.Model.ResponseObjects;
// using eTravelAgencija.Model.RequestObjects;
// using eTravelAgencija.Services.Database;
// using Microsoft.EntityFrameworkCore;

// namespace eTravelAgencija.Services.Services
// {
//     public class OfferPlanDayService : IOfferPlanDayService
//     {
//         private readonly eTravelAgencijaDbContext _context;
//         private readonly IOfferService _offerService;

//         public OfferPlanDayService(eTravelAgencijaDbContext context, IOfferService offerService)
//         {
//             _context = context;
//             _offerService = offerService;
//         }

//         public async Task<List<OfferPlanDayResponse>> GetOfferPlanDaysAsync(int offerId)
//         {
//             var list = await _context.OfferPlanDays
//                 .Where(x => x.OfferDetailsId == offerId)
//                 .OrderBy(x => x.DayNumber)
//                 .Select(x => new OfferPlanDayResponse
//                 {
//                     OfferDetailsId = x.OfferDetailsId,
//                     DayNumber = x.DayNumber,
//                     Title = x.DayTitle,
//                     Description = x.DayDescription
//                 }).ToListAsync();

//             return list;
//         }

//         public async Task<OfferPlanDayResponse> GetByKeyAsync(int offerId, int dayNumber)
//         {
//             var entity = await _context.OfferPlanDays
//                 .FirstOrDefaultAsync(x => x.OfferDetailsId == offerId && x.DayNumber == dayNumber);

//             if (entity == null)
//                 return null;

//             return new OfferPlanDayResponse
//             {
//                 OfferDetailsId = entity.OfferDetailsId,
//                 DayNumber = entity.DayNumber,
//                 Title = entity.DayTitle,
//                 Description = entity.DayDescription
//             };
//         }

//         public async Task<OfferPlanDayResponse> InsertAsync(OfferPlanDayUpsertRequest request)
//         {
//             // 1️⃣ Dohvati ponudu putem OfferService
//             var offer = await _offerService.GetOfferById(request.OfferDetailsId);
//             if (offer == null)
//                 throw new Exception($"Ponuda s ID {request.OfferDetailsId} ne postoji.");

//             // 2️⃣ Provjeri postoji li već taj dan
//             bool dayExists = await _context.OfferPlanDays
//                 .AnyAsync(x => x.OfferDetailsId == request.OfferDetailsId && x.DayNumber == request.DayNumber);

//             if (dayExists)
//                 throw new Exception($"Dan broj {request.DayNumber} već postoji za ovu ponudu.");

//             // 3️⃣ Provjeri maksimalan broj dana prema DaysInTotal
//             int existingDays = await _context.OfferPlanDays
//                 .CountAsync(x => x.OfferDetailsId == request.OfferDetailsId);

//             if (existingDays >= offer.DaysInTotal)
//                 throw new Exception($"Ne možete dodati više od {offer.DaysInTotal} dana za ovu ponudu.");

//             // 4️⃣ Ako je sve u redu – kreiraj zapis
//             var entity = new OfferPlanDay
//             {
//                 OfferDetailsId = request.OfferDetailsId,
//                 DayNumber = request.DayNumber,
//                 DayTitle = request.Title,
//                 DayDescription = request.Description
//             };

//             _context.OfferPlanDays.Add(entity);
//             await _context.SaveChangesAsync();

//             return new OfferPlanDayResponse
//             {
//                 OfferDetailsId = entity.OfferDetailsId,
//                 DayNumber = entity.DayNumber,
//                 Title = entity.DayTitle,
//                 Description = entity.DayDescription
//             };
//         }

//         public async Task<OfferPlanDayResponse> UpdateAsync(int offerId, int dayNumber, OfferPlanDayUpsertRequest request)
//         {
//             var entity = await _context.OfferPlanDays
//                 .FirstOrDefaultAsync(x => x.OfferDetailsId == offerId && x.DayNumber == dayNumber);

//             if (entity == null)
//                 throw new Exception($"Dan {dayNumber} za ponudu {offerId} nije pronađen.");

//             entity.DayTitle = request.Title;
//             entity.DayDescription = request.Description;

//             await _context.SaveChangesAsync();

//             return new OfferPlanDayResponse
//             {
//                 OfferDetailsId = entity.OfferDetailsId,
//                 DayNumber = entity.DayNumber,
//                 Title = entity.DayTitle,
//                 Description = entity.DayDescription
//             };
//         }

//         public async Task<bool> DeleteAsync(int offerId, int dayNumber)
//         {
//             var entity = await _context.OfferPlanDays
//                 .FirstOrDefaultAsync(x => x.OfferDetailsId == offerId && x.DayNumber == dayNumber);

//             if (entity == null)
//                 return false;

//             _context.OfferPlanDays.Remove(entity);
//             await _context.SaveChangesAsync();
//             return true;
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;
using eTravelAgencija.Model.SearchObjects;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace eTravelAgencija.Services.Services
{
    
    public class OfferPlanDayService : BaseCRUDService<Model.model.OfferPlanDay,OfferPlanDaySearchObject,Database.OfferPlanDay,OfferPlanDayUpsertRequest,OfferPlanDayUpsertRequest>, IOfferPlanDayService
    {
        public OfferPlanDayService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<OfferPlanDay> ApplyFilter(IQueryable<OfferPlanDay> query, OfferPlanDaySearchObject search)
        {
            if (search.OfferId.HasValue)
            {
                query = query.Where(x => x.OfferDetailsId == search.OfferId.Value);
            }

            if (search.dayNumber.HasValue)
            {
                query = query.Where(x => x.DayNumber == search.dayNumber);
            }
            return base.ApplyFilter(query, search);
        }
    }
}
