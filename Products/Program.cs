using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
//Add Keyvault client
builder.AddAzureKeyVaultClient("secrets", settings => settings.DisableHealthChecks = true);
builder.Services.AddDbContext<ProductDataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductsContext") ?? throw new InvalidOperationException("Connection string 'ProductsContext' not found.")));

// Add services to the container.
var app = builder.Build();

app.MapDefaultEndpoints();

var secretClient = app.Services.GetService<SecretClient>();

// Configure the HTTP request pipeline.
app.MapProductEndpoints(secretClient);

app.UseStaticFiles();

app.CreateDbIfNotExists();

app.Run();
