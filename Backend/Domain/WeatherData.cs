namespace Backend.Domain;

public record WeatherData(
    List<float> Temperature2MMax,
    List<float> Temperature2MMin,
    List<float> SunshineDuration,
    List<float> RainSum,
    List<float> SnowfallSum)
{
    public List<float> Temperature2MMax { get; set; } = Temperature2MMax;
    public List<float> Temperature2MMin { get; set; } = Temperature2MMin;
    public List<float> SunshineDuration { get; set; } = SunshineDuration;
    public List<float> RainSum { get; set; } = RainSum;
    public List<float> SnowfallSum { get; set; } = SnowfallSum;
}