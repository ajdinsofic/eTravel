using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace eTravelAgencija.Services.Database
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }

        public ICollection<UserRole> UserRoles {get; set;} = new List<UserRole>();
    
    }

}
