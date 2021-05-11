using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RedditService.Integration.Tests
{
    [Trait ("Category", "Integration")]
    public class RedditServiceTests
    {
        private readonly HttpClient _httpClient;

        public RedditServiceTests()
        {
            _httpClient = new HttpClient();
        }

        [Theory]
        [InlineData("https://www.reddit.com/r/angular.json")]
        [InlineData("https://www.reddit.com/r/dotnet.json ")]
        [InlineData("https://www.reddit.com/r/sql.json")]
        public async Task GivenValidUrl_WhenGetSubrettitDataAsync_DoesReturnJson(string url)
        {
            //Arrange
            var sut = new Api.Services.RedditService(NullLogger<Api.Services.RedditService>.Instance, _httpClient);

            //Act
            var result = await sut.GetSubrettitDataAsync(url);

            //Assert
            Assert.NotNull(result);
            result.Should().Contain("children");
        }
    }
}
