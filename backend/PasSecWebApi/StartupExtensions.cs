using PasSecWebApi.Application;
using PasSecWebApi.Extensions;
using PasSecWebApi.Persistence;
using PasSecWebApi.Repositories;

namespace PasSecWebApi
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add custom services here
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceService(builder.Configuration);
            builder.Services.AddReporitoryService();
            builder.Services.AddIdentityService(builder.Configuration);

            // 3rd party services configuration
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            if(builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }


            // return the build
            return builder.Build();
        }

        // flow or order of the configuration matters
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
