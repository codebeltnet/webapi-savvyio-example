using System;
using Cuemon;
using Cuemon.Extensions.Collections.Generic;
using Savvyio.Commands;
using Savvyio_Example.App.Domain;

namespace Savvyio_Example.App.Commands;

public class ChangeLocationForecast : Command
{
    public ChangeLocationForecast(WeatherId id)
    {
        Id = id;
        WeatherSymbol = WeatherSymbol.All.RandomOrDefault();
        Temperature = Generate.RandomNumber(-5, 5);
    }

    public Guid Id { get; }

    public WeatherSymbol WeatherSymbol { get; }

    public double Temperature { get; }
}