using Backend.Domain;
using Backend.Infrastructure.API;

namespace Backend.Application;

/// <summary>
/// Wrapper for the WeatherApiCallerByCoordinates
/// </summary>
/// <param name="apiCaller"></param>
/// <param name="locationToCoordinatesConverter"></param>

public class WeatherApiCallerByLocation(
    WeatherApiCallerByCoordinates apiCaller,
    LocationToCoordinatesConverter locationToCoordinatesConverter)
{
    /// <summary>
    /// Transforms the Location to coordinates with the LocationToCoordinatesConverter and then calls the
    /// WeatherApiCallerByCoordinates to get the WeatherData.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="endDate"></param>
    /// <returns>WeatherData object containing the Weather data of the data including the previous 6 days.</returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<WeatherData> GetWeatherData(string location, DateTime endDate)
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