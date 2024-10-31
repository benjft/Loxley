using System.Reflection;

namespace Loxley.Core.Extensions;

public static class AssemblyExtensions {
    public static IEnumerable<Type> GetTypesFromLoadedAssembly(this Assembly assembly) {
        Assembly[] assemblies = [
            assembly,
            ..assembly.GetReferencedAssemblies()
                      .Select(Assembly.Load)
        ];

        return assemblies.SelectMany(a => a.GetTypes());
    }
}
