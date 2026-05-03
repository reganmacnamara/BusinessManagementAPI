using Hangfire;
using MacsBusinessManagementAPI.Infrastructure.Authentication;
using MacsBusinessManagementAPI.Infrastructure.Jobs;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Infrastructure.Services.Allocations;
using MacsBusinessManagementAPI.Infrastructure.Services.Auth;
using MacsBusinessManagementAPI.Infrastructure.Services.Email;
using MacsBusinessManagementAPI.Infrastructure.Services.Pdf;
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
            services.AddScoped<IEmailService, SmtpEmailService>();
            services.AddScoped<OverdueInvoiceReminderJob>();
            services.AddScoped<ITenantProvider, TenantProvider>();

            return services;
        }

        public static IServiceCollection AddHangfireInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();

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
                            PermitLimit = 100,
                            Window = TimeSpan.FromSeconds(30),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        }));

                // Unauthenticated: partition by IP address
                options.AddPolicy("Unauthenticated", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromSeconds(60),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }));
            });

            return services;
        }

        public static IServiceCollection AddUseCaseInfrastructure(this IServiceCollection services, Assembly assembly)
        {
            services.AddUseCaseEntityValidation(assembly);
            services.AddUseCaseHandling(assembly);
            services.AddUseCaseMediation(assembly);

            return services;
        }

        public static IServiceCollection AddUseCaseEntityValidation(this IServiceCollection services, Assembly assembly)
        {
            var handlerEntityValidatorPairs = assembly.GetTypes()
                .Where(t => t is { IsAbstract: false, IsInterface: false })
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType
                             && i.GetGenericTypeDefinition() == typeof(IUseCaseEntityValidator<>))
                    .Select(i => (Service: i, Impl: t)));

            foreach (var (service, impl) in handlerEntityValidatorPairs)
                services.AddScoped(service, impl);

            services.AddScoped<EntityValidator.EntityValidator>();

            return services;
        }

        public static IServiceCollection AddUseCaseHandling(this IServiceCollection services, Assembly assembly)
        {
            var handlerUseCasePairs = assembly.GetTypes()
                .Where(t => t is { IsAbstract: false, IsInterface: false })
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType
                             && i.GetGenericTypeDefinition() == typeof(IUseCaseHandler<>))
                    .Select(i => (Service: i, Impl: t)));

            foreach (var (service, impl) in handlerUseCasePairs)
                services.AddScoped(service, impl);

            return services;
        }

        public static IServiceCollection AddUseCaseMediation(this IServiceCollection services, Assembly assembly)
        {
            var _UseCaseRequests = assembly.GetTypes()
                .Where(t => t is { IsAbstract: false, IsInterface: false }
                         && t.GetInterfaces().Contains(typeof(IUseCaseRequest)));

            foreach (var _Request in _UseCaseRequests)
            {
                var _MediatorType = typeof(UseCaseMediator<>).MakeGenericType(_Request);
                services.AddScoped(_MediatorType);
            }

            return services;
        }
    }
}
