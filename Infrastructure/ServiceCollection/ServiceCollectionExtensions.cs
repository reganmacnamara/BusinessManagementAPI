using MacsBusinessManagementAPI.Infrastructure.Authentication;
using MacsBusinessManagementAPI.Infrastructure.Authentication.Service;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Services.Allocations;
using MacsBusinessManagementAPI.Services.Pdf;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

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

        public static IServiceCollection AddRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                // Authenticated: partition by user ID from JWT claims
                options.AddPolicy("Authenticated", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                      ?? context.Connection.RemoteIpAddress?.ToString()
                                      ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromSeconds(12),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        }));

                // Unauthenticated: partition by IP address
                options.AddPolicy("Unauthenticated", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 3,
                            Window = TimeSpan.FromSeconds(30),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }));
            });

            return services;
        }

        public static IServiceCollection AddUseCaseInfrastructure(this IServiceCollection services, Assembly assembly)
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
