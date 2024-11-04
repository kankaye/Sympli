using Microsoft.Extensions.Caching.Memory;
using Sympli.Application.Abstractions;

namespace Sympli.Infrastructure.Caches;

public class AppMemoryCache : ICacheManager
{
    private readonly IMemoryCache _cache;

    public AppMemoryCache()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    public T? Get<T>(string key)
    {
        if(_cache.TryGetValue(key, out T? value))
        {
            return value;
        }

        return default;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        _cache.Set(key, value, expiration);
    }
}
