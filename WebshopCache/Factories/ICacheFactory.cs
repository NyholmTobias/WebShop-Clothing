using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopCache.Factories
{
    public interface ICacheFactory
    {
        ICache GetCache(CacheName name);
    }
}
