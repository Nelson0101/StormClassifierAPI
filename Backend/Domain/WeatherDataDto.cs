using System.Text.Json.Serialization;
namespace Backend.Domain;

public record WeatherDataDto(
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
