using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
  public class Game
  {
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }

  }
}
