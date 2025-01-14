using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopLite.Core.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Forecast?> GetForecastAsync(DateOnly date);
        Task AddForecastAsync(Forecast resultValue);
    }
}
