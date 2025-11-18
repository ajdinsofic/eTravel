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
            if (search.SubCategoryId == -1 || search.SubCategoryId > 0)
            {
                query = query.Where(o => o.SubCategoryId == search.SubCategoryId);
            }

            if (!string.IsNullOrWhiteSpace(search.FTS))
            {
                query = query.Where(o => o.Title.ToLower().Contains(search.FTS.ToLower()));
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

        public List<Model.model.Offer> RecommendOffers(int offerId)
        {
            var ml = new MLContext();

            // 1️⃣ Load all reservations grouped by user
            var reservations = _context.Reservations
                .GroupBy(r => r.UserId)
                .ToList();

            var data = new List<OfferEntry>();

            // 2️⃣ Generate co-occurrence pairs OfferID -> CoOfferID
            foreach (var group in reservations)
            {
                var offerIds = group.Select(r => r.OfferId).Distinct().ToList();

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

            if (data.Count == 0)
                return new List<Model.model.Offer>();

            // 3️⃣ Load data
            var trainData = ml.Data.LoadFromEnumerable(data);

            // 4️⃣ REQUIRED: convert OfferID and CoOfferID to KEY columns
            var pipeline = ml.Transforms.Conversion
                .MapValueToKey("OfferID")
                .Append(ml.Transforms.Conversion.MapValueToKey("CoOfferID"));

            var pipelineModel = pipeline.Fit(trainData);
            var transformed = pipelineModel.Transform(trainData);

            // 5️⃣ MatrixFactorization configuration
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

            var est = ml.Recommendation().Trainers.MatrixFactorization(options);

            // 6️⃣ Fit model
            var model = est.Fit(transformed);

            // 7️⃣ Score all other offers
            var allOffers = _context.Offers.Include(o => o.Details).Where(o => o.Id != offerId).ToList();
            var scores = new List<(Database.Offer offer, float score)>();

            foreach (var offer in allOffers)
            {
                // 7.1 – Prepare single-row data
                var singleInput = new List<OfferEntry>()
        {
            new OfferEntry
            {
                OfferID = (uint)offerId,
                CoOfferID = (uint)offer.Id
            }
        };

                var predictionData = ml.Data.LoadFromEnumerable(singleInput);

                // 7.2 – Apply SAME key-mapping pipeline
                var transformedPrediction = pipelineModel.Transform(predictionData);

                // 7.3 – Score using model.Transform()
                var scored = model.Transform(transformedPrediction);

                // 7.4 – Extract the score column
                var score = scored.GetColumn<float>("Score").First();

                scores.Add((offer, score));
            }

            // 8️⃣ Return Top 5 recommendations
            var final = scores
                .OrderByDescending(s => s.score)
                .Take(5)
                .Select(s => s.offer)
                .ToList();

            return _mapper.Map<List<Model.model.Offer>>(final);
        }

    }
}
