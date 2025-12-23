using System.Threading.Tasks;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Services;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model;
using eTravelAgencija.Model.SearchObjects;
using System.Security.Cryptography.X509Certificates;
using eTravelAgencija.Services.Interfaces;
using Microsoft.ML;
using eTravelAgencija.Services.Recommendation;
using Microsoft.ML.Trainers;
using Microsoft.ML.Data;

namespace eTravelAgencija.Services.Database
{
    public class OfferService : BaseCRUDService<Model.model.Offer, OfferSearchObject, Database.Offer, OfferInsertRequest, OfferUpdateRequest>, IOfferService
    {
        public OfferService(eTravelAgencijaDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Offer> ApplyFilter(IQueryable<Offer> query, OfferSearchObject search)
        {
            if (search?.offerId.HasValue == true)
            {
                query = query.Where(u => u.Id == search.offerId);
            }

            if (search.SubCategoryId == -1 || search.SubCategoryId > 0)
            {
                query = query.Where(o => o.SubCategoryId == search.SubCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(search.FTS))
            {
                query = query.Where(o => o.Title.ToLower().Contains(search.FTS.ToLower()));
            }

            if (search?.isPopularOffers == true)
            {
                query = query
                    .OrderByDescending(o => o.Details.TotalCountOfReservations)
                    .Take(3);
            }

            return base.ApplyFilter(query, search);
        }

        public override IQueryable<Offer> AddInclude(IQueryable<Offer> query, OfferSearchObject search)
        {
            query = query.Include(o => o.Details);

            if (search.isMainImage)
            {
                query = query
                    .Include(o => o.Details)
                        .ThenInclude(d => d.OfferImages.Where(img => img.isMain));
            }
            else
            {
                query = query
                    .Include(o => o.Details)
                        .ThenInclude(d => d.OfferImages);
            }

            if (search.isOfferHotels)
                query = query.Include(o => o.Details.OfferHotels);


            if (search.isOfferPlanDays)
                query = query.Include(o => o.Details.OfferPlanDays);

            return base.AddInclude(query, search);

        }

        public override async Task<Offer> SetDetails(Offer entity)
        {
            if (entity == null)
                return null;

            return await _context.Offers
                .Include(u => u.Details)
                .FirstOrDefaultAsync(u => u.Id == entity.Id);
        }

        public override async Task BeforeInsertAsync(Database.Offer entity, OfferInsertRequest request)
        {

            var details = new OfferDetails
            {
                MinimalPrice = request.MinimalPrice,
                ResidenceTotal = request.ResidenceTotal,
                TravelInsuranceTotal = request.TravelInsuranceTotal,
                ResidenceTaxPerDay = request.ResidenceTotal / request.DaysInTotal,
                City = request.City,
                Country = request.Country,
                Description = request.Description,
                TotalCountOfReservations = 0
            };

            entity.Details = details;

            await base.BeforeInsertAsync(entity, request);
        }

        public override async Task BeforeUpdateAsync(Database.Offer entity, OfferUpdateRequest request)
        {
            await _context.Entry(entity)
            .Reference(o => o.Details)
            .LoadAsync();

            if (entity.Details != null)
            {
                entity.Details.MinimalPrice = request.MinimalPrice;
                entity.Details.ResidenceTotal = request.ResidenceTotal;
                entity.Details.TravelInsuranceTotal = request.TravelInsuranceTotal;
                entity.Details.City = request.City;
                entity.Details.Country = request.Country;
                entity.Details.Description = request.Description;
            }

            await base.BeforeUpdateAsync(entity, request);
        }

        public async Task IncreaseTotalReservation(int offerId)
        {
            var offerDetails = await _context.OfferDetails
                .FirstOrDefaultAsync(x => x.OfferId == offerId);

            if (offerDetails == null)
                throw new Exception("OfferDetails nije pronađen.");

            offerDetails.TotalCountOfReservations += 1;

            await _context.SaveChangesAsync();
        }

        public async Task DecreaseTotalReservation(int offerId)
        {
            var offerDetails = await _context.OfferDetails
                .FirstOrDefaultAsync(x => x.OfferId == offerId);

            if (offerDetails == null)
                throw new Exception("OfferDetails nije pronađen.");

            // zaštita da ne ode u minus
            if (offerDetails.TotalCountOfReservations > 0)
                offerDetails.TotalCountOfReservations -= 1;

            await _context.SaveChangesAsync();
        }

        private Dictionary<int, float> GetItemToItemScores(int baseOfferId)
        {
            var ml = new MLContext();

            // 1️⃣ Rezervacije po korisniku
            var reservations = _context.Reservations
                .GroupBy(r => r.UserId)
                .ToList();

            var data = new List<OfferEntry>();

            foreach (var group in reservations)
            {
                var offerIds = group
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
                                CoOfferID = (uint)o2,
                                Label = 1
                            });
                        }
                    }
                }
            }

            if (!data.Any())
                return new Dictionary<int, float>();

            // 2️⃣ Load data
            var trainData = ml.Data.LoadFromEnumerable(data);

            var pipeline = ml.Transforms.Conversion
                .MapValueToKey("OfferID")
                .Append(ml.Transforms.Conversion.MapValueToKey("CoOfferID"));

            var pipelineModel = pipeline.Fit(trainData);
            var transformed = pipelineModel.Transform(trainData);

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "OfferID",
                MatrixRowIndexColumnName = "CoOfferID",
                LabelColumnName = "Label",
                LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                Alpha = 0.01,
                Lambda = 0.025,
                NumberOfIterations = 80
            };

            var model = ml.Recommendation().Trainers.MatrixFactorization(options)
                .Fit(transformed);

            // 3️⃣ Score svih ostalih offera
            var scores = new Dictionary<int, float>();

            var allOffers = _context.Offers
                .Where(o => o.Id != baseOfferId)
                .Select(o => o.Id)
                .ToList();

            foreach (var otherId in allOffers)
            {
                var input = new List<OfferEntry>
        {
            new OfferEntry
            {
                OfferID = (uint)baseOfferId,
                CoOfferID = (uint)otherId
            }
        };

                var inputData = ml.Data.LoadFromEnumerable(input);
                var transformedInput = pipelineModel.Transform(inputData);
                var scored = model.Transform(transformedInput);

                var score = scored.GetColumn<float>("Score").First();
                scores[otherId] = score;
            }

            return scores;
        }

        public List<Model.model.Offer> RecommendOffersForUser(int userId)
        {
            // 1️⃣ Sve ponude koje je korisnik rezervisao
            var userOfferIds = _context.Reservations
                .Where(r => r.UserId == userId)
                .Select(r => r.OfferId)
                .Distinct()
                .ToList();

            if (!userOfferIds.Any())
                return new List<Model.model.Offer>();

            var aggregatedScores = new Dictionary<int, float>();

            // 2️⃣ Za svaku korisnikovu ponudu → ML score
            foreach (var offerId in userOfferIds)
            {
                var scores = GetItemToItemScores(offerId);

                foreach (var kvp in scores)
                {
                    // preskoči već rezervisane
                    if (userOfferIds.Contains(kvp.Key))
                        continue;

                    if (!aggregatedScores.ContainsKey(kvp.Key))
                        aggregatedScores[kvp.Key] = 0;

                    // agregacija (implicitni user profile)
                    aggregatedScores[kvp.Key] += kvp.Value;
                }
            }

            if (!aggregatedScores.Any())
                return new List<Model.model.Offer>();

            // 3️⃣ Top 5 preporuka
            var recommendedIds = aggregatedScores
                .OrderByDescending(x => x.Value)
                .Take(5)
                .Select(x => x.Key)
                .ToList();

            var offers = _context.Offers
                .Include(o => o.Details)
                .Where(o => recommendedIds.Contains(o.Id))
                .ToList();

            return _mapper.Map<List<Model.model.Offer>>(offers);
        }
    }

}
