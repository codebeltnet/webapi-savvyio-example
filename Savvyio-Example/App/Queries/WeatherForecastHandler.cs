using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Savvyio.Data;
using Savvyio.Extensions.Dapper;
using Savvyio.Handlers;
using Savvyio.Queries;

namespace Savvyio_Example.App.Queries
{
    public class WeatherForecastHandler : QueryHandler
    {
        private readonly IReadableDataAccessObject<WeatherForecast, DapperOptions> _dao;

        public WeatherForecastHandler(IReadableDataAccessObject<WeatherForecast, DapperOptions> dao)
        {
            _dao = dao;
        }

        protected override void RegisterDelegates(IRequestReplyRegistry<IQuery> handlers)
        {
            handlers.RegisterAsync<GetWeatherForecast, IEnumerable<WeatherForecast>>(GetWeatherForecastAsync);
        }

        private async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync(GetWeatherForecast q, CancellationToken stoppingToken)
        {
            var dao = await _dao.ReadAllAsync(null, o =>
            {
                o.CommandText = "SELECT * FROM WF";
            });
            return dao;
        }
    }

    public class GetWeatherForecast : Query<IEnumerable<WeatherForecast>>
    {
        public GetWeatherForecast()
        {
        }
    }
}
