using Backend.Application;
using Backend.Domain;
using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;
using Microsoft.ML.OnnxRuntime;

namespace Backend.Infrastructure.Classifier;

public class Classifier(IOptions<Settings> settings)
{
    private readonly ZScoreNormalizer _zScoreNormalizer = new();
    private readonly TensorFactory _tensorFactory = new();

    public int Classify(ClassifierData classifierData)
    {
        if (!classifierData.IsNormalized)
        {
            classifierData = _zScoreNormalizer.Normalize(classifierData);
        }

        var inputTensor = _tensorFactory.CreateTensorFromClassifierData(classifierData);
        using var session = new InferenceSession(settings.Value.ModelUri);
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("input", inputTensor)
        };
        
        using var results = session.Run(inputs);
        var output = (int) results[0].AsEnumerable<float>().ToArray()[0];
        return output;
    }
}