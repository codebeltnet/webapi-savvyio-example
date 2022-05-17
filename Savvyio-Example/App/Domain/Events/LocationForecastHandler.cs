using System.Threading.Tasks;
using Savvyio.Domain;
using Savvyio.Handlers;

namespace Savvyio_Example.App.Domain.Events
{
    public class LocationForecastHandler : DomainEventHandler
    {
        protected override void RegisterDelegates(IFireForgetRegistry<IDomainEvent> handlers)
        {
            handlers.RegisterAsync<LocationForecastInitiated>(OnLocationForecastInitiated);
            handlers.RegisterAsync<TemperatureChanged>(OnTemperatureChanged);
            handlers.RegisterAsync<WeatherSymbolChanged>(OnWeatherSymbolChanged);
        }

        private Task OnWeatherSymbolChanged(WeatherSymbolChanged e)
        {
            return Task.CompletedTask;
        }

        private Task OnTemperatureChanged(TemperatureChanged e)
        {
            return Task.CompletedTask;
        }

        private Task OnLocationForecastInitiated(LocationForecastInitiated e)
        {
            return Task.CompletedTask;
        }
    }
}
