using System;
using Savvyio.EventDriven;
using Savvyio_Example.App.Domain;

namespace Savvyio_Example.App.Events;

public class LocationForecastCreated : IntegrationEvent
{
    public LocationForecastCreated(Guid id, DateTime time, WeatherSymbol ws, double temperature)
    {
        Id = id;
        Time = time;
        Temperature = temperature;
        WeatherCode = ws.Code;
        WeatherDescription = ws.Description;
        WeatherIcon = ws.Icon;
    }

    public Guid Id { get; }

    public DateTime Time { get; }

    public string WeatherCode { get; }

    public string WeatherDescription { get; }

    public string WeatherIcon { get; }

    public double Temperature { get; }
}