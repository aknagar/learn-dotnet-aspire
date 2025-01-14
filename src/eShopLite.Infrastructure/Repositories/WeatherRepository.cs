using Application.Weather;
using eShopLite.Core;
using eShopLite.Core.Interfaces;
using eShopLite.Infrastructure.WeatherData;
using Microsoft.EntityFrameworkCore;

namespace eShopLite.Infrastructure.Repositories;

public class WeatherRepository(WeatherDatabaseContext context) : IWeatherRepository
{
    public async Task<Forecast?> GetForecastAsync(DateOnly date)
    {
        return await context.Forecasts
            .Where(f => !f.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public async Task AddForecastAsync(Forecast forecast)
    {
        await context.Forecasts.AddAsync(forecast);
    }
}