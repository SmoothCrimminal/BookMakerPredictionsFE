using Microsoft.Extensions.Caching.Distributed;
using System.Collections;
using System.Text.Json;

namespace BookMakerPredictionsFE.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<DistributedCacheEntryOptions, Task<T>> factory)
        {
            var cached = await cache.GetStringAsync(key);
            if (!string.IsNullOrWhiteSpace(cached))
            {
                return JsonSerializer.Deserialize<T>(cached);
            }

            var options = new DistributedCacheEntryOptions();
            var result = await factory(options);

            if (result is not null)
            {
                if (result is IEnumerable enumerable && !enumerable.Cast<object>().Any())
                    return result;

                var serialized = JsonSerializer.Serialize(result);
                await cache.SetStringAsync(key, serialized, options);
            }

            return result;
        }
    }
}
