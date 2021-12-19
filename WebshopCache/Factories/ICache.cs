using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopCache.Factories
{
    public interface ICache
    {
        /// <summary>
        /// Hämtar Cache infon om den inte är tom
        /// </summary>
        /// <returns></returns>
        Task<byte[]> GetCacheAsync(Guid guid);

        /// <summary>
        /// Sparar i cachen.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        Task SetCacheAsync(byte[] byteArray, Guid guid);
    }
}
