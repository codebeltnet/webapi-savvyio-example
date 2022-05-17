using Savvyio.Domain.EventSourcing;

namespace Savvyio_Example.App.Domain.Events;

public class TemperatureChanged : TracedDomainEvent
{
    public TemperatureChanged(double temperature)
    {
        Temperature = temperature;
    }

    public double Temperature { get; }
}