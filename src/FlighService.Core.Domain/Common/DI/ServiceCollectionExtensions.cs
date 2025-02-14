using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlighService.Core.Domain.Common.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesWithTheirLifetimes(this IServiceCollection services, Action<DIOptions> configureDependencies)
    {
        var dependencyInjectionOptions = new DIOptions();
        configureDependencies.Invoke(dependencyInjectionOptions);

        dependencyInjectionOptions.LoadAssembelies();

        services.AddServicesWithSingletonLifetime(dependencyInjectionOptions.Assemblies, [typeof(ISingleton)])
            .AddServicesWithTransienLifetime(dependencyInjectionOptions.Assemblies, [typeof(ITransient)])
            .AddServicesWithScopedLifetime(dependencyInjectionOptions.Assemblies, [typeof(IScoped)]);

        return services;
    }

    private static IServiceCollection AddServicesWithSingletonLifetime(this IServiceCollection services, IEnumerable<Assembly> assemblies, IEnumerable<Type> types)
    {
        services.Scan(sourceSelecetor => sourceSelecetor.FromAssemblies(assemblies)
            .AddClasses(type => type.AssignableToAny(types), false)
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;
    }

    private static IServiceCollection AddServicesWithScopedLifetime(this IServiceCollection services, IEnumerable<Assembly> assemblies, IEnumerable<Type> types)
    {
        services.Scan(sourceSelector => sourceSelector.FromAssemblies(assemblies)
            .AddClasses(type => type.AssignableToAny(types), false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddServicesWithTransienLifetime(this IServiceCollection services, IEnumerable<Assembly> assemblies, IEnumerable<Type> types)
    {
        services.Scan(sourceSelector => sourceSelector.FromAssemblies(assemblies)
            .AddClasses(type => type.AssignableToAny(types), false)
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        return services;
    }
}
