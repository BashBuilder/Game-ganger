using System.ComponentModel.DataAnnotations;

namespace api.Dto;

public record class GameSummayDto(
  int Id,
  string Name,
  string Genre,
  decimal Price,
  DateOnly ReleaseDate
);