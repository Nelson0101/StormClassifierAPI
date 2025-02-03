namespace Backend.Presentation.Dtos;

public record InvalidLocationDto(string ErrorMessage): ErrorDto(ErrorMessage);