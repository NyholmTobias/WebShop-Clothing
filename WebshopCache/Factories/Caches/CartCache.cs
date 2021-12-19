using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopCache.Factories.Caches
{
    public class CartCache : ICache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;
        public CartCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<byte[]> GetCacheAsync(Guid guid)
        {
            var cachedListOfUsedKolliIds = await _distributedCache.GetAsync(guid.ToString());
            return cachedListOfUsedKolliIds;
        }
        /// <summary>
        /// Finns overload med HTTPContent eller ByteArray
        /// </summary>
        /// <param name="httpContent"></param>
        /// <returns></returns>

        public async Task SetCacheAsync(byte[] byteArray, Guid guid)
        {

            var options = new DistributedCacheEntryOptions()
           .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
           .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await _distributedCache.SetAsync(guid.ToString(), byteArray, options);

        }



    }
}
