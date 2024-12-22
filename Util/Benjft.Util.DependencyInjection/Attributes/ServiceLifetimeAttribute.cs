using Microsoft.Extensions.DependencyInjection;

namespace Benjft.Util.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceLifetimeAttribute(ServiceLifetime lifetime) : Attribute {
    public ServiceLifetime Lifetime { get; } = lifetime;

    public void Deconstruct(out ServiceLifetime lifetime) {
        lifetime = Lifetime;
    }
}

public class SingletonAttribute() : ServiceLifetimeAttribute(ServiceLifetime.Singleton);

public class ScopedAttribute() : ServiceLifetimeAttribute(ServiceLifetime.Scoped);

public class TransientAttribute() : ServiceLifetimeAttribute(ServiceLifetime.Transient);
