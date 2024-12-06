namespace Benjft.Util.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceTypeAttribute(Type serviceType) : Attribute {
    public Type ServiceType { get; } = serviceType;

    public void Deconstruct(out Type serviceType) {
        serviceType = ServiceType;
    }
}

public class ServiceTypeAttribute<T>() : ServiceTypeAttribute(typeof(T));
