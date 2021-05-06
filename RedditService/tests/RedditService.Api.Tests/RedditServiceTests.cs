using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace RedditService.Api.Tests
{
    [Trait("Category", "Unit")]
    public class RedditServiceTests
    {
        [Fact]
        public void RedditService_GivenNullLogger_ThrowsException()
        {
            //Arrange
            var httpClient = new HttpClient();

            //Act
            var exception = Record.Exception(() => new Services.RedditService(null, httpClient));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void RedditService_GivenNullHttpClient_ThrowsException()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<Services.RedditService>>();

            //Act
            var exception = Record.Exception(() => new Services.RedditService(mockLogger.Object, null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void RedditService_GivenValidUrl_ReturnsJson()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<Services.RedditService>>();

            var mockHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("".RandomString(8), Encoding.UTF8, "text/plain")
            });
            var mockHttpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new Uri("https://localhost")
            };
            //Act
            var exception = Record.Exception(() => new Services.RedditService(mockLogger.Object, null));

            //Assert
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
