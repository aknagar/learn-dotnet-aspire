using Api.Routes.Weather;
using eShopLite.Api.Routes;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers(); 
// Add other layers
builder.AddApplication();
builder.AddInfrastructure();

//Add Keyvault client
builder.AddAzureKeyVaultClient("secrets", settings => settings.DisableHealthChecks = true);

var app = builder.Build();

app.MapDefaultEndpoints();

// https://github.com/varianter/dotnet-template
app.MapWeatherUserGroup()
   .MapWeatherAdminGroup();
app.MapControllers();

app.UseStaticFiles();

app.Run();
