namespace Backend.Presentation.Dtos;

/// <summary>
/// Dto for invalid Location input
/// </summary>
/// <param name="ErrorMessage"></param>
public record InvalidLocationDto(string ErrorMessage): ErrorDto(ErrorMessage);