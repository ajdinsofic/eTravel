// 
using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.WebAPI.Controllers
{

    public class OfferImageController
        : BaseCRUDController<
            OfferImage,
            OfferImageSearchObject,
            OfferImageInsertRequest,
            OfferImageUpdateRequest>
    {


        public OfferImageController(
            ILogger<BaseCRUDController<OfferImage, OfferImageSearchObject, OfferImageInsertRequest, OfferImageUpdateRequest>> logger,
            IOfferImageService offerImageService)
            : base(logger, offerImageService)
        {

        }

        public override Task<ActionResult<OfferImage>> Create([FromForm] OfferImageInsertRequest insert)
        {
            return base.Create(insert);
        }

    }
}
