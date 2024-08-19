using Microsoft.Extensions.DependencyInjection;
using PasSecWebApi.Repositories.Contracts.Persistence;
using PasSecWebApi.Repositories.Contracts.Persistence.Users;
using PasSecWebApi.Repositories.Repositories;
using PasSecWebApi.Repositories.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories
{
    public static class RepositoryServiceRegistration
    {
        public static IServiceCollection AddReporitoryService(this IServiceCollection services)
        {
            // add scopes
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
