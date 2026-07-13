using api.Data;
using api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("Games");

builder.Services.AddSqlite<GameStoreContext>(connectionString);

WebApplication app = builder.Build();

app.MapGet("/health", () => Results.Ok("Server is healthy and deployed with docker version 2"));
app.MapGameEndpoints();

await app.MigrateDbAsync();


app.Run();
