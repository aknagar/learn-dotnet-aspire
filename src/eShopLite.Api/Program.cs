using Api.Routes.Weather;
using eShopLite.Api.Routes;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add other layers
builder.AddApplication();
builder.AddInfrastructure();

var app = builder.Build();

app.MapDefaultEndpoints();

// https://github.com/varianter/dotnet-template
app.MapWeatherUserGroup()
   .MapWeatherAdminGroup();

app.UseStaticFiles();

app.Run();
