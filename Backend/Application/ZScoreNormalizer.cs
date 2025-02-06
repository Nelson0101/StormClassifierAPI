using Backend.Domain;

namespace Backend.Application;

public class ZScoreNormalizer
{
    /// <summary>
    /// Normalizes the WeatherData, as this is required for the Classifier to work with.
    /// </summary>
    /// <param name="classifierData"></param>
    /// <returns>ZScore normalized ClassifierData</returns>
    public ClassifierData Normalize(ClassifierData classifierData)
    {
        Daily dailyNormalized = new Daily(classifierData.WeatherDataDaily.Time,
            Normalize(classifierData.WeatherDataDaily.Temperature2mMax),
            Normalize(classifierData.WeatherDataDaily.Temperature2mMin),
            Normalize(classifierData.WeatherDataDaily.SunshineDuration),
            Normalize(classifierData.WeatherDataDaily.RainSum),
            Normalize(classifierData.WeatherDataDaily.SnowfallSum));
        // just replaces the new values
        return classifierData with { IsNormalized = true, WeatherDataDaily = dailyNormalized };

    }
    
    /// <summary>
    /// From value.Select manual:
    /// Returns: An IEnumerable<out T> whose elements are the result of invoking the transform function on each element of source
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>

    private List<double> Normalize(List<double> values)
    {
        double mean = CalculateMean(values);
        double std = CalculateStd(values);
        return values.Select(v => (v - mean) / std).ToList();
    } 

    private double CalculateMean(List<double> values)
    {
        return values.Sum() / values.Count;
    }

    private double CalculateStd(List<double> values)
    {
        double mean = CalculateMean(values);
        //calculate 
        double variance = values.Sum(v => Math.Pow(v - mean, 2)) / values.Count;
        return Math.Sqrt(variance);
    }
}