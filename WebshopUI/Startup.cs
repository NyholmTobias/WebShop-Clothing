using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopUI.Services.Items;
using WebshopUI.Services.Tokens;
using WebshopUI.Services.Users;
using WebshopUI.Services.Categories;
using TabBlazor.Services;
using WebshopCache;
using WebshopUI.Services.Carts;
using WebshopUI.Services.Orders;
using TabBlazor;

namespace WebshopUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddTabler();
            services.AddServerSideBlazor();

            services.AddScoped<TokenProvider>(); // Aktiverar vår egna token provider och lägger sedan till den i _Host
            services.AddScoped<TokenManager>(); // Aktiverer vår token Refresher
            services.AddScoped<ToastService>(); // Aktiverar Tabblazor ToastService
            services.AddScoped<ICartDataService, CartDataService>();
            services.AddCacheServices(Configuration);

            services.AddHttpClient<IItemDataService, ItemDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/");
            });
            services.AddHttpClient<IOrderDataService, OrderDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/");
            });
            services.AddHttpClient<IUserDataService, UserDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7212/");
            });

            services.AddHttpClient<ICategoriesDataService, CategoriesDataService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44334/");
            });

            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new MappingProfileUI());
            });
            
            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddAuthorizationCore(authorizationOptions =>
            {
                //Lägger till policy så detta projectet kan använda de
                authorizationOptions.AddPolicy(
                    DreamTeam.IDP.Shared.Policies.Admin,
                    DreamTeam.IDP.Shared.Policies.AdminPolicy());
                authorizationOptions.AddPolicy(
                    DreamTeam.IDP.Shared.Policies.Customer,
                    DreamTeam.IDP.Shared.Policies.CustomerPolicy());

                //Glöm inte lägga till i appsettings
            });

            services.AddAuthentication(options =>
            {

                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => //Kopplar ihop våra cookies med OpenIdConnect
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; //Ställer in att vi använder cookies för inloggning i Blazor
                    options.Authority = "https://localhost:5001"; //Ska vara inställt mot vår IDP (DreamTeam.IDP)
                    options.ClientId = "projectclient"; // Ska matcha en uppsatt client som har tillgång till detta projektet
                    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";// ska matcha secret ifrån IDP
                    options.ResponseType = "code"; //Sätter igång PKCE och sätter defaults till att middleware sköter hanteringen
                    options.Scope.Add("openid"); //Lägger till vilka claims som ska finnas tillgängliga
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("admin");
                    options.Scope.Add("customer");
                    options.Scope.Add("offline_access"); //Aktiverar auto refresh för tokens
                    options.ClaimActions.MapUniqueJsonKey("admin", "admin"); //Mappar om så Blazor hittar claimsen
                    options.ClaimActions.MapUniqueJsonKey("customer", "customer");

                    //options.CallbackPath = ""; //Sätter redirect URI för detta projektet
                    options.SaveTokens = true; //Gör att middleware sparar Token
                    options.GetClaimsFromUserInfoEndpoint = true; //Hämtar claims ifrån vår Endpoint
                    options.TokenValidationParameters.NameClaimType = "email"; //Sätter name till email claimen vilket gör att vi kan alltid komma åt email via
                                                                               // EXEMPEL
                                                                               //var identity = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                                                                               //var user = await UserDataService.GetUserByEmail(identity.User.Identity.Name);


                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
