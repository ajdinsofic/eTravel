using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferPlanDayService
    {
        Task<List<OfferPlanDayResponse>> GetOfferPlanDaysAsync(int offerId);
        Task<OfferPlanDayResponse> GetByKeyAsync(int offerId, int dayNumber);
        Task<OfferPlanDayResponse> InsertAsync(OfferPlanDayUpsertRequest request);
        Task<OfferPlanDayResponse> UpdateAsync(int offerId, int dayNumber, OfferPlanDayUpsertRequest request);
        Task<bool> DeleteAsync(int offerId, int dayNumber);
    }
}
