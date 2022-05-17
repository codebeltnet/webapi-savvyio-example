using System.Threading;
using System.Threading.Tasks;
using Savvyio.Data;
using Savvyio.EventDriven;
using Savvyio.Extensions.Dapper;
using Savvyio.Handlers;

namespace Savvyio_Example.App.Events
{
    public class LocationForecastHandler : IntegrationEventHandler
    {
        private readonly IPersistentDataAccessObject<WeatherForecast, DapperOptions> _dao;

        public LocationForecastHandler(IPersistentDataAccessObject<WeatherForecast, DapperOptions> dao)
        {
            _dao = dao;
        }

        protected override void RegisterDelegates(IFireForgetRegistry<IIntegrationEvent> handlers)
        {
            handlers.RegisterAsync<LocationForecastCreated>(OnLocationForecastCreated);
        }

        private async Task OnLocationForecastCreated(LocationForecastCreated ie, CancellationToken stoppingToken)
        {
            await _dao.CreateAsync(new WeatherForecast()
            {
                Date = ie.Time,
                Summary = $"{ie.WeatherDescription} {ie.WeatherIcon}",
                TemperatureC = (int)ie.Temperature
            }, o => o.CommandText = "INSERT INTO WF (Date, TemperatureC, Summary) VALUES (@Date, @TemperatureC, @Summary)");
        }
    }
}
