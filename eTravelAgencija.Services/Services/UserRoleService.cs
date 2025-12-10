using System.Linq;
using AutoMapper;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Services
{
    public class UserRoleService : BaseCRUDService<Model.model.UserRole, UserRoleSearchObject, Database.UserRole, UserRoleUpsertRequest, UserRoleUpsertRequest>, IUserRoleService
    {
        public UserRoleService(eTravelAgencijaDbContext context, IMapper mapper): base(context,mapper)
        {
            
        }

        public override IQueryable<Database.UserRole> ApplyFilter(IQueryable<Database.UserRole> query, UserRoleSearchObject search)
        {
            if (search.userId.HasValue)
            {
                query = query.Where(x => x.UserId == search.userId);
            }

            return base.ApplyFilter(query, search);
        }
    }
}
