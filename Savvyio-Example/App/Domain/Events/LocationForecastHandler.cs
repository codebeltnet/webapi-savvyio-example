using System.Threading.Tasks;
using Savvyio.Domain;
using Savvyio.Handlers;

namespace Savvyio_Example.App.Domain.Events
{
    public class LocationForecastHandler : DomainEventHandler
    {
        protected override void RegisterDelegates(IFireForgetRegistry<IDomainEvent> handlers)
        {
            handlers.RegisterAsync<LocationForecastInitiated>(_ => Task.CompletedTask);
            handlers.RegisterAsync<TemperatureChanged>(_ => Task.CompletedTask);
            handlers.RegisterAsync<WeatherSymbolChanged>(_ => Task.CompletedTask);
        }
    }
}
