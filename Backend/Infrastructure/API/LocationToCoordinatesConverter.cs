using System.Text.Json;
using Backend.Domain;
using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Backend.Infrastructure.API;

public class LocationToCoordinatesConverter(IOptions<Settings> settings, HttpClient httpClient)
{
    private readonly string _baseUrl = settings.Value.CoordinatsApiUrl;
    public async Task<Coordinates?> GetCoordinatesForLocation(string location)
    {
        var url = $"{_baseUrl}city={location}&country=Switzerland";

        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<Coordinates>(json);
            return weatherData;
        }

        throw new HttpRequestException($"Error: {response.StatusCode}");
    }
}