using System;
using System.Globalization;
using Cuemon;
using Cuemon.Extensions.Collections.Generic;
using Savvyio.Commands;
using Savvyio_Example.App.Domain;

namespace Savvyio_Example.App.Commands;

public class CreateLocationForecast : Command
{
    public CreateLocationForecast()
    {
        Id = new WeatherId(DateTime.UtcNow);
        Time = DateTime.UtcNow;
        WeatherSymbol = WeatherSymbol.All.RandomOrDefault();
        Temperature = Convert.ToDouble($"{Generate.RandomNumber(-5, 41)}.{Generate.RandomNumber(0, 9)}", CultureInfo.InvariantCulture);
    }

    public Guid Id { get; }

    public WeatherSymbol WeatherSymbol { get; }

    public double Temperature { get; }

    public DateTime Time { get; }
}