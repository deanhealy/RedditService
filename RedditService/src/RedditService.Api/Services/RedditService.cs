using Microsoft.Extensions.Logging;
using RedditService.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditService.Api.Services
{
    public class RedditService : IRedditService
    {
        private readonly ILogger<RedditService> _logger;
        private readonly HttpClient _httpClient;

        public RedditService( ILogger<RedditService> logger, HttpClient httpClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        }

        public async Task<string> GetSubrettitDataAsync(string subreddit)
        {
            var json =  await _httpClient.GetStringAsync(subreddit);
            return json;
        }
    }
}
