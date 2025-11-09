using Acme.Models;
using Acme.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Acme.Services.Test;

public class ProductServiceTests: IDisposable
{
    private readonly AcmeDbContext _context;
    private readonly ProductService _service;

    // Runs before each test
    public ProductServiceTests()
    {
        // 1. Configure DbContextOptions for In-Memory Provider
        var options = new DbContextOptionsBuilder<AcmeDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // 2. Create the actual DbContext instance
        _context = new AcmeDbContext(options);

        // 3. (Optional) Seed the database with test data:
        _ = SeedDatabase();

        _service = new ProductService(_context);
    }

    private async Task SeedDatabase()
    {
        _context.Products.AddRange(new List<Product>
        {
            new() { Id = 1, Name = "Product 1", Price = 1200m },
            new() { Id = 2, Name = "Product 2", Price = 1500m },
            new() { Id = 3, Name = "Product 3", Price = 1800m }
        });

        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
    }
}