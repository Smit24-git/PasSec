using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PasSecWebApi.Persistence
{
    public class PasSecContextFactory : IDesignTimeDbContextFactory<PasSecDatabaseContext>
    {
        private const string AdminConnectionString = "PAS_SEC_ADMIN_CONNECTION_STRING";
        public PasSecDatabaseContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable(AdminConnectionString);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException($"Please set the environment variable {AdminConnectionString}");
            }

            var options = new DbContextOptionsBuilder<PasSecDatabaseContext>()
                .UseMySql(connectionString,
                          ServerVersion.AutoDetect(connectionString),
                          x => x.MigrationsAssembly("DatabaseMigration"))
                .Options;
            return new PasSecDatabaseContext(options);
        }
    }
}
