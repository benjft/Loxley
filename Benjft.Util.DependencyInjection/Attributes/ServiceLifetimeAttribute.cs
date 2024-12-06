using Microsoft.Extensions.DependencyInjection;

namespace Benjft.Util.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceLifetimeAttribute(ServiceLifetime lifetime) : Attribute {
    public ServiceLifetime Lifetime { get; } = lifetime;

    public void Deconstruct(out ServiceLifetime lifetime) {
        lifetime = Lifetime;
    }
}
