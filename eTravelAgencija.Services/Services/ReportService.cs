using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly eTravelAgencijaDbContext _context;
        public ReportService(eTravelAgencijaDbContext context)
        {
            _context = context;
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.UtcNow.Date;

            int age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }


        private string GetAgeGroup(int age)
        {
            if (age >= 18 && age <= 24) return "18-24";
            if (age >= 25 && age <= 34) return "25-34";
            if (age >= 35 && age <= 44) return "35-44";
            if (age >= 45 && age <= 60) return "45-60";
            return "60+";
        }


        public async Task<List<AgeGroupStatsResponse>> GetAgeReport(string range)
        {
            DateTime now = DateTime.UtcNow.Date;
            DateTime fromDate;
            DateTime toDate = now;

            switch (range)
            {
                case "dan":
                    fromDate = now;
                    break;

                case "sedmica":
                    int dayOfWeek = (int)now.DayOfWeek;
                    if (dayOfWeek == 0) dayOfWeek = 7;

                    fromDate = now.AddDays(-(dayOfWeek - 1));
                    break;

                case "mjesec":
                    fromDate = now.AddMonths(-1);
                    break;

                default:
                    throw new Exception("Nevažeći interval (dan, sedmica, mjesec).");
            }

            var usersQuery = _context.Users
                .Include(u => u.UserRoles)
                .Where(u => u.UserRoles.Any(ur => ur.RoleId == 1))
                .AsQueryable();

            // Filtar po vremenskom intervalu
            if (range == "mjesec")
            {
                usersQuery = usersQuery
                    .Where(u => u.CreatedAt.Year == now.Year && u.CreatedAt.Month == now.Month);
            }
            else
            {
                usersQuery = usersQuery
                    .Where(u => u.CreatedAt.Date >= fromDate && u.CreatedAt.Date <= toDate);
            }

            var users = await usersQuery.ToListAsync();
            int total = users.Count;

            var groups = users
                .GroupBy(u => GetAgeGroup(CalculateAge(u.DateBirth)))
                .Select(g => new AgeGroupStatsResponse
                {
                    AgeGroup = g.Key,
                    Count = g.Count(),
                    Percentage = total == 0 ? 0 :
                        Math.Round(((decimal)g.Count() / total) * 100, 2)
                })
                .OrderByDescending(g => g.Count)
                .ToList();

            return groups; // ← sada vraća samo listu
        }


        public async Task<List<DestinationStatsResponse>> GetTopDestinations()
        {
            var offers = await _context.OfferDetails
                .Include(o => o.Offer)
                .OrderByDescending(o => o.TotalCountOfReservations)
                .Take(5)
                .ToListAsync();

            int total = offers.Sum(o => o.TotalCountOfReservations);

            var result = offers
                .Select(o => new DestinationStatsResponse
                {
                    DestinationName = o.Offer.Title,
                    Count = o.TotalCountOfReservations,
                    Percentage = total == 0 ? 0 :
                        Math.Round(((decimal)o.TotalCountOfReservations / total) * 100, 2)
                })
                .ToList();

            return result;
        }

    }
}