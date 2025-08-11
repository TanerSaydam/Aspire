using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Aspire.ProductWebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}

public sealed class Product
{
    public static List<Product> Products = new();
    public static void SeedDataProduct()
    {
        for (int i = 0; i < 100; i++)
        {
            Faker faker = new();
            Product product = new()
            {
                Id = Guid.CreateVersion7(),
                Name = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription()
            };
            Products.Add(product);
        }
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}