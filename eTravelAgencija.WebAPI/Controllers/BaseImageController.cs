using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseImageController<TEntity, TResponse> : ControllerBase
    where TEntity : class,  new()
    where TResponse :  new()
    {
        private readonly IBaseImageService<TResponse> _imageService;

        protected BaseImageController(IBaseImageService<TResponse> imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{referenceId}/images")]
        public async Task<ActionResult<List<TResponse>>> GetAll(int referenceId)
        {
            var images = await _imageService.GetAllImagesAsync(referenceId);
            return Ok(images);
        }

        [HttpPost("{referenceId}/image")]
        public async Task<ActionResult<int>> Add(int referenceId, [FromBody] string imageUrl)
        {
            var id = await _imageService.AddImageAsync(referenceId, imageUrl);
            return Ok(id);
        }

        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> Delete(int imageId)
        {
            var result = await _imageService.DeleteImageByIdAsync(imageId);
            return result ? NoContent() : NotFound();
        }
    }

}