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
        private readonly IReservationService _reservationService;
        public PaymentService(eTravelAgencijaDbContext context, IMapper mapper, IReservationService reservationService) : base(context, mapper)
        {
             _reservationService = reservationService;
        }

        public override IQueryable<Payment> ApplyFilter(IQueryable<Payment> query, PaymentSearchObject search)
        {
            if (search.reservationId.HasValue)
            {
                query = query.Where(r => r.ReservationId == search.reservationId);
            }
            if (search.rateId.HasValue)
            {
                query = query.Where(r => r.RateId == search.rateId);
            }
            return base.ApplyFilter(query, search);
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
            var forth = departureDate.AddDays(-7);
            var fifth = departureDate.AddDays(-7);

            return installmentNumber switch
            {
                1 => first,
                2 => second,
                3 => third,
                4 => forth,
                5 => fifth,
                _ => throw new ArgumentException("Nevažeći broj rate.")
            };
        }

        public async Task ProcessPayments()
        {
            var payments = await _context.Payments
                .Include(p => p.Reservation)
                .ThenInclude(r => r.Payments)
                .ToListAsync();
        
            var today = DateTime.UtcNow.Date;
        
            foreach (var payment in payments)
            {
                var reservation = payment.Reservation;
        
                if (reservation == null)
                    continue;
        
                if (payment.RateId == 4)
                {
                    await HandleFullAmountPayment(payment, reservation, today);
                    continue;
                }
        
                if (payment.RateId == 1 || payment.RateId == 2 || payment.RateId == 3)
                {
                    await HandleRatePayment(payment, reservation, today);
                    continue;
                }
        
                if (payment.RateId == 5)
                {
                    await HandleRemainingPayment(payment, reservation, today);
                    continue;
                }
            }
        
            await _context.SaveChangesAsync();
        }

        private async Task HandleFullAmountPayment(Payment payment, Reservation reservation, DateTime today)
        {
            if (payment.IsConfirmed)
                return;
        
            // Deadline prošao?
            if (today > payment.PaymentDeadline.Date)
            {
                if (!payment.DeadlineExtended)
                {
                    payment.PaymentDeadline = payment.PaymentDeadline.AddDays(3);
                    payment.DeadlineExtended = true;
                    return;
                }
        

                await _reservationService.DeleteAsync(reservation.Id);
            }
        }

        private async Task HandleRatePayment(Payment payment, Reservation reservation, DateTime today)
        {
            if (payment.IsConfirmed)
                return;
        

            if (today > payment.PaymentDeadline.Date)
            {
                if (!payment.DeadlineExtended)
                {
                    payment.PaymentDeadline = payment.PaymentDeadline.AddDays(3);
                    payment.DeadlineExtended = true;
                    return;
                }
        

                await _reservationService.DeleteAsync(reservation.Id);
            }
        }

        private async Task HandleRemainingPayment(Payment payment, Reservation reservation, DateTime today)
        {
            if (payment.IsConfirmed)
                return;
        
            if (today > payment.PaymentDeadline.Date)
            {
                if (!payment.DeadlineExtended)
                {
                    payment.PaymentDeadline = payment.PaymentDeadline.AddDays(3);
                    payment.DeadlineExtended = true;
                    return;
                }
        
                await _reservationService.DeleteAsync(reservation.Id);
            }
        }




    }
}