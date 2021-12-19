// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using DreamTeam.IDP.Data;
using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace DreamTeam.IDP
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Aktiverar services
            services.AddIDPServices(Configuration);

          


        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }

            InitializeDatabase(app);

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider
                  .GetRequiredService<PersistedGrantDbContext>()
                  .Database.Migrate();

                serviceScope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>()
                    .Database.Migrate();

                var context = serviceScope.ServiceProvider
                    .GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                
                var clients = context.Clients.ToList(); //Lägger till nya Clienter på restart
                foreach (var client in Config.Clients)
                {
                    if (clients.Any(x => x.ClientName == client.ClientName))
                    {
                        var clientToBeDeleted = context.Clients.FirstOrDefault(x => x.ClientName == client.ClientName);
                        context.Clients.Remove(clientToBeDeleted);
                        context.SaveChanges();
                        context.Clients.Add(client.ToEntity());
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Clients.Add(client.ToEntity());
                        context.SaveChanges();
                    }

                }

                var resources = context.IdentityResources.ToList(); //Lägger till nya IdentityResources på restart
                foreach (var resource in Config.IdentityResources)
                {
                    if (!resources.Any(x => x.Name == resource.Name))
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                        context.SaveChanges();
                    }

                }

            }
        }
    }
}