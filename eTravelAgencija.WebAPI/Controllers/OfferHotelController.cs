using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class OfferHotelController : BaseCRUDController<Model.model.OfferHotels, BaseSearchObject, OfferHotelInsertRequest, OfferHotelUpdateRequest>
    {
        public OfferHotelController(ILogger<BaseCRUDController<Model.model.OfferHotels, BaseSearchObject, OfferHotelInsertRequest, OfferHotelUpdateRequest>> logger, IOfferHotelService offerHotelService):base(logger, offerHotelService)
        {
            
        }

        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpGet("{offerId}/{hotelId}")]
        public async Task<Model.model.OfferHotels?> GetByCompositeKey(int offerId, int hotelId)
        {
            return await _service.GetByCompositeKeysAsync(offerId, hotelId);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpPut("{offerId}/{hotelId}")]
        public async Task<Model.model.OfferHotels?> UpdateByCompositeKey(int offerId, int hotelId, OfferHotelUpdateRequest plan)
        {
            return await _service.UpdateCompositeAsync([offerId,hotelId], plan);
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpDelete("{offerId}/{hotelId}")]
        public async Task<bool> DeleteByCompositeKey(int offerId, int hotelId)
        {
            return await _service.DeleteCompositeAsync(offerId, hotelId);
        }
    }
}