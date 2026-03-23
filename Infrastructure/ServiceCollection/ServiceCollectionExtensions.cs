using MacsBusinessManagementAPI.Infrastructure.Authentication;
using MacsBusinessManagementAPI.Infrastructure.Authentication.Service;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Services.Allocations;
using MacsBusinessManagementAPI.Services.Pdf;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace MacsBusinessManagementAPI.Infrastructure.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IAllocationService, AllocationService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPdfService, PdfService>();

            return services;
        }

        public static IServiceCollection AddJwtAuth(this IServiceCollection services, JwtConfig jwtConfig)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtConfig.Secret))
                    };
                });

            return services;
        }

        public static IServiceCollection AddUseCaseHandlers(this IServiceCollection services, Assembly assembly)
        {
            var handlerPairs = assembly.GetTypes()
                .Where(t => t is { IsAbstract: false, IsInterface: false })
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType
                             && i.GetGenericTypeDefinition() == typeof(IUseCaseHandler<>))
                    .Select(i => (Service: i, Impl: t)));

            foreach (var (service, impl) in handlerPairs)
                services.AddScoped(service, impl);

            return services;
        }
    }
}
