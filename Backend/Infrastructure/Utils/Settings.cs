namespace Backend.Infrastructure.Utils;

/// <summary>
/// Settings Class to access the appsettings.json file.
/// </summary>
public class Settings
{
    public required string WeatherApiUrl { get; init; }
    public required string OpenMeteoExtension { get; init; }
    public required string CoordinatsApiUrl { get; init; }
    public required string CoordinatesApiKey { get; init; }
    public required string ModelUri { get; init; }
    
    public string BaseErrorMessage { get; init; } = "Please Try Again";
    public required string InvalidDateErrorMessage { get; init; }
    public required string InvalidMainProcessErrorMessage { get; init; }
    public required string InvalidLocationErrorMessage { get; init; }
}