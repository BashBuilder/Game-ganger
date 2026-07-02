using api.Data;
using api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Games");

builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGet("/health", () => Results.Ok("Server is healthy"));
app.MapGameEndpoints();

await app.MigrateDbAsync();


app.Run();
