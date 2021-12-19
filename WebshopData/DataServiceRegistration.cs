using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebshopData
{
    public static class DataServiceRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebshopDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("WebshopConnectionString"),
                b => b.MigrationsAssembly(typeof(WebshopDbContext).Assembly.FullName)));

            return services;
        }
    }
}
