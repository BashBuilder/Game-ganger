using System.ComponentModel.DataAnnotations;

namespace api.Dto;

public record class CreateGameDto(
  [Required]
  [StringLength(100)]
  string Name,

  [Required]
  int GenreId,

  [Required]
  [Range(1, 100)]
  decimal Price,

  [Required]
  DateOnly ReleaseDate
);