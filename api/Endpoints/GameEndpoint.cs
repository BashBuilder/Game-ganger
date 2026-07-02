using api.Data;
using api.Dto;
using api.Entities;
using api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace api.Endpoints
{
  public static class GameEndpoint
  {
    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
      const string GetEndpointName = "GetGameById";
      RouteGroupBuilder group = app.MapGroup("games").WithParameterValidation();
      // get all games
      group.MapGet("/", async (GameStoreContext db) =>
      {
        try
        {
          List<GameSummayDto> games = await db.Games
            .Include(game => game.Genre)
            .Select(game => game.ToSummaryDto())
            .AsNoTracking()
            .ToListAsync();
          return Results.Ok(games);
        }
        catch (Exception ex)
        {
          return Results.Problem(ex.Message);
        }
      });

      // get game by id
      group.MapGet("/{id}", async (int id, GameStoreContext db) =>
        {
          try
          {
            Game? game = await db.Games.FindAsync(id);
            if (game is not null)
            {
              return Results.Ok(game.ToDetailsDto());
            }
            return Results.NotFound();
          }
          catch (Exception ex)
          {
            return Results.Problem(ex.Message);
          }
        }
      ).WithName(GetEndpointName);

      // create a new game
      group.MapPost("/", async (CreateGameDto game, GameStoreContext db) =>
      {
        try
        {
          Game newGame = game.ToEntity();
          newGame.Genre = db.Genres.Find(game.GenreId);

          if (newGame is null)
          {
            return Results.NotFound();
          }

          await db.Games.AddAsync(newGame);
          await db.SaveChangesAsync();

          return Results.CreatedAtRoute(GetEndpointName, new { id = newGame.Id }, newGame.ToDetailsDto());

        }
        catch (Exception ex)
        {
          return Results.Problem(ex.Message);
        }
      });


      // update a game 
      group.MapPut("/{id}", async (int id, UpdateGameDto newGame, GameStoreContext db) =>
      {
        try
        {
          Game? game = await db.Games.FindAsync(id);
          if (game is null)
          {
            return Results.NotFound("Game not found");
          }
          db.Entry(game).CurrentValues.SetValues(newGame);
          await db.SaveChangesAsync();
          return Results.NoContent();
        }
        catch (Exception ex)
        {
          return Results.Problem(ex.Message);
        }
      });

      // delete a game
      group.MapDelete("/{id}", async (int id, GameStoreContext db) =>
      {
        try
        {
          await db.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
          return Results.NoContent();

        }
        catch (Exception ex)
        {
          return Results.Problem(ex.Message);
        }
      });


      return group;

    }
  }
}
