namespace Backend.Domain;

public record ClassifierData(bool IsNormalized, Daily WeatherDataDaily, MainProcess MainProcess, DateTime DateTime );