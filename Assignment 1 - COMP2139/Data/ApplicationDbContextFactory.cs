using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Assignment_1___COMP2139.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Use your PostgreSQL connection string
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=VirtualEventDB;Username=postgres;Password=mjamesh");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
