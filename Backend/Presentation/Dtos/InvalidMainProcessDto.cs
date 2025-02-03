namespace Backend.Presentation.Dtos;

public record InvalidMainProcessDto(string ErrorMessage): ErrorDto(ErrorMessage);