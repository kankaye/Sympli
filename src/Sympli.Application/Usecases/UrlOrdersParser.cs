using Sympli.Application.Abstractions;
using Sympli.Application.Domain;

namespace Sympli.Application.Usecases;

public class UrlOrdersParser(string targetUrl) : IMatchResultParser<IEnumerable<int>>
{
    public IEnumerable<int> Parse(List<SearchResult> searchResults)
    {
        for (int i = 0; i < searchResults.Count; i++)
        {
            if (searchResults[i].Url.Contains(targetUrl, StringComparison.OrdinalIgnoreCase))
            {
                yield return i + 1;
            }
        }
    }
}
