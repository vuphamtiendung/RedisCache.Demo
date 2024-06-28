using Microsoft.Extensions.Caching.Distributed;
using RedisCache.Demo02.Model;
using System.Data;
using System.Text.Json;

namespace RedisCache.Demo02.Services
{
    public class RedisCachingService
    {
        private readonly IDistributedCache _cache;

        public RedisCachingService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T GetCacheData<T>(string key)
        {
            var jsonData = _cache.GetString(key);
            if (jsonData == null) return default(T);
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public void SetCacheData<T>(string key, T value, TimeSpan cacheDuration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration,
                SlidingExpiration = cacheDuration
            };
            var jsonData = JsonSerializer.Serialize(value);
            _cache.SetString(key, jsonData, options);
        }

        public void RemoveCacheData(string key)
        {
            _cache.Remove(key);
        }
    }
}
