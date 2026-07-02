using api.Dto;
using api.Entities;

namespace api.Mapping;

public static class GameMapping
{
  public static Game ToEntity(this CreateGameDto game)
  {
    return new Game()
    {
      Name = game.Name,
      GenreId = game.GenreId,
      Price = game.Price,
      ReleaseDate = game.ReleaseDate
    };
  }
  public static Game ToEntity(this UpdateGameDto game)
  {
    return new Game()
    {
      Name = game.Name,
      GenreId = game.GenreId,
      Price = game.Price,
      ReleaseDate = game.ReleaseDate
    };
  }

  public static GameSummayDto ToSummaryDto(this Game game)
  {
    return new(
      game.Id,
      game.Name,
      game.Genre!.Name,
      game.Price,
      game.ReleaseDate
    );
  }
  public static GameDetailsDto ToDetailsDto(this Game game)
  {
    return new(
      Id: game.Id,
      Name: game.Name,
      GenreId: game.GenreId,
      Price: game.Price,
      ReleaseDate: game.ReleaseDate
    );
  }



}
