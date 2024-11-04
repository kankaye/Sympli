using Sympli.Application.Domain;

namespace Sympli.Application.Abstractions;

public interface ISearchEngine
{
    Task<List<SearchResult>> FetchSearchResults(string keyword, int maximumPage = 10);

    Task<T> SearchAndParseMatchesAsync<T>(string keywords, IMatchResultParser<T> resultParser, int maximumPage = 10);
}
