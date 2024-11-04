using Microsoft.Extensions.DependencyInjection;
using Sympli.Application.Abstractions;
using Sympli.Application.Usecases;

namespace Sympli.Application;

public static class ServiceRegister
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUrlLookupUseCase, UrlLookupUseCase>()
            .AddScoped<ISearchEngineFactory, SearchEngineFactory>();

        return services;
    }
}
