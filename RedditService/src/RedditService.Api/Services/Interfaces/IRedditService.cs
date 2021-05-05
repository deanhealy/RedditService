using System;
using System.Threading.Tasks;

namespace RedditService.Api.Services.Interfaces
{
    public interface IRedditService
    {
        Task<String> GetSubrettitDataAsync(string subreddit);
    }
}
