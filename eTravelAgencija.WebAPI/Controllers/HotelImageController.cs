// using Microsoft.AspNetCore.Mvc;
// using eTravelAgencija.Services.Services;
// using eTravelAgencija.Model.SearchObjects;
// using eTravelAgencija.Model.RequestObjects;
// using System.Threading.Tasks;

// namespace eTravelAgencija.API.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class HotelImageController : ControllerBase
//     {
//         private readonly IHotelImageService _hotelImageService;

//         public HotelImageController(IHotelImageService hotelImageService)
//         {
//             _hotelImageService = hotelImageService;
//         }

//         // 🔹 GET: api/HotelImage
//         [HttpGet]
//         public async Task<IActionResult> Get([FromQuery] BaseSearchObject search)
//         {
//             var result = await _hotelImageService.GetAsync(search);
//             return Ok(result);
//         }

//         // 🔹 GET: api/HotelImage/5
//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById(int id)
//         {
//             var result = await _hotelImageService.GetByIdAsync(id);
//             if (result == null)
//                 return NotFound();

//             return Ok(result);
//         }

//         // 🔹 POST: api/HotelImage
//         [HttpPost]
//         public async Task<IActionResult> Create([FromBody] HotelImageInsertRequest request)
//         {
//             if (!ModelState.IsValid)
//                 return BadRequest(ModelState);

//             var result = await _hotelImageService.CreateAsync(request);
//             return CreatedAtAction(nameof(GetById), new { id = (result as dynamic).Id }, result);
//         }

//         // 🔹 PUT: api/HotelImage/5
//         [HttpPut("{id}")]
//         public async Task<IActionResult> Update(int id, [FromBody] HotelImageUpdateRequest request)
//         {
//             if (!ModelState.IsValid)
//                 return BadRequest(ModelState);

//             var result = await _hotelImageService.UpdateAsync(id, request);
//             if (result == null)
//                 return NotFound();

//             return Ok(result);
//         }

//         // 🔹 DELETE: api/HotelImage/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var deleted = await _hotelImageService.DeleteAsync(id);
//             if (!deleted)
//                 return NotFound();

//             return NoContent();
//         }
//     }
// }

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
    public class HotelImageController 
        : BaseCRUDController<
            HotelImages,
            BaseSearchObject,
            HotelImageUpsertRequest,
            HotelImageUpsertRequest>
    {

        public HotelImageController(
            ILogger<BaseCRUDController<HotelImages, BaseSearchObject, HotelImageUpsertRequest, HotelImageUpsertRequest>> logger,
            IHotelImageService hotelImageService)
            : base(logger, hotelImageService)
        {
        }
    }
}
