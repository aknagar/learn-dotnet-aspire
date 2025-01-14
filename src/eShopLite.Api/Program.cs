using Api.Routes.Weather;
using eShopLite.Api.Routes;
using Microsoft.Extensions.Azure;
using eShopLite.Api.Routes.Orders;
using Scalar.AspNetCore;
using Application;
using Infrastructure;
using eShopLite.Api;
using Microsoft.EntityFrameworkCore;
using eShopLite.Api.Endpoints;
using Azure.Security.KeyVault.Secrets;
using Dapr.Workflow;
using eShopLite.Api.Workflow;
using eShopLite.Api.Activities;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();  // OpenAPI = swagger

builder.Services.AddControllers();

// Add Dapr Workflow
builder.Services.AddDaprWorkflow(options =>
{  
    options.RegisterWorkflow<OrderProcessingWorkflow>();
        
    // These are the activities that get invoked by the workflow(s).
    options.RegisterActivity<NotifyActivity>();
    options.RegisterActivity<ReserveInventoryActivity>();
    options.RegisterActivity<ProcessPaymentActivity>();
    options.RegisterActivity<UpdateInventoryActivity>();

});


// Add other layers
builder.AddApplication();
builder.AddInfrastructure();

//Add Keyvault client
builder.AddAzureKeyVaultClient("secrets", settings => settings.DisableHealthChecks = true);

// Add Service Bus client
builder.AddAzureServiceBusClient("serviceBus");



builder.Services.AddDbContext<ProductDataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductsContext") ?? throw new InvalidOperationException("Connection string 'ProductsContext' not found.")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); //publish endpoint at /openapi/v1.json
    app.MapScalarApiReference(); // similar to swagger UI at /scalar/v1
};

app.MapDefaultEndpoints();

var secretClient = app.Services.GetService<SecretClient>();

// This is a plug and play mechanism where we are plugging /product endpoints
app.MapProductEndpoints(secretClient);

// https://github.com/varianter/dotnet-template
app.MapWeatherUserGroup()
   .MapWeatherAdminGroup();

app.MapNotify();

app.MapControllers();

app.UseStaticFiles();

app.Run();
