using Microsoft.AspNetCore.Identity;
using PasSecWebApi.Persistence;

namespace PasSecWebApi.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            // configure identity service.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PasSecDatabaseContext>()
                .AddDefaultTokenProviders();

            // add authentication service with jwt bearer token configuration here.


            return services;
        }
    }
}
