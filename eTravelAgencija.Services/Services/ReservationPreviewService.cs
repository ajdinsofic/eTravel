using System;
using System.Threading.Tasks;
using eTravelAgencija.Model.model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Database;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class ReservationPreviewService : IResevationPreviewService
    {
        private readonly eTravelAgencijaDbContext _context;

        public ReservationPreviewService(eTravelAgencijaDbContext context)
        {
            _context = context;
        }

        public async Task<Model.model.ReservationPreview> GeneratePreviewAsync(ReservationPreviewRequest request)
        {
            var offer = await _context.OfferDetails
                .Include(o => o.Offer)
                .FirstOrDefaultAsync(o => o.OfferId == request.OfferId);

            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == request.HotelId);
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == request.RoomId);

            if (offer == null || hotel == null || room == null)
                throw new Exception("Nevažeća ponuda, hotel ili soba.");

            decimal total = request.BasePrice;
            if (request.IncludeInsurance)
            {
                total += offer.TravelInsuranceTotal;
            }

            total += offer.ResidenceTotal;

            if (!string.IsNullOrEmpty(request.VoucherCode))
            {
                var voucher = await _context.Vouchers
                    .FirstOrDefaultAsync(v => v.VoucherCode == request.VoucherCode);

                if (voucher == null)
                    throw new Exception("Nevažeći kod vaučera.");

                bool userHasVoucher = await _context.UserVouchers
                    .AnyAsync(uv => uv.UserId == request.UserId && uv.VoucherId == voucher.Id);

                if (!userHasVoucher)
                    throw new Exception("Nevažeći kod — ovaj vaučer nije povezan s vašim računom.");

                // Primijeni popust
                total -= total * voucher.Discount;
            }

            // 🔹 4. Generiši rezultat
            var preview = new ReservationPreview
            {
                UserId = request.UserId,
                OfferId = request.OfferId,
                HotelId = request.HotelId,
                RoomId = request.RoomId,
                OfferTitle = offer?.Offer?.Title ?? "",
                HotelTitle = hotel?.Name ?? "",
                HotelStars = hotel?.Stars.ToString() ?? "",
                RoomType = room?.RoomType ?? "",
                ResidenceTaxTotal = offer.ResidenceTotal,
                Insurance = offer.TravelInsuranceTotal,
                IncludeInsurance = true,
                VoucherCode = request.VoucherCode,
                TotalPrice = total
            };

            return preview;
        }
    }
}
