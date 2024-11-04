using Sympli.Application.Domain;

namespace Sympli.Tests.TestData;

public class MockSearchResultData
{
    public static List<SearchResult> Google { get; set; } =
    [
        new SearchResult { Title = "Title", Url = "https://example.com", Snippet = "Snippet for result 1" },
        new SearchResult { Title = "Title 2", Url = "https://example-2.com", Snippet = "Snippet for result 2" },
        new SearchResult { Title = "Title 3", Url = "https://example-3.com", Snippet = "Snippet for result 3" },
        new SearchResult { Title = "Title 4", Url = "https://example-4.com", Snippet = "Snippet for result 4" },
        new SearchResult { Title = "Title 5", Url = "https://example.com", Snippet = "Snippet for result 5" },
        new SearchResult { Title = "Title 6", Url = "https://example-6.com", Snippet = "Snippet for result 6" },
        new SearchResult { Title = "Title 7", Url = "https://example-7.com", Snippet = "Snippet for result 7" },
        new SearchResult { Title = "Title 8", Url = "https://example-8.com", Snippet = "Snippet for result 8" },
        new SearchResult { Title = "Title 9", Url = "https://example-9.com", Snippet = "Snippet for result 9" },
        new SearchResult { Title = "Title 10", Url = "https://example-10.com", Snippet = "Snippet for result 10" }
    ];

    public static List<SearchResult> Bing { get; set; } =
    [
        new SearchResult { Title = "Title 1", Url = "https://example-1.com", Snippet = "Snippet for result 1" },
        new SearchResult { Title = "Title 2", Url = "https://example-2.com", Snippet = "Snippet for result 2" },
        new SearchResult { Title = "Title 3", Url = "https://example-3.com", Snippet = "Snippet for result 3" },
        new SearchResult { Title = "Title 4", Url = "https://example.com", Snippet = "Snippet for result 4" },
        new SearchResult { Title = "Title 5", Url = "https://example-5.com", Snippet = "Snippet for result 5" },
        new SearchResult { Title = "Title 6", Url = "https://example-6.com", Snippet = "Snippet for result 6" },
        new SearchResult { Title = "Title 7", Url = "https://example.com", Snippet = "Snippet for result 7" },
        new SearchResult { Title = "Title 8", Url = "https://example-8.com", Snippet = "Snippet for result 8" },
        new SearchResult { Title = "Title 9", Url = "https://example-9.com", Snippet = "Snippet for result 9" },
        new SearchResult { Title = "Title 10", Url = "https://example-10.com", Snippet = "Snippet for result 10" }
    ];
}
