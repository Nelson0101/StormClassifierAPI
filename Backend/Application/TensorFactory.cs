using Backend.Domain;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Backend.Application;

public class TensorFactory
{
    public DenseTensor<float> CreateTensorFromClassifierData(ClassifierData classifierData)
    {
        var features = new float[37];
        features[0] = (float) classifierData.DateTime.Month;
        features[1] = (float) classifierData.MainProcess;
        Array.Copy(classifierData.WeatherDataDaily.Temperature2mMax.Select(x=> (float)x).ToArray(), 0, features, 2, 7);
        Array.Copy(classifierData.WeatherDataDaily.Temperature2mMin.Select(x=> (float)x).ToArray(), 0, features, 9, 7);
        Array.Copy(classifierData.WeatherDataDaily.SunshineDuration.Select(x=> (float)x).ToArray(), 0, features, 16, 7);
        Array.Copy(classifierData.WeatherDataDaily.RainSum.Select(x=> (float)x).ToArray(), 0, features, 23, 7);
        Array.Copy(classifierData.WeatherDataDaily.SnowfallSum.Select(x=> (float)x).ToArray(), 0, features, 30, 7);
        return new DenseTensor<float>(features, [1, 37]);
        
    }
    
}