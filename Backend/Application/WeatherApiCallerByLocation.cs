using Backend.Domain;
using Backend.Infrastructure.API;

namespace Backend.Application;

public class WeatherApiCallerByLocation(
    WeatherApiCallerByCoordinates apiCaller,
    LocationToCoordinatesConverter locationToCoordinatesConverter)
{
    public async Task<WeatherDataDto> GetWeatherData(string location, DateTime endDate)
    {
        Coordinates coordinates;
        try
        {
            coordinates = await locationToCoordinatesConverter.GetCoordinatesForLocation(location);
        }
        catch (Exception e)
        {
            throw new Exception("Received Coordinates of API are null. Maybe a invalid City was provided." +
                                "NOTE: Only municipalities in Switzerland are supported");
        }
        var weatherData = await apiCaller.GetWeatherData(coordinates, endDate);
        if (weatherData == null)
        {
            throw new NullReferenceException("Null WeatherData");
        }

        return weatherData;
    }
}