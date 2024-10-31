using System.Reflection;
using Loxley.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Loxley.Core.Extensions;

public static class ServiceExtensions {
    public static IServiceCollection AddAppServices(this IServiceCollection services, Assembly topLevelAssembly) {
        var types = topLevelAssembly.GetTypesFromLoadedAssembly()
                                    .Where(t => t.GetCustomAttributes<ServiceForAttribute>().Any());

        foreach (var type in types) {
            var lifetime = type.GetCustomAttribute<ServiceLifetimeAttribute>()?.ServiceLifetime
                        ?? ServiceLifetime.Transient;
            foreach (var serviceForAttribute in type.GetCustomAttributes<ServiceForAttribute>()) {
                var service = serviceForAttribute.ServiceType;
                services.AddServiceWithLifetime(type, service, lifetime);
            }
        }

        return services;
    }

    private static IServiceCollection AddServiceWithLifetime(this IServiceCollection services, Type type, Type service,
                                                             ServiceLifetime         lifetime) {
        return lifetime switch {
            ServiceLifetime.Singleton => services.AddSingleton(service, type),
            ServiceLifetime.Scoped    => services.AddScoped(service, type),
            ServiceLifetime.Transient => services.AddTransient(service, type),
            _                         => throw new ArgumentOutOfRangeException(nameof(lifetime))
        };
    }
}
