using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.ResponseObject;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IUserService : ICRUDService<Model.model.User, UserSearchObject, UserUpsertRequest, UserUpsertRequest>
    {
        Task<UserLoginResponse?> AuthenticateAsync(UserLoginRequest request);

    }

}
