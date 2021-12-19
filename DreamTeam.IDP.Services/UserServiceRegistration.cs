using DreamTeam.IDP.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Services
{
    public static class UserServiceRegistration
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddIDPServices(configuration);

            return services;
        }


    }
}
