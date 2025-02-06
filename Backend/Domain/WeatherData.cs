using System.Text.Json.Serialization;
namespace Backend.Domain;

/// <summary>
/// Record for the response of the Open Meteo Api
/// </summary>
/// <param name="Latitude"></param>
/// <param name="Longitude"></param>
/// <param name="GenerationtimeMs"></param>
/// <param name="UtcOffsetSeconds"></param>
/// <param name="Timezone"></param>
/// <param name="TimezoneAbbreviation"></param>
/// <param name="Elevation"></param>
/// <param name="DailyUnits"></param>
/// <param name="Daily"></param>
public record WeatherData(
    double Latitude,
    double Longitude,
    double GenerationtimeMs,
    int UtcOffsetSeconds,
    string Timezone,
    string TimezoneAbbreviation,
    double Elevation,
    DailyUnits DailyUnits,
    Daily Daily
);

public record DailyUnits(
    string Time,
    string Temperature2mMax,
    string Temperature2mMin,
    string SunshineDuration,
    string RainSum,
    string SnowfallSum
);



public record Daily(
    [property: JsonPropertyName("time")] List<string> Time,
    [property: JsonPropertyName("temperature_2m_max")] List<double> Temperature2mMax,
    [property: JsonPropertyName("temperature_2m_min")] List<double> Temperature2mMin,
    [property: JsonPropertyName("sunshine_duration")] List<double> SunshineDuration,
    [property: JsonPropertyName("rain_sum")] List<double> RainSum,
    [property: JsonPropertyName("snowfall_sum")] List<double> SnowfallSum
);
