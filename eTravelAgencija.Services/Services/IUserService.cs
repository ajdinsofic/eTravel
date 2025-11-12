using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IUserService : ICRUDService<Model.model.User, UserSearchObject, UserUpsertRequest, UserUpsertRequest>
    {
        Task<Model.model.User?> AuthenticateAsync(UserLoginRequest request);

    }

}
