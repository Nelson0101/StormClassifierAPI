using Backend.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Backend.Presentation.Dtos;

public class DtoFactory(IOptions<Settings> settings)
{
    private readonly string _baseErrorMessage = settings.Value.BaseErrorMessage;
    private readonly string _invalidDateErrorMessage = settings.Value.InvalidDateErrorMessage;
    private readonly string _invalidMainProcessMessage = settings.Value.InvalidMainProcessErrorMessage;
    private readonly string _invalidLocationMessage = settings.Value.InvalidLocationErrorMessage;
    
    public InvalidDateDto InvalidDateDto()
    {
        return new InvalidDateDto(CreateMessage(_invalidDateErrorMessage));
    }

    public ClassificationDto ClassificationDto(int classification)
    {
        return new ClassificationDto(classification);
    }

    public InvalidMainProcessDto InvalidMainProcessDto()
    {
        return new InvalidMainProcessDto(CreateMessage(_invalidMainProcessMessage));
    }

    public InvalidLocationDto InvalidLocationDto()
    {
        return new InvalidLocationDto(CreateMessage(_invalidLocationMessage));
    }

    private string CreateMessage(string message)
    {
        return message + "\n" + _baseErrorMessage;
    }
}