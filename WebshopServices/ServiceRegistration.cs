using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebshopData;
using WebshopData.Repositories;
using WebshopServices.Features.CategoryServices;
using WebshopServices.Features.Interfaces;
using WebshopServices.Features.ItemServices;
using WebshopServices.Features.LineItemServices;
using WebshopServices.Features.OrderServices;
using WebshopServices.Features.SeedServices;
using WebshopServices.Validation;
using WebshopServices.Validation.Interfaces;
using WebshopShared.IRepository;

namespace WebshopServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            //Order service
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            //Item service
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemService, ItemService>();

            //LineItem service
            services.AddScoped<ILineItemRepository, LineItemRepository>();
            services.AddScoped<ILineItemService, LineItemService>();

            //Category service
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //ValidationFactory
            services.AddScoped<IValidatorFactory, ValidatorFactory>();

            //CategoryService
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //SeedService
            services.AddScoped<ISeedService, SeedService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDataServices(configuration);

            return services;
        }
    }
}
