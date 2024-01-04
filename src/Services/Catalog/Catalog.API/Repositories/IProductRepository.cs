using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();

    Task<Product> GetProductAsync(string id);

    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);

    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName);

    Task CreateAsync(Product product);

    Task<bool> UpdateAsync(Product product);

    Task<bool> DeleteAsync(string id);
}
