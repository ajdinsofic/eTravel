using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class PaymentService : BaseCRUDService<Model.model.Payment, PaymentSearchObject, Database.Payment, PaymentInsertRequest, PaymentUpdateRequest>, IPaymentService
    {
        public PaymentService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task BeforeInsertAsync(Payment entity, PaymentInsertRequest request)
        {

            var reservation = await _context.Reservations
                .Include(r => r.OfferDetails)
                    .ThenInclude(o => o.OfferHotels)
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId);

            if (reservation == null)
                throw new Exception("Rezervacija nije pronađena.");


            var departureDate = reservation.OfferDetails
                .OfferHotels
                .FirstOrDefault()
                ?.DepartureDate;

            if (departureDate == null)
                throw new Exception("Polazak nije definisan za ovu ponudu.");

            var deadline = GetInstallmentDate(
                DateTime.UtcNow,
                departureDate.Value,
                request.RateId
            );

            entity.PaymentDeadline = deadline;
        }



        public DateTime GetInstallmentDate(DateTime today, DateTime departureDate, int installmentNumber)
        {
            
            if (installmentNumber == 4)
            {
                
                return default(DateTime); 
            }

            if (installmentNumber < 1 || installmentNumber > 3)
                throw new ArgumentException("Broj rate mora biti 1, 2 ili 3 za rate. Za punu uplatu koristite 4.");


            var first = today.AddDays(3);
            var third = departureDate.AddDays(-7);
            var second = first.AddDays((third - first).TotalDays / 2);

            return installmentNumber switch
            {
                1 => first,
                2 => second,
                3 => third,
                _ => throw new ArgumentException("Nevažeći broj rate.")
            };
        }
    }
}