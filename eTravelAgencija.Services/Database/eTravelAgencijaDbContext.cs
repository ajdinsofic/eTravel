using eTravelAgencija.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class eTravelAgencijaDbContext : IdentityDbContext<User, Role, string>
{
    public eTravelAgencijaDbContext(DbContextOptions<eTravelAgencijaDbContext> options) : base(options)
    {
        
    }
}
