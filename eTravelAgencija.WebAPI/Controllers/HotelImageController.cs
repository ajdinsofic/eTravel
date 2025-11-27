using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class HotelImageController 
        : BaseCRUDController<
            HotelImages,
            HotelImageSearchObject,
            HotelImageInsertRequest,
            HotelImageUpdateRequest>
    {

        public HotelImageController(
            ILogger<BaseCRUDController<HotelImages, HotelImageSearchObject, HotelImageInsertRequest, HotelImageUpdateRequest>> logger,
            IHotelImageService hotelImageService)
            : base(logger, hotelImageService)
        {
        }

        public override Task<ActionResult<HotelImages>> Create([FromForm] HotelImageInsertRequest insert)
        {
            return base.Create(insert);
        }
    }
}
