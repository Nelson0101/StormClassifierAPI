using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Backend.Infrastructure.Classifier;

public class Classifier
{
    public int Classify(DenseTensor<float> inputTensor)
    {
        using var session = new InferenceSession("Backend/Infrastructure/Classifier/model.onnx");
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("input", inputTensor)
        };
        
        using var results = session.Run(inputs);

        var output = results.First().AsEnumerable<int>().ToArray()[0];
        return output;
    }
}