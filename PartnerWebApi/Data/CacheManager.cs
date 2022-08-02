using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace PartnerWebApi.Data
{
    public class CacheManager<TItem>
    {
        private static readonly TimeSpan TIME_SPAN = new TimeSpan(1, 0, 0);
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions()
        {
            ExpirationScanFrequency = TIME_SPAN
        });

        public TItem GetOrCreate(object key, Func<TItem> createItem)
        {
            if (!_cache.TryGetValue(key, out TItem cacheEntry))
            {
                cacheEntry = createItem();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TIME_SPAN);

                _cache.Set(key, cacheEntry, cacheEntryOptions);
            }
            return cacheEntry;
        }

        public bool Update(object key, TItem data)
        {
            if (!_cache.TryGetValue(key, out TItem cacheEntry))
            {
                return false;
            }
            _cache.Set(key, data);
            return true;
        }

        public void ClearCache()
        {
            _cache.Dispose();
            _cache = new MemoryCache(new MemoryCacheOptions()
            {
                ExpirationScanFrequency = TIME_SPAN
            });
        }
    }
}