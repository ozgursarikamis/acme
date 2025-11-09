using Acme.Models;
using Acme.Models.Entity;
using Acme.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acme.Services;

public class ProductService(AcmeDbContext context) : IProductService
{
    private AcmeDbContext Context { get; } = context;

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await Context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await Context.Products.ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        Context.Products.Add(product);
        await Context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var existingProduct = await Context.Products.FindAsync(product.Id);
        if (existingProduct == null)
            return null;

        Context.Entry(existingProduct).CurrentValues.SetValues(product);
        await Context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await Context.Products.FindAsync(id);
        if (product == null)
            return false;

        Context.Products.Remove(product);
        await Context.SaveChangesAsync();

        return true;
    }
}