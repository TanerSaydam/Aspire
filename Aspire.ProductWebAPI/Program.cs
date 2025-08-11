using System.Text.Json;
using Aspire.ProductWebAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

builder.AddRedisDistributedCache("cache");

builder.AddSqlServerDbContext<ApplicationDbContext>("ProductDb");

builder.AddServiceDefaults();

var app = builder.Build();

Product.SeedDataProduct();

app.MapGet("/", () => "Hello World!");
app.MapGet("/getall", (IDistributedCache cache) =>
{
    List<Product>? res;
    var resString = cache.GetString("products");
    if (resString is null)
    {
        res = Product.Products;
        cache.SetString("products", JsonSerializer.Serialize(res));
    }
    else
    {
        res = JsonSerializer.Deserialize<List<Product>>(resString);
    }

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