using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllProductBrandsAsync(CancellationToken cancellationToken);
    Task<ProductBrand?> GetProductBrandByIdAsync(string productBrandId, CancellationToken cancellationToken);
}