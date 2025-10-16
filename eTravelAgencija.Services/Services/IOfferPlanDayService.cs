using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IOfferPlanDayService
    {
        Task<List<OfferPlanDayResponse>> GetOfferPlanDays(int offerId);
    }
}