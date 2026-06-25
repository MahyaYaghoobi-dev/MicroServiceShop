using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Entities.Product>> GetAllProductsAsync(ProductSpecParams specParams,CancellationToken cancellationToken);
    Task<long> GetProductsCountAsync(ProductSpecParams specParams, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(string productId, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByNameAsync(string productName, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByBrandIdAsync(string brandId,CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByTypeIdAsync(string typeId,CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByBrandNameAsync(string brandName,CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetProductsByTypeNameAsync(string typeName,CancellationToken cancellationToken);

    Task<bool> UpdateProductAsync(Product product,CancellationToken cancellationToken);
    Task<bool> DeleteProductAsync(string productId,CancellationToken cancellationToken);
    Task<Product?> CreateProductAsync(Product product, CancellationToken cancellationToken);
}