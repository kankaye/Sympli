using Sympli.Application.Abstractions;
using Sympli.Application.Enums;

namespace Sympli.Application.Usecases;

public class UrlLookupUseCase(ISearchEngineFactory searchEngineFactory) : IUrlLookupUseCase
{
    public async Task<string> HandleAsync(string keywords,
        string url,
        SearchEngineType engineType,
        bool enableCache = true,
        CancellationToken cancellationToken = default)
    {
        var _searchEngine = searchEngineFactory.GetEngine(engineType, enableCache);

        if (_searchEngine == null)
        {
            throw new InvalidOperationException("Search engine not set.");
        }

        var urlOrders = await _searchEngine.SearchAndParseMatchesAsync(keywords, new UrlOrdersParser(url));

        return string.Join(",", urlOrders);
    }
}
