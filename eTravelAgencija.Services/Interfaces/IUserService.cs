using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.ResponseObject;
using eTravelAgencija.Model.RequestObjects;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IUserService : ICRUDService<Model.model.User, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        Task<UserLoginResponse?> AuthenticateAsync(UserLoginRequest request);
        Task<bool> UnblockUser(int id);
        Task<bool> blockUser(int id);
        Task<bool> AddUserImage(UserImageRequest request);
        Task<bool> DeleteUserImageAsync(int id);
        Task<bool> CheckPasswordAsync(CheckPasswordRequest request);
        Task<bool> UpdateNewPasswordAsync(UpdateNewPasswordRequest request);
        Task<string?> GetUserImage(int userId);
        


    }

}
