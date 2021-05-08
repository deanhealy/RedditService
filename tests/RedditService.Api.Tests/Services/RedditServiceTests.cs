using Microsoft.Extensions.Logging;
using Moq;
using RedditService.Api.Exceptions;
using RedditService.Api.Tests.Mocks;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedditService.Api.Tests
{
    [Trait("Category", "Unit")]
    public class RedditServiceTests
    {
        [Fact]
        public async void Constructor_GivenNullLogger_ThrowsException()
        {
            //Arrange
            var httpClient = new HttpClient();

            //Act
            var exception = Record.Exception(() => new Services.RedditService(null, httpClient));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async void Constructor_GivenNullHttpClient_ThrowsException()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<Services.RedditService>>();

            //Act
            var exception = Record.Exception(() => new Services.RedditService(mockLogger.Object, null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetSubredditDataAsync_GivenNullUrl_ThrowArgumentNullException()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<Services.RedditService>>();
            var httpClient = new HttpClient();

            var sut = new Services.RedditService(mockLogger.Object, httpClient);

            //Act
            var result = await Record.ExceptionAsync(() => sut.GetSubrettitDataAsync(null));

            //Assert
            Assert.IsType<ArgumentNullException>(result);
        }

        [Fact]
        public async Task GetSubredditDataAsync_GivenNoJson_ThrowJsonNotFoundException()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<Services.RedditService>>();

            var mockHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("", Encoding.UTF8, "text/plain")
            });
            var mockHttpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new Uri("https://localhost")
            };

            var sut = new Services.RedditService(mockLogger.Object, mockHttpClient);
            var url = "https://www.reddit.com/r/dotnet.json";

            //Act
            var result = await Record.ExceptionAsync(() => sut.GetSubrettitDataAsync(url));

            //Assert
            Assert.IsType<JsonNotFoundException>(result);
        }

        [Fact]
        public async Task GetSubredditDataAsync_GivenValidUrl_ReturnsJson()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<Services.RedditService>>();
            var response = "test";

            var mockHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response, Encoding.UTF8, "text/plain")
            });
            var mockHttpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new Uri("https://localhost")
            };

            var sut = new Services.RedditService(mockLogger.Object, mockHttpClient);
            var url = "https://www.reddit.com/r/dotnet.json";

            //Act
            var result = await sut.GetSubrettitDataAsync(url);

            //Assert
            Assert.NotNull(result);
        }
    }
}
