using Moq;
using Moq.Protected;
using Sympli.Application.Abstractions;
using Sympli.Application.Enums;
using Sympli.Infrastructure.SearchEngines;
using System.Net;

namespace Sympli.Tests.Infrastructure;

public class SearchEngineTest
{
    public SearchEngineTest()
    {
        var _mockHttpClientFactory = new Mock<IHttpClientFactory>();
    }

    [Theory]
    [InlineData(SearchEngineType.Google, "GoogleResponse.html")]
    [InlineData(SearchEngineType.Bing, "BingResponse.html")]
    public async Task FetchSearchResults_ReturnsResults_WhenKeywordIsProvided(SearchEngineType searchEngineType, string htmlFileName)
    {
        var mockResponseContent = File.ReadAllText(Path.Combine("TestData", htmlFileName));

        var httpClientFactoryMock = CreateHttpClientFactoryMock(mockResponseContent);

        ISearchEngine searchEngine = searchEngineType switch
        {
            SearchEngineType.Google => new GoogleSearchEngine(httpClientFactoryMock.Object),
            SearchEngineType.Bing => new BingSearchEngine(httpClientFactoryMock.Object),
            _ => throw new NotImplementedException()
        };

        var result = await searchEngine.FetchSearchResults("e-settlements", 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
        Assert.True(!string.IsNullOrEmpty(result[0].Title) && !string.IsNullOrEmpty(result[0].Url));
    }

    private Mock<IHttpClientFactory> CreateHttpClientFactoryMock(string responseContent)
    {
        var responseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(responseContent),
        };

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock
            .Setup(factory => factory.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(handlerMock.Object));

        return httpClientFactoryMock;
    }
}
