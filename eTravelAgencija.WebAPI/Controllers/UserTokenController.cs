using eTravelAgencija.Model.model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{
    public class UserTokenController : BaseCRUDController<Model.model.UserToken, UserTokenSearchObject, UserTokenUpsertRequest, UserTokenUpsertRequest>
    {
        public UserTokenController(ILogger<BaseCRUDController<Model.model.UserToken, UserTokenSearchObject, UserTokenUpsertRequest, UserTokenUpsertRequest>> logger, IUserTokenService userTokenService): base(logger, userTokenService)
        {
            
        }

        [HttpPost("{userId}/increase")]
        public async Task<ActionResult<UserToken>> Increase(int userId)
        {
            try
            {
                var result = await (_service as IUserTokenService).IncreaseTokensToUserAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// ============================
        /// −10 tokena
        /// ============================
        [HttpPost("{userId}/decrease")]
        public async Task<ActionResult<UserToken>> Decrease(int userId)
        {
            try
            {
                var result = await (_service as IUserTokenService).DecreaseTokensToUserAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}