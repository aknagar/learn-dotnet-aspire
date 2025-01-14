using Application;
using Application.Weather;
using eShopLite.Core.Interfaces;
using eShopLite.Infrastructure.Repositories;
using eShopLite.Infrastructure.WeatherData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShopLite.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder
            .AddInfrastructureConfig();
        
        builder.Services.AddDbContext<WeatherDatabaseContext>();

        builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
        
        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WeatherDatabaseContext>());

        return builder;
    }
    
    public static IHealthChecksBuilder AddInfrastructureHealthChecks(this IHealthChecksBuilder healthChecksBuilder)
    {
        healthChecksBuilder.AddDbContextCheck<WeatherDatabaseContext>();

        return healthChecksBuilder;
    }
}