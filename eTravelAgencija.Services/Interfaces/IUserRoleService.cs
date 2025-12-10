using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IUserRoleService : ICRUDService<Model.model.UserRole, UserRoleSearchObject, UserRoleUpsertRequest, UserRoleUpsertRequest>
    {
    }
}
