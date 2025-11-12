// 
using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Authorize(Roles = "Radnik,Direktor")]
    [ApiController]
    [Route("api/[controller]")]
    public class OfferImageController 
        : BaseCRUDController<
            OfferImage,
            BaseSearchObject,
            OfferImageUpsertRequest,
            OfferImageUpsertRequest>
    {


        public OfferImageController(
            ILogger<BaseCRUDController<OfferImage, BaseSearchObject, OfferImageUpsertRequest, OfferImageUpsertRequest>> logger,
            IOfferImageService offerImageService)
            : base(logger, offerImageService)
        {
            
        }
    }
}
