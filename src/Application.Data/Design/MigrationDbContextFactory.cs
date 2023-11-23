using Application.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Application.Data.Design
{
    // This is an ef core migration class-only, use your local dev to build the migrations.
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog= WebTech.WebApplication;Integrated Security=True;multipleactiveresultsets=True;App=Apply");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
