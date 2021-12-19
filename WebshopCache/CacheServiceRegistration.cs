using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopCache.Factories;
using WebshopCache.Services;

namespace WebshopCache
{
    public static class CacheServiceRegistration
    {


        public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICacheService, CacheService>();

            services.AddScoped<ICacheFactory, CacheFactory>();

            services.AddDistributedMemoryCache();

            return services;
        }
    }
}
