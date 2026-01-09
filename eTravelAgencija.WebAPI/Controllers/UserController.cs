using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.WebAPI.Controllers
{

    public class UserController
        : BaseCRUDController<User, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        private readonly IUserService _userService;

        public UserController(
            ILogger<BaseCRUDController<User, UserSearchObject, UserInsertRequest, UserUpdateRequest>> logger,
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

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpPut("unblock/{id}")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            var success = await _userService.UnblockUser(id);

            if (!success)
                return BadRequest("Greška pri odblokiranju korisnika.");

            return Ok(new { message = "Korisnik je uspješno odblokiran." });
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpPut("block/{id}")]
        public async Task<IActionResult> blockUser(int id)
        {
            var success = await _userService.blockUser(id);

            if (!success)
                return BadRequest("Greška pri odblokiranju korisnika.");

            return Ok(new { message = "Korisnik je uspješno odblokiran." });
        }

        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpPost("userImage")]
        public async Task<ActionResult<bool>> AddImage([FromForm] UserImageRequest request)
        {
            var result = await _userService.AddUserImage(request);
            return Ok(result);
        }

        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpGet("{userId}/userImage")]
        public async Task<ActionResult<string>> GetUserImage(int userId)
        {
            var imageName = await _userService.GetUserImage(userId);

            if (imageName == null)
                return NotFound();

            return Ok(imageName);
        }


        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpDelete("{id}/delete-image")]
        public async Task<IActionResult> DeleteImage(int id)
        {

            var result = await _userService.DeleteUserImageAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpPost("check-password")]
        public async Task<IActionResult> CheckPassword([FromBody] CheckPasswordRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.CurrentPassword))
                return BadRequest("Invalid request");

            var isValid = await _userService.CheckPasswordAsync(request);

            return Ok(new { isValid });
        }

        [Authorize(Roles = "Korisnik,Radnik,Direktor")]
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdateNewPassword([FromBody] UpdateNewPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateNewPasswordAsync(request);

            if (!result)
                return BadRequest("Password nije moguće ažurirati.");

            return Ok(new { message = "Password uspješno promijenjen." });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
        [FromBody] ForgotPasswordRequest request)
        {
            await _userService.ForgotPasswordAsync(request.Email);

            // uvijek ista poruka (security)
            return Ok("Ako email postoji, poslan je link za reset lozinke.");
        }

    }
}
