using Acme.Models;
using Acme.Models.Entity;
using Acme.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Acme.Services;

public class ProductService(AcmeDbContext context, ILogger<ProductService> logger) : IProductService
{
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        logger.LogInformation("Getting product with ID");
        return await context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var existingProduct = await context.Products.FindAsync(product.Id);
        if (existingProduct == null)
            return null;

        context.Entry(existingProduct).CurrentValues.SetValues(product);
        await context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
            return false;

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return true;
    }
}