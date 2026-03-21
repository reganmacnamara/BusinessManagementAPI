using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.Services.Allocations;
using MacsBusinessManagementAPI.Services.Pdf;
using System.Reflection;

namespace MacsBusinessManagementAPI.Services.IServiceCollection_Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IAllocationService, AllocationService>();
            services.AddScoped<IPdfService, PdfService>();

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
