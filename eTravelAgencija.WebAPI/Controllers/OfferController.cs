using System.Threading.Tasks;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        // GET: api/Offer/admin-search
        [HttpGet("admin-search")]
        public async Task<ActionResult<PagedResult<OfferAdminResponse>>> GetSearchOffersForAdmin([FromQuery] OfferSearchObject search)
        {
            var result = await _offerService.GetSearchOffersForAdmin(search);
            return Ok(result);
        }

        // GET: api/Offer/user-search
        [HttpGet("user-search")]
        public async Task<ActionResult<PagedResult<OfferUserResponce>>> GetSearchOffersForUser([FromQuery] OfferSearchObject search)
        {
            var result = await _offerService.GetSearchOffersForUser(search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferAdminDetailResponse>> GetOfferById(int id)
        {
            var result = await _offerService.GetOfferById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // GET: api/Offer/admin-details/{id}
        [HttpGet("admin-details/{id}")]
        public async Task<ActionResult<OfferAdminDetailResponse>> GetOfferDetailsByIdForAdmin(int id)
        {
            var result = await _offerService.GetOfferDetailsByIdForAdmin(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/Offer/user-details/{id}
        [HttpGet("user-details/{id}")]
        public async Task<ActionResult<OfferUserDetailResponse>> GetOfferDetailsByIdForUser(int id)
        {
            var result = await _offerService.GetOfferDetailsByIdForUser(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/Offer
        [HttpPost]
        public async Task<ActionResult<OfferUpsertResponse>> PostOffer([FromBody] OfferRequest request)
        {
            var createdOffer = await _offerService.PostOffer(request);
            return CreatedAtAction(nameof(GetOfferDetailsByIdForAdmin), new { id = createdOffer.Id }, createdOffer);
        }

        // PUT: api/Offer/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<OfferUpsertResponse>> PutOffer(int id, [FromBody] OfferRequest request)
        {
            var updatedOffer = await _offerService.PutOffer(id, request);
            if (updatedOffer == null)
                return NotFound();

            return Ok(updatedOffer);
        }

        // DELETE: api/Offer/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOffer(int id)
        {
            var deleted = await _offerService.DeleteOffer(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
