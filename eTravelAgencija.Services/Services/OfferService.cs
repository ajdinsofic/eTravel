using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public class OfferService : IOfferService
    {
        public Task<List<UserResponse>> GetAsync(OfferSearchObject? search = null)
        {
            throw new NotImplementedException();
        }
    }
}