using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferPlanDayService : ICRUDService<Model.model.OfferPlanDay,OfferPlanDaySearchObject,OfferPlanDayUpsertRequest,OfferPlanDayUpsertRequest>
    {
    }
}
