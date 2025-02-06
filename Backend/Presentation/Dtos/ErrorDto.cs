namespace Backend.Presentation.Dtos;

/// <summary>
/// Base record for ErrorDto's
/// </summary>
/// <param name="ErrorMessage"></param>
public record ErrorDto(string ErrorMessage):IDto;