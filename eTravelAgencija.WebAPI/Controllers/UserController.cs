using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eTravelAgencija.Model.Requests;

namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController 
        : BaseCRUDController<User, UserSearchObject, UserUpsertRequest, UserUpsertRequest>
    {
        private readonly IUserService _userService;

        public UserController(
            ILogger<BaseCRUDController<User, UserSearchObject, UserUpsertRequest, UserUpsertRequest>> logger,
            IUserService userService)
            : base(logger, userService)
        {
            _userService = userService;
        }

        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.AuthenticateAsync(request);
            if (result == null)
                return Unauthorized(new { message = "Neispravno korisničko ime ili lozinka." });

            return Ok(result);
        }
    }
}
