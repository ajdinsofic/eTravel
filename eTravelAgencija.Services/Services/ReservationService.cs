using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Interfaces;
using AutoMapper;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Services.Utils.Pdf;
using eTravelAgencija.Models.Requests;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using eTravelAgencija.EmailConsumer.Messages;

namespace eTravelAgencija.Services.Services
{
    public class ReservationService : BaseCRUDService<Model.model.Reservation, ReservationSearchObject, Database.Reservation, ReservationUpsertRequest, ReservationUpsertRequest>, IReservationService
    {
        private readonly IConnection _rabbitConnection;
        public ReservationService(eTravelAgencijaDbContext context, IMapper mapper, IConnection rabbitConnection) : base(context, mapper)
        {
            _rabbitConnection = rabbitConnection;
        }

        public override IQueryable<Reservation> ApplyFilter(IQueryable<Reservation> query, ReservationSearchObject search)
        {
            if (search.UserId.HasValue)
            {
                query = query.Where(r => r.UserId == search.UserId);
            }

            if (search.isActive == true)
            {
                query = query.Where(r => r.IsActive == true);
            }
            else if (search.isActive == false)
            {
                query = query.Where(r => r.IsActive == false);
            }

            return base.ApplyFilter(query, search);
        }

        public override async Task BeforeInsertAsync(Reservation entity, ReservationUpsertRequest request)
        {
            var hotelRoom = await _context.HotelRooms
                .FirstOrDefaultAsync(hr =>
                    hr.HotelId == entity.HotelId &&
                    hr.RoomId == entity.RoomId);

            if (hotelRoom != null)
            {
                if (hotelRoom.RoomsLeft > 0)
                {
                    hotelRoom.RoomsLeft--;
                }
                else
                {
                    throw new Exception("Nema više slobodnih soba za odabrani hotel i tip sobe.");
                }
            }

            await base.BeforeInsertAsync(entity, request);
        }

        public override async Task<IEnumerable<Reservation>> AfterGetAsync(
    IEnumerable<Reservation> entities,
    ReservationSearchObject? search = null)
        {
            foreach (var reservation in entities)
            {
                var confirmedPayments = await _context.Payments
                    .Where(p => p.ReservationId == reservation.Id && p.IsConfirmed)
                    .ToListAsync();

                var paidAmount = confirmedPayments.Sum(p => p.Amount);

                reservation.PriceLeftToPay = reservation.TotalPrice - paidAmount;

                if (reservation.PriceLeftToPay < 0)
                    reservation.PriceLeftToPay = 0;
            }

            return await base.AfterGetAsync(entities, search);
        }

        public async Task CheckAllReservationsActive()
        {
            var today = DateTime.UtcNow.Date;

            // 1️⃣ Učitaj sve još aktivne rezervacije
            var reservations = await _context.Reservations
                .Where(r => r.IsActive == true)
                .ToListAsync();

            if (!reservations.Any())
                return;

            foreach (var reservation in reservations)
            {
                // 2️⃣ Nađi OfferHotel za tu rezervaciju
                var offerHotel = await _context.OfferHotels
                    .FirstOrDefaultAsync(oh =>
                        oh.OfferDetailsId == reservation.OfferId &&
                        oh.HotelId == reservation.HotelId);

                if (offerHotel == null)
                    continue; // skip if something is missing

                var returnDate = offerHotel.ReturnDate.Date;

                // 3️⃣ Ako je putovanje završeno → deaktiviraj rezervaciju
                if (today >= returnDate)
                {
                    reservation.IsActive = false;
                }
            }

            // 4️⃣ Sačuvaj sve promjene odjednom
            await _context.SaveChangesAsync();
        }

        public async Task<byte[]> GenerateBillPdf(BillRequest req)
        {
            var reservation = await _context.Reservations
                .FirstAsync(x => x.Id == req.ReservationId);

            var offer = await _context.OfferDetails
                .FirstAsync(x => x.OfferId == reservation.OfferId);

            decimal insurance = reservation.IncludeInsurance
                ? offer.TravelInsuranceTotal
                : 0;

            decimal travelPrice =
                reservation.TotalPrice
                - offer.ResidenceTotal
                - insurance;

            var bill = new BillResponse
            {
                ReservationId = reservation.Id,
                CreatedAt = DateTime.UtcNow,

                UserFullName = req.UserFullName,
                OfferTitle = req.OfferTitle,
                HotelName = req.HotelName,
                HotelStars = req.HotelStars,
                RoomType = req.RoomType,

                TravelPrice = travelPrice,
                ResidenceTax = offer.ResidenceTotal,
                Insurance = insurance,

                IsDiscountUsed = reservation.isDiscountUsed,
                DiscountPercent = reservation.DiscountValue * 100,

                Total = reservation.TotalPrice
            };

            return BillPdf.Generate(bill);
        }

        public async Task ConfirmReservationAsync(
            int reservationId,
            DateTime departureDate,
            DateTime returnDate)
        {
            var reservation = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.OfferDetails).ThenInclude(x => x.Offer)
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                throw new Exception("Rezervacija ne postoji.");

            await _context.SaveChangesAsync();

            await SendReservationConfirmEmail(reservation, departureDate, returnDate);
        }


        private async Task SendReservationConfirmEmail(Reservation reservation, DateTime departureDate, DateTime returnDate)
        {
            var channel = await _rabbitConnection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "email.reservation-confirmation",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = new
            {
                To = reservation.User.Email,
                FullName = $"{reservation.User.FirstName} {reservation.User.LastName}",
                OfferName = reservation.OfferDetails.Offer.Title,
                HotelName = reservation.Hotel.Name,
                HotelStars = reservation.Hotel.Stars,
                RoomType = reservation.Room.RoomType,
                DepartureDate = departureDate,
                ReturnDate = returnDate,
                TotalPrice = reservation.TotalPrice
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "email.reservation-confirmation",
                body: body
            );
        }

        public async Task<bool> CancelReservation(int reservationId, string? emailOverride = null)
    {
        // 1) Učitaj sve što treba
        var res = await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.OfferDetails).ThenInclude(r => r.Offer)
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (res == null) return false;

        // 2) Pripremi podatke za mail
        var toEmail = emailOverride ?? res.User?.Email;
        if (string.IsNullOrWhiteSpace(toEmail)) return false;

        var msg = new ReservationCancelledEmailMessage
        {
            To = toEmail,
            FullName = $"{res.User!.FirstName} {res.User!.LastName}",
            ReservationId = res.Id,
            DestinationName = res.OfferDetails?.Offer?.Title ?? "Vaše putovanje",
            CancelReason = "Premalog broja prijavljenih putnika",
            VoucherUsed = res.isDiscountUsed,
            PaidAmount = res.TotalPrice - res.PriceLeftToPay,
            AgencyName = "eTravel",
            Phone = "☎ +387 61 123 456"
        };

        // 3) Publish u RabbitMQ
        await PublishReservationCancelledAsync(msg);

        return true;
    }

    private async Task PublishReservationCancelledAsync(ReservationCancelledEmailMessage msg)
    {
        var channel = await _rabbitConnection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "email.reservation-cancelled",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msg));

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "email.reservation-cancelled",
            body: body
        );
    }
    }
}