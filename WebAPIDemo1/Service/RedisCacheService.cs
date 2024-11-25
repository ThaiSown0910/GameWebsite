using StackExchange.Redis;
namespace WebAPIDemo1.Service
{
    public interface IRedisCacheService
    {
        Task SetCacheAsync(string key, string value, TimeSpan? expiration = null);
        Task<string?> GetCacheAsync(string key);
    }

    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task SetCacheAsync(string key, string value, TimeSpan? expiration = null)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(key, value, expiration);
        }

        public async Task<string?> GetCacheAsync(string key)
        {
            var db = _redis.GetDatabase();
            return await db.StringGetAsync(key);
        }
    }
}
