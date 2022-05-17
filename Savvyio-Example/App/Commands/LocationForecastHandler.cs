using System;
using System.Threading;
using System.Threading.Tasks;
using Savvyio.Commands;
using Savvyio.Domain;
using Savvyio.Domain.EventSourcing;
using Savvyio.Extensions;
using Savvyio.Handlers;
using Savvyio_Example.App.Domain;
using Savvyio_Example.App.Events;

namespace Savvyio_Example.App.Commands
{
    public class LocationForecastHandler : CommandHandler
    {
        private readonly ITracedAggregateRepository<LocationForecast, Guid> _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;

        public LocationForecastHandler(IMediator mediator, ITracedAggregateRepository<LocationForecast, Guid> repository, IUnitOfWork uow)
        {
            _mediator = mediator;
            _repository = repository;
            _uow = uow;
        }

        protected override void RegisterDelegates(IFireForgetRegistry<ICommand> handlers)
        {
            handlers.RegisterAsync<CreateLocationForecast>(CreateLocationForecastAsync);
            handlers.RegisterAsync<ChangeLocationForecast>(UpdateLocationForecastAsync);
        }

        private async Task UpdateLocationForecastAsync(ChangeLocationForecast c, CancellationToken stoppingToken)
        {
            var lf = await _repository.GetByIdAsync(c.Id);
            lf.ChangeTemperature(lf.Temperature + c.Temperature);
            lf.ChangeWeatherSymbol(c.WeatherSymbol);
            _repository.Add(lf);
            await _uow.SaveChangesAsync(o => o.CancellationToken = stoppingToken);
            await _mediator.PublishAsync(new LocationForecastCreated(lf.Id, lf.Time, lf.WeatherSymbol, lf.Temperature));
        }

        private async Task CreateLocationForecastAsync(CreateLocationForecast c, CancellationToken stoppingToken)
        {
            var lf = new LocationForecast(c.Id, c.Time, c.WeatherSymbol, c.Temperature);
            _repository.Add(lf);
            await _uow.SaveChangesAsync(o => o.CancellationToken = stoppingToken);
            await _mediator.PublishAsync(new LocationForecastCreated(lf.Id, lf.Time, lf.WeatherSymbol, lf.Temperature));
        }
    }
}
