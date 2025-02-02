using System.Text.Json;
using Backend.Domain;
using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Backend.Infrastructure.API;

public class WeatherApiCallerByCoordinates(IOptions<Settings> settings, HttpClient httpClient)
{
    private readonly string _baseUrl = settings.Value.Weatherapiurl;

    public async Task<WeatherData?> GetWeatherData(Coordinates coordinates, DateTime endDate)
    {
        var startDate = endDate.AddDays(-6);
        var url =
            $"{_baseUrl}?latitude={coordinates.Latitude}&longitude={coordinates.Longitude}&start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}&daily=temperature_2m_max,temperature_2m_min,sunshine_duration,rain_sum,snowfall_sum&timezone=Europe/Berlin";

        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherData>(json,
                new JsonSerializerOptions());
            return weatherData;
        }

        throw new HttpRequestException($"Error: {response.StatusCode}");
    }
}