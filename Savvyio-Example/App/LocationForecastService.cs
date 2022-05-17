using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Savvyio.Domain.EventSourcing;
using Savvyio.Extensions;
using Savvyio_Example.App.Commands;
using Savvyio_Example.App.Domain;

namespace Savvyio_Example.App
{
    public class LocationForecastService : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ITracedAggregateRepository<LocationForecast, Guid> _repository;

        public LocationForecastService(IServiceScopeFactory scopeFactory)
        {
            var scope = scopeFactory.CreateScope();

            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            _repository = scope.ServiceProvider.GetRequiredService<ITracedAggregateRepository<LocationForecast, Guid>>();

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var id = new WeatherId(DateTime.UtcNow);
                var lf = await _repository.GetByIdAsync(id, o => o.CancellationToken = stoppingToken);
                if (lf.Version == 0)
                {
                    await _mediator.CommitAsync(new CreateLocationForecast(), o => o.CancellationToken = stoppingToken);
                }
                else
                {
                    await _mediator.CommitAsync(new ChangeLocationForecast(id), o => o.CancellationToken = stoppingToken);
                }
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
