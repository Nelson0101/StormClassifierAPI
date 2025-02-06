namespace Backend.Presentation.Dtos;

/// <summary>
/// Dto for invalid Date input
/// </summary>
/// <param name="ErrorMessage"></param>
public record InvalidDateDto(string ErrorMessage): ErrorDto(ErrorMessage);