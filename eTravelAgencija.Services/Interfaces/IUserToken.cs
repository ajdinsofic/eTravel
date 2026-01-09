using System.Threading.Tasks;
using eTravelAgencija.Model.model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IUserTokenService : ICRUDService<Model.model.UserToken, UserTokenSearchObject, UserTokenUpsertRequest, UserTokenUpsertRequest>
    {
        Task<UserToken> DecreaseTokensToUserAsync(int userId);

        Task<UserToken> IncreaseTokensToUserAsync(int userId);
        
    }
}
