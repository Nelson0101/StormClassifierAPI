namespace Backend.Presentation.Dtos;

public record InvalidDateDto(string ErrorMessage): ErrorDto(ErrorMessage);