using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Authorize(Roles = "Korisnik")]
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController 
        : BaseCRUDController<
            Offer,
            OfferSearchObject,
            OfferInsertRequest,
            OfferUpdateRequest>
    {

        public OfferController(
            ILogger<BaseCRUDController<Offer, OfferSearchObject, OfferInsertRequest, OfferUpdateRequest>> logger,
            IOfferService offerService)
            : base(logger, offerService)
        {
            
        }
    }
}




