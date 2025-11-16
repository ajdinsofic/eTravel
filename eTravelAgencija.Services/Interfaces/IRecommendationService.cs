using System.Collections.Generic;
using eTravelAgencija.Model.model;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IRecommendationService
    {
        public List<Model.model.Offer> RecommendOffers(int offerId);
        public List<Model.model.Hotel> RecommendHotels(int hotelId);
    }
}
