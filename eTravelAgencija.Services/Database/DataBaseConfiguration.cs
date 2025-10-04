using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eTravelAgencija.Services.Database
{
    public class eTravelAgencijaDbContextFactory : IDesignTimeDbContextFactory<eTravelAgencijaDbContext>
    {
        public eTravelAgencijaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<eTravelAgencijaDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Database=eTravel;Username=postgres;Password=1234");

            return new eTravelAgencijaDbContext(optionsBuilder.Options);
        }
    }
}
