using Microsoft.Extensions.DependencyInjection;

namespace Loxley.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceLifetimeAttribute(ServiceLifetime serviceLifetime) : Attribute {
    public ServiceLifetime ServiceLifetime { get; } = serviceLifetime;
}