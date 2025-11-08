using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using Microsoft.EntityFrameworkCore;

namespace eTravelAgencija.Services.Services
{
    public class BookingPreviewService : IBookingPreviewService
    {
        private readonly eTravelAgencijaDbContext _context;
        private readonly IOfferService _offerService;
        private readonly IHotelService _hotelService;
        private readonly IHotelImageService _hotelImageService;
        private readonly IOfferHotelService _offerHotelService;
        private readonly IHotelRoomsService _hotelRoomService;

        public BookingPreviewService(
            eTravelAgencijaDbContext context,
            IOfferService offerService,
            IHotelService hotelService,
            IHotelImageService hotelImageService,
            IOfferHotelService offerHotelService)
        {
            _context = context;
            _offerService = offerService;
            _hotelService = hotelService;
            _hotelImageService = hotelImageService;
            _offerHotelService = offerHotelService;
        }

        public async Task<BookingPreviewResponse> GetBookingPreviewAsync(BookingPreviewRequest request)
        {
            // 1️⃣ Provjeri da li postoji veza između hotela i ponude
            var offerHotel = await _offerHotelService.GetOfferHotelLinkAsync(request.OfferId, request.HotelId);
            if (offerHotel == null)
                throw new Exception("Odabrani hotel nije povezan s ovom ponudom.");

            var hotelRoom = await _hotelRoomService.GetByKeyForAdminAsync(request.HotelId, request.RoomId);
            if (hotelRoom == null)
                throw new Exception("Odabrana soba nije povezan s ovim hotelom.");

            // 2️⃣ Dohvati detalje ponude i hotela
            var offer = await _offerService.GetOfferWithDetailsById(request.OfferId);
            var hotel = await _hotelService.GetHotelByIdAsync(request.HotelId);

            if (offer == null || hotel == null)
                throw new Exception("Ponuda ili hotel nisu pronađeni.");

            
            // 3️⃣ Izračunaj dodatne troškove
            var residenceTaxTotal = offer.ResidenceTotal;
            var insurance = request.IncludeInsurance ? offer.TravelInsuranceTotal : 0;
            var total = request.CalculatedPrice + residenceTaxTotal + insurance;

            // 4️⃣ Glavna slika hotela
            var mainImages = await _hotelImageService.GetImagesAsync(request.HotelId, true);
            var mainImageUrl = mainImages?.FirstOrDefault()?.ImageUrl;

            // 5️⃣ Sastavi odgovor
            return new BookingPreviewResponse
            {
                OfferTitle = offer.Title,
                HotelTitle = hotel.HotelName,
                RoomName = hotelRoom.RoomType,
                HotelMainImage = mainImageUrl,
                HotelStars = hotel.Stars,
                BasePrice = request.CalculatedPrice,
                ResidenceTaxTotal = residenceTaxTotal,
                TravelInsurance = offer.TravelInsuranceTotal,
                IncludeInsurance = request.IncludeInsurance,
                TotalPrice = total
            };
        }
    }
}