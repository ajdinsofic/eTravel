using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Services.Recommendation;
using eTravelAgencija.Services.Recommendations.MLModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace eTravelAgencija.Services.Services
{
    public class HotelService : BaseCRUDService<Model.model.Hotel, HotelSearchObject, Database.Hotel, HotelUpsertRequest, HotelUpsertRequest>, IHotelService
    {
        private static readonly MLContext _ml = new MLContext(seed: 42);
        private static ITransformer? _hotelModel;
        private static PredictionEngine<UserHotelEntry, UserHotelPrediction>? _hotelEngine;
        private static ITransformer? _roomModel;
        private static PredictionEngine<UserRoomEntry, UserRoomPrediction>? _roomEngine;
        private static DateTime _lastTrainUtc = DateTime.MinValue;
        private static readonly TimeSpan _retrainEvery = TimeSpan.FromMinutes(30);

        private static readonly object _lock = new();
        public HotelService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        // DA ZNAS VRACAMO OPET LISTU ALI IZ LISTE CEMO VADITI KOJI SE TO HOTEL KLIKNUO I PREKO FRONTENDA CEMO
        // SAMO NJEGA ZAMJENITI
        public override async Task<IEnumerable<Database.Hotel>> AfterGetAsync(IEnumerable<Database.Hotel> entities, HotelSearchObject? search = null)
        {
            foreach (var hotel in entities)
            {
                if (search?.isMainImage == true)
                {
                    hotel.HotelImages = hotel.HotelImages
                        .Where(img => img.IsMain)
                        .ToList();
                }
                else
                {
                    hotel.HotelImages = hotel.HotelImages.ToList();
                }

                if (search?.RoomId != null)
                {
                    hotel.HotelRooms = hotel.HotelRooms
                        .Where(hr => hr.RoomId == search.RoomId)
                        .ToList();

                    await SetCalculatedPriceAsync(hotel, search.RoomId.Value);
                }
                else
                {
                    var firstRoomId = hotel.HotelRooms.FirstOrDefault()?.RoomId ?? 0;

                    if (firstRoomId != 0)
                        await SetCalculatedPriceAsync(hotel, firstRoomId);
                }

            }

            return entities;
        }



        public override IQueryable<Hotel> AddInclude(IQueryable<Hotel> query, HotelSearchObject? search = null)
        {
            query = query
                .Include(h => h.HotelImages)
                .Include(h => h.HotelRooms)
                    .ThenInclude(hr => hr.Rooms)
                .Include(h => h.OfferHotels)
                    .ThenInclude(h => h.OfferDetails);

            return base.AddInclude(query, search);
        }

        public override IQueryable<Hotel> ApplyFilter(IQueryable<Hotel> query, HotelSearchObject search)
        {
            if (search.OfferId.HasValue)
                query = query.Where(h => h.OfferHotels.Any(oh => oh.OfferDetailsId == search.OfferId));

            if (search.DepartureDate.HasValue)
                query = query.Where(h => h.OfferHotels.Any(oh => oh.DepartureDate == search.DepartureDate));

            if (search.RoomId.HasValue)
                query = query.Where(h => h.HotelRooms.Any(hr => hr.RoomId == search.RoomId));

            if (search?.isMainImage == true)
            {
                query = query.Where(h => h.HotelImages.Any(hr => hr.IsMain == search.isMainImage));
            }

            return base.ApplyFilter(query, search);
        }

        public decimal CalculateCalculatedPrice(decimal basePrice, string roomType)
        {
            switch (roomType.ToLower())
            {
                case "dvokrevetna":
                    return basePrice; // ostaje ista cijena
                case "trokrevetna":
                    return basePrice + 100; // +100 KM
                case "cetverokrevetna":
                    return basePrice + 200; // +200 KM
                case "petokrevetna":
                    return basePrice + 300;
                default:
                    return basePrice; // fallback ako nije prepoznata soba
            }
        }

        private async Task SetCalculatedPriceAsync(Hotel hotel, int roomId)
        {
            var offerDetailsId = hotel.OfferHotels.FirstOrDefault()?.OfferDetailsId;

            if (offerDetailsId == null)
                return;

            var offer = await _context.OfferDetails
                .FirstOrDefaultAsync(o => o.OfferId == offerDetailsId);

            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

            if (offer != null && room != null)
            {
                hotel.CalculatedPrice = CalculateCalculatedPrice(offer.MinimalPrice, room.RoomType);
            }
        }

        public async Task<RecommendedHotelRoomDto?> RecommendHotelRoomForOfferAsync(int offerId, int userId)
        {
            // 1) Skupi hotele koji pripadaju ponudi (RULES)
            var validHotels = await _context.OfferHotels
                .Where(x => x.OfferDetailsId == offerId)
                .Select(x => x.HotelId)
                .Distinct()
                .ToListAsync();

            if (validHotels.Count == 0) return null;

            // 2) Skupi user's history (da vidimo da li ima smisla ML ili fallback)
            var userReservations = await _context.Reservations
                .Where(r => r.UserId == userId)
                .ToListAsync();

            // Ako user nema historiju → fallback
            if (userReservations.Count < 2)
            {
                return await FallbackBestHotelRoomForOfferAsync(offerId, validHotels);
            }

            // 3) Osiguraj/treniraj ML modele (User→Hotel, User→Room)
            await EnsureModelsTrainedAsync();

            // Ako iz bilo kog razloga nema modela → fallback
            if (_hotelEngine == null || _roomEngine == null)
                return await FallbackBestHotelRoomForOfferAsync(offerId, validHotels);

            // 4) Rangiraj samo valid hotels (ML)
            int bestHotelId = validHotels.First();
            float bestHotelScore = float.MinValue;

            foreach (var hid in validHotels)
            {
                var pred = _hotelEngine.Predict(new UserHotelEntry
                {
                    UserId = (uint)userId,
                    HotelId = (uint)hid
                });

                if (pred.Score > bestHotelScore)
                {
                    bestHotelScore = pred.Score;
                    bestHotelId = hid;
                }
            }

            // 5) Za taj hotel, uzmi dostupne sobe (RULES)
            var availableRoomIds = await _context.HotelRooms
                .Where(hr => hr.HotelId == bestHotelId && hr.RoomsLeft > 0)
                .Select(hr => hr.RoomId)
                .Distinct()
                .ToListAsync();

            // Ako nema soba → fallback na bilo koji hotel u ponudi sa sobom
            if (availableRoomIds.Count == 0)
            {
                return await FallbackBestHotelRoomForOfferAsync(offerId, validHotels);
            }

            // 6) ML rangiranje roomId (User→Room), ali samo među sobama tog hotela
            int bestRoomId = availableRoomIds.First();
            float bestRoomScore = float.MinValue;

            foreach (var rid in availableRoomIds)
            {
                var pred = _roomEngine.Predict(new UserRoomEntry
                {
                    UserId = (uint)userId,
                    RoomId = (uint)rid
                });

                if (pred.Score > bestRoomScore)
                {
                    bestRoomScore = pred.Score;
                    bestRoomId = rid;
                }
            }

            // 7) Vrati DTO sa nazivima
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == bestHotelId);
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == bestRoomId);

            return new RecommendedHotelRoomDto
            {
                HotelId = bestHotelId,
                RoomId = bestRoomId,
            };
        }

        // ================================
        // TRAINING / CACHING
        // ================================
        private async Task EnsureModelsTrainedAsync()
        {
            // retrain na X minuta (ili kad prvi put pozove)
            if (_hotelModel != null &&
                _roomModel != null &&
                DateTime.UtcNow - _lastTrainUtc < _retrainEvery)
                return;

            lock (_lock)
            {
                if (_hotelModel != null &&
                    _roomModel != null &&
                    DateTime.UtcNow - _lastTrainUtc < _retrainEvery)
                    return;
            }

            // ===============================
            // 1️⃣ UČITAJ REZERVACIJE
            // ===============================
            var allReservations = await _context.Reservations
                .AsNoTracking()
                .Select(r => new { r.UserId, r.HotelId, r.RoomId })
                .ToListAsync();

            if (allReservations.Count == 0)
                return;

            // ===============================
            // 2️⃣ HOTEL TRAINING DATA
            // ===============================
            var hotelData = allReservations
                .Where(x => x.HotelId != null)
                .Select(x => new UserHotelEntry
                {
                    UserId = (uint)x.UserId,
                    HotelId = (uint)x.HotelId!,
                    Label = 1f
                })
                .ToList();

            // ===============================
            // 3️⃣ ROOM TRAINING DATA
            // ===============================
            var roomData = allReservations
                .Where(x => x.RoomId != null)
                .Select(x => new UserRoomEntry
                {
                    UserId = (uint)x.UserId,
                    RoomId = (uint)x.RoomId!,
                    Label = 1f
                })
                .ToList();

            // Ako nema dovoljno podataka → nema ML
            if (hotelData.Count < 5 || roomData.Count < 5)
                return;

            // ===============================
            // 4️⃣ HOTEL MODEL
            // ===============================
            var hotelTrain = _ml.Data.LoadFromEnumerable(hotelData);

            var hotelPipeline =
                _ml.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "UserId",
                    inputColumnName: nameof(UserHotelEntry.UserId))
                .Append(
                _ml.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "HotelId",
                    inputColumnName: nameof(UserHotelEntry.HotelId)))
                .Append(
                    _ml.Recommendation().Trainers.MatrixFactorization(
                        new MatrixFactorizationTrainer.Options
                        {
                            MatrixColumnIndexColumnName = "UserId",
                            MatrixRowIndexColumnName = "HotelId",
                            LabelColumnName = nameof(UserHotelEntry.Label),
                            LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                            Alpha = 0.01,
                            Lambda = 0.025,
                            NumberOfIterations = 100,
                            ApproximationRank = 32
                        }
                    )
                );

            var trainedHotelModel = hotelPipeline.Fit(hotelTrain);

            // ===============================
            // 5️⃣ ROOM MODEL
            // ===============================
            var roomTrain = _ml.Data.LoadFromEnumerable(roomData);

            var roomPipeline =
                _ml.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "UserId",
                    inputColumnName: nameof(UserRoomEntry.UserId))
                .Append(
                _ml.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "RoomId",
                    inputColumnName: nameof(UserRoomEntry.RoomId)))
                .Append(
                    _ml.Recommendation().Trainers.MatrixFactorization(
                        new MatrixFactorizationTrainer.Options
                        {
                            MatrixColumnIndexColumnName = "UserId",
                            MatrixRowIndexColumnName = "RoomId",
                            LabelColumnName = nameof(UserRoomEntry.Label),
                            LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                            Alpha = 0.01,
                            Lambda = 0.025,
                            NumberOfIterations = 100,
                            ApproximationRank = 32
                        }
                    )
                );

            var trainedRoomModel = roomPipeline.Fit(roomTrain);

            // ===============================
            // 6️⃣ CACHE + ENGINE
            // ===============================
            lock (_lock)
            {
                _hotelModel = trainedHotelModel;
                _hotelEngine =
                    _ml.Model.CreatePredictionEngine<UserHotelEntry, UserHotelPrediction>(_hotelModel);

                _roomModel = trainedRoomModel;
                _roomEngine =
                    _ml.Model.CreatePredictionEngine<UserRoomEntry, UserRoomPrediction>(_roomModel);

                _lastTrainUtc = DateTime.UtcNow;
            }
        }


        private async Task<RecommendedHotelRoomDto?> FallbackBestHotelRoomForOfferAsync(int offerId, List<int> validHotels)
        {
            // 1) Hotel koji ima najviše rezervacija ukupno (među valid hotels)
            int? bestHotelId = await _context.Reservations
                 .Where(r => validHotels.Contains(r.HotelId))
                 .GroupBy(r => r.HotelId)
                 .OrderByDescending(g => g.Count())
                 .Select(g => g.Key)
                 .FirstOrDefaultAsync();


            // Ako nema rezervacija → uzmi prvi valid
            var chosenHotelId = bestHotelId ?? validHotels.First();

            // 2) Room: uzmi sobu sa najviše RoomsLeft (ili prvu dostupnu)
            var bestRoomId = await _context.HotelRooms
                .Where(hr => hr.HotelId == chosenHotelId && hr.RoomsLeft > 0)
                .OrderByDescending(hr => hr.RoomsLeft)
                .Select(hr => hr.RoomId)
                .FirstOrDefaultAsync();

            if (bestRoomId == 0)
            {
                // nema dostupnih soba
                return null;
            }

            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == chosenHotelId);
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == bestRoomId);

            return new RecommendedHotelRoomDto
            {
                HotelId = chosenHotelId,
                RoomId = bestRoomId,

            };
        }

    }
}
