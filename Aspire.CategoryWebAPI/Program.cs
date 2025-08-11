using Aspire.CategoryWebAPI.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<ApplicationDbContext>("categorydb", null, action =>
{
    action.UseSnakeCaseNamingConvention();
});

builder.AddServiceDefaults();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/update-database", (ApplicationDbContext dbContext) =>
{
    dbContext.Database.Migrate();
    return Results.Ok(new { message = "Database baþarýyla migrate edildi" });
});

app.MapDefaultEndpoints();

app.Run();
