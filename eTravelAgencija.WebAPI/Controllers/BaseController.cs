using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TResponse, TSearch> : ControllerBase
        where TResponse : class
        where TSearch : BaseSearchObject
    {
        protected readonly IService<TResponse, TSearch> _service;
        protected readonly ILogger<BaseController<TResponse, TSearch>> _logger;

        public BaseController(
            ILogger<BaseController<TResponse, TSearch>> logger,
            IService<TResponse, TSearch> service)
        {
            _logger = logger;
            _service = service;
        }

        // 🟢 GET — svi podaci (paged)
        [AllowAnonymous]
        [HttpGet]
        public virtual async Task<ActionResult<PagedResult<TResponse>>> Get([FromQuery] TSearch? search = null)
        {
            // try
            // {
            //     var result = await _service.GetAsync(search);
            //     return Ok(result);
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(ex, "Greška prilikom dohvaćanja podataka za {Type}", typeof(TResponse).Name);
            //     return StatusCode(500, "Došlo je do greške na serveru.");
            // }
            var result = await _service.GetAsync(search);
            return Ok(result);
        }

        // 🟡 GET — po ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResponse>> GetById(int id)
        {
            try
            {
                var entity = await _service.GetByIdAsync(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Greška prilikom dohvaćanja entiteta {Type} sa ID={Id}", typeof(TResponse).Name, id);
                return StatusCode(500, "Došlo je do greške na serveru.");
            }
        }
    }
}
