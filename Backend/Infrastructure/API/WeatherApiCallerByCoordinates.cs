using System.Text.Json;
using Backend.Domain;
using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Backend.Infrastructure.API;

public class WeatherApiCallerByCoordinates(IOptions<Settings> settings, HttpClient httpClient)
{
    private readonly string _baseUrl = settings.Value.WeatherApiUrl;
    private readonly string _openMeteoExtension = settings.Value.OpenMeteoExtension;

    /// <summary>
    /// Queries the Open Meteo Api to receive the Weather Data for the given Coordinates and Data.
    /// NOTE: The Response includes also Weather Data of the previous 6 Days. This is needed by the Classifier.
    /// </summary>
    /// <param name="coordinates"></param>
    /// <param name="endDate"></param>
    /// <returns>WeatherData? Object</returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<WeatherData?> GetWeatherData(Coordinates coordinates, DateTime endDate)
    {
        var startDate = endDate.AddDays(-6);
        var url =
            $"{_baseUrl}?latitude={coordinates.Latitude}&longitude={coordinates.Longitude}&start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}{_openMeteoExtension}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<WeatherData>(json,new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        return weatherData;
    }
}