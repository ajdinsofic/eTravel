using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IUserTokenService : ICRUDService<Model.model.UserToken, BaseSearchObject, UserTokenUpsertRequest, UserTokenUpsertRequest>
    {
    }
}
