namespace DistributedCacheWithRedis.Service.DistributedCache
{
    public interface IDistributedCacheService
    {
        Task SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null);
        Task<T> GetAsync<T>(string key);
    }
}
