namespace Backend.Infrastructure.Utils;

public class Settings
{
    public required string WeatherApiUrl { get; init; }
    public required string CoordinatsApiUrl { get; init; }
    public required string CoordinatesApiKey { get; init; }
    public required string ModelUri { get; init; }
    
    public string BaseErrorMessage { get; init; } = "Please Try Again";
    public required string InvalidDateErrorMessage { get; init; }
    public required string InvalidMainProcessErrorMessage { get; init; }
    public required string InvalidLocationErrorMessage { get; init; }
}