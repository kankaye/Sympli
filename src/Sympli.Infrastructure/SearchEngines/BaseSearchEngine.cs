using Sympli.Application.Abstractions;
using Sympli.Application.Domain;
using System.Text.RegularExpressions;

namespace Sympli.Infrastructure.SearchEngines;

public abstract class BaseSearchEngine(IHttpClientFactory httpClientFactory): ISearchEngine
{
    protected readonly HttpClient _httpClient = httpClientFactory.CreateClient();

    protected abstract string _baseUrl { get; }

    protected abstract IEnumerable<SearchResult> ConvertToSearchResults(string resultString);

    protected abstract string PrepareUrl(string keyword, int pageNumber);

    public virtual async Task<List<SearchResult>> FetchSearchResults(string keyword, int maximumPage = 10)
    {
        var results = new List<SearchResult>();

        for (int pageNumber = 1; pageNumber <= maximumPage; pageNumber++)
        {
            string searchUrl = PrepareUrl(keyword, pageNumber);

            var response = await _httpClient.GetAsync(searchUrl);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();

                var convertedResults = ConvertToSearchResults(resultString).ToList();

                results.AddRange(convertedResults);
            }
        }

        return results;
    }

    public async Task<T> SearchAndParseMatchesAsync<T>(string keywords, IMatchResultParser<T> resultParser, int maximumPage = 10)
    {
        var results = await FetchSearchResults(keywords, maximumPage);

        return resultParser.Parse(results);
    }

    protected string CleanHtml(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty).Trim();
    }
}
