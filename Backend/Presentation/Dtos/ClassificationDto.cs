using Backend.Domain;

namespace Backend.Presentation.Dtos;

/// <summary>
/// Dto which includes the Classification Result
/// </summary>
/// <param name="Classification"></param>
public record ClassificationDto(Classification Classification):IDto;