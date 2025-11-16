using System.Collections.Generic;
using System.Linq;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Services.Database;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using eTravelAgencija.Services.Recommendation;

namespace eTravelAgencija.Services.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly eTravelAgencijaDbContext _context;
        private readonly IMapper _mapper;

        private static MLContext _mlContextOffers;
        private static ITransformer _offerModel;

        private static MLContext _mlContextHotels;
        private static ITransformer _hotelModel;

        private static readonly object _lockOffers = new object();
        private static readonly object _lockHotels = new object();

        public RecommendationService(eTravelAgencijaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ----------------------------------------------------------
        //                  O F F E R   M O D E L
        // ----------------------------------------------------------

        public List<Model.model.Offer> RecommendOffers(int offerId)
        {
            lock (_lockOffers)
            {
                if (_mlContextOffers == null)
                {
                    _mlContextOffers = new MLContext();
                    TrainOfferModel();
                }

                if (_offerModel == null)
                    return new List<Model.model.Offer>();

                var engine = _mlContextOffers.Model.CreatePredictionEngine<OfferEntry, OfferPrediction>(_offerModel);

                var offers = _context.Offers.Where(o => o.Id != offerId).ToList();
                var predictions = new List<(Database.Offer offer, float score)>();

                foreach (var offer in offers)
                {
                    var p = engine.Predict(new OfferEntry
                    {
                        OfferID = (uint)offerId,
                        CoOfferID = (uint)offer.Id
                    });

                    predictions.Add((offer, p.Score));
                }

                var final = predictions
                    .OrderByDescending(x => x.score)
                    .Take(5)
                    .Select(x => x.offer)
                    .ToList();

                return _mapper.Map<List<Model.model.Offer>>(final);
            }
        }

        private void TrainOfferModel()
        {
            var reservations = _context.Reservations
                .Include(r => r.OfferDetails)
                .GroupBy(r => r.UserId)
                .ToList();

            var data = new List<OfferEntry>();

            foreach (var userReservations in reservations)
            {
                var offerIds = userReservations
                    .Select(r => r.OfferId)
                    .Distinct()
                    .ToList();

                foreach (var o1 in offerIds)
                {
                    foreach (var o2 in offerIds)
                    {
                        if (o1 != o2)
                        {
                            data.Add(new OfferEntry
                            {
                                OfferID = (uint)o1,
                                CoOfferID = (uint)o2
                            });
                        }
                    }
                }
            }

            if (data.Count == 0) return;

            var train = _mlContextOffers.Data.LoadFromEnumerable(data);

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(OfferEntry.OfferID),
                MatrixRowIndexColumnName = nameof(OfferEntry.CoOfferID),
                LabelColumnName = nameof(OfferEntry.Label),
                LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                Alpha = 0.01,
                Lambda = 0.025,
                NumberOfIterations = 100,
            };

            var trainer = _mlContextOffers.Recommendation().Trainers.MatrixFactorization(options);
            _offerModel = trainer.Fit(train);
        }

        // ----------------------------------------------------------
        //                  H O T E L   M O D E L
        // ----------------------------------------------------------

        public List<Model.model.Hotel> RecommendHotels(int hotelId)
        {
            lock (_lockHotels)
            {
                if (_mlContextHotels == null)
                {
                    _mlContextHotels = new MLContext();
                    TrainHotelModel();
                }

                if (_hotelModel == null)
                    return new List<Model.model.Hotel>();

                var engine = _mlContextHotels.Model.CreatePredictionEngine<HotelEntry, HotelPrediction>(_hotelModel);

                var hotels = _context.Hotels.Where(h => h.Id != hotelId).ToList();
                var predictions = new List<(Database.Hotel hotel, float score)>();

                foreach (var hotel in hotels)
                {
                    var p = engine.Predict(new HotelEntry
                    {
                        HotelID = (uint)hotelId,
                        CoHotelID = (uint)hotel.Id
                    });

                    predictions.Add((hotel, p.Score));
                }

                var final = predictions
                    .OrderByDescending(x => x.score)
                    .Take(5)
                    .Select(x => x.hotel)
                    .ToList();

                return _mapper.Map<List<Model.model.Hotel>>(final);
            }
        }

        private void TrainHotelModel()
        {
            var reservations = _context.Reservations
                .Include(r => r.Hotel)
                .GroupBy(r => r.UserId)
                .ToList();

            var data = new List<HotelEntry>();

            foreach (var userReservations in reservations)
            {
                var hotelIds = userReservations
                    .Select(r => r.HotelId)
                    .Distinct()
                    .ToList();

                foreach (var h1 in hotelIds)
                {
                    foreach (var h2 in hotelIds)
                    {
                        if (h1 != h2)
                        {
                            data.Add(new HotelEntry
                            {
                                HotelID = (uint)h1,
                                CoHotelID = (uint)h2
                            });
                        }
                    }
                }
            }

            if (data.Count == 0) return;

            var train = _mlContextHotels.Data.LoadFromEnumerable(data);

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(HotelEntry.HotelID),
                MatrixRowIndexColumnName = nameof(HotelEntry.CoHotelID),
                LabelColumnName = nameof(HotelEntry.Label),
                LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                Alpha = 0.01,
                Lambda = 0.025,
                NumberOfIterations = 100,
            };

            var trainer = _mlContextHotels.Recommendation().Trainers.MatrixFactorization(options);
            _hotelModel = trainer.Fit(train);
        }
    }
}
