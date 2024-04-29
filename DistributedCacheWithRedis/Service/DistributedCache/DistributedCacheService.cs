using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DistributedCacheWithRedis.Service.DistributedCache
{
    public class DistributedCacheService : IDistributedCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null)
        {
            DistributedCacheEntryOptions options = new();

            if (absoluteExpireTime != null)
                options.AbsoluteExpiration = DateTime.UtcNow.Add(absoluteExpireTime.Value);
            if (slidingExpireTime != null)
                options.SlidingExpiration = slidingExpireTime.Value;

            string dataJSON = JsonSerializer.Serialize(data);
            await _distributedCache.SetStringAsync(key, dataJSON, options);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            string dataJSON = await _distributedCache.GetStringAsync(key);
            if (dataJSON == null) return default;
            return JsonSerializer.Deserialize<T>(dataJSON);
        }
    }
}
