using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedditService.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedditService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedditController : ControllerBase
    {
        private readonly ILogger<RedditController> _logger;
        private readonly IRedditService _redditService;
        public RedditController(ILogger<RedditController> logger, IRedditService redditService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redditService = redditService ?? throw new ArgumentNullException(nameof(redditService));
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAsync([Required] string subreddit)
        {
            _logger.LogInformation("Start processing GetAsync request for Subreddit: {subreddit}", subreddit);
            var data = await _redditService.GetSubrettitDataAsync(subreddit);
            _logger.LogInformation("Finish processing GetAsync request for Subreddit: {subreddit}", subreddit);

            return data;
        }
    }
}
