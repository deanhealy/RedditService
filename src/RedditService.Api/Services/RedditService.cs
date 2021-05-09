using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedditService.Api.Exceptions;
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
            CheckUrl(subreddit);

            _logger.LogInformation("Start requesting json for Subreddit: {subreddit}", subreddit);

            var response =  await _httpClient.GetAsync(subreddit);

            response.EnsureSuccessStatusCode();

            string json = null;
            if (response.Content != null)
            {
                var result = await response.Content.ReadAsStringAsync();

                json = result;
            }

            if (string.IsNullOrEmpty(json))
            {
                throw new JsonNotFoundException();
            }

            _logger.LogInformation("Finish getting json successfully for Subreddit: {subreddit}", subreddit);

            string output = JsonConvert.SerializeObject(json);
            return output;
        }

        private static void CheckUrl(string subreddit)
        {
            if (subreddit == null)
            {
                throw new ArgumentNullException(nameof(subreddit));
            }
        }
    }
}
