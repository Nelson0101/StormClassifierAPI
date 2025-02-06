namespace Backend.Presentation.Dtos;

/// <summary>
/// Dto for invalid MainProcess input
/// </summary>
/// <param name="ErrorMessage"></param>
public record InvalidMainProcessDto(string ErrorMessage): ErrorDto(ErrorMessage);