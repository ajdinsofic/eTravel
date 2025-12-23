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
    }
}