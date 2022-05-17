using Savvyio.Domain.EventSourcing;

namespace Savvyio_Example.App.Domain.Events;

public class WeatherSymbolChanged : TracedDomainEvent
{
    public WeatherSymbolChanged(WeatherSymbol ws)
    {
        WeatherSymbol = ws;
    }

    public WeatherSymbol WeatherSymbol { get; }
}