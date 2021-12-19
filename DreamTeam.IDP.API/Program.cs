
using DreamTeam.IDP.API;
using DreamTeam.IDP.Services;
using IdentityServer4.AccessTokenValidation;

AddSwaggerConfig addSwaggerConfig = new AddSwaggerConfig();
var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();  

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUserServices(configuration);

addSwaggerConfig.AddSwagger(builder.Services);

//Ställer in vilken server Authenticationen ska ske emot
builder.Services.AddAuthentication(
    IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(Options =>
    {
        Options.Authority = "https://localhost:5001"; //Ska peka på IDP
                });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


