using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cuemon.Extensions;
using Cuemon.Threading;
using Dapper;
using Savvyio.Extensions.Dapper;

namespace Savvyio_Example.App.Data
{
    public class WeatherForecastDataStore : DapperDataStore<WeatherForecast, DapperQueryOptions>
    {
        public WeatherForecastDataStore(IDapperDataSource source) : base(source)
        {
        }

        public override Task CreateAsync(WeatherForecast dto, Action<AsyncOptions> setup = null)
        {
            return Source.ExecuteAsync("INSERT INTO WF (Date, TemperatureC, Summary) VALUES (@Date, @TemperatureC, @Summary)", dto);
        }

        public override Task UpdateAsync(WeatherForecast dto, Action<AsyncOptions> setup = null)
        {
            throw new NotImplementedException();
        }

        public override Task<WeatherForecast> GetByIdAsync(object id, Action<AsyncOptions> setup = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<WeatherForecast>> FindAllAsync(Action<DapperQueryOptions> setup = null)
        {
            return setup == null ? Source.QueryAsync<WeatherForecast>("SELECT * FROM WF") : Source.QueryAsync<WeatherForecast>(setup.Configure());
        }

        public override Task DeleteAsync(WeatherForecast dto, Action<AsyncOptions> setup = null)
        {
            throw new NotImplementedException();
        }
    }
}
