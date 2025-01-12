using Api.Routes.Weather;
using eShopLite.Api.Routes;
using Application;
using Infrastructure;
using Microsoft.Extensions.Azure;
using eShopLite.Api.Routes.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers(); 
// Add other layers
builder.AddApplication();
builder.AddInfrastructure();

//Add Keyvault client
builder.AddAzureKeyVaultClient("secrets", settings => settings.DisableHealthChecks = true);

// Add Service Bus client
builder.AddAzureServiceBusClient("serviceBus");

var app = builder.Build();

app.MapDefaultEndpoints();

// https://github.com/varianter/dotnet-template
app.MapWeatherUserGroup()
   .MapWeatherAdminGroup();

app.MapNotify();

app.MapControllers();

app.UseStaticFiles();

app.Run();
