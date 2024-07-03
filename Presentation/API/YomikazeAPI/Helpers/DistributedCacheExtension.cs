using Newtonsoft.Json;
using System.Text;

namespace Yomikaze.API.Main.Helpers;

public static class DistributedCacheExtension
{
    public static async Task<TC> GetOrSetAsync<TC>(this IDistributedCache cache, string key, Func<TC> valueFactory,
        DistributedCacheEntryOptions? options = default!, CancellationToken token = new(), ILogger? logger = null)
    {
        if (cache is NoCache)
        {
            logger?.LogDebug("NoCache is being used. The value will be generated directly.");
            return valueFactory();
        }
        options ??= new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5), AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) };
        
        byte[]? cachedData = await cache.GetAsync(key, token);
        if (cachedData != null)
        {
            logger?.LogDebug("Cache hit for key {Key}", key);
            TC? deserialized = JsonConvert.DeserializeObject<TC>(Encoding.UTF8.GetString(cachedData));
            if (deserialized is not null)
            {
                return deserialized;
            }
            logger?.LogWarning("Failed to deserialize the cached data for key {Key}", key);
        }   

        var value = valueFactory();
        if (value is null)
        {
            logger?.LogWarning("The value factory returned null for key {Key}", key);
            return default!;
        }
        logger?.LogDebug("Setting the cache for key {Key}", key);
        await cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options, token);
        return value;
    }
    
    public static TC GetOrSet<TC>(this IDistributedCache cache, string key, Func<TC> valueFactory,
        DistributedCacheEntryOptions? options = default!, ILogger? logger = null)
    {
        if (cache is NoCache)
        {
            logger?.LogDebug("NoCache is being used. The value will be generated directly.");
            return valueFactory();
        }
        options ??= new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5), AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) };
        
        byte[]? cachedData = cache.Get(key);
        if (cachedData != null)
        {
            logger?.LogDebug("Cache hit for key {Key}", key);
            TC? deserialized = JsonConvert.DeserializeObject<TC>(Encoding.UTF8.GetString(cachedData));
            if (deserialized is not null)
            {
                return deserialized;
            }
            logger?.LogWarning("Failed to deserialize the cached data for key {Key}", key);
        }   

        var value = valueFactory();
        if (value is null)
        {
            logger?.LogWarning("The value factory returned null for key {Key}", key);
            return default!;
        }
        logger?.LogDebug("Setting the cache for key {Key}", key);
        cache.Set(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options);
        return value;
    }
    
    
}

internal class NoCache : IDistributedCache
{
    private static NoCache? _instance;
    private static readonly object Lock = new();
    
    public static NoCache Instance
    {
        get
        {
            lock (Lock)
            {
                return _instance ??= new NoCache();
            }
        }
    }
    public byte[]? Get(string key)
    {
        return null;
    }

    public Task<byte[]?> GetAsync(string key, CancellationToken token = new())
    {
        return Task.FromResult<byte[]?>(null);
    }

    public void Refresh(string key)
    {
        // do nothing
    }

    public Task RefreshAsync(string key, CancellationToken token = new())
    {
        return Task.CompletedTask;
    }

    public void Remove(string key)
    {
        // do nothing
    }

    public Task RemoveAsync(string key, CancellationToken token = new())
    {
        return Task.CompletedTask;
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        // do nothing
    }

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options,
        CancellationToken token = new())
    {
        return Task.CompletedTask;
    }
}