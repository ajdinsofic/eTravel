using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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



        public DateTime GetInstallmentDate(
        DateTime today,
        DateTime departureDate,
        int installmentNumber)
        {
            // Puna uplata nema rate / datuma
            if (installmentNumber == 4)
            {
                return default(DateTime);
            }

            // Dozvoljene rate: 1, 2, 3 i 5 (preostali iznos)
            if (installmentNumber < 1 ||
                (installmentNumber > 3 && installmentNumber != 5))
            {
                throw new ArgumentException(
                    "Broj rate mora biti 1, 2, 3 ili 5. Za punu uplatu koristite 4.");
            }

            var first = today.AddDays(3);
            var third = departureDate.AddDays(-7);
            var second = first.AddDays((third - first).TotalDays / 2);

            return installmentNumber switch
            {
                1 => first,
                2 => second,
                3 => third,
                5 => third, // ✅ PREOSTALI IZNOS → isti rok kao III rata
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

        public async Task<Model.model.PaymentSummary> GetPaymentSummary(int reservationId)
{
    // =========================
    // 1️⃣ Rezervacija
    // =========================
    var reservation = await _context.Reservations
                .Include(r => r.OfferDetails)
                    .ThenInclude(o => o.OfferHotels)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

    if (reservation == null)
        throw new Exception("Rezervacija nije pronađena.");

    // =========================
    // 2️⃣ Plaćanja
    // =========================
    var payments = await _context.Payments
        .Where(p => p.ReservationId == reservationId)
        .ToListAsync();

    var confirmed = payments.Where(p => p.IsConfirmed).ToList();
    var pending   = payments.Where(p => !p.IsConfirmed).ToList();

    var confirmedRateIds = confirmed
        .Select(p => p.RateId)
        .ToHashSet();

    bool rate1Confirmed      = confirmedRateIds.Contains(1);
    bool rate2Confirmed      = confirmedRateIds.Contains(2);
    bool fullConfirmed       = confirmedRateIds.Contains(4);
    bool remainingConfirmed  = confirmedRateIds.Contains(5);

    bool rate1Pending     = pending.Any(p => p.RateId == 1);
    bool rate2Pending     = pending.Any(p => p.RateId == 2);
    bool fullPending      = pending.Any(p => p.RateId == 4);
    bool remainingPending = pending.Any(p => p.RateId == 5);

    bool anyPending = pending.Any();

    int? pendingRateId = pending
        .Select(p => (int?)p.RateId)
        .FirstOrDefault();

    var summary = new Model.model.PaymentSummary();

    // ======================================================
    // 🔴 FULLY PAID CHECK (HARD STOP)
    // ======================================================
    bool isPaid_1_5   = confirmedRateIds.SetEquals(new[] { 1, 5 });
    bool isPaid_1_2_5 = confirmedRateIds.SetEquals(new[] { 1, 2, 5 });
    bool isPaid_4     = confirmedRateIds.SetEquals(new[] { 4 });

    if (isPaid_1_5 || isPaid_1_2_5 || isPaid_4)
    {
        if (confirmedRateIds.Contains(1))
        {
            summary.IsFirstRateVisible  = true;
            summary.IsFirstRateDisabled = true;
            summary.IsFirstRatePending  = true;
        }

        if (confirmedRateIds.Contains(2))
        {
            summary.IsSecondRateVisible  = true;
            summary.IsSecondRateDisabled = true;
            summary.IsSecondRatePending  = true;
        }

        if (confirmedRateIds.Contains(5))
        {
            summary.IsRemainingVisible  = true;
            summary.IsRemainingDisabled = true;
            summary.IsRemainingPending  = true;
        }

        if (confirmedRateIds.Contains(4))
        {
            summary.IsFullAmountVisible  = true;
            summary.IsFullAmountDisabled = true;
            summary.IsFullAmountPending  = true;
        }

        return summary;
    }

    if (pendingRateId.HasValue)
    {
        var departureDate = reservation.OfferDetails
                .OfferHotels
                .FirstOrDefault()
                ?.DepartureDate;
        
        summary.PaymentDeadline = GetInstallmentDate(
            DateTime.Today,
            departureDate.Value,
            pendingRateId.Value
        );
    }

    // =========================
    // 4️⃣ I RATA
    // =========================
    summary.IsFirstRateVisible = true;

    if (rate1Confirmed)
    {
        summary.IsFirstRateDisabled = true;
        summary.IsFirstRatePending  = true;
    }
    else if (rate1Pending)
    {
        summary.IsFirstRateDisabled = true;
        summary.IsFirstRatePending  = false;
    }
    else if (anyPending)
    {
        summary.IsFirstRateDisabled = true;
    }

    // =========================
    // 5️⃣ II RATA
    // =========================
    if (rate1Confirmed)
    {
        summary.IsSecondRateVisible = true;

        if (rate2Confirmed)
        {
            summary.IsSecondRateDisabled = true;
            summary.IsSecondRatePending  = true;
        }
        else if (rate2Pending)
        {
            summary.IsSecondRateDisabled = true;
            summary.IsSecondRatePending  = false;
        }
        else if (anyPending)
        {
            summary.IsSecondRateDisabled = true;
        }
    }

    // =========================
    // 6️⃣ PREOSTALI IZNOS
    // =========================
    if (!fullConfirmed && !fullPending)
    {
        if (rate1Confirmed && !rate1Pending)
        {
            summary.IsRemainingVisible = true;

            if (remainingConfirmed)
            {
                summary.IsRemainingVisible = false;
            }
            else if (remainingPending)
            {
                summary.IsRemainingDisabled = true;
                summary.IsRemainingPending  = false;
            }
            else if (rate2Pending || anyPending)
            {
                summary.IsRemainingDisabled = true;
            }
        }
    }

    // =========================
    // 7️⃣ PUNI IZNOS
    // =========================
    if (!rate1Confirmed)
    {
        summary.IsFullAmountVisible = true;

        if (fullConfirmed)
        {
            summary.IsFullAmountDisabled = true;
            summary.IsFullAmountPending  = true;
        }
        else if (fullPending)
        {
            summary.IsFullAmountDisabled = true;
            summary.IsFullAmountPending  = false;
        }
        else if (rate1Pending || anyPending)
        {
            summary.IsFullAmountDisabled = true;
        }
    }

    return summary;
}



    }
}