using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // auto-mapper to map the dtos to database objects
            // learn more about app domain: https://learn.microsoft.com/en-us/dotnet/framework/app-domains/
            // learn more about assemblies: https://learn.microsoft.com/en-us/dotnet/standard/assembly/

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // mediatR to mediate transactions (CQRS)
            /**
             * Command Query Responsibility Segregation treats data retrival(read) and Updation (write) separately.
             * Query in a command query responsibility represents read part of the read/write responses.
             * Command in the command query responsibility represents write part of the read/wite responses.
             * 
             * CQRS has its' own downsides but for the sake of learning, it has been implemented for this project.
             */
            services.AddMediatR((conf) => { 
                conf.RegisterServicesFromAssemblies(
                    AppDomain.CurrentDomain.GetAssemblies()); 
            });

            return services;
        }
    }
}
