using Acme.Models.Entity;

namespace Acme.Services.Interfaces;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
}
