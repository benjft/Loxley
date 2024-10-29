namespace Loxley.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ServiceForAttribute(Type target) : Attribute {
    public Type ServiceType { get; } = target;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ServiceForAttribute<TInterface>() : ServiceForAttribute(typeof(TInterface));