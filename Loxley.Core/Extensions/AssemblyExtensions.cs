using System.Reflection;

namespace Loxley.Core.Extensions;

public static class AssemblyExtensions {
    public static IEnumerable<Type> GetTypesFromLoadedAssembly(this Assembly assembly,
                                                               string        assemblyFilter = nameof(Loxley)) {
        return assembly.GetAllReferencedAssemblies(assemblyFilter).SelectMany(a => a.GetTypes());
    }

    private static IEnumerable<Assembly> GetAllReferencedAssemblies(this Assembly assembly, string assemblyFilter) {
        var assemblies = InnerGetAllReferencedAssemblies(assembly);
        return assemblies.DistinctBy(asm => asm.FullName);

        IEnumerable<Assembly> InnerGetAllReferencedAssemblies(Assembly parentAssembly) {
            yield return parentAssembly;
            foreach (var assemblyName in parentAssembly.GetReferencedAssemblies()
                                                       .Where(a => a.Name?.Contains(assemblyFilter) ?? false)) {
                var nextAssembly = Assembly.Load(assemblyName);
                foreach (var childAssembly in InnerGetAllReferencedAssemblies(nextAssembly)) {
                    yield return childAssembly;
                }
            }
        }
    }
}
