using Microsoft.EntityFrameworkCore;

namespace Aspire.CategoryWebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
}

public sealed class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
