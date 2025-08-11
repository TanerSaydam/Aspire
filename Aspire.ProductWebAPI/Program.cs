using Aspire.ProductWebAPI.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddSqlServerDbContext<ApplicationDbContext>("ProductDb");

builder.AddServiceDefaults();

var app = builder.Build();

Product.SeedDataProduct();

app.MapGet("/", () => "Hello World!");
app.MapGet("/getall", () =>
{
    var res = Product.Products;
    return Results.Ok(res);
});

app.MapGet("/update-database", (ApplicationDbContext dbContext) =>
{
    dbContext.Database.Migrate();
    return Results.Ok(new { message = "Database baþarýyla migrate edildi" });
});

app.MapDefaultEndpoints();

//using(var scoped = app.Services.CreateScope())
//{
//    var dbContext = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate();
//}

app.Run();