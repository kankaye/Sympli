using Microsoft.Extensions.DependencyInjection;
using Sympli.Application.Abstractions;
using Sympli.Application.Enums;

namespace Sympli.Application;

public class SearchEngineFactory(ICacheManager _cacheManager, IServiceProvider _serviceProvider) : ISearchEngineFactory
{
    public ISearchEngine GetEngine(SearchEngineType engineType, bool enableCache)
    {
        var searchEngine = _serviceProvider.GetRequiredKeyedService<ISearchEngine>(engineType);

        if (searchEngine == null)
        {
            throw new Exception("");
        }

        return enableCache ? new CachingSearchDecorator(searchEngine, _cacheManager) : searchEngine;
    }
}
