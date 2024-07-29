using Newtonsoft.Json;
using System.Text;

namespace Yomikaze.API.Main.Helpers;

public static class DistributedCacheExtension
{
    public static async Task<T> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<T> valueFactory,
        DistributedCacheEntryOptions? options = default!, ILogger? logger = null, CancellationToken token = new())
    {
        if (cache is NoCache)
        {
            logger?.LogDebug("NoCache is being used. The value will be generated directly.");
            return valueFactory();
        }

        options ??= new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

        byte[]? cachedData = await cache.GetAsync(key, token);
        if (cachedData != null)
        {
            logger?.LogDebug("Cache hit for key {Key}", key);
            T? deserialized = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(cachedData));
            if (deserialized is not null)
            {
                return deserialized;
            }

            logger?.LogWarning("Failed to deserialize the cached data for key {Key}", key);
        }

        T value = valueFactory();
        if (value is null)
        {
            logger?.LogWarning("The value factory returned null for key {Key}", key);
            return default!;
        }

        // set cache without waiting
        logger?.LogDebug("Setting the cache for key {Key}", key);
        _ = cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options, token)
            .ContinueWith(t => logger?.LogWarning(t.Exception, "Failed to set the cache for key {Key}", key),
                TaskContinuationOptions.OnlyOnFaulted);
        return value;
    }

    public static T GetOrSet<T>(this IDistributedCache cache, string key, Func<T> valueFactory,
        DistributedCacheEntryOptions? options = default!, ILogger? logger = null)
    {
        if (cache is NoCache)
        {
            logger?.LogDebug("NoCache is being used. The value will be generated directly.");
            return valueFactory();
        }

        options ??= new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

        byte[]? cachedData = cache.Get(key);
        if (cachedData != null)
        {
            logger?.LogDebug("Cache hit for key {Key}", key);
            T? deserialized = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(cachedData));
            if (deserialized is not null)
            {
                return deserialized;
            }

            logger?.LogWarning("Failed to deserialize the cached data for key {Key}", key);
        }

        T value = valueFactory();
        if (value is null)
        {
            logger?.LogWarning("The value factory returned null for key {Key}", key);
            return default!;
        }

        // set cache without waiting    
        logger?.LogDebug("Setting the cache for key {Key}", key);
        _ = cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options)
            .ContinueWith(t => logger?.LogWarning(t.Exception, "Failed to set the cache for key {Key}", key),
                TaskContinuationOptions.OnlyOnFaulted);
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