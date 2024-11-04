using Sympli.Application.Abstractions;
using Sympli.Application.Attributes;
using Sympli.Application.Domain;
using Sympli.Application.Enums;
using System.Text.RegularExpressions;

namespace Sympli.Infrastructure.SearchEngines;

[SearchEngineType(SearchEngineType.Google)]
public class GoogleSearchEngine(IHttpClientFactory httpClientFactory) : BaseSearchEngine(httpClientFactory), ISearchEngine
{
    protected override string _baseUrl => "https://www.google.com.au";

    protected override IEnumerable<SearchResult> ConvertToSearchResults(string resultString)
    {
        var searchResults = new List<SearchResult>();

        string resultBlockPattern = @"<div[^>]*class=""[^""]*""[^>]*><div[^>]*class=""[^""]*""[^>]*><a href=""/url\?q=([^""]+)""(.*?)</div></div></div></div></div></div></div>";
        string titlePattern = @"<h3[^>]*>\s*<div[^>]*>(.*?)</div>";
        string urlPattern = @"href=""/url\?q=([^&]+)";
        string snippetPattern = @"<div[^>]*>\s*<div[^>]*>\s*<div[^>]*>\s*<div[^>]*>\s*<div[^>]*>(.*?)</div>";

        var matches = Regex.Matches(resultString, resultBlockPattern);

        foreach (Match match in matches)
        {
            var urlMatch = Regex.Match(match.Value, urlPattern);
            var url = urlMatch.Success ? urlMatch.Groups[1].Value : throw new Exception("URL not found");

            var titleMatch = Regex.Match(match.Value, titlePattern);
            var title = titleMatch.Success ? titleMatch.Groups[1].Value : throw new Exception("Title not found");

            var snippetMatch = Regex.Match(match.Value, snippetPattern, RegexOptions.Singleline);
            var snippet = snippetMatch.Success ? snippetMatch.Groups[1].Value.Trim() : throw new Exception("Snippet not found");

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
        int startResult = (pageNumber - 1) * 10;
        return $"{_baseUrl}/search?q={Uri.EscapeDataString(keyword)}&start={startResult}";
    }

   
}
