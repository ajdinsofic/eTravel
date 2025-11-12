using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Authorize(Roles = "Korisnik,Radnik,Direktor")]
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController 
        : BaseCRUDController<
            Hotel,
            HotelSearchObject,
            HotelUpsertRequest,
            HotelUpsertRequest>
    {


        public HotelController(
            ILogger<BaseCRUDController<Hotel, HotelSearchObject, HotelUpsertRequest, HotelUpsertRequest>> logger,
            IHotelService hotelService)
            : base(logger, hotelService)
        {
            
        }
    }
}
