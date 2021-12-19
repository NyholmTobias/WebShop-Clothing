using DreamTeam.IDP.Data;
using DreamTeam.IDP.Repository;
using DreamTeam.IDP.Shared.Models;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using System.Reflection;

namespace DreamTeam.IDP
{
    public static class IDPServiceRegistration
    {
        public static IServiceCollection AddIDPServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddScoped<IUserRepository, UserRepository>();

            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>() //lägger till vilken users som används, ApplicationUser
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>();


            var migrationsAssembly = typeof(Startup)
                .GetTypeInfo().Assembly.GetName().Name;

            builder.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),  // this adds the config data from DB (clients, resources)
                    options => options.MigrationsAssembly(migrationsAssembly));
            });

            builder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), // this adds the operational data from DB (codes, tokens, consents)
                    options => options.MigrationsAssembly(migrationsAssembly));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 30;
            });





            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to https://localhost:5001/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });
            return services;
        }
    }
}
