using Microsoft.OpenApi.Models;
using PasSecWebApi.Application;
using PasSecWebApi.Extensions;
using PasSecWebApi.Middleware;
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "use Bearer Token.",
                    Name="Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =  new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            if(builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                });
            }
            else
            {
                var whitelistURLs = builder.Configuration["WhitelistUrls"]?.Split(',');
                if(whitelistURLs == null  || whitelistURLs.Length == 0)
                {
                    throw new ApplicationException("Missing Whitelist URLs. please configure 'WhitelistUrls' in environment variables");
                }
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("Close", builder => builder.WithOrigins(whitelistURLs ?? []));
                });
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
                app.UseCors("Open");
            }
            else
            {
                app.UseCors("Close");
            }



            app.UseHttpsRedirection();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
            app.UseCustomExceptionHandler();

            return app;
        }
    }
}
