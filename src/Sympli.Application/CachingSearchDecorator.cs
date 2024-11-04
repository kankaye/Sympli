using Sympli.Application.Abstractions;
using Sympli.Application.Domain;

namespace Sympli.Application;

public class CachingSearchDecorator : ISearchEngine
{
    private readonly ISearchEngine _inner;
    private readonly ICacheManager _cacheManager;
    public CachingSearchDecorator(ISearchEngine inner, ICacheManager cacheManager)
    {
        _inner = inner;
        _cacheManager = cacheManager;
    }

    public async Task<List<SearchResult>> FetchSearchResults(string keyword, int maximumPage = 10)
    {
        if (_inner == null)
        {
            throw new InvalidOperationException("Search engine not set.");
        }

        var cacheKey = $"{_inner.GetType().Name}:{nameof(FetchSearchResults)}:{keyword}";

        var cachedResults = _cacheManager.Get<List<SearchResult>>(cacheKey);

        if (cachedResults == null)
        {
            cachedResults = await _inner.FetchSearchResults(keyword, maximumPage);
            _cacheManager.Set(cacheKey, cachedResults, TimeSpan.FromHours(1));
        }

        return cachedResults;
    }

    public async Task<T> SearchAndParseMatchesAsync<T>(string keywords, IMatchResultParser<T> resultParser, int maximumPage = 10)
    {
        if (_inner == null)
        {
            throw new InvalidOperationException("Search engine not set.");
        }

        var cacheKey = $"{_inner.GetType().Name}:{nameof(SearchAndParseMatchesAsync)}:{keywords}";

        var cachedResults = _cacheManager.Get<T>(cacheKey);

        if (cachedResults == null)
        {
            cachedResults = await _inner.SearchAndParseMatchesAsync(keywords, resultParser, maximumPage);
            _cacheManager.Set(cacheKey, cachedResults, TimeSpan.FromHours(1));
        }

        return cachedResults;
    }
}
