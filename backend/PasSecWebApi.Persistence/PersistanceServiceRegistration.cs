using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PasSecWebApi.Persistence
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            // configure database context
            services.AddDbContext<PasSecDatabaseContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("mysqlDatabase"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("mysqlDatabase")),
                    x=>x.MigrationsAssembly("DatabaseMigration"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            return services;
        }
    }
}
