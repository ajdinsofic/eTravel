using System.Collections.Generic;
using System.Threading.Tasks;
using eTravelAgencija.Model.Responses;
using eTravelAgencija.Model.Requests;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.SearchObjects;

namespace eTravelAgencija.Services.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAsync(UserSearchObject search = null);
        Task<UserResponse> GetByIdAsync(int id);
        Task<UserResponse> PostAsync(UserUpsertRequest user);
        Task<UserResponse> PutAsync(int id, UserUpsertRequest user);
    }
}
