using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseCRUDController<TResponse, TSearch, TInsert, TUpdate> 
        : BaseController<TResponse, TSearch>
        where TResponse : class
        where TSearch : BaseSearchObject
        where TInsert : class
        where TUpdate : class
    {
        protected new readonly ICRUDService<TResponse, TSearch, TInsert, TUpdate> _service;
        protected readonly ILogger<BaseCRUDController<TResponse, TSearch, TInsert, TUpdate>> _logger;

        public BaseCRUDController(
            ILogger<BaseCRUDController<TResponse, TSearch, TInsert, TUpdate>> logger,
            ICRUDService<TResponse, TSearch, TInsert, TUpdate> service)
            : base(logger, service)
        {
            _service = service;
            _logger = logger;
        }

        // 🟢 CREATE
        [Authorize]
        [HttpPost]
        public virtual async Task<ActionResult<TResponse>> Create([FromBody] TInsert insert)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _service.CreateAsync(insert);
            return Ok(entity);
        }

        // 🟡 UPDATE
        [Authorize]
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResponse>> Update(int id, [FromBody] TUpdate update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, update);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // 🔴 DELETE
        [Authorize(Roles = "Direktor,Radnik")]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
