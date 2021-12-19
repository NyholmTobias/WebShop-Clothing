using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopCache.Factories.Caches;

namespace WebshopCache.Factories
{
    public class CacheFactory : ICacheFactory
    {
        private readonly IDistributedCache _distributedCache;
        public CacheFactory(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public ICache GetCache(CacheName name)
        {
            switch (name)
            {
                case CacheName.Carts:
                    return new CartCache(_distributedCache);

                default:
                    throw new ApplicationException(string.Format($"Cache {name} cant be found!"));
            }
        }
    }
}
