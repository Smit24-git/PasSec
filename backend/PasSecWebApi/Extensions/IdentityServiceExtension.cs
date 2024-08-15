using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PasSecWebApi.Persistence;
using System.Text;
using System.Text.Json;

namespace PasSecWebApi.Extensions
{
    public static class IdentityServiceExtension
    {
        private const string SecretKeyEnvPath = "ApiSettings:Secret";
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetValue<string>(SecretKeyEnvPath)!;

            if(string.IsNullOrEmpty(key) || key == "AddScretKeyHere")
            {
                throw new ApplicationException($"Please configure {SecretKeyEnvPath}");
            }

            // configure identity service.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PasSecDatabaseContext>()
                .AddDefaultTokenProviders();

            // add authentication service with jwt bearer token configuration here.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result =  JsonSerializer.Serialize(new { Message = "401 Not Authorized"});
                        return context.Response.WriteAsync(result);
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new { Message = "403 Not Authorized"});
                        return context.Response.WriteAsync(result);
                    }
                };
            });

            return services;
        }
    }
}
