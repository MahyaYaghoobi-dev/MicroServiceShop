using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface ITypeRepository
{
    Task<IEnumerable<ProductType>> GetAllProductTypesAsync(CancellationToken cancellationToken);
    Task<ProductType?> GetProductTypeByIdAsync(string productTypeId, CancellationToken cancellationToken);
}