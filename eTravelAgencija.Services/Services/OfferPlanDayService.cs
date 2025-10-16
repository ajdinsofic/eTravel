using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class OfferPlanDayService : IOfferPlanDayService
    {
        private readonly eTravelAgencijaDbContext _context;

        public OfferPlanDayService(eTravelAgencijaDbContext context)
        {
            _context = context;
        }

        public async Task<List<OfferPlanDayResponse>> GetOfferPlanDays(int offerId)
        {
            var planDays = await _context.OfferPlanDays
                .Where(p => p.OfferDetailsId == offerId)
                .OrderBy(p => p.DayNumber) 
                .Select(p => new OfferPlanDayResponse
                {
                    DayNumber = p.DayNumber,
                    Title = p.DayTitle,
                    Description = p.DayDescription
                })
                .ToListAsync();

            return planDays;
        }

    }
}