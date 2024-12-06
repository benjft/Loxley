using System.Reflection;
using Benjft.Util.DependencyInjection.Attributes;
using Benjft.Util.DependencyInjection.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Benjft.Util.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection ScanAppDomainForServices(
        this IServiceCollection serviceCollection,
        ServiceLifetime defaultLifetime = ServiceLifetime.Scoped,
        Func<Assembly, bool>? assemblyFilter = null) {
        var types = GetServices(assemblyFilter);

        foreach (var type in types) {
            var serviceType = type.GetCustomAttribute<ServiceTypeAttribute>()!.ServiceType;
            var serviceLifetime = type.GetCustomAttribute<ServiceLifetimeAttribute>()?.Lifetime ?? defaultLifetime;

            var factoryMethod = GetServiceFactoryMethod(type, serviceType);

            if (factoryMethod != null) {
                serviceCollection.RegisterServiceFactory(serviceLifetime, serviceType, factoryMethod);
            } else {
                serviceCollection.RegisterDefaultService(serviceLifetime, type, serviceType);
            }
        }

        return serviceCollection;
    }

    private static void RegisterDefaultService(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime,
        Type type,
        Type serviceType) {
        switch (serviceLifetime) {
            case ServiceLifetime.Scoped:
                serviceCollection.AddScoped(serviceType, type);
                break;
            case ServiceLifetime.Transient:
                serviceCollection.AddTransient(serviceType, type);
                break;
            case ServiceLifetime.Singleton:
            default:
                serviceCollection.AddSingleton(serviceType, type);
                break;
        }
    }

    private static void RegisterServiceFactory(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime,
        Type serviceType,
        MethodInfo factoryMethod) {
        switch (serviceLifetime) {
            case ServiceLifetime.Scoped:
                serviceCollection.AddScoped(serviceType, InvokeFactory);
                break;
            case ServiceLifetime.Transient:
                serviceCollection.AddTransient(serviceType, InvokeFactory);
                break;
            case ServiceLifetime.Singleton:
            default:
                serviceCollection.AddSingleton(serviceType, InvokeFactory);
                break;
        }

        return;

        object InvokeFactory(IServiceProvider sp) =>
            factoryMethod.Invoke(null, [sp]) ?? throw ExceptionHelper.FactoryReturnedNull;
    }

    private static IEnumerable<Type> GetServices(Func<Assembly, bool>? assemblyFilter) {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        if (assemblyFilter != null) {
            assemblies = assemblies.Where(assemblyFilter).ToArray();
        }

        var types = assemblies.SelectMany(assembly => assembly.GetTypes())
           .Where(
                type => type is { IsClass: true, IsAbstract: false }
                 && type.GetCustomAttribute<ServiceTypeAttribute>() != null);
        return types;
    }

    private static MethodInfo? GetServiceFactoryMethod(Type type, Type serviceType) {
        var serviceFactoryMethod = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
           .SingleOrDefault(methodInfo => methodInfo.GetCustomAttribute<ServiceFactoryAttribute>() != null);

        if (serviceFactoryMethod == null) {
            return null;
        }

        if (!serviceFactoryMethod.ReturnType.IsAssignableTo(serviceType)) {
            throw ExceptionHelper.FactoryReturnsWrongType(serviceFactoryMethod, serviceType);
        }

        var factoryParameters = serviceFactoryMethod.GetParameters();
        if (factoryParameters.Length != 1) {
            throw ExceptionHelper.FactoryHasWrongNumberOfArguments(serviceFactoryMethod);
        }

        if (!factoryParameters[0].ParameterType.IsAssignableFrom(typeof(IServiceProvider))) {
            throw ExceptionHelper.FactoryAcceptsWrongArgumentType(
                serviceFactoryMethod,
                factoryParameters[0].ParameterType);
        }

        return serviceFactoryMethod;
    }
}
