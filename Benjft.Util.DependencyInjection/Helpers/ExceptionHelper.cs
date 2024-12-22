using System.Reflection;

namespace Benjft.Util.DependencyInjection.Helpers;

internal static class ExceptionHelper {
    public static Exception FactoryReturnedNull =>
        new NullReferenceException("Factory method did not produce an output");

    public static Exception FactoryReturnsWrongType(MethodInfo serviceFactoryMethod, Type serviceType) {
        return new Exception(
            $"Method {serviceFactoryMethod.Name} does not produce a service of the expected type! Expected {serviceType.Name} but found {serviceFactoryMethod.ReturnType.Name}.");
    }

    public static Exception FactoryHasWrongNumberOfArguments(MethodInfo serviceFactoryMethod) {
        return new Exception(
            $"Method {serviceFactoryMethod.Name} should have exactly 1 parameter of type {nameof(IServiceProvider)}");
    }

    public static Exception FactoryAcceptsWrongArgumentType(MethodInfo serviceFactoryMethod, Type parameterType) {
        return new Exception(
            $"Method {serviceFactoryMethod.Name} does not accept a parameter of type {nameof(IServiceProvider)}. Found {parameterType.Name}.");
    }
}
