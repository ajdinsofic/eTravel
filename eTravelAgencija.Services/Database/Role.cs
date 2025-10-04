using Microsoft.AspNetCore.Identity;

namespace eTravelAgencija.Services.Database
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    
    }

}
