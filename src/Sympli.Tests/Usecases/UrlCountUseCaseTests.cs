using Moq;
using Sympli.Application.Abstractions;
using Sympli.Application.Domain;
using Sympli.Application.Enums;
using Sympli.Application.Usecases;
using Sympli.Infrastructure.SearchEngines;
using Sympli.Tests.TestData;

namespace Sympli.Tests.Usecases;

public class UrlCountUseCaseTests
{
    private const string Keywords = "example";
    private const string Url = "https://example.com";

    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly Mock<ISearchEngineFactory> _mockSearchEngineFactory;
    private readonly UrlLookupUseCase _urlLookupUseCase;

    public UrlCountUseCaseTests()
    {
        _mockSearchEngineFactory = new Mock<ISearchEngineFactory>();
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        var googleEngine = CreateMockEngine<GoogleSearchEngine>(MockSearchResultData.Google);
        var bingEngine = CreateMockEngine<BingSearchEngine>(MockSearchResultData.Bing);

        _mockSearchEngineFactory.Setup(factory => factory.GetEngine(SearchEngineType.Google, It.IsAny<bool>()))
            .Returns(googleEngine.Object);

        _mockSearchEngineFactory.Setup(factory => factory.GetEngine(SearchEngineType.Bing, It.IsAny<bool>()))
            .Returns(bingEngine.Object);

        _urlLookupUseCase = new UrlLookupUseCase(_mockSearchEngineFactory.Object);
    }

    [Theory]
    [InlineData(SearchEngineType.Google, true, "1,5")]
    [InlineData(SearchEngineType.Bing, false, "4,7")]
    public async Task HandleAsync_ReturnsFormattedResults_WhenResultsExist(SearchEngineType engineType, bool enableCache, string expectedResult)
    {
        var result = await _urlLookupUseCase.HandleAsync(Keywords, Url, engineType, enableCache);
        Assert.Equal(expectedResult, result);
    }

    private Mock<TSearchEngine> CreateMockEngine<TSearchEngine>(List<SearchResult> mockData) where TSearchEngine : class, ISearchEngine
    {
        var mockEngine = new Mock<TSearchEngine>(_mockHttpClientFactory.Object);
        mockEngine.Setup(engine => engine.FetchSearchResults(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(mockData);
        return mockEngine;
    }
}
