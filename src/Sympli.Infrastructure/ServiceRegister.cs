using Microsoft.Extensions.DependencyInjection;
using Sympli.Application.Abstractions;
using Sympli.Application.Attributes;
using Sympli.Infrastructure.Caches;
using System.Reflection;

namespace Sympli.Infrastructure;

public static class ServiceRegister
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient()
            .AddSingleton<ICacheManager, AppMemoryCache>();

        AddSearchEngines(services);
        return services;
    }

    private static void AddSearchEngines(IServiceCollection services)
    {
        var serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
                                   .Where(t => typeof(ISearchEngine).IsAssignableFrom(t)
                                   && t.GetCustomAttribute<SearchEngineTypeAttribute>() != null
                                   && t.IsClass
                                   && !t.IsAbstract)
                                   .ToList();

        foreach (var type in serviceTypes)
        {
            var engineTypeAttribute = type.GetCustomAttribute<SearchEngineTypeAttribute>();
            services.AddKeyedScoped(typeof(ISearchEngine), engineTypeAttribute!.Type, type);
        }
    }
}
