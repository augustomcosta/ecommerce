namespace ecommerce_api.Domain.Services.Interfaces;

public interface IResponseCacheService
{
    Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

    Task<string> GetCachedResponseAsync(string cacheKey);
}