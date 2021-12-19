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

            services.AddScoped<TokenProvider>(); // Aktiverar v�r egna token provider och l�gger sedan till den i _Host
            services.AddScoped<TokenManager>(); // Aktiverer v�r token Refresher
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
                //L�gger till policy s� detta projectet kan anv�nda de
                authorizationOptions.AddPolicy(
                    DreamTeam.IDP.Shared.Policies.Admin,
                    DreamTeam.IDP.Shared.Policies.AdminPolicy());
                authorizationOptions.AddPolicy(
                    DreamTeam.IDP.Shared.Policies.Customer,
                    DreamTeam.IDP.Shared.Policies.CustomerPolicy());

                //Gl�m inte l�gga till i appsettings
            });

            services.AddAuthentication(options =>
            {

                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => //Kopplar ihop v�ra cookies med OpenIdConnect
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; //St�ller in att vi anv�nder cookies f�r inloggning i Blazor
                    options.Authority = "https://localhost:5001"; //Ska vara inst�llt mot v�r IDP (DreamTeam.IDP)
                    options.ClientId = "projectclient"; // Ska matcha en uppsatt client som har tillg�ng till detta projektet
                    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";// ska matcha secret ifr�n IDP
                    options.ResponseType = "code"; //S�tter ig�ng PKCE och s�tter defaults till att middleware sk�ter hanteringen
                    options.Scope.Add("openid"); //L�gger till vilka claims som ska finnas tillg�ngliga
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("admin");
                    options.Scope.Add("customer");
                    options.Scope.Add("offline_access"); //Aktiverar auto refresh f�r tokens
                    options.ClaimActions.MapUniqueJsonKey("admin", "admin"); //Mappar om s� Blazor hittar claimsen
                    options.ClaimActions.MapUniqueJsonKey("customer", "customer");

                    //options.CallbackPath = ""; //S�tter redirect URI f�r detta projektet
                    options.SaveTokens = true; //G�r att middleware sparar Token
                    options.GetClaimsFromUserInfoEndpoint = true; //H�mtar claims ifr�n v�r Endpoint
                    options.TokenValidationParameters.NameClaimType = "email"; //S�tter name till email claimen vilket g�r att vi kan alltid komma �t email via
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
