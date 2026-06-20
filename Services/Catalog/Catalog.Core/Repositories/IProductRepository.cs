using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Entities.Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(string productId);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string productName);
    Task<IEnumerable<Product>> GetProductsByBrandIdAsync(string brandId);
    Task<IEnumerable<Product>> GetProductsByTypeIdAsync(string typeId);
    Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName);
    Task<IEnumerable<Product>> GetProductsByTypeNameAsync(string productName);

    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(string productId);
    Task<Product> CreateProductAsync(Product product);
}