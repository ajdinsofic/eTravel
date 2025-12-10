using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{

    public class UserRoleController : BaseCRUDController<Model.model.UserRole, UserRoleSearchObject, UserRoleUpsertRequest, UserRoleUpsertRequest>
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(ILogger<BaseCRUDController<Model.model.UserRole, UserRoleSearchObject, UserRoleUpsertRequest, UserRoleUpsertRequest>> logger, IUserRoleService userRoleService) : base (logger, userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [Authorize(Roles = "Radnik,Direktor")]
        [HttpDelete("deleteComposite/{userId}/{roleId}")]
        public async Task<IActionResult> DeleteComposite(int userId, int roleId)
        {
            var success = await _userRoleService.DeleteCompositeAsync(userId, roleId);

            if (!success)
                return NotFound("UserRole kombinacija nije pronađena.");

            return Ok(true);
        }

        

        
    }
}
