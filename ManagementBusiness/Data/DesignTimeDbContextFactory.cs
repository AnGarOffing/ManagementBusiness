using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ManagementBusiness.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ManagementBusinessContext>
    {
        public ManagementBusinessContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManagementBusinessContext>();
            optionsBuilder.UseSqlServer("Server=Offing;Database=ManagementBusiness;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true");

            return new ManagementBusinessContext(optionsBuilder.Options);
        }
    }
}
