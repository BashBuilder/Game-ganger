using System.ComponentModel.DataAnnotations;

namespace api.Dto;

public record class GameDetailsDto(
  int Id,
  string Name,
  int GenreId,
  decimal Price,
  DateOnly ReleaseDate
);