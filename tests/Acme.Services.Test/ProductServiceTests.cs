using Acme.Models;
using Acme.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

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

        var loggerObject = new Mock<ILogger<ProductService>>();
        _service = new ProductService(_context, loggerObject.Object);
    }


    [Fact]
    public async Task GetAllProductsAsyncShouldReturnAllProducts()
    {
        // Arrange is handled by the constructor

        // Act:
        var products = await _service.GetAllProductsAsync();

        // Assert:
        Assert.NotNull(products);

        var productList = products.ToList();
        Assert.Equal(3, productList.Count);
    }

    [Fact]
    public async Task GetProductByIdAsync_WithValidId_ShouldReturnProduct()
    {
        // Arrange is handled by the constructor

        const int productId = 1;

        // Act:
        var product = await _service.GetProductByIdAsync(productId);

        // Assert:
        Assert.NotNull(product);
        Assert.Equal(productId, product.Id);
        Assert.Equal("Product 1", product.Name);
        Assert.Equal(1200m, product.Price);
    }

    [Fact]
    public async Task CreateProductAsync_WithValidProduct_ShouldCreateProduct()
    {
        // Arrange:
        var productToAdd = new Product
        {
            Name = "Product 4",
            Price = 1000m
        };

        // Act
        var createdProduct = await _service.CreateProductAsync(productToAdd);

        // Assert:
        Assert.NotNull(createdProduct);
        Assert.Equal("Product 4", createdProduct.Name);

        var productInDb = await _context.Products.FindAsync(createdProduct.Id);
        Assert.NotNull(productInDb);
        Assert.Equal(1000.00m, productInDb.Price);
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