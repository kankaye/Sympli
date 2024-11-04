using Sympli.Application.Abstractions;
using Sympli.Application.Domain;
using Sympli.Application.Enums;
using System.Text.RegularExpressions;

namespace Sympli.Infrastructure.SearchEngines;

[Application.Attributes.SearchEngineType(SearchEngineType.Bing)]
public class BingSearchEngine(IHttpClientFactory httpClientFactory) : BaseSearchEngine(httpClientFactory), ISearchEngine
{
    protected override string _baseUrl => "https://www.bing.com.au";

    protected override IEnumerable<SearchResult> ConvertToSearchResults(string resultString)
    {
        var searchResults = new List<SearchResult>();

        string blockPattern = @"<li\b[^>]*?class=""[^""]*b_algo[^""]*"".*?>(.*?)</div></li>";
        string urlPattern = @"<a.*?href=""(https?://[^""]+)"""; 
        string titlePattern = @"<h2><a.*?>(.*?)</a></h2>";
        string snippetPattern = @"<p\b[^>]*>(.*?)</p>";

        MatchCollection blockMatches = Regex.Matches(resultString, blockPattern);


        foreach (Match match in blockMatches)
        {
            var urlMatch = Regex.Match(match.Value, urlPattern);
            var url = urlMatch.Success ? urlMatch.Groups[1].Value : throw new Exception("URL not found");

            var titleMatch = Regex.Match(match.Value, titlePattern);
            var title = titleMatch.Success ? titleMatch.Groups[1].Value : throw new Exception("Title not found");

            var snippetMatch = Regex.Match(match.Value, snippetPattern);
            var snippet = snippetMatch.Success ? snippetMatch.Groups[1].Value.Trim() : "Snippet not found";

            searchResults.Add(new SearchResult
            {
                Title = CleanHtml(title),
                Url = CleanHtml(url),
                Snippet = CleanHtml(snippet)
            });
        }

        return searchResults;
    }

    protected override string PrepareUrl(string keyword, int pageNumber)
    {
        int firstResult = (pageNumber - 1) * 10 + 1;
        return $"{_baseUrl}/search?q={Uri.EscapeDataString(keyword)}&first={firstResult}";
    }
}