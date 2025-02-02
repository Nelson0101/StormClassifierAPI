using Backend.Domain;
using Backend.Infrastructure.API;

namespace Backend.Application;

public class WeatherApiCallerByLocation(
    WeatherApiCallerByCoordinates apiCaller,
    LocationToCoordinatesConverter locationToCoordinatesConverter)
{
    public async Task<WeatherData> GetWeatherData(string location, DateTime endDate)
    {
        var coordinates = await locationToCoordinatesConverter.GetCoordinatesForLocation(location);
        if (coordinates == null)
        {
            throw new NullReferenceException("Null Coordinates");
        }
        var weatherData = await apiCaller.GetWeatherData(coordinates, endDate);
        if (weatherData == null)
        {
            throw new NullReferenceException("Null WeatherData");
        }

        return weatherData;
    }
}