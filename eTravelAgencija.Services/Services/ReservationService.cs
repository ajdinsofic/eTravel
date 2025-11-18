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

namespace eTravelAgencija.Services.Services
{
    public class ReservationService : BaseCRUDService<Model.model.Reservation,ReservationSearchObject,Database.Reservation,ReservationUpsertRequest, ReservationUpsertRequest>, IReservationService
    {
        public ReservationService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
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

        public override async Task<IEnumerable<Reservation>> AfterGetAsync(IEnumerable<Reservation> entities, ReservationSearchObject? search = null)
        {
            foreach (var reservation in entities)
            {

                var payments = await _context.Payments
                    .Where(p => p.ReservationId == reservation.Id)
                    .ToListAsync();
        
                var confirmedPayments = payments
                    .Where(p => p.IsConfirmed)
                    .ToList();
        
                if (confirmedPayments.Any(p => p.RateId == 4))
                {
                    reservation.PriceLeftToPay = 0;
                    continue;
                }
        
                decimal remaining = reservation.TotalPrice;
        
        
                foreach (var rateId in new[] { 1, 2, 3 })
                {
                    var payment = confirmedPayments.FirstOrDefault(p => p.RateId == rateId);
                    if (payment != null)
                    {
                        remaining -= payment.Amount;
                    }
                }
        
                bool allThreePaid = confirmedPayments.Count(p => p.RateId is 1 or 2 or 3) == 3;
                if (allThreePaid)
                {
                    reservation.PriceLeftToPay = 0;
                }
                else
                {
                    reservation.PriceLeftToPay = remaining < 0 ? 0 : remaining;
                }
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



    }
}