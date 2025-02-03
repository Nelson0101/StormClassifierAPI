namespace Backend.Infrastructure.Utils;

public class Settings
{
    public required string WeatherApiUrl { get; set; }
    public required string CoordinatsApiUrl { get; set; }
    public required string CoordinatesApiKey { get; set; }
    public required string ModelUri { get; set; }
}