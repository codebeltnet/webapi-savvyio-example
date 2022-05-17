using Savvyio.Domain;

namespace Savvyio_Example.App.Domain
{
    public class WeatherSymbol : ValueObject
    {
        public static readonly WeatherSymbol[] All = new[] { ClearSky, Cloudy, Fog, HeavyRain, HeavyRainAndThunder, HeavySnow, PartlyCloudy };

        public static WeatherSymbol ClearSky => new("clearsky", "Clear sky", "☀");

        public static WeatherSymbol Cloudy => new("cloudy", "Cloudy", "☁");

        public static WeatherSymbol Fog => new("fog", "Fog", "🌫");

        public static WeatherSymbol HeavyRain => new("heavyrain", "Heavy rain", "🌧");

        public static WeatherSymbol HeavyRainAndThunder => new("heavyrainandthunder", "Heavy rain and thunder", "⛈");

        public static WeatherSymbol HeavySnow => new("heavysnow", "Heavy snow", "🌨");

        public static WeatherSymbol PartlyCloudy => new("partlycloudy", "Partly cloudy", "⛅");


        public WeatherSymbol(string code, string description, string icon)
        {
            Code = code;
            Description = description;
            Icon = icon;
        }

        public string Code { get; }

        public string Description { get; }

        public string Icon { get; }

        public override string ToString()
        {
            return Description;
        }
    }
}
