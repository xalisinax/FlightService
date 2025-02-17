using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace FlightService.Core.Domain.Common.DI;

public class DIOptions
{
    public string[]? AssemblyNames { get; set; }

    private readonly List<Assembly> _assemblies = [];
    internal IReadOnlyList<Assembly> Assemblies => _assemblies;

    internal void LoadAssembelies()
    {
        if (AssemblyNames == null || AssemblyNames.Length == 0)
            throw new ArgumentException("Assembly name are required!", nameof(AssemblyNames));

        foreach (var library in DependencyContext.Default.RuntimeLibraries)
        {
            if (AssemblyNames.Any(x => library.Name.Contains(x, StringComparison.OrdinalIgnoreCase)))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                _assemblies.Add(assembly);
            }
        };
    }
}
