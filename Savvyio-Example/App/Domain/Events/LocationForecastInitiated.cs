using System;
using Savvyio.Domain.EventSourcing;

namespace Savvyio_Example.App.Domain.Events;

public class LocationForecastInitiated : TracedDomainEvent
{
    public LocationForecastInitiated(Guid id, DateTime time, WeatherSymbol weatherSymbol, double temperature)
    {
        Id = id;
        WeatherSymbol = weatherSymbol;
        Time = time;
        Temperature = temperature;
    }

    public Guid Id { get; }

    public DateTime Time { get; }

    public WeatherSymbol WeatherSymbol { get; }

    public double Temperature { get; }
}