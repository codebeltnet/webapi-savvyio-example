using System;
using System.Collections.Generic;
using Savvyio.Domain.EventSourcing;
using Savvyio.Handlers;
using Savvyio_Example.App.Domain.Events;

namespace Savvyio_Example.App.Domain
{
    public class LocationForecast : TracedAggregateRoot<Guid>
    {
        LocationForecast(Guid id, IEnumerable<ITracedDomainEvent> events) : base(new WeatherId(id), events)
        {
        }

        public LocationForecast(WeatherId id, IEnumerable<ITracedDomainEvent> events) : base(id, events)
        {
        }

        public LocationForecast(WeatherId id, DateTime time, WeatherSymbol weatherSymbol, double temperature)
        {
            Time = time;
            WeatherSymbol = weatherSymbol;
            Temperature = temperature;
            AddEvent(new LocationForecastInitiated(id, time, weatherSymbol, temperature));
        }

        protected override void RegisterDelegates(IFireForgetRegistry<ITracedDomainEvent> handler)
        {
            handler.Register<LocationForecastInitiated>(OnInitiated);
            handler.Register<WeatherSymbolChanged>(OnWeatherSymbolChanged);
            handler.Register<TemperatureChanged>(OnTemperatureChanged);
        }

        private void OnTemperatureChanged(TemperatureChanged e)
        {
            Temperature = e.Temperature;
        }

        private void OnWeatherSymbolChanged(WeatherSymbolChanged e)
        {
            WeatherSymbol = e.WeatherSymbol;
        }

        private void OnInitiated(LocationForecastInitiated e)
        {
            Id = e.Id;
            Time = e.Time;
            WeatherSymbol = e.WeatherSymbol;
            Temperature = e.Temperature;
        }

        public DateTime Time { get; private set; }

        public WeatherSymbol WeatherSymbol { get; private set; }

        public double Temperature { get; private set; }

        public void ChangeTemperature(double temperature)
        {
            if (temperature.Equals(Temperature)) { return; }
            AddEvent(new TemperatureChanged(temperature));
        }

        public void ChangeWeatherSymbol(WeatherSymbol ws)
        {
            if (ws.Equals(WeatherSymbol)) { return; }
            AddEvent(new WeatherSymbolChanged(ws));
        }
    }
}
