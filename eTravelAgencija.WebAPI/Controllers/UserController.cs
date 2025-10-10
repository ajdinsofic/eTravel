using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eTravelAgencija.Services.Services;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.SearchObjects;
using Microsoft.AspNetCore.Authorization;

namespace eTravelAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserSearchObject? search = null)
        {
            var users = await _userService.GetAsync(search ?? new UserSearchObject());

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }


        [HttpPost]
        [Authorize(Roles = "Direktor", AuthenticationSchemes = "BasicAuthentication")]
        public async Task<IActionResult> Post([FromBody] UserUpsertRequest request)
        {
            var createdUser = await _userService.PostAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserUpsertRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = await _userService.PutAsync(id, request);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _userService.AuthenticateAsync(request);
            return Ok(user);
        }

        
    }
}