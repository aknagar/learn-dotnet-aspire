using Application;
using eShopLite.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class WeatherDatabaseContext(IOptions<InfrastructureConfig> config) : DbContext, IUnitOfWork
{
    public DbSet<Forecast> Forecasts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(config.Value.ConnectionString)
            .EnableSensitiveDataLogging(config.Value.EnableSensitiveDataLogging);
    }
}
